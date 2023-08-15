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


    internal class Online_Speech_Recognition:MainWindow
    {
        private static Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Constraints = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.WebSearch, "web-search", "web");
        private static System.Threading.Thread ParallelProcessing;


        // COMPONENTS THAT INTERACT WITH SECURTY SENSITIVE FEATURES ARE CONTAINED INSIDE PRIVATE SEALED CLASSES FOR EXTRA PROTECTION
        //
        // [ BEGIN ]

        private sealed class Natural_Language_Processing_Mitigator:Natural_Language_Processing
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

                            System.Diagnostics.Debug.WriteLine("Result: " + Result.Text);
                            switch (Result.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                            {
                                case true:
                                   
                                    switch ((Result.Text == String.Empty) || (Result == null))
                                    {
                                        case true:
                                            if (OnlineSpeechRecognition != null)
                                            {
                                                await OnlineSpeechRecognition.StopRecognitionAsync();
                                                OnlineSpeechRecognition.Dispose();
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

                                            await Natural_Language_Processing_Mitigator.PreProcessing_Initiation(Result.Text.ToLower());

                                        Function_Not_Initiated:
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
                                    break;
                            }
                            break;

                        case false:
                            if (OnlineSpeechRecognition != null)
                            {
                                await OnlineSpeechRecognition.StopRecognitionAsync();
                                OnlineSpeechRecognition.Dispose();
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

            lock (Online_Speech_Recogniser_Listening)
            {
                Online_Speech_Recogniser_Listening = "false";
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.Now;
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


        protected static Task<bool> OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation operation)
        {

            try
            {
                System.Diagnostics.Process[] online_speech_recognition_interface_instances = System.Diagnostics.Process.GetProcessesByName("SpeechRuntime");

                if(online_speech_recognition_interface_instances.Count() > 0)
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
