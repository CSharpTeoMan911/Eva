using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Media.ClosedCaptioning;

namespace Eva_5._0
{
    internal class Online_Speech_Recognition:IDisposable
    {

        private static System.Threading.Thread ParallelProcessing;

        private Windows.Media.SpeechRecognition.SpeechRecognizer OnlineSpeechRecognitionEngine;





        public static async Task<bool> Online_Speech_Recognition_Session_Creation_And_Initiation()
        {


            bool Online_Speech_Recognition_Engine_Initiation_Successful = false;



            try
            {

                





                // Ensures that the virtual interface to the online speech recognition server on Windows 10/11 for the online speech recognition system is open, during the speech recognition session.

                // [ BEGIN ] 

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





                using (System.Diagnostics.Process Online_Speech_Recognition_Search_Initiation = new System.Diagnostics.Process())
                {
                    Online_Speech_Recognition_Search_Initiation.StartInfo.FileName = @"C:\Windows\SystemApps\Microsoft.Windows.Search_cw5n1h2txyewy\SearchApp.exe";
                    Online_Speech_Recognition_Search_Initiation.StartInfo.UseShellExecute = true;
                    Online_Speech_Recognition_Search_Initiation.Start();
                }

                // [ END ]






                // INITIATE AN ONLINE SPEECH RECOGNITION ENGINE OBJECT AND AN ONLINE SPEECH RECOGNITION SESSION FOR THAT SPEECH RECOGNITION ENGINE OBJECT

                // [ BEGIN ]

                Online_Speech_Recognition_Engine_Initiation_Successful = await Initiate_The_Online_Speech_Recognition_Engine();

                // [ END ]

            }
            catch { }


            



            return Online_Speech_Recognition_Engine_Initiation_Successful;
        }










        private static async Task<bool> Initiate_The_Online_Speech_Recognition_Engine()
        {

            bool Online_Speech_Recognition_Engine_Initiation_Successful = true;

            try
            {

                using (Online_Speech_Recognition online_Speech_Recognition = new Online_Speech_Recognition())
                {
                    online_Speech_Recognition.OnlineSpeechRecognitionEngine = new Windows.Media.SpeechRecognition.SpeechRecognizer();

                    Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.FormFilling, "form filling");

                    Constraints.Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max;

                    online_Speech_Recognition.OnlineSpeechRecognitionEngine.Constraints.Add(Constraints);

                    Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult ConstratintsCompilation = await online_Speech_Recognition.OnlineSpeechRecognitionEngine.CompileConstraintsAsync();



                    switch (ConstratintsCompilation.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                    {


                        case true:
                            online_Speech_Recognition.OnlineSpeechRecognitionEngine.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.FromSeconds(7);
                            online_Speech_Recognition.OnlineSpeechRecognitionEngine.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
                            online_Speech_Recognition.OnlineSpeechRecognitionEngine.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
                            online_Speech_Recognition.OnlineSpeechRecognitionEngine.StateChanged += OnlineSpeechRecognition_StateChanged;
                            online_Speech_Recognition.OnlineSpeechRecognitionEngine.HypothesisGenerated += OnlineSpeechRecognition_HypothesisGenerated;



                            // START MULTIPLE ONLINE SPEECH RECOGNITION SESSIONS ASYNCHRONOUSLY (ON DIFFERENT THREADS IF THE RUNTIME DECIDES TO DO SO)
                            // [ START ]

                            for (int session = 1; session < 1000; session++)
                            {
                                await online_Speech_Recognition.OnlineSpeechRecognitionEngine.ContinuousRecognitionSession.StartAsync();

                                lock (MainWindow.Online_Speech_Recogniser_Listening)
                                {
                                    MainWindow.Online_Speech_Recogniser_Listening = "true";
                                }
                            }

                            // [ END ]
                            break;



                        case false:
                            Online_Speech_Recognition_Engine_Initiation_Successful = false;
                            await Stop_The_Online_Speech_Recognition();
                            break;

                    }

                }

            }
            catch (Exception E)
            {

                if (E.HResult == -2147199735)
                {

                    Online_Speech_Recognition_Engine_Initiation_Successful = false;

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


            return Online_Speech_Recognition_Engine_Initiation_Successful;
        }



        private static void OnlineSpeechRecognition_StateChanged(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognizerStateChangedEventArgs args)
        {

            lock (MainWindow.Online_Speech_Recogniser_Listening)
            {

                if (MainWindow.Online_Speech_Recogniser_Listening == "true")
                {

                    switch (args.State)
                    {
                        case Windows.Media.SpeechRecognition.SpeechRecognizerState.Idle:
                            MainWindow.Online_Speech_Recogniser_Taking_Input = "false";
                            Task.Run(async () =>
                            {
                                await sender.ContinuousRecognitionSession.StartAsync();
                            });
                            break;

                        case Windows.Media.SpeechRecognition.SpeechRecognizerState.Paused:
                            MainWindow.Online_Speech_Recogniser_Taking_Input = "false";
                            Task.Run(async () =>
                            {
                                await sender.ContinuousRecognitionSession.StartAsync();
                            });
                            break;

                        default:
                            MainWindow.Online_Speech_Recogniser_Taking_Input = "true";
                            break;
                    }

                }

            }

        }






        private static async void ContinuousRecognitionSession_Completed(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs args)
        {
            await Stop_The_Online_Speech_Recognition();
        }






        private static void OnlineSpeechRecognition_HypothesisGenerated(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            lock (MainWindow.Online_Speech_Recogniser_Listening)
            {
                MainWindow.Online_Speech_Recogniser_Listening = "true";
            }
        }






        private async static void ContinuousRecognitionSession_ResultGenerated(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {

            try
            {

                if ((args.Result.Text != String.Empty) || (args.Result.Text != null))
                {

                    if (App.StopRecognitionSession == false)
                    {
                        ParallelProcessing = new System.Threading.Thread(async () =>
                        {
                            try
                            {
                                await Natural_Language_Processing.PreProcessing<string>(args.Result.Text);
                            }
                            catch { }
                        });
                        ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.MTA);
                        ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                        ParallelProcessing.IsBackground = false;
                        ParallelProcessing.Start();
                    }

                }

                await Stop_The_Online_Speech_Recognition();

            }
            catch { }

        }









        public static Task<bool> Stop_The_Online_Speech_Recognition()
        {
            try
            {

                lock (MainWindow.Online_Speech_Recogniser_Listening)
                {

                    lock (MainWindow.FunctionInitiated)
                    {

                        lock (MainWindow.Online_Speech_Recogniser_Taking_Input)
                        {

                            MainWindow.Online_Speech_Recogniser_Taking_Input = "false";


                            MainWindow.Online_Speech_Recogniser_Listening = "false";


                            MainWindow.FunctionInitiated = "false";

                        }

                    }

                }

            }
            catch
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }







        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch { }
        }




        private async void Dispose(bool disposing)
        {
            try
            {
                Online_Speech_Recognition online_Speech_Recognition = new Online_Speech_Recognition();

                if (online_Speech_Recognition.OnlineSpeechRecognitionEngine != null)
                {
                    await online_Speech_Recognition.OnlineSpeechRecognitionEngine.ContinuousRecognitionSession?.CancelAsync();
                    online_Speech_Recognition.OnlineSpeechRecognitionEngine?.Dispose();
                }

            } 
            catch { }
        }




        ~Online_Speech_Recognition()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }

    }
}
