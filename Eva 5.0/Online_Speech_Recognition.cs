using Eva_5._0.Properties;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Maps;

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


    internal class Online_Speech_Recognition : MainWindow
    {
        private static Windows.Media.SpeechRecognition.SpeechRecognizer OnlineSpeechRecognition;
        private static Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.FormFilling, "form-filling", "form");

        public enum Online_Speech_Recognition_Error_Type
        {
            Online_Speech_Recognition_Access_Denied,
            Mircrophone_Access_Denied
        }


        // COMPONENTS THAT INTERACT WITH SECURTY SENSITIVE FEATURES ARE CONTAINED INSIDE PRIVATE SEALED CLASSES FOR EXTRA PROTECTION
        //
        // [ BEGIN ]

        private sealed class Natural_Language_Processing_Mitigator : Natural_Language_Processing
        {
            internal static async Task<bool> PreProcessing_Initiation(string Sentence)
            {
                return await PreProcessing(Sentence);
            }
        }

        // [ END ]



        protected static void Online_Speech_Recognition_Session_Creation_And_Initiation()
        {

            // Initiate the online speech recognizer on another thread and lock multiple objects in memory
            // in order to block other threads from modifying them
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
                                async void Execute()
                                {
                                    await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Sound_Player.Sounds.AppActivationSoundEffect);
                                    Online_Speech_Recogniser_Listening = "true";
                                }
                                Execute();

                                System.Threading.Thread ParallelProcessing = new System.Threading.Thread(() =>
                                {
                                    Initiate_The_Online_Speech_Recognition_Engine();
                                });
                                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                                ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                ParallelProcessing.IsBackground = false;
                                ParallelProcessing.Start();
                            }
                        }
                    }
                }
            }

            // [ END ]
        }


        private static async void Initiate_The_Online_Speech_Recognition_Engine()
        {
            try
            {
                online_speech_recognition_timeout = DateTime.Now;
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;
                OnlineSpeechRecognition = new Windows.Media.SpeechRecognition.SpeechRecognizer();


                Constraints.Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max;
                OnlineSpeechRecognition.Constraints.Add(Constraints);

                Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult ConstratintsCompilation = await OnlineSpeechRecognition.CompileConstraintsAsync();

                switch (ConstratintsCompilation.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                {
                    case true:
                        OnlineSpeechRecognition.StateChanged += OnlineSpeechRecognition_StateChanged;
                        OnlineSpeechRecognition.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.FromSeconds(9);
                        OnlineSpeechRecognition.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
                        OnlineSpeechRecognition.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;

                        await OnlineSpeechRecognition.ContinuousRecognitionSession.StartAsync();

                        // CLEAR THE CACHE OF THE ONLINE SPEECH RECOGNITION INTERFACE
                        await OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Clear_Cache);
                        break;

                    case false:
                        Close_Speech_Recognition_Interface();
                        break;
                }



            }
            catch (Exception E)
            {
                if (E.HResult == -2147199735)
                {
                    Close_Speech_Recognition_Interface();
                    Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type.Online_Speech_Recognition_Access_Denied);
                }
            }
        }

        private static async void ContinuousRecognitionSession_ResultGenerated(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            try
            {
                switch ((args.Result.Text == String.Empty) || (args.Result == null))
                {
                    case true:
                        Close_Speech_Recognition_Interface();
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

                        await Natural_Language_Processing_Mitigator.PreProcessing_Initiation(args.Result.Text.ToLower());

                    Function_Not_Initiated:
                        Close_Speech_Recognition_Interface();
                        break;
                }
            }
            catch (Exception E)
            {
                if (E.HResult == -2147199735)
                {
                    Close_Speech_Recognition_Interface();
                    Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type.Online_Speech_Recognition_Access_Denied);
                }
            }


            lock (Online_Speech_Recogniser_Listening)
            {
                Online_Speech_Recogniser_Listening = "false";
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;
            }
        }

        private static void ContinuousRecognitionSession_Completed(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs args)
        {
            lock (Online_Speech_Recogniser_Listening)
            {
                Online_Speech_Recogniser_Listening = "false";
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;
            }

            Close_Speech_Recognition_Interface();
        }

        private static void OnlineSpeechRecognition_StateChanged(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognizerStateChangedEventArgs args)
        {

            if (sender.State == Windows.Media.SpeechRecognition.SpeechRecognizerState.SpeechDetected)
            {
                lock (Speech_Detected)
                {
                    if (Speech_Detected == "false")
                    {
                        Speech_Detected = "true";
                        Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;

                        lock (Initiated)
                        {
                            if (Initiated == "false")
                            {
                                online_speech_recognition_timeout = DateTime.Now;
                                Initiated = "true";
                            }
                        }
                    }
                }
            }

            lock (Online_Speech_Recogniser_State)
            {
                Online_Speech_Recogniser_State = sender.State.ToString();
            }
        }


        protected static void Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type error)
        {
            switch (error)
            {
                case Online_Speech_Recognition_Error_Type.Online_Speech_Recognition_Access_Denied:
                    if (App.PermisissionWindowOpen == false)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Online Speech Recognition Access Denied");
                            OpenPermissionDeclinedWindow.Show();
                        });
                    }
                    break;
                case Online_Speech_Recognition_Error_Type.Mircrophone_Access_Denied:
                    if (App.PermisissionWindowOpen == false)
                    {
                        ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Mircrophone Access Denied");
                        OpenPermissionDeclinedWindow.Show();
                    }
                    break;
            }
        }


        private static async void Close_Speech_Recognition_Interface()
        {

            if (OnlineSpeechRecognition != null)
            {
                try
                {
                    await OnlineSpeechRecognition.ContinuousRecognitionSession.StopAsync();
                    OnlineSpeechRecognition.Dispose();
                }
                catch { }
            }

        }


        protected static Task<bool> OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation operation)
        {

            try
            {
                System.Diagnostics.Process[] online_speech_recognition_interface_instances = System.Diagnostics.Process.GetProcessesByName("SpeechRuntime");

                if (online_speech_recognition_interface_instances.Count() > 0)
                {
                    switch (operation)
                    {
                        case Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Clear_Cache:
                            // CLEAR THE CACHE OF THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE PROCESS ("SpeechRuntime.exe")
                            //
                            // BEGIN

                            foreach (System.Diagnostics.Process online_speech_recognition_interface in online_speech_recognition_interface_instances)
                            {
                                online_speech_recognition_interface.Start();
                                online_speech_recognition_interface.Refresh();
                            }

                            // END
                            break;



                        case Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Shutdown:
                            // SHUT DOWN THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE PROCESS ("SpeechRuntime.exe")
                            //
                            // BEGIN

                            foreach (System.Diagnostics.Process online_speech_recognition_interface in online_speech_recognition_interface_instances)
                            {
                                online_speech_recognition_interface.Dispose();
                            }

                            // END
                            break;
                    }
                }
            }
            catch
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
