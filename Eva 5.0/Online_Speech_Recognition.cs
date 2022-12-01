using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.ClosedCaptioning;

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


    internal class Online_Speech_Recognition
    {
     
        private static System.Threading.Thread ParallelProcessing;



        public static Task<bool> Online_Speech_Recognition_Session_Creation_And_Initiation()
        {


            // Initiate the online speech recognizer on another thread.
            //
            // [ BEGIN ] 


            ParallelProcessing = new System.Threading.Thread(async() =>
            {
                bool Online_Speech_Recognition_Engine_Initiation_Successful = await Initiate_The_Online_Speech_Recognition_Engine();
            });
            ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.MTA);
            ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
            ParallelProcessing.IsBackground = true;
            ParallelProcessing.Start();


            // [ END ]

            return Task.FromResult(true);
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
                                                    lock(MainWindow.FunctionInitiated)
                                                    {
                                                        if (MainWindow.FunctionInitiated != "true")
                                                        {
                                                            goto Function_Not_Initiated;
                                                        }
                                                    }

                                                    await Natural_Language_Processing.PreProcessing(Result.Text);

                                                Function_Not_Initiated:;
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
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced);
        }

    }
}
