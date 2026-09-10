using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Eva_5._0.Command_Pallet;

namespace Eva_5._0.Classes
{
    internal class MoonshineASR:MainWindow
    {
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static readonly string enginePath = "";//@$"{Environment.CurrentDirectory}\";
        private static bool engineStarted = true;
        private static bool engineLoaded = false;
        private static bool transcriptionStarted = false;

        private const string stt_engine_loaded = "[ loaded ]";
        private const string cancel_wake_word = "stop listening";
        private const string wake_word = "listen";


        private static Process? sttEngine;

        public MoonshineASR()
        {

        }

        private static async Task CreatePipe()
        {
            try
            {
                using (NamedPipeServerStream namedPipe = new NamedPipeServerStream("eva_stt_engine", PipeDirection.In, 5, PipeTransmissionMode.Message, PipeOptions.Asynchronous))
                {
                    bool started = false;
                    while (engineStarted == true)
                    {
                        Interlocked.MemoryBarrier();
                        Interlocked.SpeculationBarrier();

                        await namedPipe?.WaitForConnectionAsync();

                        if (!started)
                        {
                            Interlocked.Exchange(ref Online_Speech_Recogniser_Listening, 1);
                            Interlocked.Exchange(ref Initiated, 1);
                            started = true;
                        }

                        if (App.Application_Error_Shutdown == false)
                        {
                            if (sttEngine.HasExited == false)
                            {

                                // READ THE DATA RECEIVED FROM THE NAMED PIPE CONNECTION OF THE "Python" PROCESS ASYNCHRONOUSLY.
                                byte[] buffer = new byte[1024];
                                int bytes_read = await namedPipe?.ReadAsync(buffer, 0, buffer.Length);

                                if (bytes_read > 0)
                                {
                                    speech_recognition_timeout = DateTime.UtcNow;
                                    string pipe_message_value = Encoding.ASCII.GetString(buffer, 0, bytes_read);
                                    Natural_Language_Processing.PreProcessing(pipe_message_value.Trim().ToLower());
                                    Debug.WriteLine($"pipe_message_value: {pipe_message_value}");
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

        public static async Task StartEngine()
        {
            //-c \"from Controller import PipeController; PipeController().run()\"
            sttEngine = new Process();
            sttEngine.StartInfo.FileName = "C:\\Users\\teodo\\source\\repos\\MoonshineEngine\\Scripts\\python.exe";
            sttEngine.StartInfo.Arguments = "\"C:\\Users\\teodo\\source\\repos\\MoonshineEngine\\Controller.py\"";
            sttEngine.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            sttEngine.StartInfo.UseShellExecute = false;
            sttEngine.StartInfo.RedirectStandardError = false;
            sttEngine.StartInfo.RedirectStandardOutput = false;
            sttEngine.StartInfo.RedirectStandardInput = false;
            sttEngine.StartInfo.CreateNoWindow = false;
            sttEngine.Start();

            await CreatePipe();
        }

        public static void StopEngine()
        {
            try
            {
                engineStarted = false;
                sttEngine?.Dispose();
                KillChildProcesses();
            }
            catch { }
        }

        private static void KillChildProcesses()
        {
            sttEngine.Kill();

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
        }

        private static void SttEngine_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLine($"Received: {e.Data}");
        }

        private void SttEngine_ErrorDataReceived(object sender, DataReceivedEventArgs e) => StopEngine();
    }
}
