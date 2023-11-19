from vosk import Model, KaldiRecognizer
import os
import pyaudio
import sys
import time


def wake_word_engine_thread_management():
    ##############################################
    # VOSK SPEECH-TO-TEXT SPEECH LANGUAGE MODEL  #
    # USING KALDI SPEECH-TO-TEXT ENGINE          #
    ##############################################

    # CREDIT TO https://buddhi-ashen-dev.vercel.app/posts/offline-speech-recognition

    try:
        # LOAD THE VOSK SPEECH RECOGNITION MODEL FROM THE APPLICATION'S DIRECTORY
        model = Model(model_path=os.getcwd() + "\\" + "vosk-model-small-en-us-zamia-0.5", model_name="tedlium")

        # INITIATE KALDI SPEECH RECOGNIZER INSTANCE USING THE VOSK MODEL AND A FREQUENCY OF 16000 HZ
        recognizer = KaldiRecognizer(model, 16000)
        recognizer.SetWords(True)

        # INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 1600 FRAMES
        mic = pyaudio.PyAudio()
        stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=1600)
        stream.start_stream()

        # BUFFER THAT STORES THE INITIAL TIME VALUE
        start_time = time.time()

        while True:

            # BUFFER THAT STORES THE CURRENT TIME VALUE
            current_time = time.time()

            """
            IF THE TIME ELAPSED BETWEEN THE INITIAL POINT AND THE CURRENT
            POINT IS GREATER OR EQUAL THAN 4, RESET THE RECOGNISER AND SET
            THE INITIAL POINT AS THE CURRENT TIME.
            """
            if current_time - start_time >= 4:
                start_time = time.time()
                """
                THE RECOGNISER MUST BE RESET TO MINIMISE THE RESPONSE TIME 
                AND INCREASE THE ACCURACY. THE DECREASE OF ACCURACY AND 
                RESPONSE TIME IS CAUSED BY THE FACT THAT A BUFFER FILLS 
                WITH RECOGNISED WORDS UNTIL THE SPEECH FINISHES, AND IF 
                THE BUFFER BECOMES LARGER, THE RECOGNISER WILL HAVE A 
                HARDER TIME PROCESSING THE INFORMATION, THUS DECREASING T
                HE ACCURACY AND SPEED OF THE RECOGNISER.
                """
                recognizer.Reset()

            # READ FROM THE AUDIO DATA STREAM A 800 FRAMES PER CYCLE
            data = stream.read(800)

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
            print("stop listening")
        else:
            if "listen" in sentence:
                print("listen")
        sys.stdout.flush()
    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    wake_word_engine_thread_management()
