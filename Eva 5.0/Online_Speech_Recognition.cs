using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Media.ClosedCaptioning;

namespace Eva_5._0
{
    internal class Online_Speech_Recognition
    {

        private static System.Threading.Thread ParallelProcessing;



        public static async Task<bool> Online_Speech_Recognition_Session_Creation_And_Initiation()
        {


            // Ensures that the virtual interface to the online speech recognition server on Windows 10/11 for the online speech recognition system is open, during the speech recognition session.

            // [ BEGIN ] 


            bool Online_Speech_Recognition_Engine_Initiation_Successful = await Initiate_The_Online_Speech_Recognition_Engine();




            if (Online_Speech_Recognition_Engine_Initiation_Successful == true)
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

            // [ END ]

            return true;
        }










        private static async Task<bool> Initiate_The_Online_Speech_Recognition_Engine()
        {

            try
            {


                using (Windows.Media.SpeechRecognition.SpeechRecognizer OnlineSpeechRecognition = new Windows.Media.SpeechRecognition.SpeechRecognizer())
                {

                    Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.FormFilling, "form filling");

                    Constraints.Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max;

                    OnlineSpeechRecognition.Constraints.Add(Constraints);

                    Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult ConstratintsCompilation = await OnlineSpeechRecognition.CompileConstraintsAsync();








                    switch (ConstratintsCompilation.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                    {
                        case true:

                            lock(MainWindow.Online_Speech_Recogniser_Listening)
                            {
                                MainWindow.Online_Speech_Recogniser_Listening = "true";
                            }

                            OnlineSpeechRecognition.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(7);
                            OnlineSpeechRecognition.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(7);
                            OnlineSpeechRecognition.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(7);

                            Windows.Media.SpeechRecognition.SpeechRecognitionResult Result = await OnlineSpeechRecognition.RecognizeAsync();





                            switch (Result.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                            {
                                case true:

                                    lock (MainWindow.Online_Speech_Recogniser_Listening)
                                    {
                                        MainWindow.Online_Speech_Recogniser_Listening = "true";
                                    }

                                    switch ((Result.Text == String.Empty) || (Result.Text == null))
                                    {
                                        case true:
                                            await OnlineSpeechRecognition.StopRecognitionAsync();
                                            OnlineSpeechRecognition.Dispose();
                                            break;


                                        case false:
                                            switch (App.StopRecognitionSession)
                                            {
                                                case false:
                                                    
                                                    if(MainWindow.FunctionInitiated == "true")
                                                    {
                                                        ParallelProcessing = new System.Threading.Thread(async () =>
                                                        {
                                                            await Natural_Language_Processing.PreProcessing<string>(Result.Text);
                                                        });
                                                        ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.MTA);
                                                        ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                                        ParallelProcessing.IsBackground = false;
                                                        ParallelProcessing.Start();
                                                    }


                                                    await OnlineSpeechRecognition.StopRecognitionAsync();
                                                    OnlineSpeechRecognition.Dispose();
                                                    break;

                                                case true:
                                                    await OnlineSpeechRecognition.StopRecognitionAsync();
                                                    OnlineSpeechRecognition.Dispose();
                                                    break;
                                            }
                                            break;
                                    }

                                    break;





                                case false:
                                    await OnlineSpeechRecognition.StopRecognitionAsync();
                                    OnlineSpeechRecognition.Dispose();
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception E)
            {

                if (E.HResult == -2147199735)
                {
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


            lock (MainWindow.Online_Speech_Recogniser_Listening)
            {
                MainWindow.Online_Speech_Recogniser_Listening = "false";
            }

            lock(MainWindow.FunctionInitiated)
            {
                MainWindow.FunctionInitiated = "false";
            }


            return true;
        }




        ~Online_Speech_Recognition()
        { }

    }
}
