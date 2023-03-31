from pocketsphinx import LiveSpeech
import sounddevice
import threading
import multiprocessing
import socket
import sys
import time

speech = LiveSpeech(lm=False, keyphrase=' wake eva ', kws_threshold=0.00000000000000000000000000000045)
process_list = []


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


def Wake_Word_Engine_Thread_Management():
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

    global speech
    global process_list



    while True:
        speech = LiveSpeech(lm=False, keyphrase=' wake eva ', kws_threshold=0.0000000000000000000000000000005)
        process = multiprocessing.Process(target=Wake_Word_Initiation)

        try:
            process.start()
            process_list.append(process)

            if len(process_list) > 2:
                process_list[0].terminate()
                process_list.remove(process_list[0])

            time.sleep(5)
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


if __name__ == '__main__':
    Wake_Word_Engine_Thread_Management()




