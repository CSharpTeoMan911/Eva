using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using static Eva_5._0.Online_Speech_Recognition;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Threading;
using Windows.Devices.AllJoyn;

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

        public static bool? invisibility_mode;


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
        private static readonly double Online_Speech_Recogniser_Activation_Delay = 2.4;

        // END




        private System.Threading.Thread ParallelProcessing;

        private System.Timers.Timer AnimationAndFunctionalityTimer;

        private System.Timers.Timer SpeechRecognitionInterfaceControlTimer;



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


        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Check the administartive rights with which the application session is running. If the application rights are the ones of administrator, the application will close.
            // This is done to prevent any security problems due to the fact that the application is operating at a low level within the operating system.

            // [ BEGIN ]

            new Check_Role();

            // [ END ]

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;


            InitialRotatorWidth = Rotator.ActualWidth;



            AnimationAndFunctionalityTimer = new System.Timers.Timer();
            AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
            AnimationAndFunctionalityTimer.Interval = 45;
            AnimationAndFunctionalityTimer.Start();

            SpeechRecognitionInterfaceControlTimer = new System.Timers.Timer();
            SpeechRecognitionInterfaceControlTimer.Elapsed += SpeechRecognitionInterfaceControlTimer_Elapsed;
            SpeechRecognitionInterfaceControlTimer.Interval = 1000;
            SpeechRecognitionInterfaceControlTimer.Start();
        }

        private async void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            await Wake_Word_Engine.Stop_The_Wake_Word_Engine();
        }



        private async void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            await Wake_Word_Engine.Stop_The_Wake_Word_Engine();
        }



        private void AnimationAndFunctionalityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // VERIFY IF MAIN WINDOW IS CLOSING
            switch (MainWindowIsClosing)
            {
                // IF MAIN WINDOW IS CLOSING STOP THE TIMER
                case true:
                    try
                    {
                        AnimationAndFunctionalityTimer.Stop();
                    }
                    catch { }
                    break;

                // ELSE, CONTINUE EXECUTING THE TIMER'S CURRENT ITERATION
                case false:


                    // IF THE APPLICATION'S UI THREAD DISPATCHER ( THE COMPONENT THAT EXECUTES ACTIONS ON THE UI THREAD ) STOPS, STOP THE TIMER
                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case true:
                            try
                            {
                                AnimationAndFunctionalityTimer.Stop();
                            }
                            catch { }
                            break;


                        // ELSE, CONTINUE EXECUTING THE TIMER'S CURRENT ITERATION
                        case false:


                            // COMPONENT THAT MANIPULATES THE SPEECH RECOGNITION ENABLE/DISABLE BUTTON'S TIMEOUT
                            if (Button_Timeout > 0)
                            {
                                Button_Timeout -= 10;
                            }



                            // METHODS AND PARAMETERS THAT MUST BE EXECUTED AND/OR MANIPULATED ON THE UI THREAD,
                            // ARE MOVED ON THE UI THREAD VIA THE "Application.Current.Dispatcher.Invoke()" METHOD
                            Application.Current.Dispatcher.Invoke(async () =>
                            {
                                switch (Application.Current.MainWindow == null)
                                {

                                    // IF THE APPLICATION'S MAIN WINDOW IS NULL STOP THE TIMER
                                    case true:

                                        try
                                        {
                                            AnimationAndFunctionalityTimer.Stop();
                                        }
                                        catch { }

                                        break;





                                    // ELSE, CONTINUE EXECUTING THE TIMER'S CURRENT ITERATION
                                    case false:

                                        switch (invisibility_mode)
                                        {
                                            case true:
                                                this.Height = 0;
                                                this.Width = 0;
                                                invisibility_mode = null;
                                                break;

                                            case false:
                                                Crop_Or_UnCrop();
                                                invisibility_mode = null;
                                                break;
                                        }
                                        


                                        // KEEP THE MAIN WINDOW AS TOPMOST WINDOW (THE ONLINE SPEECH RECOGNITION ENGINE WORKS ONLY IF THE APPLICATION'S WINDOW IS ACTIVE)
                                        Application.Current.MainWindow.Topmost = true;


                                        // IF THE CALCULATED ONLINE SPEECH RECOGNITION ACTIVATION DELAY INTERVAL IS NOT NULL
                                        if (Online_Speech_Recogniser_Activation_Delay_Detector != null)
                                        {

                                            // IF THE INTERVAL OF TIME BETWEEN THE CURRENT TIME AND THE TIME WHEN THE ONLINE SPEECH RECOGNITION ENGINE EXCEEDS THE 
                                            // AMOUNT OF SECONDS SET FOR THE SET ONLINE SPEECH RECOGNITION DELAY, MAKE THE APPLICATION MAIN WINDOW'S
                                            // CIRCULAR STATUS INDICATOR BLUE
                                            if (((TimeSpan)(DateTime.Now - Online_Speech_Recogniser_Activation_Delay_Detector)).TotalSeconds > Online_Speech_Recogniser_Activation_Delay)
                                            {
                                                switch (chatgpt_mode_enabled)
                                                {
                                                    case true:
                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_outer_elipse_offset_color);
                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_outer_elipse_gradient_color);
                                                        break;
                                                    case false:
                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(normal_mode_outer_elipse_offset_color);
                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(normal_mode_outer_elipse_gradient_color);
                                                        break;
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
                                                        System.Threading.Thread ParallelProcessing = new System.Threading.Thread(async () =>
                                                        {
                                                            // CALCULATE THE ACTIVATION DELAY TO BE SET FOR THE ONLINE SPEECH RECOGNITION ENGINE
                                                            // AND INITIATE THE ONLINE SPEECH RECOGNITION ENGINE.
                                                            if (await Online_Speech_Recogniser_Delay_Calculator() == true)
                                                            {
                                                                Online_Speech_Recognition.Online_Speech_Recognition_Session_Creation_And_Initiation();
                                                            }
                                                        });
                                                        ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                                                        ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                                        ParallelProcessing.IsBackground = false;
                                                        ParallelProcessing.Start();
                                                    }
                                                }

                                            }
                                        }


                                        lock (Online_Speech_Recogniser_Listening)
                                        {

                                            switch (Online_Speech_Recogniser_Listening)
                                            {
                                                // IF THE ONLINE SPEECH RECOGNITION ENGINE IS LISTENING
                                                case "true":


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
                                                        switch (Online_Speech_Recogniser_State == "Idle" || Online_Speech_Recogniser_State == "Paused")
                                                        {
                                                            // IF THE ONLINE SPEECH RECOGNITION ENGINE IS IN THE 'Idle' OR 'Paused' STATES
                                                            case true:

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
                                                                        async void Shutdown()
                                                                        {
                                                                            Close_Speech_Recognition_Interface();
                                                                            await OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Shutdown);
                                                                        }
                                                                        Shutdown();
                                                                    }
                                                                }
                                                                break;

                                                            // ELSE, IT MEANS THAT THE ONLINE SPEECH RECOGNITION ENGINE RESUMED ITS OPERATION AND
                                                            // THE LOCK STATE TIMER IS STOPPED AND RESET
                                                            case false:
                                                                online_speech_recogniser_lock_state_time_elapsed.Stop();
                                                                online_speech_recogniser_lock_state_time_elapsed.Reset();
                                                                online_speech_recogniser_lock_state_time_elapsed_is_enabled = false;
                                                                break;
                                                        }
                                                    }

                                                    // IF THE TIMEOUT FOR THE ONLINE SPEECH RECOGNITION ENGINE SPEECH TO TEXT OPERATION IS NOT NULL
                                                    if (online_speech_recognition_timeout != null)
                                                    {
                                                        switch (((TimeSpan)(DateTime.Now - online_speech_recognition_timeout)).TotalMilliseconds >= 20000)
                                                        {
                                                            // IF THE DIFFERENCE BETWEEN THE CURRENT TIME AND THE TIME WHEN THE ONLINE SPEECH RECOGNITION ENGINE
                                                            // BEGAN THE SPEECH TO TEXT OPERATION IS GREATER THAN 20 SECONDS ADUJUST THE GUI TO DISPLAY THAT
                                                            // THE ONLINE SPEECH RECOGNITION ENGINE DOES NOT TAKE INPUT AND STOP THE ONLINE SPEECH
                                                            // RECOGNITION ENGINE SPEECH FROM TAKING INPUT
                                                            case true:
                                                                Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index = 0;
                                                                Online_Speech_Recognition_Timer_Display.Text = String.Empty;
                                                                Online_Speech_Recogniser_Listening = "false";
                                                                break;


                                                            // ELSE IF THE DIFFERENCE BETWEEN THE CURRENT TIME AND THE TIME WHEN THE ONLINE SPEECH RECOGNITION ENGINE
                                                            // BEGAN THE SPEECH TO TEXT OPERATION IS LESS THAN 20 SECONDS
                                                            case false:
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
                                                                if (((TimeSpan)(DateTime.Now - online_speech_recognition_timeout)).TotalMilliseconds >= target_value - 300)
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

                                                                switch (chatgpt_mode_enabled)
                                                                {
                                                                    case true:
                                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_activated_outer_elipse_offset_color);
                                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(chatbot_mode_activated_outer_elipse_gradient_color);
                                                                        break;
                                                                    case false:
                                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString(normal_mode_activated_outer_elipse_offset_color);
                                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString(normal_mode_activated_outer_elipse_gradient_color);
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    break;




                                                // ELSE IF THE ONLINE SPEECH RECOGNITION ENGINE IS NOT LISTENING
                                                // RESET THE GUI TIMEOUT INDICATOR, LOCK STATE TIMERS, AND TARGET
                                                // VALUE TO THEIR DEFAULT VALUES.
                                                case "false":
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
                                                    break;
                                            }
                                        }


                                        switch (Timer_Interval._isTimer)
                                        {
                                            // IF THE APPLICATION'S TIMER WAS SET, CHANGE THE COLOR OF THE TIMER BUTTON
                                            // TO BLUE AND PERIODICALLY CALCULATE THE TIMER'S INTERVAL TO DETERMINE
                                            // IF THE TIMER REACHED ITS TERMINAL VALUE
                                            case true:
                                                OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FF11497F");

                                                bool Time_Interval_Elapsed = await Timer_Interval.Calculate_Time_Interval_Left();


                                                // IF THE TIMER REACHED ITS TERMINAL VALUE
                                                if (Time_Interval_Elapsed == true)
                                                {

                                                    switch (App.TimerWindowOpen)
                                                    {
                                                        // IF THE TIMER WINDOW IS ALREADY OPENED RING THE TIMER
                                                        case true:
                                                            Timer_Window.Ring_Timer = true;
                                                            break;


                                                        // IF THE TIMER WINDOW IS NOT OPENED, OPEN THE TIMER WINDOW
                                                        // AND RING THE TIMER
                                                        case false:
                                                            Timer_Window.Ring_Timer = true;
                                                            Timer_Window timer = new Timer_Window();
                                                            timer.ShowDialog();
                                                            break;
                                                    }
                                                }
                                                break;

                                            // IF THE APPLICATION'S TIMER IS NOT SET, CHANGE THE TIMER BUTTON COLOR TO RED
                                            case false:
                                                OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("Red");
                                                OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FFF13434");
                                                break;
                                        }





                                        switch (RotationValue == 360)
                                        {
                                            // IF THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR 
                                            // MADE  A 360 DEGREES ROTATION, RESET THE RECTANGLE TO 
                                            // ITS ORIGINAL POSITION
                                            case true:
                                                RotationValue = 0;
                                                break;

                                            // IF THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR 
                                            // DID NOT MAKE A 360 DEGREES ROTATION, INCREMENT ITS 
                                            // CURRENT ROTATIONAL ANGLE BY 7.5 DEGREES
                                            case false:
                                                RotationValue += 7.5;
                                                break;
                                        }

                                        lock (BeginExecutionAnimation)
                                        {
                                            switch (BeginExecutionAnimation == "true")
                                            {
                                                // IF A PROCESS WAS EXECUTED, BEGIN THE PROCESS EXECUTION ANIMATION
                                                // BY MAKING THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR
                                                // HAVE 0 WIDTH
                                                case true:
                                                    Rotator.Width = 0;

                                                    switch (ExecutionAnimationArithmetic == 40)
                                                    {
                                                        // IF THE 'ExecutionAnimationArithmetic' VARIABLE EQUALS WITH 40
                                                        // SET THE COLOR CIRCULAR STATUS INDICATOR TO RED AND SET THE
                                                        // 'ExecutionAnimationArithmetic' TO 0
                                                        case true:
                                                            OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                            OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                            ExecutionAnimationArithmetic = 0;
                                                            BeginExecutionAnimation = "false";
                                                            break;

                                                        // IF THE 'ExecutionAnimationArithmetic' VARIABLE IS LESS THAN 40,
                                                        // INCREMENT THE 'ExecutionAnimationArithmetic' BY 4 EACH ITERATION
                                                        case false:
                                                            ExecutionAnimationArithmetic += 4;

                                                            // IF THE 'ExecutionAnimationArithmetic' VALUE IS GREATER THAN 4 AND
                                                            // A MULTIPLE OF 4
                                                            if (ExecutionAnimationArithmetic >= 4 && ExecutionAnimationArithmetic % 4 == 0)
                                                            {
                                                                // MAKE THE COLORS ALTERNATE BETWEEN PALE BLUE AND WHITE EVERY ITERATION
                                                                switch (Colour_Switch)
                                                                {
                                                                    case true:
                                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                        Colour_Switch = false;
                                                                        break;

                                                                    case false:
                                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                        Colour_Switch = true;
                                                                        break;
                                                                }
                                                            }
                                                            break;
                                                    }
                                                    break;

                                                // IF NO PROCESS WAS EXECUTED, RESET THE RECTANGLE WITHIN THE CIRCULAR STATUS INDICATOR
                                                // TO ITS ORIGINAL WIDTH
                                                case false:
                                                    Rotator.Width = InitialRotatorWidth;
                                                    break;

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

                                        switch (SwitchOffset)
                                        {
                                            case true:

                                                switch (WindowOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        WindowOffsetArithmetic--;
                                                        MinimiseTheWindowOffset.Offset += 0.04;
                                                        ContractOrExpandTheWindowOffset.Offset += 0.04;
                                                        CloseTheWindowButtonOffset.Offset += 0.04;
                                                        OpenSettingsMenuButtonOffset.Offset += 0.04;
                                                        OpenTimerMenuButtonOffset.Offset += 0.04;
                                                        SpeechRecognitionButtonOffset.Offset += 0.04;
                                                        OuterElipseOffset.Offset += 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (WindowOffsetArithmetic < 22)
                                                {
                                                    case true:
                                                        WindowOffsetArithmetic++;
                                                        MinimiseTheWindowOffset.Offset -= 0.04;
                                                        ContractOrExpandTheWindowOffset.Offset -= 0.04;
                                                        CloseTheWindowButtonOffset.Offset -= 0.04;
                                                        OpenSettingsMenuButtonOffset.Offset -= 0.04;
                                                        OpenTimerMenuButtonOffset.Offset -= 0.04;
                                                        SpeechRecognitionButtonOffset.Offset -= 0.04;
                                                        OuterElipseOffset.Offset -= 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOffset = true;
                                                        break;
                                                }

                                                break;
                                        }


                                        // [ END ]

                                        break;
                                }
                            });

                            break;
                    }

                    break;
            }
        }

        private void SpeechRecognitionInterfaceControlTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (Online_Speech_Recogniser_Listening)
            {
                lock (Wake_Word_Detected)
                {
                    // IF THE ONLINE SPEECH RECOGNITION ENGINE IS NOT LISTENING,
                    // ENSURE THAT THE INTERFACE OF THE ONLINE SPEECH
                    // RECOGNITION IS CLOSED
                    async void Inactivity_Shutdown()
                    {
                        await OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Shutdown);
                    }
                    Inactivity_Shutdown();
                }
            }
        }

        private async void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
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

                try
                {
                    SpeechRecognitionInterfaceControlTimer?.Close();
                    SpeechRecognitionInterfaceControlTimer?.Dispose();
                }
                catch { }

                BeginExecutionAnimation = null;

                ParallelProcessing = null;


                await Wake_Word_Engine.Stop_The_Wake_Word_Engine();
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
            switch (Cropped)
            {
                case true:
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
                    break;


                case false:
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
                    break;
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

        private async void CloseTheApplication(object sender, RoutedEventArgs e)
        {
            if (MainWindowIsClosing == false)
            {
                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {
                    if (Application.Current.MainWindow != null)
                    {
                        await Wake_Word_Engine.Stop_The_Wake_Word_Engine();
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

                            switch (Microphone_Available)
                            {
                                // IF THE MICROPHONE IS AVAILABLE
                                case true:

                                    // RUN THE WAKE WORD WNGINE INITIATION / TERMINATION
                                    switch (OnOff)
                                    {
                                        case 0:
                                            await SpeechOn();
                                            break;



                                        case 1:
                                            await SpeechOff();
                                            break;
                                    }
                                    break;

                                case false:
                                    // INITIATE THE ERROR PAGE SIGNIFYING THAT THE MICROPHONE IS NOT AVAILABLE
                                    // AND/OR THE OS DOES NOT HAVE THE PERMISSION TO ACCESSS THE MICROPHONE
                                    Online_Speech_Recognition.Online_Speech_Recognition_Error_Management(
                                                              Online_Speech_Recognition.Online_Speech_Recognition_Error_Type
                                                              .Microphone_Access_Denied);
                                    break;
                            }

                        }

                    }

                }

            }

        }


        private Task<bool> SpeechOn()
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

            return Task.FromResult(true);
        }


        private async Task<bool> SpeechOff()
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
                await Wake_Word_Engine.Stop_The_Wake_Word_Engine();
            }

            return true;
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


        protected static Task<bool> Online_Speech_Recogniser_Delay_Calculator()
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
                return Task.FromResult(true);
            }
            else if (((TimeSpan)(DateTime.Now - Online_Speech_Recogniser_Activation_Delay_Detector)).TotalSeconds > Online_Speech_Recogniser_Activation_Delay)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }



        ~MainWindow()
        {
            async void Execute()
            {
                await Wake_Word_Engine.Stop_The_Wake_Word_Engine();
            }
            Execute();
        }

        private void Open_ChatGPT_Query_Window(object sender, RoutedEventArgs e)
        {
            if (App.ChatGPTResponseWindowOpened == false)
            {
                App.chatGPT_Response_Window = new ChatGPT_Response_Window();
                App.chatGPT_Response_Window.Show();
            }
        }
    }
}
