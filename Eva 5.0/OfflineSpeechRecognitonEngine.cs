using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;

namespace Eva_5._0
{
    class OfflineSpeechRecognitonEngine : MainWindow
    {
        private static Process stt_engine;
        private static CancellationTokenSource cancellationTokenSource;
        private static bool dependencies_loaded = false;

        public static void Offline_Speech_Recognition_Session_Creation_And_Initiation()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Initiate_The_Offline_Speech_Recognition_Engine(cancellationToken);

            /*
            bool operational = false;

            lock (Online_Speech_Recogniser_Disabled)
            {
                lock (Window_Minimised)
                {
                    lock (Online_Speech_Recogniser_Listening)
                    {
                        if (Online_Speech_Recogniser_Disabled == "false" && Online_Speech_Recogniser_Listening == "false" && Window_Minimised == "false")
                            operational = true;
                    }
                }
            }

            if (operational == true)
                Initiate_The_Offline_Speech_Recognition_Engine(cancellationToken);
            */
        }

        private static async void Initiate_The_Offline_Speech_Recognition_Engine(CancellationToken cancellationToken)
        {
            try
            {
                stt_engine = new System.Diagnostics.Process();
                stt_engine.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                stt_engine.StartInfo.FileName = Environment.CurrentDirectory + "\\python.exe";
                stt_engine.StartInfo.CreateNoWindow = false;
                stt_engine.StartInfo.UseShellExecute = false;
                stt_engine.StartInfo.Arguments = "offline_speech_recognition.py";
                stt_engine.Start();

                using (NamedPipeServerStream namedPipe = new NamedPipeServerStream("eva_offline_speech_recognition_engine", PipeDirection.In, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous))
                {
                    while (true)
                    {
                        await namedPipe.WaitForConnectionAsync(cancellationToken);

                        byte[] buffer = new byte[1024];
                        int bytes_read = await namedPipe.ReadAsync(buffer, 0, buffer.Length);

                        string message = Encoding.UTF8.GetString(buffer);
                        Debug.WriteLine(message);

                        if (message == "[loaded]")
                        {
                            dependencies_loaded = true;
                        }
                        else
                        {
                            
                        }

                        namedPipe.Disconnect();
                    }
                }
            }
            catch { }
        }


        public static void Stop_The_Offline_Speech_Recognition_Engine() 
        {
            dependencies_loaded = false;
            cancellationTokenSource?.Cancel();
            stt_engine?.Dispose();
        }

        public static bool AreDependenciesLoaded() => dependencies_loaded;
    }
}
