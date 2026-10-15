
import os
import queue
import threading
from collections import deque
from contextlib import contextmanager
from pathlib import Path
import subprocess

import numpy as np
import pyaudio
import transcribe_cpp


#-----------------------------------------------------------
#           AUDIO BUFFERING ARCHITECTURE
#-----------------------------------------------------------


################################################################################################################
#                                                                                                              #
#  [pre_roll] --> contains a maximum of 1000ms of speech. It is used to store audio before speech is detected  #
#                                                                                                              #
#  [utterance] --> [ [ pre_roll ] + [ speech_audio ] + [non_speech_audio] ] --> it is using the 'pre_roll'     #
#                                                                               buffer as well as well as      #
#                                                                               the speech audio and 1000 MS   #
#                                                                               of non-speech audio. This is   #
#                                                                               done to prevent audio          #
#                                                                               clipping.                      #
#                                                                                                              #
################################################################################################################


##################################################################################################################
#                                                                                                                #
#  utterance = queue(max_num_frames)                                                                             #
#  pre_roll = []                                                                                                 #
#                                                                                                                #
##################################################################################################################
#                                                                                                                #
#  Audio --> is_speech |---> no : append to the pre_roll buffer append if user is not speaking, if user          #
#                      |          speaks the current utterance becomes the accumulated pre_roll buffer           #
#                      |                                                                                         #
#                      |---> yes : if user not speaking submit the number of speech frames as well               #
#                                  as the utterance buffer to be transcribed and set the num of                  #
#                                  silence frames to 0, else set the number of silence frames                    #
#                                  to 0 and increment the number of speech frames                                #
#                                                                                                                #
##################################################################################################################







# ------------------------------------------------------------
# Configuration
# ------------------------------------------------------------

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

FRAME_MS = 10
FRAME_SAMPLES = SAMPLE_RATE * FRAME_MS // 1000

SPEECH_THRESHOLD = 0.005

END_SILENCE_MS = 1000
MIN_SPEECH_MS = 1
PRE_ROLL_MS = 1000


# ------------------------------------------------------------
# Logging
# ------------------------------------------------------------

def log_callback(level, message):
    pass


transcribe_cpp.set_log_callback(log_callback)


# ------------------------------------------------------------
# Queues
# ------------------------------------------------------------

audio_queue = queue.Queue()
asr_queue = queue.Queue()


# ------------------------------------------------------------
# Microphone
# ------------------------------------------------------------

def microphone_callback(in_data, frame_count, time_info, status_flags):
    # PyAudio input-overflow flags are intentionally ignored; keep the stream running.
    if in_data:
        samples = np.frombuffer(in_data, dtype=np.float32)
        if samples.size == frame_count * CHANNELS:
            frame = samples.reshape(frame_count, CHANNELS)[:, 0].copy()
            audio_queue.put(frame)

    return (None, pyaudio.paContinue)


@contextmanager
def pyaudio_input_stream(callback):
    audio = pyaudio.PyAudio()
    stream = None

    try:
        stream = audio.open(
            format=pyaudio.paFloat32,
            channels=CHANNELS,
            rate=SAMPLE_RATE,
            input=True,
            frames_per_buffer=FRAME_SAMPLES,
            stream_callback=callback,
        )
        yield stream
    finally:
        try:
            if stream is not None:
                try:
                    if stream.is_active():
                        stream.stop_stream()
                finally:
                    stream.close()
        finally:
            audio.terminate()


# ------------------------------------------------------------
# Transcription
# ------------------------------------------------------------

def asr_worker(session):
    while True:
        audio = asr_queue.get()

        try:
            if audio is None:
                return

            audio = np.ascontiguousarray(audio, dtype=np.float32)

            result = session.run(audio)
            text = result.text.strip()

            if text:
                print(f"[Result: {text}]", flush=True)

        except Exception:
            pass

        finally:
            asr_queue.task_done()


# ------------------------------------------------------------
# Utterance submission
# ------------------------------------------------------------

def submit_utterance(utterance, speech_frame_count, minimum_frames):
    try:
        if speech_frame_count < minimum_frames or not utterance:
            return

        # Preserve every captured frame, including leading and trailing
        # silence. Do not trim the beginning or end of the audio.
        audio = np.concatenate(utterance).astype(
            np.float32,
            copy=False,
        )

        asr_queue.put(audio)
    except Exception:
        pass


# ------------------------------------------------------------
# Main
# ------------------------------------------------------------

def main():
    if not MODEL_PATH.is_file():
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

                        rms = float(
                            np.sqrt(np.mean(frame * frame))
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

            except Exception:
                pass

            finally:
                if speaking and utterance:
                    submit_utterance(
                        utterance,
                        speech_frame_count,
                        minimum_frames,
                    )

                asr_queue.join()
                asr_queue.put(None)
                worker.join()


def process_checkup(shutdown_event: threading.Event):
    while not shutdown_event.is_set():
        if subprocess.getoutput('powershell -Command "(Get-Process \'Eva 5.0\' -ErrorAction SilentlyContinue).Id"') == "":
            shutdown_event.set()
            return
        shutdown_event.wait(1)

if __name__ == "__main__":
    shutdown_event = threading.Event()
    checkup_thread = threading.Thread(
        target=process_checkup,
        args=(shutdown_event,),
        daemon=True,
    )
    checkup_thread.start()
    main()
