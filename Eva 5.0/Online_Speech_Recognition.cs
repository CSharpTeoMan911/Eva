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


        public static void Recogniser_Thread_Creation_And_Initiation()
        {
            // Initiates the online speech recognition engine 
            // on another thread for parallel processing

           
            ParallelProcessing = new System.Threading.Thread(() =>
            {

                // Ensures that the virtual interface with the Cortana's online speech recognition system is open
                // during the speech recognition session.

                try
                {
                   
                    using (System.Diagnostics.Process Speech_Recognition_Executable_Initiation = new System.Diagnostics.Process())
                    {
                        Speech_Recognition_Executable_Initiation.StartInfo.FileName = @"C:Windows\SystemApps\Microsoft.Windows.Search_cw5n1h2txyewy\SearchApp.exe";
                        Speech_Recognition_Executable_Initiation.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                        Speech_Recognition_Executable_Initiation.StartInfo.UseShellExecute = true;
                        Speech_Recognition_Executable_Initiation.Start();
                    }
                }
                catch { }



                Initiate_The_Online_Speech_Recognition_Engine();
            });
            ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
            ParallelProcessing.IsBackground = true;
            ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
            ParallelProcessing.Start();
            ParallelProcessing.Join();
        }


        private static async void Initiate_The_Online_Speech_Recognition_Engine()
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

                            MainWindow.Online_Speech_Recogniser_Listening = true;

                            OnlineSpeechRecognition.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(7);
                            OnlineSpeechRecognition.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(7);
                            OnlineSpeechRecognition.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(7);

                            Windows.Media.SpeechRecognition.SpeechRecognitionResult Result = await OnlineSpeechRecognition.RecognizeAsync();


                            


                            switch (Result.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                            {
                                case true:

                                    MainWindow.Online_Speech_Recogniser_Listening = true;

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

                                                   

                                                    await Natural_Language_Processing.PreProcessing<string>(Result.Text);
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


            MainWindow.Online_Speech_Recogniser_Listening = false;

            MainWindow.FunctionInitiated = false;


            ParallelProcessing.Join();
            ParallelProcessing.Abort();
        }


        ~Online_Speech_Recognition()
        {
            ParallelProcessing = null;

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
