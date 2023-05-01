from vosk import Model, KaldiRecognizer
import os
import pyaudio
import threading
import socket
import sys


def wake_word_operation_application_socket():
    ################################################
    # INTERPROCESS COMMUNICATION USING TCP SOCKETS #
    ################################################
    # SOCKET CLIENT THAT IS SENDING A SIGNAL TO    #
    # THE MAIN C# APPLICATION WHEN THE WAKE WORD   #
    # 'wake eva' IS DETECTED IN ORDER FOR THE      #
    # MAIN C# APPLICATION TO ACTIVATE THE ONLINE   #
    # SPEECH RECOGNITION ENGINE                    #
    ################################################

    try:
        try:
            host = "127.0.0.1"
            port = 1025
            application_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            application_socket.connect((host, port))
            application_socket.send("listen".encode())
            application_socket.close()
            application_socket.shutdown(socket.SHUT_RDWR)
        except socket.error:
            pass
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

        # INITIATE PYAUDIO OBJECT, LISTEN TO THE DEFAULT MIC ON 1 CHANNEL, WITH A RATE OF 16000 HZ AND A BUFFER OF 8192 FRAMES
        mic = pyaudio.PyAudio()
        stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=8192)
        stream.start_stream()

        while True:
            # READ FROM THE AUDIO DATA STREAM A 4096 FRAMES PER CYCLE
            data = stream.read(4096)

            # RETRIEVE THE AUDIO WAVEFORM DATA AND PERFORM SPEECH TO TEXT CONVERSION
            if recognizer.AcceptWaveform(data):

                # RESULT'S JSON BODY
                result = recognizer.Result()

                # IF THE RECOGNIZED PHRASE CONTAINS "listen", CALL THE WAKE WORD SOCKET ON A DIFFERENT
                # THREAD IN ORDER NOT TO HINDER THE SPEED OF THE SPEECH RECOGNITION ENGINE
                # AND ALSO FOR THE OPERATIONS TO BE EXECUTED IN PARALLEL ON THE CPU
                if "listen" in result:
                    thread = threading.Thread(target=wake_word_operation_application_socket)
                    thread.start()


    except KeyboardInterrupt:
        pass


if __name__ == '__main__':
    Wake_Word_Engine_Thread_Management()
