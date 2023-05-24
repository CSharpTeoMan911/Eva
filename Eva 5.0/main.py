from vosk import Model, KaldiRecognizer
import os
import pyaudio
import threading
import socket
import sys


def wake_word_operation_stdout_stream():
    ################################################
    #   INTERPROCESS COMMUNICATION USING STDOUT    #
    ################################################
    # WAKE WORD IS SENT THROUGH THE STDOUT STREAM  #
    ################################################

    try:
        print("listen")
        sys.stdout.flush()
    except KeyboardInterrupt:
        sys.exit(0)




def Wake_Word_Engine_Thread_Management():
    ##############################################
    # VOSK SPEECH-TO-TEXT SPEECH LANGUAGE MODEL  #
    # USING KALDI SPEECH-TO-TEXT ENGINE          #
    ##############################################

    # CREDIT TO https://buddhi-ashen-dev.vercel.app/posts/offline-speech-recognition

    try:
        # LOAD THE VOSK SPEECH RECOGNITION MODEL FROM THE APPLICATION'S DIRECTORY
        model = Model(os.getcwd() + "\\" + "vosk-model-small-en-us-0.15")

        # INITIATE KALDI SPEECH RECOGNIZER INSTANCE USING THE VOSK MODEL AND A FREQUENCY OF 16000 HZ
        recognizer = KaldiRecognizer(model, 16000)

        # INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 300 FRAMES
        mic = pyaudio.PyAudio()
        stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=300)
        stream.start_stream()

        while True:
            # READ FROM THE AUDIO DATA STREAM A 300 FRAMES PER CYCLE
            data = stream.read(300)

            # RETRIEVE THE AUDIO WAVEFORM DATA AND PERFORM SPEECH TO TEXT CONVERSION
            if recognizer.AcceptWaveform(data):

                # IF THE RECOGNIZED PHRASE CONTAINS "listen", THE WAKE WORD IS PASSED
                # TO THE PARENT PROCESS ON A DIFFERENT THREAD 
                if "listen" in recognizer.FinalResult():
                    thread = threading.Thread(target=wake_word_operation_stdout_stream)
                    thread.start()
                elif "listen" in recognizer.Result():
                    thread = threading.Thread(target=wake_word_operation_stdout_stream)
                    thread.start()
            else:
                if "listen" in recognizer.PartialResult():
                    thread = threading.Thread(target=wake_word_operation_stdout_stream)
                    thread.start()

    except KeyboardInterrupt:
        sys.exit(0)


if __name__ == '__main__':
    Wake_Word_Engine_Thread_Management()
