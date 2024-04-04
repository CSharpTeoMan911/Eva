using Eva_5._0.Properties;
using System;
using System.Diagnostics;
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

        public enum Online_Speech_Recognition_Interface_Operation
        {
            Online_Speech_Recognition_Interface_Clear_Cache,
            Online_Speech_Recognition_Interface_Shutdown
        }



        public static void Online_Speech_Recognition_Session_Creation_And_Initiation()
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

                                System.Threading.Thread ParallelProcessing = new System.Threading.Thread(async () =>
                                {
                                    await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Sound_Player.Sounds.AppActivationSoundEffect);
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
                // ENSURE THAT THE ONLINE SPEECH RECOGNITION INTERFACE IS CLOSED
                await OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Shutdown);


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
                        await Natural_Language_Processing.PreProcessing(args.Result.Text.ToLower());

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


        public static void Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type error)
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


        public static async void Close_Speech_Recognition_Interface()
        {
            try
            {
                await OnlineSpeechRecognition.ContinuousRecognitionSession?.StopAsync();
                OnlineSpeechRecognition?.Dispose();
            }
            catch { }
        }


        public static Process[] Get_Recogniser_Interfaces()
        {
            Process[] interfaces = new Process[] { };

            try
            {
                interfaces = Process.GetProcessesByName("SpeechRuntime");
            }
            catch { }

            return interfaces;
        }


        public static Task<bool> OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation operation)
        {
            try
            {
                switch (operation)
                {
                    case Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Clear_Cache:
                        // CLEAR THE CACHE OF THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE PROCESS ("SpeechRuntime.exe")
                        //
                        // BEGIN

                        foreach (System.Diagnostics.Process online_speech_recognition_interface in Get_Recogniser_Interfaces())
                        {
                            online_speech_recognition_interface.Refresh();
                        }

                        // END
                        break;



                    case Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Shutdown:
                        lock (Online_Speech_Recogniser_Listening)
                        {
                            lock (Wake_Word_Detected)
                            {
                                if (Online_Speech_Recogniser_Listening == "false" && Wake_Word_Detected == "false")
                                {
                                    if (Get_Recogniser_Interfaces().Count() > 0)
                                    {
                                        // SHUT DOWN THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE PROCESS ("SpeechRuntime.exe")
                                        //
                                        // BEGIN

                                        // DISPOSE THE ONLINE SPEECH RECOGNITION INTERFACE OBJECT
                                        Close_Speech_Recognition_Interface();


                                        // TERMINATE THE WINDOWS OS ONLINE SPEECH RECOGNITION INTERFACE USING
                                        // THE OS' SHELL TO ENSURE THAT THE PROCESS WAS TERMINATED
                                        Process force_kill_speech_recognition_interface = new Process();
                                        force_kill_speech_recognition_interface.StartInfo.FileName = "powershell.exe";
                                        force_kill_speech_recognition_interface.StartInfo.Arguments = "Stop-Process -Name \"SpeechRuntime\" -Force";
                                        force_kill_speech_recognition_interface.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                        force_kill_speech_recognition_interface.StartInfo.CreateNoWindow = true;
                                        force_kill_speech_recognition_interface.StartInfo.UseShellExecute = false;
                                        force_kill_speech_recognition_interface.Start();

                                        // END
                                    }
                                }
                            }
                        }
                        break;
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
