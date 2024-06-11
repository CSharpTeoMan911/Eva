from vosk import Model, KaldiRecognizer
import os
import pyaudio
import sys

# LOAD THE VOSK SPEECH RECOGNITION MODEL FROM THE APPLICATION'S DIRECTORY
model = Model(model_path=os.getcwd() + "\\" + "vosk-model-small-en-us-zamia-0.5")

# INITIATE KALDI SPEECH RECOGNIZER INSTANCE USING THE VOSK MODEL AND A FREQUENCY OF 16000 HZ
recognizer = KaldiRecognizer(model, 16000)

# INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
mic = pyaudio.PyAudio()
stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=1600)
stream.start_stream()

wake_word_engine_loaded = False


def wake_word_engine_operation():
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
    try:
        if "stop listening" in sentence:
            sys.stdout.write("stop listening")
        else:
            if "listen" in sentence:
                sys.stdout.write("listen")
        sys.stdout.flush()
    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    wake_word_engine_operation()
