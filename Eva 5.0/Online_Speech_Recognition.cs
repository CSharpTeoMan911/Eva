using System;
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


    internal class Online_Speech_Recognition:MainWindow
    {
        private static Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.WebSearch, "web search");
        private static System.Threading.Thread ParallelProcessing;


        public static Task<bool> Online_Speech_Recognition_Session_Creation_And_Initiation()
        {

            // Initiate the online speech recognizer on another thread.
            //
            // [ BEGIN ]
            // 

            lock (Online_Speech_Recogniser_Listening)
            {
                lock (Online_Speech_Recogniser_Disabled)
                {
                    lock (Window_Minimised)
                    {
                        if (Online_Speech_Recogniser_Disabled == "false")
                        {
                            if (Window_Minimised == "false")
                            {
                                Online_Speech_Recogniser_Listening = "true";

                                ParallelProcessing = new System.Threading.Thread(async () =>
                                {
                                    bool Online_Speech_Recognition_Engine_Initiation_Successful = await Initiate_The_Online_Speech_Recognition_Engine();
                                });
                                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.MTA);
                                ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                ParallelProcessing.IsBackground = false;
                                ParallelProcessing.Start();
                            }
                        }
                    }
                }
            }

            // [ END ]

            return Task.FromResult(true);
        }










        private static async Task<bool> Initiate_The_Online_Speech_Recognition_Engine()
        {
            int Online_Speech_Recogniser_Constraint_Compilation_Procedure_Error_Counter = 0;
            int Online_Speech_Recogniser_Speech_Recognition_Procedure_Error_Counter = 0;

        Online_Speech_Recognition_Session_Initiation:
            try
            {
                online_speech_recognition_timeout = DateTime.Now;
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;


                using (Windows.Media.SpeechRecognition.SpeechRecognizer OnlineSpeechRecognition = new Windows.Media.SpeechRecognition.SpeechRecognizer())
                {
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
                            await OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Clear_Cache);


                            switch (Result.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                            {
                                case true:
                                   
                                    switch ((Result.Text == String.Empty) || (Result.Text == null))
                                    {
                                        case true:
                                            if (OnlineSpeechRecognition != null)
                                            {
                                                await OnlineSpeechRecognition.StopRecognitionAsync();
                                                OnlineSpeechRecognition.Dispose();
                                            }

                                            if (Online_Speech_Recogniser_Speech_Recognition_Procedure_Error_Counter < 20)
                                            {
                                                Online_Speech_Recogniser_Speech_Recognition_Procedure_Error_Counter++;
                                                goto Online_Speech_Recognition_Session_Initiation;
                                            }
                                            break;


                                        case false:

                                            lock (Online_Speech_Recogniser_Disabled)
                                            {
                                                lock (Window_Minimised)
                                                {
                                                    if (Window_Minimised == "true" || Online_Speech_Recogniser_Disabled == "true")
                                                    {
                                                        goto Function_Not_Initiated;
                                                    }
                                                }
                                            }

                                            await Natural_Language_Processing.PreProcessing(Result.Text);

                                        Function_Not_Initiated:;
                                            if (OnlineSpeechRecognition != null)
                                            {
                                                await OnlineSpeechRecognition.StopRecognitionAsync();
                                                OnlineSpeechRecognition.Dispose();
                                            }
                                            break;
                                    }
                                    break;



                                case false:
                                    if(OnlineSpeechRecognition != null)
                                    {
                                        await OnlineSpeechRecognition.StopRecognitionAsync();
                                        OnlineSpeechRecognition.Dispose();
                                    }

                                    if (Online_Speech_Recogniser_Speech_Recognition_Procedure_Error_Counter < 20)
                                    {
                                        Online_Speech_Recogniser_Speech_Recognition_Procedure_Error_Counter++;
                                        goto Online_Speech_Recognition_Session_Initiation;
                                    }
                                    break;
                            }
                            break;

                        case false:
                            if (OnlineSpeechRecognition != null)
                            {
                                await OnlineSpeechRecognition.StopRecognitionAsync();
                                OnlineSpeechRecognition.Dispose();
                            }

                            if (Online_Speech_Recogniser_Constraint_Compilation_Procedure_Error_Counter < 20)
                            {
                                Online_Speech_Recogniser_Constraint_Compilation_Procedure_Error_Counter++;
                                goto Online_Speech_Recognition_Session_Initiation;
                            }
                            break;

                    }

                }

            }
            catch (Exception E)
            {
                if(E.HResult == -2147199735)
                {
                    if (App.PermisissionWindowOpen == false)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Online Speech Recognition Access Denied");
                            OpenPermissionDeclinedWindow.Show();
                        });
                    }
                }
            }

            lock(Online_Speech_Recogniser_Listening)
            {
                Online_Speech_Recogniser_Listening = "false";
            }

            return true;
        }

        private static void OnlineSpeechRecognition_StateChanged(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognizerStateChangedEventArgs args)
        {
            if(sender.State == Windows.Media.SpeechRecognition.SpeechRecognizerState.SpeechDetected)
            {
                lock(Speech_Detected)
                {
                    Speech_Detected = "true";
                }

                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;
                online_speech_recognition_timeout = DateTime.Now;
            }

            lock(Online_Speech_Recogniser_State)
            {
                Online_Speech_Recogniser_State = sender.State.ToString();
            }
        }

        ~Online_Speech_Recognition()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
