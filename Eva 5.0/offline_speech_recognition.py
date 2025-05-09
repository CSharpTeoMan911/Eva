import pyaudio
import asyncio
import time
from VAD_detection import VAD
from FasterWhisper import Whisper
from PipeConnection import Pipe

speech_enabled = False
speech_data = bytes()

enabled = True
timeout = float(2)
start = float(0)
interval = float(0)


async def audio_reader():
    global start
    global timeout
    global speech_data
    global speech_enabled
    global interval

    # INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
    mic = pyaudio.PyAudio()
    stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=3000)
    stream.start_stream()

    # INITIATE THE VOICE ACTIVITY DETECTION ENGINE
    vad = VAD()

    # INITIATE THE OFFLINE SPEECH RECOGNITION ENGINE
    whisper = Whisper()

    # INITIATE THE PIPE CONNECTION TO THE MAIN APP
    pipe = Pipe()

    # NOTIFY THE MAIN APP THAT ALL DEPENDENCIES ARE LOADED
    await pipe.send_data("[loaded]")

    try:
        while True:
            print(f"\n\nSpeech enabled: {speech_enabled}")
            print(f"Start: {time.time() - start}")
            print(f"Interval: {time.time() - interval}")


            data = stream.read(512, False)
            is_speech = await vad.audio_reader(data)

            if speech_enabled is True:
                extract_data = True

                if (time.time() - interval) > timeout or (time.time() - start) > 20:
                    speech_enabled = False
                    extract_data = False
                    interval = 0
                    start = 0
                    result = await whisper.transcription(speech_data)
                    print(f"Result:{result}")

                if extract_data is True:
                    speech_data += data

            if is_speech is True:
                if speech_enabled is False:
                    start = time.time()
                    interval = time.time()
                    speech_enabled = True
                else:
                    interval = time.time()

    except Exception as e:
        print(e)
        pass


if __name__ == "__main__":
    asyncio.run(audio_reader())
