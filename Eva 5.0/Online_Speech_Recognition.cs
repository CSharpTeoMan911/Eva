using Eva_5._0.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
#nullable enable
        private static Windows.Media.SpeechRecognition.SpeechRecognizer? OnlineSpeechRecognition;

        private static Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Form_Filling_Constraint = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.FormFilling, "form-filling", "form")
        {
            IsEnabled = true,
            Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max
        };

        private static Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Web_Search_Constraint = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.WebSearch, "web-search", "web")
        {
            IsEnabled = true,
            Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max
        };

        private static Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint Dictation_Constraint = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.FormFilling, "dictation", "dict")
        {
            IsEnabled = true,
            Probability = Windows.Media.SpeechRecognition.SpeechRecognitionConstraintProbability.Max
        };



        public enum Online_Speech_Recognition_Error_Type
        {
            Online_Speech_Recognition_Access_Denied,
            Microphone_Access_Denied,
            Language_Not_Supported
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void Online_Speech_Recognition_Session_Creation_And_Initiation()
        {
            bool operational = false;

            lock (Online_Speech_Recogniser_Disabled)
            {
                lock (Window_Minimised)
                {
                    lock (Online_Speech_Recogniser_Listening)
                    {
                        if (Online_Speech_Recogniser_Disabled == "false" && Online_Speech_Recogniser_Listening == "false" && Window_Minimised == "false")
                            operational = true;
                    }
                }
            }

            int Timeout = await Settings.Get_Speech_Timeout_Settings();

            if (operational == true)
                Initiate_The_Online_Speech_Recognition_Engine(Timeout);
        }


        private static async void Initiate_The_Online_Speech_Recognition_Engine(int Timeout)
        {
            try
            {
                DateTime start = DateTime.UtcNow;

                // ENSURE THAT THE ONLINE SPEECH RECOGNITION INTERFACE IS CLOSED
                OS_Online_Speech_Recognition_Interface_Shutdown();

                // SET THE SPEECH RECOGNITION TIMEOUT AND SPEECH RECOGNITION VARIABLES AS THE CURRENT TIME
                online_speech_recognition_timeout = DateTime.UtcNow;
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.UtcNow;


                // INITIATE THE SPEECH RECOGNITION ENGINE
                OnlineSpeechRecognition = new Windows.Media.SpeechRecognition.SpeechRecognizer(new Windows.Globalization.Language(await Settings.Get_Speech_Language_Settings()));


                OnlineSpeechRecognition.Constraints.Clear();
                switch (await Settings.Get_Speech_Operation_Settings())
                {
                    case A_p_l____And____P_r_o_c.SpeechRecognitionOperation.FormFilling:
                        OnlineSpeechRecognition.Constraints.Add(Form_Filling_Constraint);
                        break;
                    case A_p_l____And____P_r_o_c.SpeechRecognitionOperation.Dictation:
                        OnlineSpeechRecognition.Constraints.Add(Dictation_Constraint);
                        break;
                    case A_p_l____And____P_r_o_c.SpeechRecognitionOperation.WebSearch:
                        OnlineSpeechRecognition.Constraints.Add(Web_Search_Constraint);
                        break;
                }

                Windows.Media.SpeechRecognition.SpeechRecognitionCompilationResult ConstraintsCompilation = await OnlineSpeechRecognition.CompileConstraintsAsync();

                if (ConstraintsCompilation.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                {
                    lock (Online_Speech_Recogniser_Listening) 
                        Online_Speech_Recogniser_Listening = "true";

                    OnlineSpeechRecognition.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.FromSeconds(Timeout);
                    OnlineSpeechRecognition.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(Timeout);
                    OnlineSpeechRecognition.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(Timeout);
                    OnlineSpeechRecognition.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(Timeout);

                    OnlineSpeechRecognition.StateChanged += OnlineSpeechRecognition_StateChanged;
                    OnlineSpeechRecognition.HypothesisGenerated += OnlineSpeechRecognition_HypothesisGenerated;
                    OnlineSpeechRecognition.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
                    OnlineSpeechRecognition.RecognitionQualityDegrading += OnlineSpeechRecognition_RecognitionQualityDegrading;
                    OnlineSpeechRecognition.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;

                    await OnlineSpeechRecognition.ContinuousRecognitionSession.StartAsync(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionMode.PauseOnRecognition);
                    
                    await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Sound_Player.Sounds.AppActivationSoundEffect);
                }
                else
                {
                    Close_Speech_Recognition_Interface();
                }
            }
            catch (Exception e)
            {
                if (e.HResult == -2147199735)
                {
                    Close_Speech_Recognition_Interface();
                    Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type.Online_Speech_Recognition_Access_Denied);
                }
                else if (e.Message.Contains("The requested language is not supported") == true)
                {
                    Close_Speech_Recognition_Interface();
                    Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type.Language_Not_Supported);
                }
            }
        }

        private static void OnlineSpeechRecognition_HypothesisGenerated(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            if (sender?.State == Windows.Media.SpeechRecognition.SpeechRecognizerState.Paused)
                sender?.ContinuousRecognitionSession?.Resume();
        }

        private static void OnlineSpeechRecognition_RecognitionQualityDegrading(Windows.Media.SpeechRecognition.SpeechRecognizer sender, Windows.Media.SpeechRecognition.SpeechRecognitionQualityDegradingEventArgs args)
        {
            if (sender?.State == Windows.Media.SpeechRecognition.SpeechRecognizerState.Paused)
                sender?.ContinuousRecognitionSession?.Resume();
        }

        private static void ContinuousRecognitionSession_ResultGenerated(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            try
            {
                if ((args.Result.Text == String.Empty) || (args.Result == null))
                {
                    Close_Speech_Recognition_Interface();
                }
                else
                {
                    bool disabled = false;
                    lock (Online_Speech_Recogniser_Disabled)
                    {
                        lock (Window_Minimised)
                        {
                            if (Window_Minimised == "true" || Online_Speech_Recogniser_Disabled == "true")
                            {
                                disabled = true;
                            }
                        }
                    }

                    if (disabled == true)
                    {
                        Close_Speech_Recognition_Interface();
                    }
                    else
                    {
                        Close_Speech_Recognition_Interface();
                        string Result = args.Result.Text.ToLower();
                        Natural_Language_Processing.PreProcessing(StringFormatting.RemoveNewLine(new StringBuilder(Result, Result.Length)));
                    }
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
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.UtcNow;
            }
        }

        private static void ContinuousRecognitionSession_Completed(Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs args)
        {
            lock (Online_Speech_Recogniser_Listening)
            {
                Online_Speech_Recogniser_Listening = "false";
                Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.UtcNow;
            }

            lock (Online_Speech_Recogniser_Listening)
            {
                if (Online_Speech_Recogniser_Listening == "true")
                {
                    if (args.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.UserCanceled)
                    {
                        lock (Online_Speech_Recogniser_State)
                        {
                            if (Online_Speech_Recogniser_State == "Paused")
                            {
                                sender.Resume();
                            }
                        }
                    }
                    else
                    {
                        Close_Speech_Recognition_Interface();
                    }
                }
                else
                {
                    Close_Speech_Recognition_Interface();
                }
            }
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
                        Online_Speech_Recogniser_Activation_Delay_Detector = DateTime.UtcNow;

                        lock (Initiated)
                        {
                            if (Initiated == "false")
                            {
                                online_speech_recognition_timeout = DateTime.UtcNow;
                                Initiated = "true";
                            }
                        }
                    }
                }
            }

            if (sender.State == Windows.Media.SpeechRecognition.SpeechRecognizerState.Paused)
            {
                sender?.ContinuousRecognitionSession?.Resume();
            }

            lock (Online_Speech_Recogniser_State)
            {
                Online_Speech_Recogniser_State = sender?.State.ToString();
            }
        }


        public static void Online_Speech_Recognition_Error_Management(Online_Speech_Recognition_Error_Type error)
        {
            Application.Current.Dispatcher.Invoke(() =>
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
                    case Online_Speech_Recognition_Error_Type.Microphone_Access_Denied:
                        if (App.PermisissionWindowOpen == false)
                        {
                            ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Mircrophone Access Denied");
                            OpenPermissionDeclinedWindow.Show();
                        }
                        break;
                    case Online_Speech_Recognition_Error_Type.Language_Not_Supported:
                        if (App.PermisissionWindowOpen == false)
                        {
                            ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Language not supported");
                            OpenPermissionDeclinedWindow.Show();
                        }
                        break;
                }
            });
        }


        public static void Close_Speech_Recognition_Interface()
        {
            try
            {
                if (OnlineSpeechRecognition != null)
                {
                    OnlineSpeechRecognition.StateChanged -= OnlineSpeechRecognition_StateChanged;
                    OnlineSpeechRecognition.HypothesisGenerated -= OnlineSpeechRecognition_HypothesisGenerated;
                    OnlineSpeechRecognition.ContinuousRecognitionSession.Completed -= ContinuousRecognitionSession_Completed;
                    OnlineSpeechRecognition.RecognitionQualityDegrading -= OnlineSpeechRecognition_RecognitionQualityDegrading;
                    OnlineSpeechRecognition.ContinuousRecognitionSession.ResultGenerated -= ContinuousRecognitionSession_ResultGenerated;

                    OnlineSpeechRecognition.Dispose();
                    int successful = Marshal.FinalReleaseComObject(OnlineSpeechRecognition);
                    OnlineSpeechRecognition = null;

                    GC.Collect(10, GCCollectionMode.Forced);
                    GC.WaitForFullGCComplete();
                }
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

        public static bool OS_Online_Speech_Recognition_Interface_Shutdown()
        {
            try
            {
                lock (Online_Speech_Recogniser_Listening)
                {
                    lock (Wake_Word_Detected)
                    {
                        if (Online_Speech_Recogniser_Listening == "false" && Wake_Word_Detected == "false")
                        {
                            // SHUT DOWN THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE PROCESS ("SpeechRuntime.exe")
                            //
                            // BEGIN

                            // DISPOSE THE ONLINE SPEECH RECOGNITION INTERFACE OBJECT
                            Close_Speech_Recognition_Interface();


                            // TERMINATE THE WINDOWS OS ONLINE SPEECH RECOGNITION INTERFACE USING
                            // THE OS' SHELL TO ENSURE THAT THE PROCESS WAS TERMINATED
                            foreach (Process p in Get_Recogniser_Interfaces())
                            {
                                p.Kill();
                            }
                            // END
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
