
import os
import queue
import threading
from collections import deque
from contextlib import contextmanager
from pathlib import Path
import subprocess
import traceback

import numpy as np
import pyaudio
import transcribe_cpp


# ------------------------------------------------------------
# Configuration
# ------------------------------------------------------------

print(os.getcwd())
MODEL_PATH = Path(
    os.path.join(
        os.getcwd(),
        "python",
        "granite-speech-5.0-470m-turboctc-gguf",
        "granite-speech-5.0-470m-turboctc-nc-F16.gguf",
    )
)

BACKEND = "auto"

SAMPLE_RATE = 16000
CHANNELS = 1

FRAME_MS = 50
FRAME_SAMPLES = SAMPLE_RATE * FRAME_MS // 1000

SPEECH_THRESHOLD = 0.02

END_SILENCE_MS = 500
MIN_SPEECH_MS = 1
PRE_ROLL_MS = 500


# ------------------------------------------------------------
# Error Logging
# ------------------------------------------------------------

def error_logging(e: Exception):
    try:
        with open("error_log.txt", "a", encoding="utf-8") as f:
            f.write(f"\n[{threading.current_thread().name}] ")
            f.write(f"Error: {e}\n")
            f.write(traceback.format_exc())
            f.write("\n")
    except Exception:
        pass


# ------------------------------------------------------------
# Logging
# ------------------------------------------------------------

def log_callback(level, message):
    try:
        pass
    except Exception as e:
        error_logging(e)


try:
    transcribe_cpp.set_log_callback(log_callback)
except Exception as e:
    error_logging(e)


# ------------------------------------------------------------
# Queues
# ------------------------------------------------------------

audio_queue = queue.Queue()
asr_queue = queue.Queue()


# ------------------------------------------------------------
# Microphone
# ------------------------------------------------------------

def microphone_callback(in_data, frame_count, time_info, status_flags):
    try:
        if in_data:
            samples = np.frombuffer(in_data, dtype=np.float32)

            if samples.size == frame_count * CHANNELS:
                frame = samples.reshape(frame_count, CHANNELS)[:, 0].copy()
                audio_queue.put_nowait(frame)

    except Exception as e:
        error_logging(e)

    return (None, pyaudio.paContinue)


@contextmanager
def pyaudio_input_stream(callback):
    audio = None
    stream = None

    try:
        audio = pyaudio.PyAudio()

        stream = audio.open(
            format=pyaudio.paFloat32,
            channels=CHANNELS,
            rate=SAMPLE_RATE,
            input=True,
            frames_per_buffer=FRAME_SAMPLES,
            stream_callback=callback,
        )

        yield stream

    except Exception as e:
        error_logging(e)
        raise

    finally:
        try:
            if stream is not None:
                try:
                    if stream.is_active():
                        stream.stop_stream()
                except Exception as e:
                    error_logging(e)

                try:
                    stream.close()
                except Exception as e:
                    error_logging(e)

        except Exception as e:
            error_logging(e)

        finally:
            try:
                if audio is not None:
                    audio.terminate()
            except Exception as e:
                error_logging(e)


# ------------------------------------------------------------
# Transcription
# ------------------------------------------------------------

def asr_worker(session):
    while True:
        try:
            audio = asr_queue.get()

        except Exception as e:
            error_logging(e)
            continue

        try:
            if audio is None:
                return

            audio = np.ascontiguousarray(audio, dtype=np.float32)

            result = session.run(audio)
            text = result.text.strip()

            if text:
                print(f"[Result: {text}]", flush=True)

        except Exception as e:
            error_logging(e)

        finally:
            asr_queue.task_done()


# ------------------------------------------------------------
# Utterance Submission
# ------------------------------------------------------------

def submit_utterance(utterance, speech_frame_count, minimum_frames):
    try:
        if speech_frame_count < minimum_frames or not utterance:
            return

        audio = np.concatenate(utterance).astype(
            np.float32,
            copy=False,
        )

        asr_queue.put(audio)

    except Exception as e:
        error_logging(e)


# ------------------------------------------------------------
# Main
# ------------------------------------------------------------

def main():
    try:
        if not MODEL_PATH.is_file():
            error_logging(
                FileNotFoundError(f"Model not found: {MODEL_PATH}")
            )
            return

        silence_limit = max(1, END_SILENCE_MS // FRAME_MS)
        minimum_frames = max(1, MIN_SPEECH_MS // FRAME_MS)
        pre_roll_frames = max(1, PRE_ROLL_MS // FRAME_MS)

        with transcribe_cpp.Model(
            str(MODEL_PATH),
            backend=BACKEND,
        ) as model:

            with model.session() as session:
                worker = threading.Thread(
                    target=asr_worker,
                    args=(session,),
                    daemon=True,
                    name="GraniteASRWorker",
                )
                worker.start()

                pre_roll = deque(maxlen=pre_roll_frames)

                utterance = []
                speaking = False
                silence_frames = 0
                speech_frame_count = 0

                print("[Result: [ loaded ]]", flush=True)

                try:
                    with pyaudio_input_stream(microphone_callback):
                        while True:
                            try:
                                frame = audio_queue.get(timeout=0.03)

                            except queue.Empty:
                                continue

                            except Exception as e:
                                error_logging(e)
                                continue

                            try:
                                # Calculate RMS using every sample.
                                rms = float(
                                    np.sqrt(np.mean(np.square(frame)))
                                )

                                is_speech = rms >= SPEECH_THRESHOLD

                                if not speaking:
                                    pre_roll.append(frame)

                                    if is_speech:
                                        speaking = True
                                        utterance = list(pre_roll)
                                        pre_roll.clear()

                                        silence_frames = 0
                                        speech_frame_count = 1

                                else:
                                    utterance.append(frame)

                                    if is_speech:
                                        silence_frames = 0
                                        speech_frame_count += 1
                                    else:
                                        silence_frames += 1

                                    if silence_frames >= silence_limit:
                                        submit_utterance(
                                            utterance,
                                            speech_frame_count,
                                            minimum_frames,
                                        )

                                        speaking = False
                                        utterance = []
                                        silence_frames = 0
                                        speech_frame_count = 0
                                        pre_roll.clear()

                            except Exception as e:
                                error_logging(e)

                except KeyboardInterrupt:
                    pass

                except Exception as e:
                    error_logging(e)

                finally:
                    try:
                        if speaking and utterance:
                            submit_utterance(
                                utterance,
                                speech_frame_count,
                                minimum_frames,
                            )
                    except Exception as e:
                        error_logging(e)

                    try:
                        asr_queue.join()
                    except Exception as e:
                        error_logging(e)

                    try:
                        asr_queue.put(None)
                        worker.join()
                    except Exception as e:
                        error_logging(e)

    except KeyboardInterrupt:
        pass

    except Exception as e:
        error_logging(e)


# ------------------------------------------------------------
# Process Check
# ------------------------------------------------------------

def process_checkup(shutdown_event: threading.Event):
    while not shutdown_event.is_set():
        try:
            result = subprocess.getoutput(
                'powershell -Command '
                '"(Get-Process \'Eva 5.0\' '
                '-ErrorAction SilentlyContinue).Id"'
            )

            if result == "":
                shutdown_event.set()
                return

        except Exception as e:
            error_logging(e)

        try:
            shutdown_event.wait(1)
        except Exception as e:
            error_logging(e)


# ------------------------------------------------------------
# Entry Point
# ------------------------------------------------------------

if __name__ == "__main__":
    try:
        shutdown_event = threading.Event()

        checkup_thread = threading.Thread(
            target=process_checkup,
            args=(shutdown_event,),
            daemon=True,
            name="ProcessCheckup",
        )

        checkup_thread.start()
        main()

    except KeyboardInterrupt:
        pass

    except Exception as e:
        error_logging(e)
