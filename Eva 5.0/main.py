from vosk import Model, KaldiRecognizer
import os
import pyaudio
import sys
import json
import time
import asyncio
import aiofiles

config = sys.argv[2]

# INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
mic = pyaudio.PyAudio()
stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=3000)
stream.start_stream()

# LOAD THE VOSK SPEECH RECOGNITION MODEL FROM THE APPLICATION'S DIRECTORY
model = None
if str(sys.argv[1]) == "0":
    model = Model(model_path=os.path.join(os.getcwd(), "model 1"), lang="en-us")
else:
    model = Model(model_path=os.path.join(os.getcwd(), "model 2"), lang="en-us")

# INITIATE KALDI SPEECH RECOGNIZER INSTANCE USING THE VOSK MODEL AND A FREQUENCY OF 16000 HZ
recognizer = KaldiRecognizer(model, 16000)
recognizer.SetGrammar('["listen", "stop", "listening", "[unk]"]')
recognizer.SetWords(True)
recognizer.SetPartialWords(True)

boot_time = time.time()

async def get_confidence() -> float:
    confidence_threshold = 0.85
    try:
        if os.path.exists(config) is True:
            async with aiofiles.open(config, "r") as f:
                config_obj = json.loads(s = await f.read())
                confidence_threshold = float(config_obj["Vosk_Sensitivity"]) / 10
    except Exception:
        pass
    return confidence_threshold


async def pipe_messaging(message:str):
    start = time.time()
    while time.time() - start < 10:
        try:
            async with aiofiles.open(r"\\.\pipe\eva_wake_word_engine", "w") as pipe:
                await pipe.write(message)
                await pipe.flush()
            break
        except Exception:
            pass


async def wake_word_engine_operation():
    try:
        while True:
            await wake_word_engine_process()
    except KeyboardInterrupt:
        sys.exit(0)

async def wake_word_engine_spool() -> bool:
    global boot_time
    if time.time() - boot_time >= 3:
        return True
    else:
        return False

async def wake_word_engine_process():
    ##############################################
    # VOSK SPEECH-TO-TEXT SPEECH LANGUAGE MODEL  #
    # USING KALDI SPEECH-TO-TEXT ENGINE          #
    ##############################################

    # CREDIT TO https://buddhi-ashen-dev.vercel.app/posts/offline-speech-recognition

    global initial_frames

    # READ 1000 FRAMES FOR EACH ITERATION (1500 * 2 = 3000 bytes)
    data = stream.read(1500, False)

    try:
        # RETRIEVE THE AUDIO WAVEFORM DATA AND PERFORM SPEECH TO TEXT CONVERSION
        if recognizer.AcceptWaveform(data):
            await keyword_spotter(recognizer.Result())
            await keyword_spotter(recognizer.FinalResult())
        else:
            await keyword_spotter(recognizer.PartialResult())
        
        if await wake_word_engine_spool() is True:
            await pipe_messaging("[ loaded ]")
    except KeyboardInterrupt:
        sys.exit(0)


async def keyword_spotter(sentence:str):
    # IF THE RECOGNIZED PHRASE CONTAINS "listen", SEND 'listen' OVER THE PIPE,
    # IF THE PHRASE CONTAINS 'stop listening' SEND 'stop listening' ON THE PIPE

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
                        await pipe_messaging("stop listening")
                        break
            elif "listen" in j_obj["text"]:
                for word in j_obj["result"]:
                    if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                        await pipe_messaging("listen")
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
                        await pipe_messaging("stop listening")
                        break
            elif "listen" in j_obj["partial"]:
                for word in j_obj["partial_result"]:
                    if word["word"] == "listen" and word["conf"] >= confidence_threshold:
                        await pipe_messaging("listen")
                        break
    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    asyncio.run(wake_word_engine_operation())
