using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eva_5._0
{
    internal class Online_Speech_Recognition
    {
        private static System.Threading.Thread ParallelProcessing;

        private static Windows.Media.SpeechRecognition.SpeechRecognizer OnlineSpeechRecognition;


        public static async void Recogniser_Thread_Creation_And_Initiation()
        {
            // Initiates the online speech recognition engine on another thread using parallel processing



            await Initiate_The_Online_Speech_Recognition_Engine();


            ParallelProcessing = new System.Threading.Thread(() =>
            {

                // [ BEGIN ] Ensures that the virtual interface to the online speech recognition server on Windows 10/11 for the online speech recognition system is open, during the speech recognition session.

                try
                {

                    using (System.Diagnostics.Process Speech_Recognition_Server_Interface_Initiation = new System.Diagnostics.Process())
                    {
                        switch (Environment.Is64BitOperatingSystem)
                        {

                            case true:

                                Speech_Recognition_Server_Interface_Initiation.StartInfo.FileName = @"C:\Program Files\WindowsApps\Microsoft.549981C3F5F10_4.2204.13303.0_x64__8wekyb3d8bbwe\Win32Bridge.Server.exe";
                                break;


                            case false:

                                Speech_Recognition_Server_Interface_Initiation.StartInfo.FileName = @"C:\Program Files\WindowsApps\Microsoft.549981C3F5F10_4.2204.13303.0_x86__8wekyb3d8bbwe\Win32Bridge.Server.exe";
                                break;

                        }
                        Speech_Recognition_Server_Interface_Initiation.StartInfo.UseShellExecute = true;
                        Speech_Recognition_Server_Interface_Initiation.Start();
                    }


                    using (System.Diagnostics.Process Speech_Recognition_Cortana_Search_Initiation = new System.Diagnostics.Process())
                    {
                        Speech_Recognition_Cortana_Search_Initiation.StartInfo.FileName = @"C:\Windows\SystemApps\Microsoft.Windows.Search_cw5n1h2txyewy\SearchApp.exe";
                        Speech_Recognition_Cortana_Search_Initiation.StartInfo.UseShellExecute = true;
                        Speech_Recognition_Cortana_Search_Initiation.Start();
                    }

                }
                catch{ }

                // [ END ] Ensures that the virtual interface to the online speech recognition server on Windows 10/11 for the online speech recognition system is open, during the speech recognition session.

            });
            ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
            ParallelProcessing.IsBackground = true;
            ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
            ParallelProcessing.Start();
            ParallelProcessing.Join();
        }






        private static async Task<bool> Initiate_The_Online_Speech_Recognition_Engine()
        {
            try
            {

                OnlineSpeechRecognition = new Windows.Media.SpeechRecognition.SpeechRecognizer();

                Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.FormFilling, "form filling");

                Constraints.Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max;

                OnlineSpeechRecognition.Constraints.Add(Constraints);

                Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult ConstratintsCompilation = await OnlineSpeechRecognition.CompileConstraintsAsync();








                switch (ConstratintsCompilation.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                {


                    case true:

                        MainWindow.Online_Speech_Recogniser_Listening = true;

                        OnlineSpeechRecognition.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.FromSeconds(7);
                        OnlineSpeechRecognition.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
                        OnlineSpeechRecognition.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
                        await OnlineSpeechRecognition.ContinuousRecognitionSession.StartAsync();

                        break;

                    case false:

                        await Stop_The_Online_Speech_Recognition();

                        break;


                }


            }
            catch (Exception E)
            {

                if (E.HResult == -2147199735)
                {
                    MainWindow.Online_Speech_Recogniser_Listening = false;

                    switch (App.PermisissionWindowOpen)
                    {
                        case false:
                            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Online Speech Recognition Access Denied");
                                OpenPermissionDeclinedWindow.Show();
                            });
                            break;

                        case true:

                            App.InitiateErrorFunction = true;
                            App.ErrorFunction = "Online Speech Recognition Access Denied";

                            break;
                    }
                }
            }


            return true;
        }

        private static async void ContinuousRecognitionSession_Completed(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs args)
        {
            await Stop_The_Online_Speech_Recognition();
        }

        private async static void ContinuousRecognitionSession_ResultGenerated(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            try
            {


                switch ((args.Result.Text == String.Empty) || (args.Result.Text == null))
                {
                    case true:

                        await Stop_The_Online_Speech_Recognition();

                        break;


                    case false:



                        switch (App.StopRecognitionSession)
                        {
                            case false:

                                await Natural_Language_Processing.PreProcessing<string>(args.Result.Text);

                                await Stop_The_Online_Speech_Recognition();

                                break;

                            case true:

                                await Stop_The_Online_Speech_Recognition();

                                break;
                        }
                        break;
                }


                await Stop_The_Online_Speech_Recognition();
            }
            catch { }
        }

        public async static Task<bool> Stop_The_Online_Speech_Recognition()
        {
            try
            {
                MainWindow.Online_Speech_Recogniser_Listening = false;

                MainWindow.FunctionInitiated = false;


                if (OnlineSpeechRecognition != null)
                {
                    await OnlineSpeechRecognition.ContinuousRecognitionSession.CancelAsync();

                    OnlineSpeechRecognition.Dispose();
                }
            }
            catch 
            {
                return false;
            }

            return true;
        }


        ~Online_Speech_Recognition()
        {
            ParallelProcessing = null;

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }

    }
}
