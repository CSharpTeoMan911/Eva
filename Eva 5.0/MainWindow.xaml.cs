using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private System.Threading.Thread ParallelProcessing;

        private System.Timers.Timer AnimationAndFunctionalityTimer;


        private static System.Speech.Recognition.SpeechRecognitionEngine MainSpeechRecogniser;

        public static double Speech_Recognition_Accuracy = 0.80;

        private int Online_Speech_Recogniser_Listening_TimeOut;


        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>
        /// 


        // [ BEGIN ] STATIC OBJECTS OBJECTS THAT ARE ACCESSED IN A THREAD SAFE MANNER

        public static string Online_Speech_Recogniser_Listening = "false";

        public static string FunctionInitiated = "false";

        // [ END ] STATIC OBJECTS OBJECTS THAT ARE ACCESSED IN A THREAD SAFE MANNER





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


        private int Button_Timeout;

        private bool MainWindowIsClosing;

        public static dynamic BeginExecutionAnimation = false;

        private static byte ExecutionAnimationArithmetic;

        private double InitialRotatorWidth;

        int RotationValue;

        private byte OnOff;



        public MainWindow()
        {
            InitializeComponent();
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
            AnimationAndFunctionalityTimer.Interval = 10;
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

                                        lock(Online_Speech_Recogniser_Listening)
                                        {

                                            switch (Online_Speech_Recogniser_Listening)
                                            {


                                                case "true":


                                                    Online_Speech_Recogniser_Listening_TimeOut++;



                                                    switch (Online_Speech_Recogniser_Listening_TimeOut == 6000)
                                                    {
                                                        case true:
                                                            Online_Speech_Recogniser_Listening_TimeOut = 0;
                                                            break;



                                                        case false:
                                                            OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF91E1FF");
                                                            OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF3099FF");
                                                            break;
                                                    }

                                                    break;





                                                case "false":
                                                    Online_Speech_Recogniser_Listening_TimeOut = 0;
                                                    OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FFACC6D6");
                                                    OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
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
                                                RotationValue++;
                                                RotationValue++;
                                                RotationValue++;
                                                RotationValue++;
                                                break;
                                        }



                                        switch (BeginExecutionAnimation == true)
                                        {

                                            case true:
                                                Rotator.Width = 0;

                                                switch (ExecutionAnimationArithmetic == 40)
                                                {
                                                    case true:
                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                        ExecutionAnimationArithmetic = 0;
                                                        BeginExecutionAnimation = false;
                                                        break;

                                                    case false:
                                                        ExecutionAnimationArithmetic++;

                                                        switch (ExecutionAnimationArithmetic)
                                                        {
                                                            case 4:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                break;

                                                            case 8:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                break;

                                                            case 12:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                break;

                                                            case 16:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                break;

                                                            case 20:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                break;

                                                            case 24:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                break;

                                                            case 28:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                break;

                                                            case 32:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                break;

                                                            case 36:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                                                                break;

                                                            case 40:
                                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                                break;
                                                        }
                                                        break;
                                                }
                                                break;

                                            case false:
                                                Rotator.Width = InitialRotatorWidth;
                                                break;

                                        }
                                        



                                        RotateTransform Rotate = new RotateTransform()
                                        {

                                            Angle = RotationValue
                                        };

                                        Rotator.RenderTransform = Rotate;



                                        switch (SwitchMinimiseTheWindowOffset)
                                        {
                                            case true:

                                                switch (MinimiseTheWindowOffsetArithmetic > 0)
                                                {
                                                    case true:
                                                        MinimiseTheWindowOffsetArithmetic--;
                                                        MinimiseTheWindowOffset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        SwitchMinimiseTheWindowOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (MinimiseTheWindowOffsetArithmetic < 45)
                                                {
                                                    case true:
                                                        MinimiseTheWindowOffsetArithmetic++;
                                                        MinimiseTheWindowOffset.Offset -= 0.02;
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
                                                        CloseTheWindowButtonOffset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        SwitchCloseTheWindowButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (CloseTheWindowButtonOffsetArithmetic < 45)
                                                {
                                                    case true:
                                                        CloseTheWindowButtonOffsetArithmetic++;
                                                        CloseTheWindowButtonOffset.Offset -= 0.02;
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
                                                        OpenSettingsMenuButtonOffset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        SwitchOpenSettingsMenuButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (OpenSettingsMenuButtonOffsetArithmetic < 45)
                                                {
                                                    case true:
                                                        OpenSettingsMenuButtonOffsetArithmetic++;
                                                        OpenSettingsMenuButtonOffset.Offset -= 0.02;
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
                                                        OpenTimerMenuButtonOffset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        SwitchOpenTimerMenuButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (OpenTimerMenuButtonOffsetArithmetic < 45)
                                                {
                                                    case true:
                                                        OpenTimerMenuButtonOffsetArithmetic++;
                                                        OpenTimerMenuButtonOffset.Offset -= 0.02;
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
                                                        SpeechRecognitionButtonOffset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        SwitchSpeechRecognitionButtonOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (SpeechRecognitionButtonOffsetArithmetic < 45)
                                                {
                                                    case true:
                                                        SpeechRecognitionButtonOffsetArithmetic++;
                                                        SpeechRecognitionButtonOffset.Offset -= 0.02;
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
                                                        OuterElipseOffset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        SwitchOuterElipseOffset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (OuterElipseOffsetArithmetic < 45)
                                                {
                                                    case true:
                                                        OuterElipseOffsetArithmetic++;
                                                        OuterElipseOffset.Offset -= 0.02;
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

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowIsClosing = true;
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

                        lock(FunctionInitiated)
                        {
                            FunctionInitiated = "false";
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

                        this.Close();
                        Application.Current.Shutdown();
                        Environment.Exit(0);

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

                            OnOff++;

                            switch (OnOff)
                            {
                                case 1:

                                    bool Wake_Word_Engine_Initiation_Successful = await Initiate_The_Wake_Word_Engine();

                                    switch (Wake_Word_Engine_Initiation_Successful)
                                    {

                                        case true:

                                            SpeechRecognitionButton.Content = "\xE1D6";

                                            App.StopRecognitionSession = false;

                                            break;




                                        case false:

                                            OnOff = 0;

                                            App.StopRecognitionSession = true;

                                            SpeechRecognitionButton.Content = "\xF781";

                                            break;

                                    }

                                    break;

                                case 2:

                                    bool Wake_Word_Engine_Shutdown_Successful = await Close_The_Wake_Word_Engine();


                                    switch (Wake_Word_Engine_Shutdown_Successful)
                                    {

                                        case true:

                                            SpeechRecognitionButton.Content = "\xF781";

                                            App.StopRecognitionSession = true;

                                            OnOff = 0;

                                            break;




                                        case false:

                                            SpeechRecognitionButton.Content = "\xE1D6";

                                            App.StopRecognitionSession = false;

                                            break;

                                    }

                                    break;
                            }

                        }

                    }

                }
            }
        }

        private void MainSpeechRecogniser_SpeechRecognized(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {

            switch (MainWindowIsClosing)
            {
                case false:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {

                        case false:

                            Application.Current.Dispatcher.Invoke(async () =>
                            {

                                switch (Application.Current.MainWindow == null)
                                {

                                    case false:
                                        
                                        switch (e.Result.Confidence >= Speech_Recognition_Accuracy)
                                        {
                                            case true:

                                                switch (e.Result.Text)
                                                {
                                                    case "Eva":

                                                    

                                                    Wake_Word_Engine_Shutdown:

                                                        int Wake_Word_Engine_Shutdown_Error_Counter = 0;

                                                        bool Wake_Word_Engine_Shutdown_Successful = await Close_The_Wake_Word_Engine();



                                                        if (Wake_Word_Engine_Shutdown_Successful == false)
                                                        {
                                                            if(OnOff != 0)
                                                            {
                                                                switch (Wake_Word_Engine_Shutdown_Error_Counter < 5)
                                                                {

                                                                    case true:

                                                                        Wake_Word_Engine_Shutdown_Error_Counter++;

                                                                        goto Wake_Word_Engine_Shutdown;




                                                                    case false:

                                                                        SpeechRecognitionButton.Content = "\xE1D6";

                                                                        App.StopRecognitionSession = false;

                                                                        break;

                                                                }
                                                            }
                                                            
                                                        }





                                                        if (Timer_Window.Ring_Timer == false)
                                                        {

                                                            lock(FunctionInitiated)
                                                            {

                                                                if (FunctionInitiated == "false")
                                                                {

                                                                    if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                                                                    {
                                                                        FunctionInitiated = "true";

                                                                        Task.Run(async () =>
                                                                        {
                                                                            await Online_Speech_Recognition.Online_Speech_Recognition_Session_Creation_And_Initiation();
                                                                        });

                                                                    }

                                                                }

                                                            }

                                                        }



                                                        if (OnOff != 0)
                                                        {
                                                            if(Wake_Word_Engine_Shutdown_Successful == true)
                                                            {
                                                                await Initiate_The_Wake_Word_Engine();
                                                            }
                                                        }



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

        private async void MainSpeechRecogniser_RecognizeCompleted(object sender, System.Speech.Recognition.RecognizeCompletedEventArgs e)
        {

            try
            {
                
                if(e.Error != null)
                {
                    if(e.Error.HResult == -2147024891)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            App.ErrorAppShutdown = true;
                            Application.Current.MainWindow.Visibility = Visibility.Hidden;

                            switch (App.PermisissionWindowOpen)
                            {
                                case false:

                                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                                    GC.Collect(0, GCCollectionMode.Forced, true, true);

                                    ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Mircrophone Access Denied");
                                    OpenPermissionDeclinedWindow.Show();

                                    break;

                                case true:

                                    App.InitiateErrorFunction = true;
                                    App.ErrorFunction = "Mircrophone Access Denied";

                                    break;
                            }

                        });
                    }
                }
                else
                {
                    await Application.Current.Dispatcher.Invoke(async () =>
                    {
                   
                        bool Wake_Word_Engine_Shutdown_Successful = await Close_The_Wake_Word_Engine();

                        switch (Wake_Word_Engine_Shutdown_Successful)
                        {

                            case true:

                                SpeechRecognitionButton.Content = "\xF781";

                                App.StopRecognitionSession = true;

                                OnOff = 0;


                                bool Wake_Word_Engine_Initiation_Successful = await Initiate_The_Wake_Word_Engine();

                                if (Wake_Word_Engine_Initiation_Successful == true)
                                {
                                    SpeechRecognitionButton.Content = "\xE1D6";

                                    App.StopRecognitionSession = false;
                                }

                                break;




                            case false:

                                SpeechRecognitionButton.Content = "\xE1D6";

                                App.StopRecognitionSession = false;

                                break;

                        }

                    });
                }
               
            }
            catch { }


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



        public Task<bool> Initiate_The_Wake_Word_Engine()
        {
            bool Wake_Word_Engine_Initiation_Successful = true;

            try
            {
                if (OnOff == 1)
                {
                    ParallelProcessing = new System.Threading.Thread(() =>
                    {
                        try
                        {
                            MainSpeechRecogniser = new System.Speech.Recognition.SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-GB"));

                            lock (MainSpeechRecogniser)
                            {
                                if (MainSpeechRecogniser != null)
                                {
                                    MainSpeechRecogniser.BabbleTimeout = TimeSpan.FromSeconds(0);
                                    MainSpeechRecogniser.EndSilenceTimeout = TimeSpan.FromSeconds(0);
                                    MainSpeechRecogniser.InitialSilenceTimeout = TimeSpan.FromSeconds(0);
                                    MainSpeechRecogniser.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(0);


                                    MainSpeechRecogniser.RequestRecognizerUpdate();
                                    System.Speech.Recognition.Choices Choices = new System.Speech.Recognition.Choices("Eva", "Ei Ea");
                                    System.Speech.Recognition.GrammarBuilder gb = new System.Speech.Recognition.GrammarBuilder();
                                    gb.Culture = new System.Globalization.CultureInfo("en-GB");
                                    gb.Append(Choices);
                                    System.Speech.Recognition.Grammar Grammar = new System.Speech.Recognition.Grammar(gb);
                                    MainSpeechRecogniser.RequestRecognizerUpdate();

                                    MainSpeechRecogniser?.LoadGrammarAsync(Grammar);
                                    MainSpeechRecogniser?.SetInputToDefaultAudioDevice();
                                    MainSpeechRecogniser?.RequestRecognizerUpdate();


                                    MainSpeechRecogniser?.RecognizeAsync(System.Speech.Recognition.RecognizeMode.Multiple);
                                    MainSpeechRecogniser.SpeechRecognized += MainSpeechRecogniser_SpeechRecognized;
                                    MainSpeechRecogniser.RecognizeCompleted += MainSpeechRecogniser_RecognizeCompleted;
                                }
                            }
                        }
                        catch { }

                    });

                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    
                }
            }
            catch
            {
                Wake_Word_Engine_Initiation_Successful = false;
            }

            return Task.FromResult(Wake_Word_Engine_Initiation_Successful);
        }





        public Task<bool> Close_The_Wake_Word_Engine()
        {
            bool Wake_Word_Engine_Shutdown_Successful = true;


            try
            {
                ParallelProcessing = new System.Threading.Thread(() =>
                {
                    try
                    {
                        lock (MainSpeechRecogniser)
                        {
                            if (MainSpeechRecogniser != null)
                            {
                                MainSpeechRecogniser?.RecognizeAsyncCancel();
                                MainSpeechRecogniser?.Dispose();
                            }
                        }
                    }
                    catch { }

                });

                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                ParallelProcessing.IsBackground = true;
                ParallelProcessing.Start();
            }
            catch
            {
                Wake_Word_Engine_Shutdown_Successful = false;
            }

            return Task.FromResult(Wake_Word_Engine_Shutdown_Successful);
        }


        


        ~MainWindow()
        {
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

                if (MainSpeechRecogniser != null)
                {
                    try
                    {
                        lock (MainSpeechRecogniser)
                        {
                            if (MainSpeechRecogniser != null)
                            {
                                MainSpeechRecogniser?.RecognizeAsyncStop();
                                MainSpeechRecogniser?.RecognizeAsyncCancel();
                                MainSpeechRecogniser?.Dispose();
                            }
                        }
                    }
                    catch { }
                }

                BeginExecutionAnimation = null;

                ParallelProcessing = null;


                System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                GC.Collect(2, GCCollectionMode.Forced);
            }
            catch { }

        }



    }
}
