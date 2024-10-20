import json

from vosk import Model, KaldiRecognizer
import os
import pyaudio
import sys
import time

confidence_threshold = 0.95

# LOAD THE VOSK SPEECH RECOGNITION MODEL FROM THE APPLICATION'S DIRECTORY
model = Model(model_path=os.getcwd() + "\\" + "vosk-model-en-us-daanzu-20200905-lgraph", lang="en-us")

# INITIATE KALDI SPEECH RECOGNIZER INSTANCE USING THE VOSK MODEL AND A FREQUENCY OF 16000 HZ
recognizer = KaldiRecognizer(model, 16000)
recognizer.SetGrammar('["listen", "hey listen", "stop listening", "[unk]"]')
recognizer.SetWords(True)

# INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
mic = pyaudio.PyAudio()
stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=1600)
stream.start_stream()

wake_word_engine_loaded = False
interrupt = False


def wake_word_engine_operation():
    global start
    start = time.time()
    try:
        while True:
            wake_word_engine_thread_management()
    except KeyboardInterrupt:
        sys.exit(0)


def wake_word_engine_thread_management():
    ##############################################
    # VOSK SPEECH-TO-TEXT SPEECH LANGUAGE MODEL  #
    # USING KALDI SPEECH-TO-TEXT ENGINE          #
    ##############################################

    # CREDIT TO https://buddhi-ashen-dev.vercel.app/posts/offline-speech-recognition

    global wake_word_engine_loaded

    try:
        # READ FROM THE AUDIO DATA STREAM A 800 FRAMES PER CYCLE
        data = stream.read(800, False)
        if wake_word_engine_loaded is False:
            sys.stdout.write("[ loaded ]")
            sys.stdout.flush()
            wake_word_engine_loaded = True

        # RETRIEVE THE AUDIO WAVEFORM DATA AND PERFORM SPEECH TO TEXT CONVERSION
        if recognizer.AcceptWaveform(data):
            keyword_spotter(recognizer.Result())
            keyword_spotter(recognizer.FinalResult())
        else:
            keyword_spotter(recognizer.PartialResult())
    except KeyboardInterrupt:
        sys.exit(0)


def keyword_spotter(sentence):
    # IF THE RECOGNIZED PHRASE CONTAINS "listen" PRINT 'listen' ON THE STDOUT STREAM,
    # ELSE IF THE PHRASE CONTAINS 'stop listening' PRINT 'stop listening' ON THE
    # STDOUT STREAM

    global confidence_threshold
    global interrupt

    j_obj = json.loads(sentence)

    try:
        for key in j_obj:
            if interrupt is True:
                interrupt = False
                break
            if key == "result":
                if "stop listening" in j_obj["text"]:
                    stop_found = True
                    for word in j_obj["result"]:
                        if word["word"] == "stop" and word["conf"] >= confidence_threshold:
                            stop_found = True
                        elif word["word"] != "listening" and stop_found is True:
                            stop_found = False
                        elif word["word"] == "listening" and word["conf"] >= confidence_threshold and stop_found is True:
                            sys.stdout.write("stop listening")
                            interrupt = True
                            break
                elif "listen" in j_obj["text"]:
                    for word in j_obj["result"]:
                        if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                            sys.stdout.write("listen")
                            interrupt = True
                            break
            elif key == "partial_result":
                if "stop listening" in j_obj["partial"]:
                    stop_found = True
                    for word in j_obj["partial_result"]:
                        if word["word"] == "stop" and word["conf"] >= confidence_threshold:
                            stop_found = True
                        elif word["word"] != "listening" and stop_found is True:
                            stop_found = False
                        elif word["word"] == "listening" and word["conf"] >= confidence_threshold and stop_found is True:
                            sys.stdout.write("stop listening")
                            interrupt = True
                            break
                elif "listen" in j_obj["partial"]:
                    for word in j_obj["partial_result"]:
                        if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                            sys.stdout.write("listen")
                            interrupt = True
                            break
        sys.stdout.flush()
    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    wake_word_engine_operation()
