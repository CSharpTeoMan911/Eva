using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading;

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


    internal class Wake_Word_Engine : MainWindow
    {

        // THE WAKE WORD ENGINE IS THE "VOSK" WAKE WORD ENGINE AND IS AVAILABLE ONLY IN PYTHON.
        // THE WAKE WORD ENGINE IS IMPLEMENTED IN THE "main.py" FILE. IN ORDER FOR THE WAKE WORD ENGINE
        // TO BE ACTIVE AND FOR THE WAKE WORD ENGINE TO RUN, 2 PROCEDURES MUST BE IMPLEMENTED:
        //
        //
        //
        // 1) YOU MUST HAVE A PYTHON VIRTUAL ENVIRONMENT OR AN EMBEDDED STANDALONE DOWNLOAD WITHIN THE EVA APPLICATION'S DIRECTORY
        // ( WHERE THE "Eva 5.0.exe" EXECUTABLE IS LOCATED ). IN ORDER TO DO THIS YOU HAVE MULTIPLE OPTIONS:
        //
        // - USE THE COMMAND "python3 -m venv /path/to/Eva 5.0.exe"
        //
        // - DOWNLOAD PYTHON WHERE "Eva 5.0.exe" EXECUTABLE IS LOCATED.
        //
        // - COPY AN ALREADY EXISTING VIRTUAL ENVIRONMET WHERE "Eva 5.0.exe" EXECUTABLE IS LOCATED.
        //
        // - COPY AN ALREADY EXISTING PYTHON DOWNLOAD WHERE "Eva 5.0.exe" EXECUTABLE IS LOCATED.
        //
        // 
        // 2) INSTALL THE PIP PACKAGES: "Vosk", "PyAudio", AND "sounddevice" USING THE 
        //    COMMAND: /path/to/Eva 5.0.exe/python.exe -m pip install [ PACKAGE NAME ]

        private static Queue<Process> wake_word_processes = new Queue<Process>();

        private static int wake_word_engines_loaded;

        private static string wake_word_engine_loaded = "[ loaded ]";
        private static string cancel_wake_word = "stop listening";
        private static string wake_word = "listen";
        private static bool Wake_Word_Started = false;
        private static string model = "0";


        // Variable that sets in how many minutes the wake word engine is reset
        private static int wake_word_engine_reset_time = 10;
        public static DateTime resetTime;

        private static CancellationTokenSource pipeCancellationTokenSource;
        private static CancellationToken pipeCancellationToken;

        public static void Start_The_Wake_Word_Engine()
        {
            // INITIATE A WAKE WORD ENGINE PROCESS ON A DIFFERENT THREAD FOR CPU LOAD DISTRIBUTION PURPOSES
            // AND ALSO TO PREVENT THE LOCKING OF THE CALLING THREAD. AFTER THE WAKE WORD ENGINE PROCESS
            // STARTED, INITIATE A TIMER THAT PRVENTS WAKE WORD ENGINE LOCK.
            //
            // [ BEGIN ]

            try
            {
                pipeCancellationTokenSource = new CancellationTokenSource();
                pipeCancellationToken = pipeCancellationTokenSource.Token;

                wake_word_engines_loaded = 0;
                resetTime = DateTime.UtcNow;

                // INITIATE THE WAKE WORD ENGINE ON A THREAD-POOL THREAD
                Task.Run(Initiate_Wake_Word_Engine);

                Wake_Word_Started = true;

                System.Timers.Timer wake_word_management_timer = new System.Timers.Timer();
                wake_word_management_timer.Elapsed += Wake_word_management_timer_Elapsed;
                wake_word_management_timer.Interval = 10;
                wake_word_management_timer.Start();
            }
            catch { }

            // [ END ]
        }

        private static void Wake_word_management_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // RESET THE WAKE WORD ENGINE PROCESS ONCE EVERY 10 MINUTES. THIS IS DONE TO PREVENT WAKE WORD ENGINE LOCK,
            // AND ALSO TO PREVENT THE OVERFLOW OF THE DATA BUFFER USED BY THE ENGINE TO STORE THE AUDIO DATA
            //
            // [ START ]

            try
            {
                System.Timers.Timer timer = ((System.Timers.Timer)(sender));

                if (Wake_Word_Started == true)
                {
                    if (MainWindowIsClosing == false)
                    {
                        if (App.Application_Error_Shutdown == false)
                        {
                            if (DateTime.UtcNow - resetTime >= TimeSpan.FromMinutes(wake_word_engine_reset_time))
                            {
                                if (wake_word_engines_loaded == 1)
                                {
                                    if (wake_word_processes.Count == 1)
                                    {
                                        Initiate_Wake_Word_Engine();
                                    }
                                }
                            }

                            if (wake_word_engines_loaded >= 2)
                            {
                                Process process = null;

                                while (wake_word_processes.Count > 1)
                                {
                                    process = wake_word_processes?.Dequeue();
                                    process?.Kill();
                                    wake_word_engines_loaded--;
                                    resetTime = DateTime.UtcNow;
                                }
                            }
                        }
                        else
                        {
                            if (sender != null)
                            {
                                timer?.Close();
                                timer?.Dispose();
                            }
                        }
                    }
                    else
                    {
                        if (sender != null)
                        {
                            timer?.Close();
                            timer?.Dispose();
                        }
                    }
                }
                else
                {
                    if (sender != null)
                    {
                        timer?.Close();
                        timer?.Dispose();
                    }
                }
            }
            catch { }

            //
            // [ END ]
        }


        private static async void Initiate_Wake_Word_Engine()
        {
            try
            {
                // INITIATE THE WAKE WORD ENGINE USING THE VIRTUAL ENVIRONMENT WITHIN THE DIRECTORY
                // WHERE WHERE "Eva 5.0.exe" EXECUTABLE IS LOCATED. THE PROCESS WILL NOT CREATE ANY
                // WINDOW AND IT WILL BE STARTED AS A CHILD PROCESSES OF THE "Eva 5.0.exe" PROCESS.
                // ALSO,INITIATE A SOCKET THROUGH WHICH THE PYTHON PROCESS AND THE MAIN APPLICATION
                // WILL COMMUNICATE WAKE WORD ENGINE RELATED EVENTS.
                //
                // [ BEGIN ]

                System.Diagnostics.Process wake_word_process = new System.Diagnostics.Process();
                wake_word_process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                wake_word_process.StartInfo.FileName = new StringBuilder(Environment.CurrentDirectory).Append(@"\python 3.12\python.exe").ToString();
                wake_word_process.StartInfo.CreateNoWindow = true;
                wake_word_process.StartInfo.UseShellExecute = false;
                wake_word_process.StartInfo.Arguments = new StringBuilder("main.py ").Append(model).Append(' ').Append((await Settings.GetSettingsFilePath()).ToString()).ToString();
                wake_word_process.Start();

                SwitchModel();

                wake_word_processes.Enqueue(wake_word_process);

                await Wake_Word_Detector(wake_word_process);

                // [ END ]
            }
            catch { }
        }


        private static void SwitchModel()
        {
            if (model == "0")
                model = "1";
            else
                model = "0";
        }


        public static bool Stop_The_Wake_Word_Engine()
        {
            try
            {

                // STOP THE PROCESSES THROUGH THE OBJECTS AS A BACKUP METHOD
                //
                // [ START ]

                Wake_Word_Started = false;

                pipeCancellationTokenSource?.Cancel();

                Process process = null;
                while (wake_word_processes.Count > 0)
                {
                    process = wake_word_processes?.Dequeue();
                    process?.Kill();
                }

                // [ END ]


                // CREDIT TO [ mtijn ], LINK: https://stackoverflow.com/questions/7189117/find-all-child-processes-of-my-own-net-process-find-out-if-a-given-process-is
                // 
                // THE WMI ( WINDOWS MAGEMENT INTERFACE ) IS USED IN ORDER TO GATHER ALL THE CHILD PROCESSES OF THE "Eva 5.0.exe" PROCESS. THE WMI IS A MANAGEMENT SYSTEM
                // OF WINDOWS THAT ALLOWS THE OPERATING SYSTEM TO MANAGE PROCESSES AND SERVICES SECURELY AND EXPLICITLY THROUGH THE USE OF SQL QUERIES MADE TO OBJECTS
                // THAT HAVE A SPECIFIC PURPOSE WITHIN THE OS PROCESS MANAGEMENT. 
                //
                // [ BEGIN ]


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

                            if (System.Diagnostics.Process.GetProcessById((int)sub_process_Id, System.Diagnostics.Process.GetCurrentProcess().MachineName).ProcessName.Contains("python") == true)
                            {





                                // SAVE THE CURRENT SUB-PROCESS OF THE "Eva 5.0.exe" PROCES WITHIN A PROCESS OBJECT
                                System.Diagnostics.Process childProcess = System.Diagnostics.Process.GetProcessById((int)sub_process_Id);






                                // ALL THE SUB-PROCESSES OF THE MAIN SUB-PROCESSS ARE EXTRACTED USING THE "ManagementObjectSearcher" CLASS.
                                // THIS IS DONE TO GET THE "CHILDREN" OF THE INITIAL CHILD PROCESS OF THE "Eva 5.0.exe" PROCESS.
                                System.Management.ManagementObjectSearcher python_sub_processes = new System.Management.ManagementObjectSearcher("SELECT * " + "FROM Win32_Process " + "WHERE ParentProcessId=" + childProcess.Id);







                                // THE "Get()" METHOD OF THE "ManagementObjectSearcher" OBJECT EXECUTES THE SQL QUERY FOR THE "Win32_Process" OBJECT WITHIN WMI, WHICH IS RESPONSIBLE
                                // FOR PROCESS MANAGEMENT. THE EXTRACTED SUB-PROCESSES ARE SAVED IN THE OBJECT MADE FROM THE "ManagementObjectCollection" CLASS
                                System.Management.ManagementObjectCollection python_sub_processes_collection = python_sub_processes.Get();








                                if (python_sub_processes_collection.Count > 0)
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

                return true;
            }
            catch
            {
                return false;
            }
        }



        private static async Task Wake_Word_Detector(Process python)
        {
            try
            {
                using (NamedPipeServerStream namedPipe = new NamedPipeServerStream("eva_wake_word_engine", PipeDirection.In, 5, PipeTransmissionMode.Message, PipeOptions.Asynchronous))
                {
                    while (Wake_Word_Started == true)
                    {
                        await namedPipe?.WaitForConnectionAsync(pipeCancellationToken);

                        if (MainWindowIsClosing == false)
                        {
                            if (App.Application_Error_Shutdown == false)
                            {
                                if (python.HasExited == false)
                                {

                                    // READ THE DATA RECEIVED FROM THE NAMED PIPE CONNECTION OF THE "Python" PROCESS ASYNCHRONOUSLY.
                                    byte[] buffer = new byte[1024];
                                    int bytes_read = await namedPipe?.ReadAsync(buffer, 0, buffer.Length);

                                    if (bytes_read > 0)
                                    {
                                        string socket_message_value = Encoding.UTF8.GetString(buffer, 0, bytes_read);

                                        // LOCK THE "Online_Speech_Recogniser_Listening" OBJECT
                                        // IN ORDER TO BLOCK OTHER THREADS FROM MODIFYING IT
                                        //
                                        // [ BEGIN ]

                                        lock (Online_Speech_Recogniser_Listening)
                                        {
                                            lock (Wake_Word_Detected)
                                            {
                                                if (Proc.tasks_running == 0)
                                                {
                                                    if (socket_message_value == wake_word_engine_loaded)
                                                    {
                                                        resetTime = DateTime.UtcNow;
                                                        wake_word_engines_loaded++;
                                                    }
                                                    else if (socket_message_value == cancel_wake_word)
                                                    {
                                                        if (Online_Speech_Recogniser_Listening == "true")
                                                        {
                                                            Online_Speech_Recogniser_Listening = "false";
                                                            Online_Speech_Recognition.Close_Speech_Recognition_Interface();
                                                        }
                                                    }
                                                    else if (socket_message_value == wake_word)
                                                    {
                                                        if (Online_Speech_Recogniser_Listening == "false")
                                                        {
                                                            Wake_Word_Detected = "true";
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // [ END ]
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }

                        namedPipe?.Disconnect();
                    }
                }
            }
            catch { }

        }

    }

}
