using System;
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
        protected static Task<bool> Start_The_Wake_Word_Engine()
        {
            try
            {

                System.Diagnostics.Process wake_word_process = new System.Diagnostics.Process();
                wake_word_process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                wake_word_process.StartInfo.FileName = Environment.CurrentDirectory + "\\python.exe";
                wake_word_process.StartInfo.CreateNoWindow = false;
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
        }

        protected static Task<bool> Stop_The_Wake_Word_Engine()
        {
            try
            {
                System.Management.ManagementObjectSearcher sub_processes = new System.Management.ManagementObjectSearcher("SELECT * " + "FROM Win32_Process " + "WHERE ParentProcessId=" + System.Diagnostics.Process.GetCurrentProcess().Id);
                System.Management.ManagementObjectCollection sub_processes_collection = sub_processes.Get();

                if (sub_processes_collection.Count > 0)
                {
                    foreach (System.Management.ManagementObject sub_process in sub_processes_collection)
                    {
                        uint sub_process_Id = (uint)sub_process["ProcessId"];
                        if ((int)sub_process_Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                        {
                            if(System.Diagnostics.Process.GetProcessById((int)sub_process_Id, System.Diagnostics.Process.GetCurrentProcess().MachineName).ProcessName.Contains("python") == true)
                            {
                                System.Diagnostics.Process childProcess = System.Diagnostics.Process.GetProcessById((int)sub_process_Id);
                                childProcess.Kill();
                            }
                        }
                    }
                }

                return Task.FromResult(true);
            }
            catch 
            {
                return Task.FromResult(false);
            }
        }

        private static void Wake_Word_Detector()
        {
            System.Net.Sockets.Socket wake_word_detection_socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

            try
            {
                wake_word_detection_socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 1025));
                wake_word_detection_socket.Listen(1);

                while (MainWindowIsClosing == false)
                {
                    try
                    {
                        System.Net.Sockets.Socket wake_word = wake_word_detection_socket.Accept();
                        byte[] wake_word_detected = new byte[(Encoding.UTF8.GetBytes("eva").Length)];
                        wake_word.Receive(wake_word_detected, 0, wake_word_detected.Length, System.Net.Sockets.SocketFlags.None);

                        string wake_word_detected_decoded = Encoding.UTF8.GetString(wake_word_detected);

                        if (wake_word_detected_decoded == "eva")
                        {
                            lock (Wake_Word_Detected)
                            {
                                Wake_Word_Detected = "true";
                            }
                        }
                    }
                    catch { }
                }



                wake_word_detection_socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                wake_word_detection_socket.Close();
                wake_word_detection_socket.Dispose();
            }
            catch { }
        }
    }



}
