from vosk import Model, KaldiRecognizer
import os
import pyaudio
import sys
import json
import socket
import time

connection = socket.socket(family=socket.AddressFamily.AF_INET, type=socket.SocketKind.SOCK_STREAM)
connection.connect(("127.0.0.1", 6000))
connection.settimeout(None)

start = 0

confidence_threshold = float(sys.argv[2]) / 10

# INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
mic = pyaudio.PyAudio()
stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=8000)
stream.start_stream()

# LOAD THE VOSK SPEECH RECOGNITION MODEL FROM THE APPLICATION'S DIRECTORY
model = None
if str(sys.argv[1]) == "0":
    model = Model(model_path=os.getcwd() + "\\" + "model 1", lang="en-us")
else:
    model = Model(model_path=os.getcwd() + "\\" + "model 2", lang="en-us")

# INITIATE KALDI SPEECH RECOGNIZER INSTANCE USING THE VOSK MODEL AND A FREQUENCY OF 16000 HZ
recognizer = KaldiRecognizer(model, 16000)
recognizer.SetGrammar('["listen", "stop", "listening", "[unk]"]')
recognizer.SetWords(True)
recognizer.SetPartialWords(True)

wake_word_engine_loaded = False


def socket_messaging(message):
    global connection
    connection.send(message.encode("utf-8"))


def wake_word_engine_operation():
    try:
        while True:
            wake_word_engine_process()
    except KeyboardInterrupt:
        sys.exit(0)


def wake_word_engine_process():
    ##############################################
    # VOSK SPEECH-TO-TEXT SPEECH LANGUAGE MODEL  #
    # USING KALDI SPEECH-TO-TEXT ENGINE          #
    ##############################################

    # CREDIT TO https://buddhi-ashen-dev.vercel.app/posts/offline-speech-recognition

    global wake_word_engine_loaded
    global start

    data = stream.read(6000, False)

    try:
        try:
            if wake_word_engine_loaded is False:
                socket_messaging("[ loaded ]")
                wake_word_engine_loaded = True
                start = time.time()

            # RETRIEVE THE AUDIO WAVEFORM DATA AND PERFORM SPEECH TO TEXT CONVERSION
            if recognizer.AcceptWaveform(data):
                keyword_spotter(recognizer.Result())
                keyword_spotter(recognizer.FinalResult())
            else:
                keyword_spotter(recognizer.PartialResult())
        except Exception as e:
            print(e)
    except KeyboardInterrupt:
        sys.exit(0)


def keyword_spotter(sentence):
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
                            socket_messaging("stop listening")
                elif "listen" in j_obj["text"]:
                    for word in j_obj["result"]:
                        if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                            socket_messaging("listen")
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
                            socket_messaging("stop listening")
                elif "listen" in j_obj["partial"]:
                    for word in j_obj["partial_result"]:
                        if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                            socket_messaging("listen")
    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    wake_word_engine_operation()
