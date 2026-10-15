
import os
import queue
import threading
from collections import deque
from pathlib import Path
import subprocess

import numpy as np
import sounddevice as sd
import transcribe_cpp





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


'''
The audio is held in a buffer named pre_roll, this buffer is capturing the noise including 
silence and it has a max length. When speech is detected, what is accumulated in the 
pre_roll buffer is added added in the buffer that holds speech and the speech is 
accumulated there too as well as trailling noise. This is done to prevent extra 
work for the recogniser that can be done on silence, thus increasing latency 
dramatically, and also to prevent audio clipping, meaning that words can be 
'chewed' up by ignoring surrounding sound that contributes to the 
recogniser's accuracy.  
'''















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

FRAME_MS = 50
FRAME_SAMPLES = SAMPLE_RATE * FRAME_MS // 1000

SPEECH_THRESHOLD = 0.001

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

def microphone_callback(indata, frames, time_info, status):
    audio_queue.put(indata[:, 0].copy())


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
    if speech_frame_count < minimum_frames or not utterance:
        return

    # Preserve every captured frame, including leading and trailing
    # silence. Do not trim the beginning or end of the audio.
    audio = np.concatenate(utterance).astype(
        np.float32,
        copy=False,
    )

    asr_queue.put(audio)


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
                with sd.InputStream(
                    samplerate=SAMPLE_RATE,
                    channels=CHANNELS,
                    dtype="float32",
                    blocksize=FRAME_SAMPLES,
                    callback=microphone_callback,
                ):
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

            except KeyboardInterrupt:
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
