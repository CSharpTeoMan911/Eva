using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using static Eva_5._0.Online_Speech_Recognition;
using System.Windows.Controls;
using System.Threading;

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


    public partial class MainWindow : Window
    {
        private static RotateTransform Rotate = new RotateTransform();

        private static System.Collections.Generic.List<string> Online_Speech_Recognition_Timeout_Timer_UI_Intervals = new System.Collections.Generic.List<string>();

        private short Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index;

        private short target_value = 1000;

        private bool Cropped = true;

        public static bool invisibility_mode;


        // COLORS FOR THE CIRCULAR INDICATOR FOR EACH OPERATIONAL MODE (CHATBOT MODE AND NORMAL MODE)
        //
        // BEGIN

        private string normal_mode_outer_elipse_offset_color = "#FF7BBFD8";
        private string normal_mode_outer_elipse_gradient_color = "#FF052544";

        private string normal_mode_activated_outer_elipse_offset_color = "#FF91E1FF";
        private string normal_mode_activated_outer_elipse_gradient_color = "#FF3099FF";

        private string chatbot_mode_outer_elipse_offset_color = "#FF7BD889";
        private string chatbot_mode_outer_elipse_gradient_color = "#FF054406";

        private string chatbot_mode_activated_outer_elipse_offset_color = "#FF00FF1B";
        private string chatbot_mode_activated_outer_elipse_gradient_color = "#FF34F19F";

        // END




        // ONLINE SPEECH RECOGNITION ACTIVATION DELAY MACHANISM VARIABLES 
        //
        // BEGIN

        protected static DateTime? Online_Speech_Recogniser_Activation_Delay_Detector = null;
        private static readonly double Online_Speech_Recogniser_Activation_Delay = 2.9;

        // END




        private System.Timers.Timer AnimationAndFunctionalityTimer;



        protected static System.Diagnostics.Stopwatch online_speech_recogniser_lock_state_time_elapsed = new System.Diagnostics.Stopwatch();

        private static bool online_speech_recogniser_lock_state_time_elapsed_is_enabled;

        public static bool chatgpt_mode_enabled = false;


        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>
        /// 


        // [ BEGIN ] STATIC OBJECTS OBJECTS FOR THE SPEECH RECOGNITION SYSTEM STATE MACHINE THAT ARE ACCESSED IN A THREAD SAFE MANNER

        protected static string Online_Speech_Recogniser_Listening = "false";

        protected static string BeginExecutionAnimation = "false";

        protected static string Speech_Detected = "false";

        protected static string Window_Minimised = "false";

        protected static string Online_Speech_Recogniser_Disabled = "false";

        protected static string Online_Speech_Recogniser_State = "Idle";

        protected static string Wake_Word_Detected = "false";

        protected static string Initiated = "false";

        // [ END ] STATIC OBJECTS OBJECTS FOR THE SPEECH RECOGNITION SYSTEM STATE MACHINE THAT ARE ACCESSED IN A THREAD SAFE MANNER




        protected static DateTime? online_speech_recognition_timeout;





        private bool SwitchOffset;

        private double WindowOffsetArithmetic;


        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>

        protected static bool MainWindowIsClosing;

        private bool Colour_Switch;

        private int Button_Timeout;

        private static byte ExecutionAnimationArithmetic;

        private double InitialRotatorWidth;

        private double RotationValue;

        private int OnOff;

        public MainWindow()
        {
            for (int i = 20; i >= 0; i--)
                Online_Speech_Recognition_Timeout_Timer_UI_Intervals.Add(i.ToString());

            InitializeComponent();
        }

        private void AppFocus()
        {
            Task.Run(() =>
            {
                while (MainWindowIsClosing == false)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (Application.Current.MainWindow != null)
                            {
                                // KEEP THE MAIN WINDOW AS TOPMOST WINDOW (THE ONLINE SPEECH RECOGNITION ENGINE WORKS ONLY IF THE APPLICATION'S WINDOW IS ACTIVE)
                                Application.Current.MainWindow.Topmost = true;
                            }
                        });
                    }
                }
            });
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Check the administartive rights with which the application session is running. If the application rights are the ones of administrator, the application will close.
            // This is done to prevent any security problems due to the fact that the application is operating at a low level within the operating system.

            // [ BEGIN ]

            new Check_Role();

            // [ END ]

            AppFocus();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            InitialRotatorWidth = Rotator.ActualWidth;

            AnimationAndFunctionalityTimer = new System.Timers.Timer();
            AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
            AnimationAndFunctionalityTimer.Interval = 45;
            AnimationAndFunctionalityTimer.Start();
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e) => Wake_Word_Engine.Stop_The_Wake_Word_Engine();

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => Wake_Word_Engine.Stop_The_Wake_Word_Engine();

        private void AnimationAndFunctionalityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // VERIFY IF MAIN WINDOW IS CLOSING
                if (MainWindowIsClosing == true)
                {
                    AnimationAndFunctionalityTimer?.Stop();
                }
                else
                {
                    // IF THE APPLICATION'S UI THREAD DISPATCHER ( THE COMPONENT THAT EXECUTES ACTIONS ON THE UI THREAD ) STOPS, STOP THE TIMER
                    if (Application.Current.Dispatcher.HasShutdownStarted == true)
                    {
                        AnimationAndFunctionalityTimer?.Stop();
                    }
                    else
                    {

                        // COMPONENT THAT MANIPULATES THE SPEECH RECOGNITION ENABLE/DISABLE BUTTON'S TIMEOUT
                        if (Button_Timeout > 0)
                        {
                            Button_Timeout -= 10;
                        }



                        // METHODS AND PARAMETERS THAT MUST BE EXECUTED AND/OR MANIPULATED ON THE UI THREAD,
                        // ARE MOVED ON THE UI THREAD VIA THE "Application.Current.Dispatcher.Invoke()" METHOD
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (Application.Current.MainWindow == null)
                            {
                                AnimationAndFunctionalityTimer?.Stop();
                            }
                            else
                            {
                                if (invisibility_mode == true)
                                {
                                    this.Height = 0;
                                    this.Width = 0;
                                }
                                else
                                {
                                    Crop_Or_UnCrop();
                                    invisibility_mode = false;
                                }


                                // IF THE CALCULATED ONLINE SPEECH RECOGNITION ACTIVATION DELAY INTERVAL IS NOT NULL
                                if (Online_Speech_Recogniser_Activation_Delay_Detector != null)
                                {
                                    // IF THE INTERVAL OF TIME BETWEEN THE CURRENT TIME AND THE TIME WHEN THE ONLINE SPEECH RECOGNITION ENGINE EXCEEDS THE 
                                    // AMOUNT OF SECONDS SET FOR THE SET ONLINE SPEECH RECOGNITION DELAY, MAKE THE APPLICATION MAIN WINDOW'S
                                    // CIRCULAR STATUS INDICATOR BLUE
                                    if (((TimeSpan)(DateTime.UtcNow - Online_Speech_Recogniser_Activation_Delay_Detector)).TotalSeconds > Online_Speech_Recogniser_Activation_Delay)
                                    {
                                        if (chatgpt_mode_enabled == true)
                                        {
                                            OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_outer_elipse_offset_color);
                                            OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_outer_elipse_gradient_color);
                                        }
                                        else
                                        {
                                            OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(normal_mode_outer_elipse_offset_color);
                                            OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(normal_mode_outer_elipse_gradient_color);
                                        }
                                    }
                                    // ELSE MAKE THE CIRCULAR STATUS INDICATOR RED
                                    else
                                    {
                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("Red");
                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FFF13434");
                                    }
                                }

                                // IF THE APPLICATION HAS AN ERROR THAT REQUIRES THE APPLICATION TO SHUT DOWN,
                                // STOP THE TIMER AND HIDE THE WINDOW
                                if (App.Application_Error_Shutdown)
                                {
                                    try
                                    {
                                        if (AnimationAndFunctionalityTimer != null)
                                        {
                                            AnimationAndFunctionalityTimer.Stop();
                                            this.Hide();
                                        }
                                    }
                                    catch { }
                                }

                                // IF THE APPLICATION'S WINDOW IS IN A NORMAL STATE, SET THE VARIABLE THAT DISPLAYS
                                // IF THE WINDOW STATE STATUS IS MINIMISED AS "false"
                                if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                                {
                                    lock (Window_Minimised)
                                    {
                                        Window_Minimised = "false";
                                    }
                                }

                                lock (Wake_Word_Detected)
                                {
                                    // IF THE WAKE WORD ENGINE DETECTED A KEYWORD
                                    if (Wake_Word_Detected == "true")
                                    {
                                        // AFTER THE WAKE WORD DETECTION PROCEDURE IS FINISHED, RESET THE INICATOR TO ITS DEFAULT VALUE
                                        Wake_Word_Detected = "false";

                                        lock (Online_Speech_Recogniser_Disabled)
                                        {
                                            // IF THE ONLINE SPEECH RECOGNITION ENGINE IS NOT DISABLED
                                            if (Online_Speech_Recogniser_Disabled == "false")
                                            {
                                                // INITIATE ON A DIFFERENT CPU THREAD THE SET OF TASKS. THE TASKS WILL BE SYNCHRONISED ON THIS THREAD,
                                                // MENING THAT THE 'await' KEYWORD WILL CONTROL THE 'ParallelProcessing' THREAD, AND NOT THREADS
                                                // WITHIN THE THREADPOOL.

                                                Task.Run(() =>
                                                {
                                                    // CALCULATE THE ACTIVATION DELAY TO BE SET FOR THE ONLINE SPEECH RECOGNITION ENGINE
                                                    // AND INITIATE THE ONLINE SPEECH RECOGNITION ENGINE.
                                                    if (Online_Speech_Recogniser_Delay_Calculator() == true)
                                                    {
                                                        Online_Speech_Recognition.Online_Speech_Recognition_Session_Creation_And_Initiation();
                                                    }
                                                });
                                            }
                                        }

                                    }
                                }


                                lock (Online_Speech_Recogniser_Listening)
                                {

                                    if (Online_Speech_Recogniser_Listening == "true")
                                    {
                                        lock (Online_Speech_Recogniser_Disabled)
                                        {
                                            lock (Wake_Word_Detected)
                                            {
                                                // IF THE ONLINE SPEECH RECOGNITION ENGINE IS DISABLED OR THE WINDOW IS MINIMISED,
                                                // MAKE THE ONLINE SPEECH RECOGNITION ENGINE STOP TAKING INPUT
                                                if (Window_Minimised == "true" || Online_Speech_Recogniser_Disabled == "true")
                                                {
                                                    Wake_Word_Detected = "false";
                                                    Online_Speech_Recogniser_Listening = "false";
                                                    Online_Speech_Recognition.Close_Speech_Recognition_Interface();
                                                }
                                            }
                                        }


                                        lock (Online_Speech_Recogniser_State)
                                        {
                                            // IF THE ONLINE SPEECH RECOGNITION ENGINE IS IN THE 'Idle' OR 'Paused' STATES
                                            if (Online_Speech_Recogniser_State == "Idle" || Online_Speech_Recogniser_State == "Paused")
                                            {
                                                // IF THE ONLINE SPEECH RECOGNITION ENGINE IS NOT ALREADY IN THE LOCK STATE,
                                                // START THE LOCK STATE TIMER AND MARK THE ONLINE SPEECH RECOGNITION ENGINE'S
                                                // OPERATIONAL STATE AS 'LOCKED'
                                                if (online_speech_recogniser_lock_state_time_elapsed_is_enabled == false)
                                                {
                                                    online_speech_recogniser_lock_state_time_elapsed.Start();
                                                    online_speech_recogniser_lock_state_time_elapsed_is_enabled = true;
                                                }

                                                // IF THE ONLINE SPEECH RECOGNITION ENGINE IS ALREADY IN THE LOCK STATE
                                                if (online_speech_recogniser_lock_state_time_elapsed_is_enabled == true)
                                                {
                                                    // IF THE TIME IN WHICH THE ONLINE SPEECH RECOGNITION ENGINE WAS LOCKED IS GREATER
                                                    // THAN 4 SECONDS, STOP THE ONLINE SPEECH RECOGNITION ENGINE FROM TAKING INPUT
                                                    // AND STOP THE ONLINE SPEECH RECOGNITION ENGINE WINDOWS PROCESS
                                                    if (online_speech_recogniser_lock_state_time_elapsed.ElapsedMilliseconds > 4000)
                                                    {
                                                        Online_Speech_Recogniser_Listening = "false";
                                                        void Shutdown()
                                                        {
                                                            Close_Speech_Recognition_Interface();
                                                            OS_Online_Speech_Recognition_Interface_Shutdown();
                                                        }
                                                        Shutdown();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // ELSE, IT MEANS THAT THE ONLINE SPEECH RECOGNITION ENGINE RESUMED ITS OPERATION AND
                                                // THE LOCK STATE TIMER IS STOPPED AND RESET
                                                online_speech_recogniser_lock_state_time_elapsed.Stop();
                                                online_speech_recogniser_lock_state_time_elapsed.Reset();
                                                online_speech_recogniser_lock_state_time_elapsed_is_enabled = false;
                                            }
                                        }

                                        // IF THE TIMEOUT FOR THE ONLINE SPEECH RECOGNITION ENGINE SPEECH TO TEXT OPERATION IS NOT NULL
                                        if (online_speech_recognition_timeout != null)
                                        {
                                            // IF THE DIFFERENCE BETWEEN THE CURRENT TIME AND THE TIME WHEN THE ONLINE SPEECH RECOGNITION ENGINE
                                            // BEGAN THE SPEECH TO TEXT OPERATION IS GREATER THAN 20 SECONDS ADUJUST THE GUI TO DISPLAY THAT
                                            // THE ONLINE SPEECH RECOGNITION ENGINE DOES NOT TAKE INPUT AND STOP THE ONLINE SPEECH
                                            // RECOGNITION ENGINE SPEECH FROM TAKING INPUT
                                            if (((TimeSpan)(DateTime.UtcNow - online_speech_recognition_timeout)).TotalMilliseconds >= 20000)
                                            {
                                                Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index = 0;
                                                Online_Speech_Recognition_Timer_Display.Text = String.Empty;
                                                Online_Speech_Recogniser_Listening = "false";
                                            }
                                            // ELSE IF THE DIFFERENCE BETWEEN THE CURRENT TIME AND THE TIME WHEN THE ONLINE SPEECH RECOGNITION ENGINE
                                            // BEGAN THE SPEECH TO TEXT OPERATION IS LESS THAN 20 SECONDS
                                            else
                                            {
                                                lock (Speech_Detected)
                                                {
                                                    // IF THE ONLINE SPEECH RECOGNITION ENGINE DETECTED SPEECH, RESET THE GUI COUNTER
                                                    // REGARDING THE ONLINE SPEECH RECOGNITION ENGINE TIMEOUT
                                                    if (Speech_Detected == "true")
                                                    {
                                                        Speech_Detected = "false";
                                                        target_value = 1000;
                                                        Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index = 0;
                                                        Online_Speech_Recognition_Timer_Display.Text = String.Empty;
                                                    }
                                                }

                                                // IF THE DIFFERENCE BETWEEN THE CURRENT TIME AND THE TIME WHEN THE ONLINE SPEECH RECOGNITION ENGINE STARTED
                                                // ITS OPERATION IS GREATER OR EQUAL THAN THE CURRENT TARGET VALUE
                                                if (((TimeSpan)(DateTime.UtcNow - online_speech_recognition_timeout)).TotalMilliseconds >= target_value - 300)
                                                {
                                                    // IF THE TARGET VALUE IS SMALLER OR EQUAL THAN 20 SECONDS
                                                    // DECREMENT THE GUI INPUT INTERVAL COUNTDOWN TIMER VALUE
                                                    // BY ONE SECOND AND INCREMENT THE CURRENT TARGET VALUE
                                                    // BY ONE SECOND
                                                    if (target_value <= 20000)
                                                    {
                                                        target_value += 1000;
                                                        Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index++;
                                                    }
                                                }

                                                Online_Speech_Recognition_Timer_Display.Text = Online_Speech_Recognition_Timeout_Timer_UI_Intervals[Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index];

                                                // WHILE THE ONLINE SPEECH RECOGNITION ENGINE IS OPERATING SET THE CIRCULAR STATUS INDICATOR
                                                // COLOR AS BRIGHT BLUE
                                                if (chatgpt_mode_enabled == true)
                                                {
                                                    OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_activated_outer_elipse_offset_color);
                                                    OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_activated_outer_elipse_gradient_color);
                                                }
                                                else
                                                {
                                                    OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(normal_mode_activated_outer_elipse_offset_color);
                                                    OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(normal_mode_activated_outer_elipse_gradient_color);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lock (Initiated)
                                        {
                                            Initiated = "false";
                                        }

                                        target_value = 1000;
                                        Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index = 0;
                                        Online_Speech_Recognition_Timer_Display.Text = String.Empty;

                                        online_speech_recogniser_lock_state_time_elapsed.Stop();
                                        online_speech_recogniser_lock_state_time_elapsed.Reset();
                                        online_speech_recogniser_lock_state_time_elapsed_is_enabled = false;
                                    }
                                }


                                // IF THE APPLICATION'S TIMER WAS SET, CHANGE THE COLOR OF THE TIMER BUTTON
                                // TO BLUE AND PERIODICALLY CALCULATE THE TIMER'S INTERVAL TO DETERMINE
                                // IF THE TIMER REACHED ITS TERMINAL VALUE
                                if (Timer_Interval.IsTimer() == true)
                                {
                                    OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                    OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FF11497F");

                                    bool Time_Interval_Elapsed = Timer_Interval.Calculate_Time_Interval_Left();


                                    // IF THE TIMER REACHED ITS TERMINAL VALUE
                                    if (Time_Interval_Elapsed == true)
                                    {
                                        if (App.TimerWindowOpen == true)
                                        {
                                            // IF THE TIMER WINDOW IS ALREADY OPENED RING THE TIMER
                                            Timer_Window.Ring_Timer = true;
                                        }
                                        else
                                        {
                                            // IF THE TIMER WINDOW IS NOT OPENED, OPEN THE TIMER WINDOW
                                            // AND RING THE TIMER
                                            Timer_Window.Ring_Timer = true;
                                            Timer_Window timer = new Timer_Window();
                                            timer.ShowDialog();
                                        }
                                    }
                                }
                                // IF THE APPLICATION'S TIMER IS NOT SET, CHANGE THE TIMER BUTTON COLOR TO RED
                                else
                                {
                                    OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("Red");
                                    OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FFF13434");
                                }


                                if (RotationValue == 360)
                                {
                                    // IF THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR 
                                    // MADE  A 360 DEGREES ROTATION, RESET THE RECTANGLE TO 
                                    // ITS ORIGINAL POSITION
                                    RotationValue = 0;
                                }
                                else
                                {
                                    // IF THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR 
                                    // DID NOT MAKE A 360 DEGREES ROTATION, INCREMENT ITS 
                                    // CURRENT ROTATIONAL ANGLE BY 7.5 DEGREES
                                    RotationValue += 7.5;
                                }


                                lock (BeginExecutionAnimation)
                                {
                                    if (BeginExecutionAnimation == "true")
                                    {
                                        // IF A PROCESS WAS EXECUTED, BEGIN THE PROCESS EXECUTION ANIMATION
                                        // BY MAKING THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR
                                        // HAVE 0 WIDTH
                                        Rotator.Width = 0;

                                        if (ExecutionAnimationArithmetic == 40)
                                        {
                                            // IF THE 'ExecutionAnimationArithmetic' VARIABLE EQUALS WITH 40
                                            // SET THE COLOR CIRCULAR STATUS INDICATOR TO RED AND SET THE
                                            // 'ExecutionAnimationArithmetic' TO 0
                                            OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                            OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                            ExecutionAnimationArithmetic = 0;
                                            BeginExecutionAnimation = "false";
                                        }
                                        else
                                        {
                                            // IF THE 'ExecutionAnimationArithmetic' VARIABLE IS LESS THAN 40,
                                            // INCREMENT THE 'ExecutionAnimationArithmetic' BY 4 EACH ITERATION
                                            ExecutionAnimationArithmetic += 4;

                                            // IF THE 'ExecutionAnimationArithmetic' VALUE IS GREATER THAN 4 AND
                                            // A MULTIPLE OF 4
                                            if (ExecutionAnimationArithmetic >= 4 && ExecutionAnimationArithmetic % 4 == 0)
                                            {
                                                // MAKE THE COLORS ALTERNATE BETWEEN PALE BLUE AND WHITE EVERY ITERATION
                                                if (Colour_Switch == true)
                                                {
                                                    OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                    OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                    Colour_Switch = false;
                                                }
                                                else
                                                {
                                                    OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                    OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                    Colour_Switch = true;

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // IF NO PROCESS WAS EXECUTED, RESET THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR
                                        // TO ITS ORIGINAL WIDTH
                                        Rotator.Width = InitialRotatorWidth;
                                    }
                                }


                                // SET THE CURRENT ROTATIONAL ANGLE OF THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR
                                Rotate.Angle = RotationValue;
                                Rotator.RenderTransform = Rotate;



                                // GRADIENT FLUCTUATION ANIMATIONS OF ALL ELEMENTS WITHIN THE MAIN WINDOW.
                                // THE GRADIENT FLUCTUATION ANIMATIONS ARE A PRODUCT OF THE LINEAR FUNCTIONS
                                // 'y = x + u' AND 'y = x - u', WHERE THE GRADIENT ('y') IS THE PRODUCT OF
                                // THE ELEMENT OFFSET ('x') INCREMENTED OR DECREMENTED WITH A CERTAIN UNIT
                                // VALUE ('u'). THE ALTERNATION BETWEEN THE 'y = x + u' AND 'y = x - u' 
                                // FUNCTIONS IS GIVEN WHEN SOME CERTAIN LIMIT VALUES ARE REACHED, FOR
                                // EACH FUNCTION RESPECTIVELY
                                //
                                // [ BEGIN ]
                                if (SwitchOffset == true)
                                {
                                    if (WindowOffsetArithmetic > 0)
                                    {
                                        WindowOffsetArithmetic--;
                                        MinimiseTheWindowOffset.Offset += 0.04;
                                        ContractOrExpandTheWindowOffset.Offset += 0.04;
                                        CloseTheWindowButtonOffset.Offset += 0.04;
                                        OpenSettingsMenuButtonOffset.Offset += 0.04;
                                        OpenTimerMenuButtonOffset.Offset += 0.04;
                                        SpeechRecognitionButtonOffset.Offset += 0.04;
                                        OuterElipseOffset.Offset += 0.04;
                                    }
                                    else
                                    {
                                        SwitchOffset = false;
                                    }
                                }
                                else
                                {
                                    if (WindowOffsetArithmetic < 22)
                                    {
                                        WindowOffsetArithmetic++;
                                        MinimiseTheWindowOffset.Offset -= 0.04;
                                        ContractOrExpandTheWindowOffset.Offset -= 0.04;
                                        CloseTheWindowButtonOffset.Offset -= 0.04;
                                        OpenSettingsMenuButtonOffset.Offset -= 0.04;
                                        OpenTimerMenuButtonOffset.Offset -= 0.04;
                                        SpeechRecognitionButtonOffset.Offset -= 0.04;
                                        OuterElipseOffset.Offset -= 0.04;
                                    }
                                    else
                                    {
                                        SwitchOffset = true;
                                    }
                                }
                            }
                        });
                    }
                }
            }
            catch { }
        }

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowIsClosing = true;

            try
            {
                try
                {
                    AnimationAndFunctionalityTimer?.Stop();
                    AnimationAndFunctionalityTimer?.Dispose();
                }
                catch { }

                BeginExecutionAnimation = null;

                Wake_Word_Engine.Stop_The_Wake_Word_Engine();
                Environment.Exit(0);
            }
            catch { }
        }


        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            if (MainWindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        this.DragMove();

                    }

                }

            }
        }

        private void ContractOrExpandTheMainWindow(object sender, RoutedEventArgs e)
        {
            Cropped = !Cropped;
            Crop_Or_UnCrop();
        }

        private void Crop_Or_UnCrop()
        {
            if (Cropped == true)
            {
                Window_Geometry.Rect = new Rect(0, 0, 260, 159);
                ContractOrExpandTheWindowButton.Content = "\xE73F";
                this.Height = 159;
                this.Width = 260;
                Extra_Functionalities.Width = double.NaN;
                Wire1.Width = double.NaN;
                Wire2.Width = double.NaN;
                Main_Display.Width = 45;
                Main_Inner_Display.Width = 40;
                Main_Innermost_Display.Width = 38;
                Online_Speech_Recognition_Timer_Display.Width = double.NaN;
                Online_Speech_Recognition_Timer_Display.Visibility = Visibility.Visible;
                Grid.SetColumn(Main_Window_Controls, 3);
                Grid.SetColumnSpan(Main_Window_Controls, 3);
                OuterElipse.Width = 50;
                OuterElipse.Height = 50;
                InnerElipse.Width = 42;
                InnerElipse.Height = 42;
                Rotator.Height = 52;
                Grid.SetRow(SpeechRecognitionButton, 4);
                Grid.SetRowSpan(SpeechRecognitionButton, 1);
                SpeechRecognitionButton.FontSize = 22;
                ContractOrExpandTheWindowButton.FontSize = 17;
                MinimiseTheWindowButton.FontSize = 17;
                CloseTheWindowButton.FontSize = 17;
            }
            else
            {
                Window_Geometry.Rect = new Rect(0, 0, 120, 120);
                ContractOrExpandTheWindowButton.Content = "\xE740";
                this.Height = 120;
                this.Width = 120;
                Extra_Functionalities.Width = 0;
                Wire1.Width = 0;
                Wire2.Width = 0;
                Main_Display.Width = 0;
                Main_Inner_Display.Width = 0;
                Main_Innermost_Display.Width = 0;
                Online_Speech_Recognition_Timer_Display.Width = 0;
                Online_Speech_Recognition_Timer_Display.Visibility = Visibility.Hidden;
                Grid.SetColumn(Main_Window_Controls, 0);
                Grid.SetColumnSpan(Main_Window_Controls, 6);
                OuterElipse.Width = 40;
                OuterElipse.Height = 40;
                InnerElipse.Width = 33;
                InnerElipse.Height = 33;
                Rotator.Height = 42;
                Grid.SetRow(SpeechRecognitionButton, 3);
                Grid.SetRowSpan(SpeechRecognitionButton, 4);
                SpeechRecognitionButton.FontSize = 19;
                ContractOrExpandTheWindowButton.FontSize = 14;
                MinimiseTheWindowButton.FontSize = 14;
                CloseTheWindowButton.FontSize = 14;
            }
        }


        private void MinimiseTheMainWindow(object sender, RoutedEventArgs e)
        {
            if (MainWindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        Application.Current.MainWindow.WindowState = WindowState.Minimized;

                        lock (Window_Minimised)
                        {
                            Window_Minimised = "true";
                        }

                    }

                }

            }
        }

        private void CloseTheApplication(object sender, RoutedEventArgs e)
        {
            if (MainWindowIsClosing == false)
            {
                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {
                    if (Application.Current.MainWindow != null)
                    {
                        Wake_Word_Engine.Stop_The_Wake_Word_Engine();
                        this.Close();
                    }
                }
            }
        }

        private async void StartOrStopSpeechRecognition(object sender, RoutedEventArgs e)
        {
            // IF THE TIMEOUT IS 0
            if (Button_Timeout == 0)
            {
                // SET THE BUTTON TIMEOUT TO 400 MILLISECONDS
                Button_Timeout = 400;

                // IF THE MAIN WINDOW IS NOT CLOSING
                if (MainWindowIsClosing == false)
                {
                    // IF THE APPLICATION UI DISPATCHER IS NOT CLOSING
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {

                        // IF THE MAIN WINDOW IS NOT NULL
                        if (Application.Current.MainWindow != null)
                        {
                            // CHECK IF THE MICROPHONE IS AVAILABLE
                            bool Microphone_Available = await Check_Microphone_Permission.Check_If_Microphone_Available();

                            if (Microphone_Available == true)
                            {
                                if (OnOff == 0)
                                {
                                    SpeechOn();
                                }
                                else if(OnOff == 1)
                                {
                                    SpeechOff();
                                }
                            }
                            else
                            {
                                // INITIATE THE ERROR PAGE SIGNIFYING THAT THE MICROPHONE IS NOT AVAILABLE
                                // AND/OR THE OS DOES NOT HAVE THE PERMISSION TO ACCESSS THE MICROPHONE
                                Online_Speech_Recognition.Online_Speech_Recognition_Error_Management(
                                                          Online_Speech_Recognition.Online_Speech_Recognition_Error_Type
                                                          .Microphone_Access_Denied);
                            }
                        }

                    }

                }

            }

        }


        private void SpeechOn()
        {
            if (OnOff == 0)
            {
                // LOCK THE VARIABLE ON THE STACK TO BE ACCESSED ONLY BY THE CURRENT THREAD
                lock (Online_Speech_Recogniser_Disabled)
                {
                    Online_Speech_Recogniser_Disabled = "false";
                }

                // CHANGE THE BUTTON CONTENT BY INVOKING THE OPERATION ON THE UI THREAD
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SpeechRecognitionButton.Content = "\xE1D6";
                });

                // START THE WAKE WORD ENGINE PROCESS
                Wake_Word_Engine.Start_The_Wake_Word_Engine();
                Interlocked.Increment(ref OnOff);
            }
        }


        private void SpeechOff()
        {
            if (OnOff == 1)
            {
                // LOCK THE VARIABLE ON THE STACK TO BE ACCESSED ONLY BY THE CURRENT THREAD
                lock (Online_Speech_Recogniser_Disabled)
                {
                    Online_Speech_Recogniser_Disabled = "true";
                }

                // CHANGE THE BUTTON CONTENT BY INVOKING THE OPERATION ON THE UI THREAD
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SpeechRecognitionButton.Content = "\xF781";
                });

                OnOff = 0;

                // TEMINATE THE WAKE WORD ENGINE PROCESS
                Wake_Word_Engine.Stop_The_Wake_Word_Engine();
            }
        }


        private void OpenSettingsWindow(object sender, RoutedEventArgs e)
        {
            if (MainWindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        if (App.SettingsWindowOpen == false)
                        {
                            App.SettingsWindowOpen = true;
                            SettingsWindow SettingWindowObject = new SettingsWindow(new SettingsWindow.OpenSpeech(SpeechOn));
                            SettingWindowObject.Show();
                        }

                    }

                }

            }

        }



        private void OpenTimerWindow(object sender, RoutedEventArgs e)
        {
            if (MainWindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        if (App.TimerWindowOpen == false)
                        {

                            App.TimerWindowOpen = true;
                            Timer_Window TimerWindowObject = new Timer_Window();
                            TimerWindowObject.Show();

                        }

                    }

                }

            }

        }


        protected static bool Online_Speech_Recogniser_Delay_Calculator()
        {
            // DISCOVERED THROUGH RESEARCH AND EXPERIMENTATION THAT THE
            // SERVERS ARE THROWING DROPPING REQUESTS THAT ARE MADE IF
            // THE NUMBER OF REQUESTS MADE EXCEEDS A CERTAIN LIMIT.
            //
            // THIS DISCOVERY WAS MADE BY OBSERVING A GEOMETRICAL
            // RELATED TO THE FACT THAT, IF REQUESTS ARE MADE ONE
            // AFTER ANOTHER, THE 5th REQUEST IS THE ONE THAT WILL
            // FAIL, AND AFTERWARDS IF CONTINOUS REQUESTS ARE MADE
            // A NUMER OF 3 OR 4 REQUESTS WILL BE DROPPED ONE AFTER
            // ANOTHER.
            //
            // THIS BEHAVIOUR IS NORMAL IN ALL ONLINE SPEECH RECOGNITION
            // ENGINES BECAUSE ALL OF THEM HAVE A REQUEST LIMIT THAT IS
            // SET.


            if (Online_Speech_Recogniser_Activation_Delay_Detector == null)
            {
                return true;
            }
            else if (((TimeSpan)(DateTime.UtcNow - Online_Speech_Recogniser_Activation_Delay_Detector)).TotalSeconds > Online_Speech_Recogniser_Activation_Delay)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Open_ChatGPT_Query_Window(object sender, RoutedEventArgs e)
        {
            if (App.ChatGPTResponseWindowOpened == false)
            {
                App.chatGPT_Response_Window = new ChatGPT_Response_Window();
                App.chatGPT_Response_Window.Show();
            }
        }

        ~MainWindow()
        {
            Wake_Word_Engine.Stop_The_Wake_Word_Engine();
        }
    }
}
