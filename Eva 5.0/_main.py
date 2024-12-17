import json

from vosk import Model, KaldiRecognizer
import os
import pyaudio
import sys
import asyncio
import noisereduce
import numpy as np
import time

confidence_threshold = 0.85

model = None

# LOAD THE VOSK SPEECH RECOGNITION MODEL FROM THE APPLICATION'S DIRECTORY
if str(sys.argv[1]) == "0":
    model = Model(model_path=os.getcwd() + "\\" + "model 1", lang="en-us")
else:
    model = Model(model_path=os.getcwd() + "\\" + "model 2", lang="en-us")

# INITIATE KALDI SPEECH RECOGNIZER INSTANCE USING THE VOSK MODEL AND A FREQUENCY OF 16000 HZ
recognizer = KaldiRecognizer(model, 16000)
recognizer.SetGrammar('["listen", "hey listen", "stop listening", "[unk]"]')
recognizer.SetWords(True)
recognizer.SetPartialWords(True)

# INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
mic = pyaudio.PyAudio()
stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=1600)
stream.start_stream()

wake_word_engine_loaded = False

binary_data = bytearray()


async def wake_word_engine_operation():
    global binary_data
    start = time.time()
    try:
        while True:
            binary_data += bytearray(stream.read(800, False))
            if (time.time() - start) >= 0.5:
                _binary_data = binary_data.copy()
                binary_data.clear()
                await wake_word_engine_thread_management(bytes(_binary_data))
                start = time.time()
    except KeyboardInterrupt:
        sys.exit(0)


async def wake_word_engine_thread_management(data):
    ##############################################
    # VOSK SPEECH-TO-TEXT SPEECH LANGUAGE MODEL  #
    # USING KALDI SPEECH-TO-TEXT ENGINE          #
    ##############################################

    # CREDIT TO https://buddhi-ashen-dev.vercel.app/posts/offline-speech-recognition

    global wake_word_engine_loaded

    try:
        n_frames = len(data) / 2

        np_data = np.frombuffer(data, dtype=np.int16)
        np_data = np.reshape(np_data, newshape=(1, int(n_frames)))
        red_np_data = noisereduce.reduce_noise(y=np_data, sr=16000)
        data = red_np_data.tobytes()

        if wake_word_engine_loaded is False:
            sys.stdout.write("[ loaded ]")
            sys.stdout.flush()
            wake_word_engine_loaded = True

        # RETRIEVE THE AUDIO WAVEFORM DATA AND PERFORM SPEECH TO TEXT CONVERSION
        if recognizer.AcceptWaveform(data):
            await keyword_spotter(recognizer.Result())
            await keyword_spotter(recognizer.FinalResult())
        else:
            await keyword_spotter(recognizer.PartialResult())
    except KeyboardInterrupt:
        sys.exit(0)


async def keyword_spotter(sentence):
    # IF THE RECOGNIZED PHRASE CONTAINS "listen" PRINT 'listen' ON THE STDOUT STREAM,
    # ELSE IF THE PHRASE CONTAINS 'stop listening' PRINT 'stop listening' ON THE
    # STDOUT STREAM

    global confidence_threshold
    j_obj = json.loads(sentence)

    try:
        for key in j_obj:
            if key == "result":
                if "stop listening" in j_obj["text"]:
                    stop_found = True
                    for word in j_obj["result"]:
                        if word["word"] == "stop" and word["conf"] >= confidence_threshold:
                            stop_found = True
                        elif word["word"] != "listening" and stop_found is True:
                            stop_found = False
                        elif word["word"] == "listening" and word[
                            "conf"] >= confidence_threshold and stop_found is True:
                            sys.stdout.write("stop listening")
                elif "listen" in j_obj["text"]:
                    for word in j_obj["result"]:
                        if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                            sys.stdout.write("listen")
            elif key == "partial_result":
                if "stop listening" in j_obj["partial"]:
                    stop_found = True
                    for word in j_obj["partial_result"]:
                        if word["word"] == "stop" and word["conf"] >= confidence_threshold:
                            stop_found = True
                        elif word["word"] != "listening" and stop_found is True:
                            stop_found = False
                        elif word["word"] == "listening" and word[
                            "conf"] >= confidence_threshold and stop_found is True:
                            sys.stdout.write("stop listening")
                elif "listen" in j_obj["partial"]:
                    for word in j_obj["partial_result"]:
                        if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                            sys.stdout.write("listen")
        sys.stdout.flush()
    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    asyncio.run(wake_word_engine_operation())
