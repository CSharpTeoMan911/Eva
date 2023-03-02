from pocketsphinx import LiveSpeech
import sounddevice
import threading
import multiprocessing
import socket
import sys
import time

speech = LiveSpeech(lm=False, keyphrase=' wake eva ', kws_threshold=0.00000000000000000000000000000025)


def wake_word_operation_application_socket():
    ################################################
    # INTERPROCESS COMMUNICATION USING TCP SOCKETS #
    ################################################
    try:
        try:
            host = "127.0.0.1"
            port = 1025
            application_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            application_socket.connect((host, port))
            application_socket.send("eva".encode())
            application_socket.close()
            application_socket.shutdown(socket.SHUT_RDWR)
        except socket.error:
            pass
    except KeyboardInterrupt:
        del speech
        sys.exit(0)


def Wake_Word_Initiation():
    ##############################
    # WAKE WORD ENGINE OPERATION #
    ##############################
    global speech
    try:
        try:
            for phrase in speech:
                if speech is not None:
                    if phrase is not None:
                        inter_process_thread = threading.Thread(target=wake_word_operation_application_socket)
                        inter_process_thread.start()
        except sounddevice.PortAudioError:
            pass
    except KeyboardInterrupt:
        del speech
        sys.exit(0)


if __name__ == '__main__':
    ###################################################
    # THE WAKE WORD ENGINE IS STARTED AND STOPPED ON  #
    # MULTIPLE THREADS TWO TIMES EVERY 3 SECONDS      #
    #                                                 #
    # THIS IS DONE TO ENSURE THAT THE WAKE WORD       #
    # ENGINE DOES NOT LOCK. THE LOCK IS OCCURRING     #
    # WHEN THE WAKE WORD ENGINE IS RECEIVING NON      #
    # SPEECH INPUT FOR A PERIOD OF TIME.              #
    #                                                 #
    # A PROBABLE CAUSE IS CACHED DATA, WHICH IS       #
    # FREED WHEN THE WAKE WORD ENGINE IS STOPPED.     #
    ###################################################

    while True:
        process1 = multiprocessing.Process(target=Wake_Word_Initiation)
        process2 = multiprocessing.Process(target=Wake_Word_Initiation)
        process3 = multiprocessing.Process(target=Wake_Word_Initiation)
        process4 = multiprocessing.Process(target=Wake_Word_Initiation)
        try:
            process1.start()
            process2.start()
            process3.start()
            process4.start()
            time.sleep(5)
            process1.terminate()
            time.sleep(5)
            process2.terminate()
            time.sleep(5)
            process3.terminate()
            time.sleep(5)
            process4.terminate()
            try:
                del speech
            except NameError:
                pass
        except KeyboardInterrupt:
            try:
                del speech
            except NameError:
                pass
            sys.exit(0)



