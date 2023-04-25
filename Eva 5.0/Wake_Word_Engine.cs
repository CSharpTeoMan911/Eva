﻿using System;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{

    /////////////////////////////////////////////////////////////////////////////
    ///                                                                       ///
    ///                   PRODUCT: EVA A.I. ASSISTANT                         ///
    ///                                                                       ///
    ///                   AUTHOR: TEODOR MIHAIL                               ///
    ///                                                                       ///
    ///                                                                       ///
    /// ANY UNAUTHORISED TRADEMARK USE OF THIS SOFTWARE IS PUNISHABLE BY LAW  ///
    ///                                                                       ///
    /// THE AUTHOR OF THIS SOFTWARE DOES NOT LET ANY PEOPLE PATENT OR USE     ///
    /// THIS PRODUCT'S TRADEMARK.                                             ///
    ///                                                                       ///
    /// DO NOT REMOVE THIS FILE HEADER                                        ///
    ///                                                                       ///
    /////////////////////////////////////////////////////////////////////////////
    
    
    internal class Wake_Word_Engine:MainWindow
    {

        // THE WAKE WORD ENGINE IS THE "POCKETSPHINX" WAKE WORD ENGINE AND IS AVAILABLE ONLY IN PYTHON.
        // THE WAKE WORD ENGINE IS IMPLEMENTED IN THE "main.py" FILE. IN ORDER FOR THE WAKE WORD ENGINE
        // TO BE ACTIVE AND FOR THE WAKE WORD ENGINE TO RUN, 2 PROCEDURES MUST BE IMPLEMENTED:
        //
        //
        //
        // 1) YOU MUST HAVE A PYTHON VIRTUAL ENVIRONMENT WITHIN THE EVA APPLICATION'S DIRECTORY
        // ( WHERE THE "Eva 5.0.exe" EXECUTABLE IS LOCATED ). IN ORDER TO DO THIS YOU HAVE MULTIPLE 
        // OPTIONS:
        //
        // - USE THE COMMAND "python3 -m venv /path/to/Eva 5.0.exe"
        //
        // - DOWNLOAD PYTHON WHERE "Eva 5.0.exe" EXECUTABLE IS LOCATED.
        //
        // - COPY AN ALREADY EXISTING VIRTUAL ENVIRONMET WHERE "Eva 5.0.exe" EXECUTABLE IS LOCATED.
        //
        //
        // 
        // 2) INSTALL THE PIP PACKAGES: "pocketsphinx", "PyAudio", AND "sounddevice" USING THE 
        //    COMMAND: /path/to/Eva 5.0.exe/python.exe -m pip install [ PACKAGE NAME ]


        private static byte[] wake_word_data = Encoding.UTF8.GetBytes("listen");

        protected static Task<bool> Start_The_Wake_Word_Engine()
        {
            // INITIATE THE WAKE WORD ENGINE USING THE VIRTUAL ENVIRONMENT WITHIN THE DIRECTORY
            // WHERE WHERE "Eva 5.0.exe" EXECUTABLE IS LOCATED. THE PROCESS WILL NOT CREATE ANY
            // WINDOW AND IT WILL BE STARTED AS A CHILD PROCESS OF THE "Eva 5.0.exe" PROCESS.
            //
            // [ BEGIN ]

            try
            {

                System.Diagnostics.Process wake_word_process = new System.Diagnostics.Process();
                wake_word_process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                wake_word_process.StartInfo.FileName = Environment.CurrentDirectory + "\\python.exe";
                wake_word_process.StartInfo.CreateNoWindow = true;
                wake_word_process.StartInfo.Arguments = "main.py";
                wake_word_process.Start();

                if (Wake_Word_Detection_Initiated == false)
                {
                    Wake_Word_Detection_Initiated = true;
                    Wake_Word_Detector();
                }

                return Task.FromResult(true);
            }
            catch 
            {
                return Task.FromResult(false);
            }

            // [ END ]
        }

        protected static Task<bool> Stop_The_Wake_Word_Engine()
        {
            // CREDIT TO [ mtijn ], LINK: https://stackoverflow.com/questions/7189117/find-all-child-processes-of-my-own-net-process-find-out-if-a-given-process-is
            // 
            // THE WMI ( WINDOWS MAGEMENT INTERFACE ) IS USED IN ORDER TO GATHER ALL THE CHILD PROCESSES OF THE "Eva 5.0.exe" PROCESS. THE WMI IS A MANAGEMENT SYSTEM
            // OF WINDOWS THAT ALLOWS THE OPERATING SYSTEM TO MANAGE PROCESSES AND SERVICES SECURELY AND EXPLICITLY THROUGH THE USE OF SQL QUERIES MADE TO OBJECTS
            // THAT HAVE A SPECIFIC PURPOSE WITHIN THE OS PROCESS MANAGEMENT. 
            //
            // [ BEGIN ]





            try
            {
                // ALL THE SUB-PROCESSES ( CHILD PROCESSES ) OF THE "Eva 5.0.exe" PROCESS ARE EXTRACTED USING THE "ManagementObjectSearcher" CLASS
                System.Management.ManagementObjectSearcher sub_processes = new System.Management.ManagementObjectSearcher("SELECT * " + "FROM Win32_Process " + "WHERE ParentProcessId=" + System.Diagnostics.Process.GetCurrentProcess().Id);






                // THE "Get()" METHOD OF THE "ManagementObjectSearcher" OBJECT EXECUTES THE SQL QUERY FOR THE "Win32_Process" OBJECT WITHIN WMI, WHICH IS RESPONSIBLE
                // FOR PROCESS MANAGEMENT. THE EXTRACTED SUB-PROCESSES ARE SAVED IN THE OBJECT MADE FROM THE "ManagementObjectCollection" CLASS
                System.Management.ManagementObjectCollection sub_processes_collection = sub_processes.Get();






                if (sub_processes_collection.Count > 0)
                {




                    // LOOP THROUGH THE EACH ELEMENT OF THE "ManagementObjectCollection" CLASS OBJECT, IF ANY
                    //
                    // [ BEGIN ]

                    foreach (System.Management.ManagementObject sub_process in sub_processes_collection)
                    {




                        // THE PROCESS ID OF THE CURRENT INSTACE OF THE LOOP'S SUB-PROCESS IS EXTRACTED AND CASTED TO AN UNSIGNED INTEGER
                        uint sub_process_Id = (uint)sub_process["ProcessId"];







                        // IF THE CURRENT'S SUB-PROCESS ID DOES NOT EQUAL WITH "Eva 5.0.exe" PROCES ID
                        //
                        // [ BEGIN ]

                        if ((int)sub_process_Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                        {








                            // IF THE CURRENT SUB-PROCESS NAME WITH THE SELECTED ID IS EQUAL WITH "python"
                            //
                            // [ BEGIN ]

                            if(System.Diagnostics.Process.GetProcessById((int)sub_process_Id, System.Diagnostics.Process.GetCurrentProcess().MachineName).ProcessName.Contains("python") == true)
                            {





                                // SAVE THE CURRENT SUB-PROCESS OF THE "Eva 5.0.exe" PROCES WITHIN A PROCESS OBJECT
                                System.Diagnostics.Process childProcess = System.Diagnostics.Process.GetProcessById((int)sub_process_Id);






                                // ALL THE SUB-PROCESSES OF THE MAIN SUB-PROCESSS ARE EXTRACTED USING THE "ManagementObjectSearcher" CLASS.
                                // THIS IS DONE TO GET THE "CHILDREN" OF THE INITIAL CHILD PROCESS OF THE "Eva 5.0.exe" PROCESS.
                                System.Management.ManagementObjectSearcher python_sub_processes = new System.Management.ManagementObjectSearcher("SELECT * " + "FROM Win32_Process " + "WHERE ParentProcessId=" + childProcess.Id);







                                // THE "Get()" METHOD OF THE "ManagementObjectSearcher" OBJECT EXECUTES THE SQL QUERY FOR THE "Win32_Process" OBJECT WITHIN WMI, WHICH IS RESPONSIBLE
                                // FOR PROCESS MANAGEMENT. THE EXTRACTED SUB-PROCESSES ARE SAVED IN THE OBJECT MADE FROM THE "ManagementObjectCollection" CLASS
                                System.Management.ManagementObjectCollection python_sub_processes_collection = python_sub_processes.Get();








                                if(python_sub_processes_collection.Count > 0)
                                {



                                    // LOOP THROUGH THE EACH ELEMENT OF THE "ManagementObjectCollection" CLASS OBJECT THAT CONTAINS,
                                    // THE "CHILDREN" PROCESSES OF THE INITIAL SUB-PROCESS OF THE "Eva 5.0.exe" PROCESS, IF ANY
                                    //
                                    // [ BEGIN ]

                                    foreach (System.Management.ManagementObject python_sub_process in python_sub_processes_collection)
                                    {

                                        // THE PROCESS ID OF THE CURRENT "CHILD PROCESS" OF THE INITIAL SUB-PROCESS OF THE "Eva 5.0.exe" PROCESS IS EXTRACTED AND CASTED TO AN UNSIGNED INTEGER
                                        uint python_sub_process_Id = (uint)python_sub_process["ProcessId"];






                                        // IF THE CURRENT'S SUB-PROCESS ID DOES NOT EQUAL WITH "Eva 5.0.exe" PROCES ID
                                        //
                                        // [ BEGIN ]

                                        if ((int)python_sub_process_Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                                        {





                                            // IF THE CURRENT SUB-PROCESS NAME WITH THE SELECTED ID IS EQUAL WITH "python"
                                            //
                                            // [ BEGIN ]


                                            if (System.Diagnostics.Process.GetProcessById((int)sub_process_Id, System.Diagnostics.Process.GetCurrentProcess().MachineName).ProcessName.Contains("python") == true)
                                            {

                                                // SAVE THE CURRENT "CHILD PROCESS" OF THE INITIAL SUB-PROCESS OF THE "Eva 5.0.exe" PROCES WITHIN A PROCESS OBJECT
                                                System.Diagnostics.Process python_childProcess = System.Diagnostics.Process.GetProcessById((int)python_sub_process_Id);


                                                // KILL THE "CHILD PROCESS" OF THE INITIAL SUB-PROCESS OF THE "Eva 5.0.exe" PROCES
                                                python_childProcess.Kill();
                                            }


                                            // [ END ]




                                        }

                                        // [ END ]




                                    }

                                    // [ END ]



                                }



                                // KILL THE INITIAL SUB-PROCESS OF THE "Eva 5.0.exe" PROCES
                                childProcess.Kill();

                            }


                            // [ END ]




                        }


                        // [ END ]
                    }


                    // [ END ]
                }

                return Task.FromResult(true);
            }
            catch 
            {
                return Task.FromResult(false);
            }

            // [ END ]
        }



        private static void Wake_Word_Detector()
        {
            // CREATE A TCP SOCKET OBJECT FOR INTER-PROCESS COMMUNICATION BETWEEN THE "Eva 5.0.exe" AND THE PYTHON WAKE WORD ENGINE
            System.Net.Sockets.Socket wake_word_detection_socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);




            try
            {


                // BIND THE OS's LOOPBACK ADDRESS AND THE 1025 PORT NUMBER TO THE TCP SOCKET 
                wake_word_detection_socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 1025));



                // SET THE TCP SOCKET TO ACTIVELY LISTEN FOR 1 CLIENT
                wake_word_detection_socket.Listen(1);



                while (MainWindowIsClosing == false)
                {
                    try
                    {


                        // IF A CLIENT CONNECTS, SAVE THE CLIENT IN A SOCKET OBJECT
                        System.Net.Sockets.Socket wake_word = wake_word_detection_socket.Accept();




                        // BUFFER THAT STORES BINARY INFORMATION OF THE WAKE WORD DETECTED BY THE CLIENT ( WAKE WORD ENGINE ) AS BYTES
                        byte[] wake_word_detected = new byte[wake_word_data.Length];



                        // RECEIVE THE BINARY INFORMATION SENT BY THE CLIENT AND STORE IT INSIDE THE BUFFER
                        wake_word.Receive(wake_word_detected, 0, wake_word_detected.Length, System.Net.Sockets.SocketFlags.None);



                        // DECODE THE BINARY INFORMATION IN UTF-8 STRING CHARACTER SET FORMAT
                        string wake_word_detected_decoded = Encoding.UTF8.GetString(wake_word_detected);


                        // LOCK THE "Online_Speech_Recogniser_Listening" OBJECT ON THE STACK 
                        // IN ORDER TO BLOCK OTHER THREADS FROM MODIFYING IT
                        //
                        // [ BEGIN ]

                        lock (Online_Speech_Recogniser_Listening)
                        {
                            if(Online_Speech_Recogniser_Listening == "false")
                            {
                                lock (Wake_Word_Detected)
                                {
                                    if (wake_word_detected_decoded == "listen")
                                    {
                                        Wake_Word_Detected = "true";
                                    }
                                }
                            }
                        }

                        // [ END ]

                    }
                    catch { }
                }


                // SHUTDOWN THE SOCKET CONNECTION ON BOTH ENDS
                wake_word_detection_socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);


                // CLOSE THE TCP SOCKET OBJECT'S OPERATION
                wake_word_detection_socket.Close();


                // DISPOSE THE TCP SOCKET OBJECT FROM THE MEMORY
                wake_word_detection_socket.Dispose();
            }
            catch { }
        }
    }



}
