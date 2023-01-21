using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Windows.Devices.Sensors;
using Windows.Networking.Connectivity;

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

        private static System.Collections.Generic.List<string> Online_Speech_Recognition_Timeout_Timer_UI_Intervals = new System.Collections.Generic.List<string>() { "9", "8", "7", "6", "5", "4", "3", "2", "1", "0" };

        private short Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index;

        private short target_value = 1000;




        // ONLINE SPEECH RECOGNITION ACTIVATION DELAY MACHANISM VARIABLES 
        //
        // BEGIN

        protected static DateTime? Online_Speech_Recogniser_Activation_Delay_Detector = null;

        private static double Online_Speech_Recogniser_Activation_Delay = 3.55;

        // END




        private System.Threading.Thread ParallelProcessing;

        private System.Timers.Timer AnimationAndFunctionalityTimer;



        protected static System.Diagnostics.Stopwatch online_speech_recogniser_lock_state_time_elapsed = new System.Diagnostics.Stopwatch();

        private static bool online_speech_recogniser_lock_state_time_elapsed_is_enabled;




        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>
        /// 


        // [ BEGIN ] STATIC OBJECTS OBJECTS THAT ARE ACCESSED IN A THREAD SAFE MANNER

        protected static string Online_Speech_Recogniser_Listening = "false";

        protected static string BeginExecutionAnimation = "false";

        protected static string Speech_Detected = "false";

        protected static string Window_Minimised = "false";

        protected static string Online_Speech_Recogniser_Disabled = "false";

        protected static string Online_Speech_Recogniser_State = "Idle";

        protected static string Wake_Word_Detected = "false";

        // [ END ] STATIC OBJECTS OBJECTS THAT ARE ACCESSED IN A THREAD SAFE MANNER




        protected static DateTime? online_speech_recognition_timeout;





        private bool SwitchMinimiseTheWindowOffset;

        private double MinimiseTheWindowOffsetArithmetic;

        private bool SwitchCloseTheWindowButtonOffset;

        private double CloseTheWindowButtonOffsetArithmetic;

        private bool SwitchOpenSettingsMenuButtonOffset;

        private double OpenSettingsMenuButtonOffsetArithmetic;

        private bool SwitchOpenTimerMenuButtonOffset;

        private double OpenTimerMenuButtonOffsetArithmetic;

        private bool SwitchSpeechRecognitionButtonOffset;

        private double SpeechRecognitionButtonOffsetArithmetic;

        private bool SwitchOuterElipseOffset;

        private double OuterElipseOffsetArithmetic;


        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>

        protected static bool Wake_Word_Detection_Initiated;

        protected static bool MainWindowIsClosing;

        private bool Colour_Switch;

        private int Button_Timeout;

        private static byte ExecutionAnimationArithmetic;

        private double InitialRotatorWidth;

        private double RotationValue;

        private byte OnOff;


        public MainWindow()
        {
            InitializeComponent();
        }


        private sealed class Wake_Word_Engine_Mitigator : Wake_Word_Engine
        {
            public static async Task<bool> Wake_Word_Engine_Start()
            {
                return await Start_The_Wake_Word_Engine();
            }

            public static async Task<bool> Wake_Word_Engine_Stop()
            {
                return await Stop_The_Wake_Word_Engine();
            }
        }

        private sealed class Microphone_Permissions_Mitigator : Check_Microphone_Permission
        {
            public static async Task<bool> Check_Microphone_Availability()
            {
                return await Check_If_Microphone_Available();
            }
        }

        protected enum Online_Speech_Recognition_Interface_Operation
        {
            Online_Speech_Recognition_Interface_Clear_Cache,
            Online_Speech_Recognition_Interface_Shutdown
        }


        protected static Task<bool> OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation operation)
        {

            try
            {

                switch (operation)
                {
                    case Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Clear_Cache:
                        // REFRESH THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE PROCESS
                        //
                        // BEGIN

                        foreach (System.Diagnostics.Process online_speech_recognition_interface in System.Diagnostics.Process.GetProcessesByName("SpeechRuntime"))
                        {
                            if(online_speech_recognition_interface.HasExited == false)
                            {
                                online_speech_recognition_interface.Refresh();
                            }
                        }

                        // END
                        break;



                    case Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Shutdown:
                        // SHUT DOWN THE OS' MAIN ONLINE SPEECH RECOGNITION INTERFACE PROCESS
                        //
                        // BEGIN

                    Online_Speech_Recognition_Interface_Shutdown:

                        foreach (System.Diagnostics.Process online_speech_recognition_interface in System.Diagnostics.Process.GetProcessesByName("SpeechRuntime"))
                        {
                            if(online_speech_recognition_interface.HasExited == false)
                            {
                                online_speech_recognition_interface.Kill();

                                if (System.Diagnostics.Process.GetProcessesByName("SpeechRuntime").Count() > 0)
                                {
                                    goto Online_Speech_Recognition_Interface_Shutdown;
                                }
                            }
                        }

                        // END
                        break;
                }

            }
            catch
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }


        private void WindowLoaded(object sender, RoutedEventArgs e)
        {

            // Check the administartive rights with which the application session is running. If the application rights are the ones of administrator, the application will close.
            // This is done to prevent any security problems due to the fact that the application is operating at a low level within the operating system.

            // [ BEGIN ]

            new Check_Role();

            // [ END ]


            InitialRotatorWidth = Rotator.ActualWidth;



            AnimationAndFunctionalityTimer = new System.Timers.Timer();
            AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
            AnimationAndFunctionalityTimer.Interval = 45;
            AnimationAndFunctionalityTimer.Start();
        }


        private void AnimationAndFunctionalityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (MainWindowIsClosing)
            {
                case true:
                    try
                    {
                        AnimationAndFunctionalityTimer.Stop();
                    }
                    catch { }
                    break;

                case false:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case true:
                            try
                            {
                                AnimationAndFunctionalityTimer.Stop();
                            }
                            catch { }
                            break;

                        case false:





                            if(Button_Timeout > 0)
                            {
                                Button_Timeout -= 10;
                            }

                            


                            Application.Current.Dispatcher.Invoke(async() =>
                            {
                                switch (Application.Current.MainWindow == null)
                                {


                                    case true:

                                        try
                                        {
                                            AnimationAndFunctionalityTimer.Stop();
                                        }
                                        catch { }

                                        break;






                                    case false:

                                        Application.Current.MainWindow.Topmost = true;

                                        if(App.Application_Error_Shutdown)
                                        {
                                            try
                                            {
                                                if(AnimationAndFunctionalityTimer != null)
                                                {
                                                    AnimationAndFunctionalityTimer.Stop();
                                                    this.Hide();
                                                }
                                            }
                                            catch { }
                                        }

                                        if(Application.Current.MainWindow.WindowState == WindowState.Normal)
                                        {
                                            lock(Window_Minimised)
                                            {
                                                Window_Minimised = "false";
                                            }
                                        }

                                        
                                        if (Wake_Word_Detected == "true")
                                        {
                                            lock (Wake_Word_Detected)
                                            {
                                                System.Threading.Thread ParallelProcessing = new System.Threading.Thread(async () =>
                                                {
                                                    if (await Online_Speech_Recogniser_Delay_Calculator() == true)
                                                    {
                                                        await Online_Speech_Recognition.Online_Speech_Recognition_Session_Creation_And_Initiation();
                                                    }
                                                });
                                                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                                                ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                                ParallelProcessing.IsBackground = false;
                                                ParallelProcessing.Start();
                                            }

                                            Wake_Word_Detected = "false";
                                        }


                                        lock(Online_Speech_Recogniser_Listening)
                                        {

                                            switch (Online_Speech_Recogniser_Listening)
                                            {
                                                case "true":


                                                    lock(Online_Speech_Recogniser_Disabled)
                                                    {
                                                        if (Window_Minimised == "true" || Online_Speech_Recogniser_Disabled == "true")
                                                        {
                                                            Online_Speech_Recogniser_Listening = "false";
                                                        }
                                                    }


                                                    lock (Online_Speech_Recogniser_State)
                                                    {
                                                        switch (Online_Speech_Recogniser_State == "Idle" || Online_Speech_Recogniser_State == "Paused")
                                                        {
                                                            case true:
                                                                
                                                                if(online_speech_recogniser_lock_state_time_elapsed_is_enabled == false)
                                                                {
                                                                    online_speech_recogniser_lock_state_time_elapsed.Start();
                                                                    online_speech_recogniser_lock_state_time_elapsed_is_enabled = true;
                                                                }

                                                                if (online_speech_recogniser_lock_state_time_elapsed_is_enabled == true)
                                                                {
                                                                    if(online_speech_recogniser_lock_state_time_elapsed.ElapsedMilliseconds > 4000)
                                                                    {
                                                                        Task.Run(async() =>
                                                                        {
                                                                            await OS_Online_Speech_Recognition_Interface_Shutdown_Or_Refresh(Online_Speech_Recognition_Interface_Operation.Online_Speech_Recognition_Interface_Shutdown);
                                                                            Online_Speech_Recogniser_Listening = "false";
                                                                        });
                                                                    }
                                                                }
                                                                break;

                                                            case false:
                                                                online_speech_recogniser_lock_state_time_elapsed.Stop();
                                                                online_speech_recogniser_lock_state_time_elapsed.Reset();
                                                                online_speech_recogniser_lock_state_time_elapsed_is_enabled = false;
                                                                break;
                                                        }
                                                    }


                                                    if(online_speech_recognition_timeout != null)
                                                    {
                                                        switch (((TimeSpan)(DateTime.Now - online_speech_recognition_timeout)).TotalMilliseconds >= 9000)
                                                        {
                                                            case true:
                                                                Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index = 0;
                                                                Online_Speech_Recognition_Timer_Display.Text = String.Empty;
                                                                Online_Speech_Recogniser_Listening = "false";
                                                                break;



                                                            case false:
                                                                lock (Speech_Detected)
                                                                {
                                                                    if (Speech_Detected == "true")
                                                                    {
                                                                        Speech_Detected = "false";
                                                                        target_value = 1000;
                                                                        Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index = 0;
                                                                        Online_Speech_Recognition_Timer_Display.Text = String.Empty;
                                                                    }
                                                                }

                                                                if (((TimeSpan)(DateTime.Now - online_speech_recognition_timeout)).TotalMilliseconds >= target_value - 300)
                                                                {
                                                                    if (target_value <= 10000)
                                                                    {
                                                                        target_value += 1000;
                                                                        Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index++;
                                                                    }
                                                                }

                                                                Online_Speech_Recognition_Timer_Display.Text = Online_Speech_Recognition_Timeout_Timer_UI_Intervals[Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index];


                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF91E1FF");
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF3099FF");
                                                                break;
                                                        }
                                                    }
                                                    break;





                                                case "false":
                                                    online_speech_recognition_timeout = null;

                                                    target_value = 1000;
                                                    Online_Speech_Recognition_Timeout_Timer_UI_Intervals_Current_Index = 0;
                                                    Online_Speech_Recognition_Timer_Display.Text = String.Empty;
                                                    OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FFACC6D6");
                                                    OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");

                                                    online_speech_recogniser_lock_state_time_elapsed.Stop();
                                                    online_speech_recogniser_lock_state_time_elapsed.Reset();
                                                    online_speech_recogniser_lock_state_time_elapsed_is_enabled = false;
                                                    break;
                                            }
                                        }



                                        switch (Timer_Interval._isTimer)
                                        {
                                            case true:
                                                OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FF11497F");

                                                bool Time_Interval_Elapsed = await Timer_Interval.Calculate_Time_Interval_Left();

                                                if (Time_Interval_Elapsed == true)
                                                {
                                                    switch (App.TimerWindowOpen)
                                                    {
                                                        case true:
                                                            Timer_Window.Ring_Timer = true;
                                                            break;



                                                        case false:
                                                            Timer_Window.Ring_Timer = true;
                                                            Timer_Window timer = new Timer_Window();
                                                            timer.ShowDialog();
                                                            break;
                                                    }
                                                }
                                                break;

                                            case false:
                                                OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FFD67A71");
                                                OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7F1111");
                                                break;
                                        }




                                       
                                      
                                        switch (RotationValue == 360)
                                        {
                                            case true:
                                                RotationValue = 0;
                                                break;

                                            case false:
                                                RotationValue += 7.5;
                                                break;
                                        }


                                        lock(BeginExecutionAnimation)
                                        {
                                            switch (BeginExecutionAnimation == "true")
                                            {

                                                case true:
                                                    Rotator.Width = 0;

                                                    switch (ExecutionAnimationArithmetic == 40)
                                                    {
                                                        case true:
                                                            OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                            OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                            ExecutionAnimationArithmetic = 0;
                                                            BeginExecutionAnimation = "false";
                                                            break;

                                                        case false:
                                                            ExecutionAnimationArithmetic += 4;

                                                            if (ExecutionAnimationArithmetic >= 4 && ExecutionAnimationArithmetic % 4 == 0)
                                                            {
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

                                                case false:
                                                    Rotator.Width = InitialRotatorWidth;
                                                    break;

                                            }
                                        }


                                        Rotate.Angle = RotationValue;
                                        Rotator.RenderTransform = Rotate;



                                        switch (SwitchMinimiseTheWindowOffset)
                                        {
                                            case true:

                                                switch (MinimiseTheWindowOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        MinimiseTheWindowOffsetArithmetic--;
                                                        MinimiseTheWindowOffset.Offset += 0.04;
                                                        break;

                                                    case false:
                                                        SwitchMinimiseTheWindowOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (MinimiseTheWindowOffsetArithmetic < 22)
                                                {
                                                    case true:
                                                        MinimiseTheWindowOffsetArithmetic++;
                                                        MinimiseTheWindowOffset.Offset -= 0.04;
                                                        break;

                                                    case false:
                                                        SwitchMinimiseTheWindowOffset = true;
                                                        break;
                                                }

                                                break;
                                        }



                                        switch (SwitchCloseTheWindowButtonOffset)
                                        {
                                            case true:

                                                switch (CloseTheWindowButtonOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        CloseTheWindowButtonOffsetArithmetic--;
                                                        CloseTheWindowButtonOffset.Offset += 0.04;
                                                        break;

                                                    case false:
                                                        SwitchCloseTheWindowButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (CloseTheWindowButtonOffsetArithmetic < 22)
                                                {
                                                    case true:
                                                        CloseTheWindowButtonOffsetArithmetic++;
                                                        CloseTheWindowButtonOffset.Offset -= 0.04;
                                                        break;

                                                    case false:
                                                        SwitchCloseTheWindowButtonOffset = true;
                                                        break;
                                                }

                                                break;
                                        }



                                        switch (SwitchOpenSettingsMenuButtonOffset)
                                        {
                                            case true:

                                                switch (OpenSettingsMenuButtonOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        OpenSettingsMenuButtonOffsetArithmetic--;
                                                        OpenSettingsMenuButtonOffset.Offset += 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOpenSettingsMenuButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (OpenSettingsMenuButtonOffsetArithmetic < 22)
                                                {
                                                    case true:
                                                        OpenSettingsMenuButtonOffsetArithmetic++;
                                                        OpenSettingsMenuButtonOffset.Offset -= 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOpenSettingsMenuButtonOffset = true;
                                                        break;
                                                }

                                                break;
                                        }



                                        switch (SwitchOpenTimerMenuButtonOffset)
                                        {
                                            case true:

                                                switch (OpenTimerMenuButtonOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        OpenTimerMenuButtonOffsetArithmetic--;
                                                        OpenTimerMenuButtonOffset.Offset += 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOpenTimerMenuButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (OpenTimerMenuButtonOffsetArithmetic < 22)
                                                {
                                                    case true:
                                                        OpenTimerMenuButtonOffsetArithmetic++;
                                                        OpenTimerMenuButtonOffset.Offset -= 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOpenTimerMenuButtonOffset = true;
                                                        break;
                                                }

                                                break;
                                        }




                                        switch (SwitchSpeechRecognitionButtonOffset)
                                        {
                                            case true:

                                                switch (SpeechRecognitionButtonOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        SpeechRecognitionButtonOffsetArithmetic--;
                                                        SpeechRecognitionButtonOffset.Offset += 0.04;
                                                        break;

                                                    case false:
                                                        SwitchSpeechRecognitionButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (SpeechRecognitionButtonOffsetArithmetic < 22)
                                                {
                                                    case true:
                                                        SpeechRecognitionButtonOffsetArithmetic++;
                                                        SpeechRecognitionButtonOffset.Offset -= 0.04;
                                                        break;

                                                    case false:
                                                        SwitchSpeechRecognitionButtonOffset = true;
                                                        break;
                                                }

                                                break;
                                        }



                                        switch (SwitchOuterElipseOffset)
                                        {
                                            case true:

                                                switch (OuterElipseOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        OuterElipseOffsetArithmetic--;
                                                        OuterElipseOffset.Offset += 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOuterElipseOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (OuterElipseOffsetArithmetic < 22)
                                                {
                                                    case true:
                                                        OuterElipseOffsetArithmetic++;
                                                        OuterElipseOffset.Offset -= 0.04;
                                                        break;

                                                    case false:
                                                        SwitchOuterElipseOffset = true;
                                                        break;
                                                }

                                                break;
                                        }


                                        break;
                                }
                            });

                            break;
                    }

                    break;
            }
        }

        private async void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowIsClosing = true;

            try
            {
                if (AnimationAndFunctionalityTimer != null)
                {
                    try
                    {
                        AnimationAndFunctionalityTimer?.Stop();
                        AnimationAndFunctionalityTimer?.Dispose();
                    }
                    catch { }
                }

                BeginExecutionAnimation = null;

                ParallelProcessing = null;


                await Wake_Word_Engine_Mitigator.Wake_Word_Engine_Stop();
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

        private void MinimiseTheMainWindow(object sender, RoutedEventArgs e)
        {
            if (MainWindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        Application.Current.MainWindow.WindowState = WindowState.Minimized;

                        lock(Window_Minimised)
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
                        await Wake_Word_Engine_Mitigator.Wake_Word_Engine_Stop();
                        this.Close();
                    }
                }
            }
        }

        private async void StartOrStopSpeechRecognition(object sender, RoutedEventArgs e)
        {

            if (Button_Timeout == 0)
            {
                Button_Timeout = 400;

                if (MainWindowIsClosing == false)
                {

                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {

                        if (Application.Current.MainWindow != null)
                        {
                            bool Microphone_Available = await Microphone_Permissions_Mitigator.Check_Microphone_Availability();

                            switch (Microphone_Available)
                            {
                                case true:
                                    OnOff++;

                                    ParallelProcessing = new System.Threading.Thread(async () =>
                                    {
                                        switch (OnOff)
                                        {
                                            case 1:
                                                Application.Current.Dispatcher.Invoke(() =>
                                                {
                                                    SpeechRecognitionButton.Content = "\xE1D6";
                                                });

                                                await Wake_Word_Engine_Mitigator.Wake_Word_Engine_Start();
                                                break;



                                            case 2:
                                                Application.Current.Dispatcher.Invoke(() =>
                                                {
                                                    SpeechRecognitionButton.Content = "\xF781";
                                                });

                                                OnOff = 0;
                                                await Wake_Word_Engine_Mitigator.Wake_Word_Engine_Stop();
                                                break;
                                        }
                                    });
                                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                                    ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                    ParallelProcessing.IsBackground = false;
                                    ParallelProcessing.Start();
                                    break;

                                case false:
                                    if(App.PermisissionWindowOpen == false)
                                    {
                                        ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Mircrophone Access Denied");
                                        OpenPermissionDeclinedWindow.Show();
                                    }
                                    break;
                            }

                        }

                    }

                }

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
                            SettingsWindow SettingWindowObject = new SettingsWindow();
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
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;

            Task.Run(async() =>
            {
                await Wake_Word_Engine_Mitigator.Wake_Word_Engine_Stop();
            });

            GC.Collect(2, GCCollectionMode.Forced);
        }



    }
}
