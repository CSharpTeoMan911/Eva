from vosk import Model, KaldiRecognizer
import os
import pyaudio
import sys
import json
import socket
import time
import asyncio

config = sys.argv[2]
port = sys.argv[3]

connection = None

# INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
mic = pyaudio.PyAudio()
stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=2000)
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


async def get_confidence() -> int:
    confidence_threshold = 0.85
    try:
        if os.path.exists(config) is True:
            with open(config, "r") as f:
                config_obj = json.loads(s=f.read())
                confidence_threshold = float(config_obj["Vosk_Sensitivity"]) / 10
    except Exception:
        pass
    return confidence_threshold


async def socket_messaging(message):
    global connection
    try:
        if connection is None:
            connection = socket.socket(family=socket.AddressFamily.AF_INET, type=socket.SocketKind.SOCK_STREAM)
            connection.connect(("127.0.0.1", int(port)))
            connection.settimeout(None)
        connection.send(message.encode("utf-8"))
    except Exception:
        pass


async def wake_word_engine_operation():
    try:
        while True:
            await wake_word_engine_process()
    except KeyboardInterrupt:
        sys.exit(0)


async def wake_word_engine_process():
    ##############################################
    # VOSK SPEECH-TO-TEXT SPEECH LANGUAGE MODEL  #
    # USING KALDI SPEECH-TO-TEXT ENGINE          #
    ##############################################

    # CREDIT TO https://buddhi-ashen-dev.vercel.app/posts/offline-speech-recognition

    global wake_word_engine_loaded

    # READ 1000 FRAMES FOR EACH ITERATION (1000 * 2 = 2000 bytes)
    data = stream.read(1000, False)

    try:
        if wake_word_engine_loaded is False:
            await socket_messaging("[ loaded ]")
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

    confidence_threshold = await get_confidence()
    j_obj = json.loads(sentence)
    try:
        if "result" in j_obj:
            if "stop listening" in j_obj["text"]:
                stop_found = True
                for word in j_obj["result"]:
                    if word["word"] == "stop" and word["conf"] >= confidence_threshold:
                        stop_found = True
                    elif word["word"] != "listening" and stop_found is True:
                        stop_found = False
                    elif word["word"] == "listening" and word[
                        "conf"] >= confidence_threshold and stop_found is True:
                        await socket_messaging("stop listening")
                        break
            elif "listen" in j_obj["text"]:
                for word in j_obj["result"]:
                    if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                        await socket_messaging("listen")
                        break
        elif "partial_result" in j_obj:
            if "stop listening" in j_obj["partial"]:
                stop_found = True
                for word in j_obj["partial_result"]:
                    if word["word"] == "stop" and word["conf"] >= confidence_threshold:
                        stop_found = True
                    elif word["word"] != "listening" and stop_found is True:
                        stop_found = False
                    elif word["word"] == "listening" and word[
                        "conf"] >= confidence_threshold and stop_found is True:
                        await socket_messaging("stop listening")
                        break
            elif "listen" in j_obj["partial"]:
                for word in j_obj["partial_result"]:
                    if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                        await socket_messaging("listen")
                        break
    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    asyncio.run(wake_word_engine_operation())
