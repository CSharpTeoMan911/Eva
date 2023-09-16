using System;
using System.Linq;
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


    internal class Online_Speech_Recognition : MainWindow
    {
        private static Windows.Media.SpeechRecognition.SpeechRecognizer OnlineSpeechRecognition;
        private static Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.FormFilling, "form-filling", "form");
        private static System.Threading.Thread ParallelProcessing;


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



        protected static Task<bool> Online_Speech_Recognition_Session_Creation_And_Initiation()
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
                        OnlineSpeechRecognition.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(9);
                        OnlineSpeechRecognition.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(9);
                        OnlineSpeechRecognition.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(9);
                        OnlineSpeechRecognition.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
                        OnlineSpeechRecognition.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;


                        await OnlineSpeechRecognition.ContinuousRecognitionSession.StartAsync();
                        await OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Clear_Cache);
                        break;

                    case false:
                        await Close_Speech_Recognition_Interface();
                        break;

                }



            }
            catch (Exception E)
            {
                if (E.HResult == -2147199735)
                {
                    await Close_Speech_Recognition_Interface();
                    await Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type.Online_Speech_Recognition_Access_Denied);
                }
            }

            return true;
        }

        private static async void ContinuousRecognitionSession_ResultGenerated(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            try
            {
                switch (args.Result.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                {
                    case true:

                        switch ((args.Result.Text == String.Empty) || (args.Result == null))
                        {
                            case true:
                                await Close_Speech_Recognition_Interface();
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

                                System.Diagnostics.Debug.WriteLine("Result: " + args.Result.Text);
                                await Natural_Language_Processing_Mitigator.PreProcessing_Initiation(args.Result.Text.ToLower());

                            Function_Not_Initiated:
                                await Close_Speech_Recognition_Interface();
                                break;
                        }
                        break;



                    case false:
                        await Close_Speech_Recognition_Interface();
                        break;

                }
            }
            catch (Exception E)
            {
                if (E.HResult == -2147199735)
                {
                    await Close_Speech_Recognition_Interface();
                    await Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type.Online_Speech_Recognition_Access_Denied);
                }
            }


            lock (Online_Speech_Recogniser_Listening)
            {
                Online_Speech_Recogniser_Listening = "false";
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;
            }

        }

        private static async void ContinuousRecognitionSession_Completed(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs args)
        {
            lock (Online_Speech_Recogniser_Listening)
            {
                Online_Speech_Recogniser_Listening = "false";
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;
            }

            await Close_Speech_Recognition_Interface();
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


        protected static async Task<bool> Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type error)
        {
            bool result = false;

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

            return result;
        }


        private static async Task<bool> Close_Speech_Recognition_Interface()
        {
            bool result = false;

            if (OnlineSpeechRecognition != null)
            {
                try
                {
                    await OnlineSpeechRecognition.ContinuousRecognitionSession.StopAsync();
                    OnlineSpeechRecognition.Dispose();
                }
                catch { }
            }

            return result;
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
                                online_speech_recognition_interface.Kill();
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
