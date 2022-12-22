﻿using System;
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
        public static DateTime online_speech_recognition_timeout;
        public static int ThreadCounter;


        public static Task<bool> Online_Speech_Recognition_Session_Creation_And_Initiation()
        {


            // Initiate the online speech recognizer on another thread.
            //
            // [ BEGIN ] 

            if(ThreadCounter <= 5)
            {
                lock(MainWindow.Online_Speech_Recogniser_Thread_Initiated)
                {
                    MainWindow.Online_Speech_Recogniser_Thread_Initiated = "true";
                }

                ThreadCounter++;

                ParallelProcessing = new System.Threading.Thread(async () =>
                {
                    bool Online_Speech_Recognition_Engine_Initiation_Successful = await Initiate_The_Online_Speech_Recognition_Engine();
                });
                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.MTA);
                ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                ParallelProcessing.IsBackground = true;
                ParallelProcessing.Start();
            }


            // [ END ]

            return Task.FromResult(true);
        }










        private static async Task<bool> Initiate_The_Online_Speech_Recognition_Engine()
        {
            
            try
            {
                online_speech_recognition_timeout = DateTime.Now;

                using (Windows.Media.SpeechRecognition.SpeechRecognizer OnlineSpeechRecognition = new Windows.Media.SpeechRecognition.SpeechRecognizer())
                {
                    Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.WebSearch, "web search");

                    Constraints.Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max;

                    OnlineSpeechRecognition.Constraints.Add(Constraints);

                    Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult ConstratintsCompilation = await OnlineSpeechRecognition.CompileConstraintsAsync();


                    switch (ConstratintsCompilation.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                    {
                        case true:
                            OnlineSpeechRecognition.StateChanged += OnlineSpeechRecognition_StateChanged;

                            OnlineSpeechRecognition.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(9);
                            OnlineSpeechRecognition.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(9);
                            OnlineSpeechRecognition.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(9);

                            Windows.Media.SpeechRecognition.SpeechRecognitionResult Result = await OnlineSpeechRecognition.RecognizeAsync();

                            switch (Result.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                            {
                                case true:
                                   
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

                                                    lock(MainWindow.Online_Speech_Recogniser_Disabled)
                                                    {
                                                        lock(MainWindow.Window_Minimsed)
                                                        {
                                                            if (MainWindow.Window_Minimsed == "true" || MainWindow.Online_Speech_Recogniser_Disabled == "true")
                                                            {
                                                                goto Function_Not_Initiated;
                                                            }
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

            if(ThreadCounter > 0)
            {
                ThreadCounter--;
            }

            return true;
        }

        private static void OnlineSpeechRecognition_StateChanged(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognizerStateChangedEventArgs args)
        {
            if(sender.State == Windows.Media.SpeechRecognition.SpeechRecognizerState.SpeechDetected)
            {
                lock(MainWindow.Speech_Detected)
                {
                    MainWindow.Speech_Detected = "true";
                }

                online_speech_recognition_timeout = DateTime.Now;
            }
        }

        protected static Task<bool> OS_Online_Speech_Recognition_Interface_Shutdown()
        {

            // SHUT DOWN THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE
            // BEGIN

      
            try
            {
                foreach (System.Diagnostics.Process online_speech_recognition_interface in System.Diagnostics.Process.GetProcessesByName("SpeechRuntime"))
                {
                    online_speech_recognition_interface.Kill();
                }
            }
            catch
            {
                return Task.FromResult(false);
            }

            // END

            return Task.FromResult(true);
        }



        ~Online_Speech_Recognition()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced);
        }

    }
}
