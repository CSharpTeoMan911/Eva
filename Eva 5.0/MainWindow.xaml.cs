using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class MainWindow : Window
    {
        private System.Threading.Thread ParallelProcessing;

        private System.Timers.Timer AnimationAndFunctionalityTimer;



        private System.Speech.Recognition.SpeechRecognitionEngine MainSpeechRecogniser;

        public static double Speech_Recognition_Sensitivity = 0.96;

        public static bool Online_Speech_Recogniser_Listening;

        private int Online_Speech_Recogniser_Listening_TimeOut;



        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>


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

        public static bool FunctionInitiated = false;

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

            new Check_Role();




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

                                        switch (Online_Speech_Recogniser_Listening)
                                        {
                                            case true:

                                                Online_Speech_Recogniser_Listening_TimeOut++;

                                                switch(Online_Speech_Recogniser_Listening_TimeOut == 700)
                                                {
                                                    case true:

                                                        Online_Speech_Recogniser_Listening_TimeOut = 0;
                                                        Online_Speech_Recogniser_Listening = false;
                                                        break;

                                                    case false:

                                                        OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FF91E1FF");
                                                        OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF3099FF");
                                                        break;
                                                }
                                                break;


                                            case false:

                                                OuterElipseOffset.Color = (Color)ColorConverter.ConvertFromString("#FFACC6D6");
                                                OuterElipseGradient.Color = (Color)ColorConverter.ConvertFromString("#FF052544");
                                                break;
                                        }



                                        if (Timer_Interval._isTimer == true)
                                        {

                                            OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");

                                            OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FF11497F");




                                            bool Time_Interval_Elapsed = await Timer_Interval.Calculate_Time_Interval_Left();

                                            if (Time_Interval_Elapsed == true)
                                            {
                                                switch(App.TimerWindowOpen)
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

                                        }
                                        else
                                        {

                                            OpenTimerMenuButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FFD67A71");

                                            OpenTimerMenuButtonNotOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7F1111");

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



                                        if (BeginExecutionAnimation == true)
                                        {


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

                                        }
                                        else
                                        {
                                            Rotator.Width = InitialRotatorWidth;
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
            switch (MainWindowIsClosing)
            {
                case false:
                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {
                                case false:
                                    this.DragMove();
                                    break;
                            }

                            break;
                    }
                    break;
            }
        }

        private void MinimiseTheMainWindow(object sender, RoutedEventArgs e)
        {
            switch (MainWindowIsClosing)
            {
                case false:
                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {
                                case false:
                                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                                    break;
                            }

                            break;
                    }
                    break;
            }
        }

        private void CloseTheApplication(object sender, RoutedEventArgs e)
        {
            switch (MainWindowIsClosing)
            {
                case false:
                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {
                                case false:
                                    this.Close();
                                    Application.Current.Shutdown();
                                    Environment.Exit(0);
                                    break;
                            }

                            break;
                    }
                    break;
            }
        }

        private void StartOrStopSpeechRecognition(object sender, RoutedEventArgs e)
        {

            if (Button_Timeout == 0)
            {
                Button_Timeout = 400;

                switch (MainWindowIsClosing)
                {
                    case false:
                        switch (Application.Current.Dispatcher.HasShutdownStarted)
                        {
                            case false:

                                switch (Application.Current.MainWindow == null)
                                {
                                    case false:

                                        OnOff++;

                                        switch (OnOff)
                                        {
                                            case 1:

                                                SpeechRecognitionButton.Content = "\xE1D6";

                                                App.StopRecognitionSession = false;

                                                try
                                                {

                                                    ParallelProcessing = new System.Threading.Thread(() =>
                                                    {


                                                        MainSpeechRecogniser = new System.Speech.Recognition.SpeechRecognitionEngine();

                                                        MainSpeechRecogniser.BabbleTimeout = TimeSpan.FromSeconds(0);
                                                        MainSpeechRecogniser.EndSilenceTimeout = TimeSpan.FromSeconds(0);
                                                        MainSpeechRecogniser.InitialSilenceTimeout = TimeSpan.FromSeconds(0);
                                                        MainSpeechRecogniser.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(0);

                                                        MainSpeechRecogniser.RequestRecognizerUpdate();
                                                        System.Speech.Recognition.Choices Choices = new System.Speech.Recognition.Choices("Eva");
                                                        System.Speech.Recognition.GrammarBuilder gb = new System.Speech.Recognition.GrammarBuilder();
                                                        gb.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                                                        gb.Append(Choices);
                                                        System.Speech.Recognition.Grammar Grammar = new System.Speech.Recognition.Grammar(gb);
                                                        MainSpeechRecogniser.RequestRecognizerUpdate();
                                                        MainSpeechRecogniser.LoadGrammarAsync(Grammar);
                                                        MainSpeechRecogniser.SetInputToDefaultAudioDevice();
                                                        MainSpeechRecogniser.RequestRecognizerUpdate();

                                                        MainSpeechRecogniser.RecognizeAsync(System.Speech.Recognition.RecognizeMode.Multiple);
                                                        MainSpeechRecogniser.SpeechRecognized += MainSpeechRecogniser_SpeechRecognized;
                                                        MainSpeechRecogniser.RecognizeCompleted += MainSpeechRecogniser_RecognizeCompleted;


                                                    });
                                                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                                                    ParallelProcessing.IsBackground = true;
                                                    ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                                    ParallelProcessing.Start();
                                                }
                                                catch { }

                                                break;

                                            case 2:

                                                SpeechRecognitionButton.Content = "\xF781";

                                                App.StopRecognitionSession = true;

                                                OnOff = 0;

                                                try
                                                {

                                                    ParallelProcessing = new System.Threading.Thread(() =>
                                                    {
                                                        MainSpeechRecogniser.RecognizeAsyncStop();
                                                        MainSpeechRecogniser.RecognizeAsyncCancel();
                                                        MainSpeechRecogniser.Dispose();
                                                    });
                                                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                                                    ParallelProcessing.IsBackground = true;
                                                    ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                                    ParallelProcessing.Start();
                                                }
                                                catch { }

                                                break;
                                        }

                                        break;
                                }

                                break;
                        }
                        break;
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

                            Application.Current.Dispatcher.Invoke(() =>
                            {

                                switch (Application.Current.MainWindow == null)
                                {

                                    case false:

                                        switch (e.Result.Confidence >= Speech_Recognition_Sensitivity)
                                        {
                                            case true:

                                                switch (e.Result.Text)
                                                {
                                                    case "Eva":

                                                        if(Timer_Window.Ring_Timer == false)
                                                        {
                                                            switch (FunctionInitiated)
                                                            {
                                                                case false:

                                                                    switch (Application.Current.MainWindow.WindowState == WindowState.Normal)
                                                                    {
                                                                        case true:

                                                                            FunctionInitiated = true;

                                                                           
                                                                            System.Threading.Thread Online_Speech_Recognition_Thread = new System.Threading.Thread(() =>
                                                                            {
                                                                                Online_Speech_Recognition.Recogniser_Thread_Creation_And_Initiation();
                                                                            });

                                                                            Online_Speech_Recognition_Thread.SetApartmentState(System.Threading.ApartmentState.STA);
                                                                            Online_Speech_Recognition_Thread.Priority = System.Threading.ThreadPriority.AboveNormal;
                                                                            Online_Speech_Recognition_Thread.IsBackground = true;
                                                                            Online_Speech_Recognition_Thread.Start();





                                                                            Application.Current.MainWindow.Topmost = false;

                                                                            break;
                                                                    }

                                                                    break;
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

        private void MainSpeechRecogniser_RecognizeCompleted(object sender, System.Speech.Recognition.RecognizeCompletedEventArgs e)
        {
          

            try
            {
                switch (e.Error != null)
                {
                    case true:

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            App.ErrorAppShutdown = true;
                            Application.Current.MainWindow.Visibility = Visibility.Hidden;

                            switch (App.PermisissionWindowOpen)
                            {
                                case false:

                                    MainSpeechRecogniser.RecognizeAsyncCancel();
                                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                                    GC.Collect(0, GCCollectionMode.Forced, true, true);
                                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                                    GC.Collect(0, GCCollectionMode.Forced, true, true);
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

                        break;
                }

            }
            catch { }
        }



        private void OpenSettingsWindow(object sender, RoutedEventArgs e)
        {
            switch (MainWindowIsClosing)
            {
                case false:
                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {
                                case false:
                                    switch (App.SettingsWindowOpen)
                                    {
                                        case false:
                                            App.SettingsWindowOpen = true;
                                            SettingsWindow SettingWindowObject = new SettingsWindow();
                                            SettingWindowObject.Show();
                                            break;
                                    }
                                    break;
                            }

                            break;
                    }
                    break;
            }
        }

        

        private void OpenTimerWindow(object sender, RoutedEventArgs e)
        {
            switch (MainWindowIsClosing)
            {
                case false:
                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {
                                case false:
                                    switch (App.TimerWindowOpen)
                                    {
                                        case false:
                                            App.TimerWindowOpen = true;
                                            Timer_Window TimerWindowObject = new Timer_Window();
                                            TimerWindowObject.Show();
                                            break;
                                    }
                                    break;
                            }

                            break;
                    }
                    break;
            }
        }



        ~MainWindow()
        {
            try
            {
                switch (AnimationAndFunctionalityTimer != null)
                {
                    case true:
                        try
                        {
                            AnimationAndFunctionalityTimer.Stop();
                            AnimationAndFunctionalityTimer.Dispose();
                        }
                        catch { }
                        break;
                }

                switch (MainSpeechRecogniser != null)
                {
                    case true:
                        try
                        {
                            MainSpeechRecogniser.RecognizeAsyncStop();
                            MainSpeechRecogniser.RecognizeAsyncCancel();
                            MainSpeechRecogniser.Dispose();
                        }
                        catch { }
                        break;
                }

                BeginExecutionAnimation = null;



                System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                GC.Collect(2, GCCollectionMode.Forced);
            }
            catch { }

        }



    }
}
