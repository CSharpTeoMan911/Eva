using Eva_5._0.Classes;
using Eva_5._0.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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


        // COLORS FOR THE CIRCULAR INDICATOR FOR EACH OPERATIONAL MODE (CHATBOT MODE AND NORMAL MODE)
        //
        // BEGIN


        private string normal_mode_activated_outer_elipse_offset_color = "#FF91E1FF";
        private string normal_mode_activated_outer_elipse_gradient_color = "#FF3099FF";


        private string chatbot_mode_activated_outer_elipse_offset_color = "#FF00FF1B";
        private string chatbot_mode_activated_outer_elipse_gradient_color = "#FF34F19F";



        private System.Timers.Timer AnimationAndFunctionalityTimer;


        private bool SwitchOffset;

        private double WindowOffsetArithmetic;


        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>

        private bool Colour_Switch;

        private int Button_Timeout;

        private static byte ExecutionAnimationArithmetic;

        private double InitialRotatorWidth;

        private double RotationValue;

        private Wake_Word_Engine.Wake_Word_Engine_Event_Handler wake_Word_Engine_Event_Handler;

        public MainWindow()
        {
            wake_Word_Engine_Event_Handler = new Wake_Word_Engine.Wake_Word_Engine_Event_Handler(SpeechOnCallback);

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            InitializeComponent();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume) this.Activate();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;

            // Check the administartive rights with which the application session is running. If the application rights are the ones of administrator, the application will close.
            // This is done to prevent any security problems due to the fact that the application is operating at a low level within the operating system.

            // [ BEGIN ]

            Check_Role.Check_User_Role();

            // [ END ]

            A_p_l____And____P_r_o_c.sound_player.PlayBackgroundNoise();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            InitialRotatorWidth = Rotator.ActualWidth;

            AnimationAndFunctionalityTimer = new System.Timers.Timer();
            AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
            AnimationAndFunctionalityTimer.Interval = 45;
            AnimationAndFunctionalityTimer.Start();
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e) => App.stateMachine.wakeWordEngine.Stop_The_Wake_Word_Engine();

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => App.stateMachine.wakeWordEngine.Stop_The_Wake_Word_Engine();

        private async void AnimationAndFunctionalityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // VERIFY IF MAIN WINDOW IS CLOSING
                if (App.stateMachine.MainWindowIsClosing == true)
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

                        Interlocked.MemoryBarrier();
                        Interlocked.SpeculationBarrier();

                        // METHODS AND PARAMETERS THAT MUST BE EXECUTED AND/OR MANIPULATED ON THE UI THREAD,
                        // ARE MOVED ON THE UI THREAD VIA THE "Application.Current.Dispatcher.Invoke()" METHOD
                        await Application.Current.Dispatcher.InvokeAsync(async () =>
                        {

                            if (Application.Current.MainWindow == null)
                            {
                                AnimationAndFunctionalityTimer?.Stop();
                            }
                            else
                            {

                                switch (App.stateMachine.invisibility_mode)
                                {
                                    case true:
                                        this.Height = 0;
                                        this.Width = 0;
                                        break;
                                    case false:
                                        Crop_Or_UnCrop();
                                        break;
                                }

                                if (App.stateMachine.bring_to_top)
                                {
                                    this.Activate();
                                    App.stateMachine.bring_to_top = false;
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

                                    if (this.ShowInTaskbar)
                                        this.ShowInTaskbar = false;

                                    Interlocked.Exchange(ref App.stateMachine.Window_Minimised, 0);
                                }


                                // IF THE WAKE WORD ENGINE DETECTED A KEYWORD
                                if (Interlocked.Read(ref App.stateMachine.Wake_Word_Detected) == 1)
                                {
                                    // AFTER THE WAKE WORD DETECTION PROCEDURE IS FINISHED, RESET THE INICATOR TO ITS DEFAULT VALUE
                                    Interlocked.Exchange(ref App.stateMachine.Wake_Word_Detected, 0);

                                    // IF THE ONLINE SPEECH RECOGNITION ENGINE IS NOT DISABLED
                                    if (Interlocked.Read(ref App.stateMachine.Online_Speech_Recogniser_Disabled) == 0)
                                    {
                                        async void Start()
                                        {
                                            // CALCULATE THE ACTIVATION DELAY TO BE SET FOR THE ONLINE SPEECH RECOGNITION ENGINE
                                            // AND INITIATE THE ONLINE SPEECH RECOGNITION ENGINE.
                                            if (Speech_Recogniser_Delay_Calculator() == true)
                                            {
                                                App.stateMachine.moonshineASR.StartEngine();
                                            }
                                        };
                                        Start();
                                    }
                                }



                                if (App.stateMachine.Speech_Recogniser_Listening == 1)
                                {
                                    // IF THE ONLINE SPEECH RECOGNITION ENGINE IS DISABLED OR THE WINDOW IS MINIMISED,
                                    // WHILE THE ONLINE SPEECH RECOGNITION ENGINE IS OPERATING SET THE CIRCULAR STATUS INDICATOR
                                    // COLOR AS BRIGHT BLUE
                                    if (App.stateMachine.chatgpt_mode_enabled == true)
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
                                else
                                {
                                    OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                    OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
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



                                if (Interlocked.Read(ref App.stateMachine.BeginExecutionAnimation) == 1)
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
                                        Interlocked.Exchange(ref App.stateMachine.BeginExecutionAnimation, 0);
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
                        }, System.Windows.Threading.DispatcherPriority.Render);
                    }
                }
            }
            catch { }
        }

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.stateMachine.MainWindowIsClosing = true;

            try
            {
                AnimationAndFunctionalityTimer?.Dispose();

                App.stateMachine.wakeWordEngine.Stop_The_Wake_Word_Engine();
                Environment.Exit(0);
            }
            catch { }
        }


        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (App.stateMachine.MainWindowIsClosing == false)
                {
                    if (Application.Current != null)
                    {
                        if (Application.Current.Dispatcher != null)
                        {
                            if (Application.Current.MainWindow != null)
                            {
                                this.DragMove();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void ContractOrExpandTheMainWindow(object sender, RoutedEventArgs e)
        {
            App.stateMachine.Cropped = !App.stateMachine.Cropped;
            Crop_Or_UnCrop();
        }

        private void Crop_Or_UnCrop()
        {
            if (App.stateMachine.Cropped == true)
            {
                Window_Geometry.Rect = new Rect(0, 0, 260, 159);
                ContractOrExpandTheWindowButton.Content = "\xE73F";
                this.Height = 159;
                this.Width = 260;
                Extra_Functionalities.Width = double.NaN;
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
            if (App.stateMachine.MainWindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {
                        this.ShowInTaskbar = true;
                        Application.Current.MainWindow.WindowState = WindowState.Minimized;

                        Interlocked.Exchange(ref App.stateMachine.Window_Minimised, 1);
                    }

                }

            }
        }

        private void CloseTheApplication(object sender, RoutedEventArgs e)
        {
            if (App.stateMachine.MainWindowIsClosing == false)
            {
                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {
                    if (Application.Current.MainWindow != null)
                    {
                        App.stateMachine.wakeWordEngine.Stop_The_Wake_Word_Engine();
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
                if (App.stateMachine.MainWindowIsClosing == false)
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
                                Interlocked.MemoryBarrier();
                                Interlocked.SpeculationBarrier();

                                if (Interlocked.Read(ref App.stateMachine.OnOff) == 0)
                                {
                                    SpeechOn();
                                }
                                else if (Interlocked.Read(ref App.stateMachine.OnOff) == 1)
                                {
                                    SpeechOff();
                                }
                            }
                            else
                            {
                                if (App.PermisissionWindowOpen == false)
                                {
                                    ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Mircrophone Access Denied");
                                    OpenPermissionDeclinedWindow.Show();
                                }
                            }
                        }

                    }

                }

            }

        }


        private async void SpeechOn()
        {
            Interlocked.MemoryBarrier();
            Interlocked.SpeculationBarrier();

            if (Interlocked.Read(ref App.stateMachine.OnOff) == 0)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    this.SpeechRecognitionButton.IsEnabled = false;
                }, System.Windows.Threading.DispatcherPriority.Render);

                SpeechSynthesis.StartSynthesiser();
                // START THE WAKE WORD ENGINE PROCESS
                App.stateMachine.wakeWordEngine.Start_The_Wake_Word_Engine(wake_Word_Engine_Event_Handler);
            }
        }

        public async void SpeechOnCallback()
        {
            Interlocked.MemoryBarrier();
            Interlocked.SpeculationBarrier();

            if (Interlocked.Read(ref App.stateMachine.OnOff) == 0)
            {
                // LOCK THE VARIABLE ON THE STACK TO BE ACCESSED ONLY BY THE CURRENT THREAD
                Interlocked.Exchange(ref App.stateMachine.OnOff, 1);

                // LOCK THE VARIABLE ON THE STACK TO BE ACCESSED ONLY BY THE CURRENT THREAD
                Interlocked.Exchange(ref App.stateMachine.Online_Speech_Recogniser_Disabled, 0);

                // CHANGE THE BUTTON CONTENT BY INVOKING THE OPERATION ON THE UI THREAD
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    this.SpeechRecognitionButton.IsEnabled = true;
                    SpeechRecognitionButton.Content = "\xE1D6";
                }, System.Windows.Threading.DispatcherPriority.Render);
            }
        }


        private async void SpeechOff()
        {
            Interlocked.MemoryBarrier();
            Interlocked.SpeculationBarrier();

            if (Interlocked.Read(ref App.stateMachine.OnOff) == 1)
            {
                SpeechSynthesis.StopSynthesiser();

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    this.SpeechRecognitionButton.IsEnabled = false;
                }, System.Windows.Threading.DispatcherPriority.Render);

                // LOCK THE VARIABLE ON THE STACK TO BE ACCESSED ONLY BY THE CURRENT THREAD
                Interlocked.Exchange(ref App.stateMachine.Online_Speech_Recogniser_Disabled, 1);

                // TEMINATE THE WAKE WORD ENGINE PROCESS
                App.stateMachine.wakeWordEngine.Stop_The_Wake_Word_Engine();
                Interlocked.Exchange(ref App.stateMachine.OnOff, 0);

                // CHANGE THE BUTTON CONTENT BY INVOKING THE OPERATION ON THE UI THREAD
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    this.SpeechRecognitionButton.IsEnabled = true;
                    SpeechRecognitionButton.Content = "\xF781";
                }, System.Windows.Threading.DispatcherPriority.Render);
            }
        }


        private void OpenSettingsWindow(object sender, RoutedEventArgs e)
        {
            if (App.stateMachine.MainWindowIsClosing == false)
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
            if (App.stateMachine.MainWindowIsClosing == false)
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


        protected static bool Speech_Recogniser_Delay_Calculator()
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


            if (App.stateMachine.Speech_Recogniser_Activation_Delay_Detector == null)
            {
                return true;
            }
            else if (((TimeSpan)(DateTime.UtcNow - App.stateMachine.Speech_Recogniser_Activation_Delay_Detector)).TotalSeconds > App.stateMachine.Speech_Recogniser_Activation_Delay)
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
    }
}
