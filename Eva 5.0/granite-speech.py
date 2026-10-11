
import os
import queue
import threading
import time
from collections import deque
from pathlib import Path

import numpy as np
import sounddevice as sd
import transcribe_cpp


# ------------------------------------------------------------
# Configuration
# ------------------------------------------------------------

MODEL_PATH = Path(
    os.path.join(
        os.getcwd(),
        "python",
        "granite-speech-5.0-470m-turboctc-gguf",
        "granite-speech-5.0-470m-turboctc-Q8_0.gguf",
    )
)

BACKEND = "auto"

SAMPLE_RATE = 16000
CHANNELS = 1

FRAME_MS = 30
FRAME_SAMPLES = SAMPLE_RATE * FRAME_MS // 1000

# RMS-based speech detection.
# Tune against recordings from your actual microphone and environment.
SPEECH_THRESHOLD = 0.008

# Utterance segmentation.
END_SILENCE_MS = 900
MIN_SPEECH_MS = 300
PRE_ROLL_MS = 300
POST_ROLL_MS = 150

# Audio diagnostics.
CLIPPING_THRESHOLD = 0.99
QUIET_AUDIO_THRESHOLD = 0.003

# ------------------------------------------------------------


def log_callback(level, message):
    message = str(message)

    if "ggml_backend_sched_split_inputs_grow" in message:
        return

    print(message, flush=True)


transcribe_cpp.set_log_callback(log_callback)


# Separate queues keep microphone capture independent of inference.
audio_queue = queue.Queue()
asr_queue = queue.Queue()
result_queue = queue.Queue()


def microphone_callback(indata, frames, time_info, status):
    """Capture microphone frames without running inference here."""

    if status:
        print(f"[Audio warning: {status}]", flush=True)

    # Copy because the audio callback's input buffer is not ours to retain.
    audio_queue.put(indata[:, 0].copy())


def asr_worker(session):
    """Run Granite inference on completed utterances."""

    while True:
        audio = asr_queue.get()

        try:
            if audio is None:
                break

            started = time.perf_counter()

            audio = np.ascontiguousarray(audio, dtype=np.float32)

            peak = float(np.max(np.abs(audio)))
            rms = float(np.sqrt(np.mean(audio * audio)))

            if peak >= CLIPPING_THRESHOLD:
                result_queue.put(
                    ("warning", "Audio may be clipping.")
                )

            if rms < QUIET_AUDIO_THRESHOLD:
                result_queue.put(
                    ("warning", "Utterance has a very low RMS level.")
                )

            result = session.run(audio)

            elapsed = time.perf_counter() - started
            text = result.text.strip()

            if text:
                result_queue.put(("text", text, elapsed))

        except Exception as exc:
            result_queue.put(("error", str(exc)))

        finally:
            asr_queue.task_done()


def submit_utterance(
    utterance,
    speech_frame_count,
    silence_frames,
    minimum_frames,
):
    """Trim excess endpoint silence and submit valid speech."""

    if speech_frame_count < minimum_frames:
        return

    if not utterance:
        return

    post_roll_frames = max(0, POST_ROLL_MS // FRAME_MS)

    # Retain some trailing silence, but remove the rest.
    trailing_frames_to_remove = max(
        0,
        silence_frames - post_roll_frames,
    )

    if trailing_frames_to_remove:
        speech_frames = utterance[:-trailing_frames_to_remove]
    else:
        speech_frames = utterance

    if not speech_frames:
        return

    # The array owns its memory after concatenation.
    audio = np.concatenate(speech_frames).astype(
        np.float32,
        copy=False,
    )

    asr_queue.put(audio)


def print_results():
    """Drain available inference results without blocking capture."""

    while True:
        try:
            item = result_queue.get_nowait()
        except queue.Empty:
            break

        if item[0] == "text":
            _, text, elapsed = item
            print(f"[Result: {text}]", flush=True)

        elif item[0] == "warning":
            _, message = item
            print(f"[Audio warning: {message}]", flush=True)

        else:
            _, error = item
            print(f"[ASR error: {error}]", flush=True)


def main():
    if not MODEL_PATH.is_file():
        raise FileNotFoundError(
            f"Granite GGUF model not found:\n{MODEL_PATH}"
        )

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
            print(
                f"[Audio: {SAMPLE_RATE} Hz, "
                f"{FRAME_MS} ms frames, "
                f"RMS threshold {SPEECH_THRESHOLD}]",
                flush=True,
            )

            try:
                with sd.InputStream(
                    samplerate=SAMPLE_RATE,
                    channels=CHANNELS,
                    dtype="float32",
                    blocksize=FRAME_SAMPLES,
                    callback=microphone_callback,
                ):
                    while True:
                        print_results()

                        try:
                            frame = audio_queue.get(timeout=0.03)
                        except queue.Empty:
                            continue

                        # Calculate RMS for this frame.
                        rms = float(
                            np.sqrt(np.mean(frame * frame))
                        )

                        is_speech = rms >= SPEECH_THRESHOLD

                        if not speaking:
                            pre_roll.append(frame)

                            if is_speech:
                                speaking = True

                                # Include audio immediately preceding
                                # speech so initial phonemes are preserved.
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
                                    utterance=utterance,
                                    speech_frame_count=speech_frame_count,
                                    silence_frames=silence_frames,
                                    minimum_frames=minimum_frames,
                                )

                                speaking = False
                                utterance = []
                                silence_frames = 0
                                speech_frame_count = 0
                                pre_roll.clear()

            except KeyboardInterrupt:
                print("\n[Audio: stopping capture]", flush=True)

            finally:
                # Finish the utterance currently being captured.
                if speaking and utterance:
                    submit_utterance(
                        utterance=utterance,
                        speech_frame_count=speech_frame_count,
                        silence_frames=silence_frames,
                        minimum_frames=minimum_frames,
                    )

                # FIFO ordering ensures queued utterances are processed
                # before the worker receives its shutdown sentinel.
                asr_queue.put(None)
                worker.join()

                # Print any results produced during shutdown.
                print_results()

                print("[Audio: stopped]", flush=True)


if __name__ == "__main__":
    main()
