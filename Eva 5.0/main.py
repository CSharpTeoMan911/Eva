from pocketsphinx import LiveSpeech
import threading
import socket
import sys


speech = LiveSpeech(lm=False, keyphrase='eva', kws_threshold=1e-10)


def wake_word_operation_application_socket():
    try:
        try:
            host = "127.0.0.1"
            port = 1025
            application_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            application_socket.connect((host, port))
            application_socket.send("eva".encode())
            application_socket.close()
            application_socket.shutdown(socket.SHUT_RDWR)
        except socket.error as e:
            pass
    except KeyboardInterrupt:
        del speech
        sys.exit(0)


def Wake_Word_Initiation():
    global speech
    try:
        for phrase in speech:
            if speech is not None:
                if phrase is not None:
                    thread = threading.Thread(target=wake_word_operation_application_socket)
                    thread.start()
    except KeyboardInterrupt:
        del speech
        sys.exit(0)


if __name__ == '__main__':
    Wake_Word_Initiation()
    del speech
    sys.exit(0)