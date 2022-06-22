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

        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>

        private bool SwitchMinimiseTheWindowOffset;

        private double MinimiseTheWindowOffsetArithmetic;

        private bool SwitchCloseTheWindowButtonOffset;

        private double CloseTheWindowButtonOffsetArithmetic;

        private bool SwitchOpenSettingsMenuButtonOffset;

        private double OpenSettingsMenuButtonOffsetArithmetic;

        private bool SwitchSpeechRecognitionButtonOffset;

        private double SpeechRecognitionButtonOffsetArithmetic;

        private bool SwitchOuterElipseOffset;

        private double OuterElipseOffsetArithmetic;


        /// <summary>
        ///  Gradient Arithmetic For Neon Glow Chromatic Effect
        /// </summary>




        private bool MainWindowIsClosing;

        public static bool FunctionInitiated = false;

        private dynamic WindowMinimised = false;

        public static dynamic BeginExecutionAnimation = false;

        private static byte ExecutionAnimationArithmetic;

        private double InitialRotatorWindth;

        int RotationValue;

        private byte OnOff;



        public MainWindow()
        {
            InitializeComponent();
        }


        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("");
            this.Width = this.ActualWidth / 2.6;
            this.Height = this.ActualHeight / 2.6;

            this.ResizeMode = System.Windows.ResizeMode.NoResize;

            OuterElipse.Height = WidthReference.ActualWidth;
            OuterElipse.Width = WidthReference.ActualWidth;

            InnerElipse.Width = WidthReference.ActualWidth / 1.1;
            InnerElipse.Height = WidthReference.ActualWidth / 1.1;

            InitialRotatorWindth = WidthReference.ActualWidth / 4.5;
            Rotator.Width = WidthReference.ActualWidth / 4.5;


            SpeechRecognitionButton.FontSize = SpeechRecognitionButton.FontSize / 2;
            MinimiseTheWindowButton.FontSize = MinimiseTheWindowButton.FontSize / 2.6;
            CloseTheWindowButton.FontSize = CloseTheWindowButton.FontSize / 2.6;
            OpenSettingsMenuButton.FontSize = OpenSettingsMenuButton.FontSize / 2.6;

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

                            Application.Current.Dispatcher.Invoke(() =>
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

                                        switch (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                                        {
                                            case true:
                                                WindowMinimised = true;
                                                break;

                                            case false:
                                                WindowMinimised = false;
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



                                        switch (BeginExecutionAnimation)
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
                                                Rotator.Width = InitialRotatorWindth;
                                                break;
                                        }




                                        var Rotate = new RotateTransform();

                                        Rotate.Angle = RotationValue;

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
                                                    System.Speech.Recognition.Choices Choices = new System.Speech.Recognition.Choices("Eva Listen");
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
                                            try
                                            {
                                                ParallelProcessing = new System.Threading.Thread(() =>
                                                {
                                                    MainSpeechRecogniser.RecognizeAsyncStop();
                                                    MainSpeechRecogniser.RecognizeAsyncCancel();
                                                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                                                    GC.Collect(0, GCCollectionMode.Forced);
                                                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                                                    GC.Collect(1, GCCollectionMode.Forced);
                                                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                                                    GC.Collect(2, GCCollectionMode.Forced);

                                                    ParallelProcessing.Join();
                                                    ParallelProcessing.Abort();
                                                });
                                                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                                                ParallelProcessing.IsBackground = true;
                                                ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                                                ParallelProcessing.Start();
                                            }
                                            catch { }

                                            App.StopRecognitionSession = true;

                                            OnOff = 0;
                                            break;
                                    }

                                    break;
                            }

                            break;
                    }
                    break;
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

                                        switch (e.Result.Confidence >= 0.90)
                                        {
                                            case true:

                                                switch (e.Result.Text)
                                                {
                                                    case "Eva Listen":

                                                        switch (FunctionInitiated)
                                                        {
                                                            case false:

                                                                switch (WindowMinimised)
                                                                {
                                                                    case false:

                                                                        FunctionInitiated = true;


                                                                        Application.Current.MainWindow.Topmost = true;
                                                                        Application.Current.MainWindow.Activate();

                                                                        new Online_Speech_Recognition();

                                                                        Application.Current.MainWindow.Topmost = false;
                                                                        break;
                                                                }

                                                                break;
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

                                    var OpenPermissionDeclinedWindow = new ErrorWindow("Mircrophone Access Denied");
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
                                            var SettingWindowObject = new SettingsWindow();
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

        ~MainWindow()
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
            WindowMinimised = null;


            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(0, GCCollectionMode.Forced);
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(1, GCCollectionMode.Forced);
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced);
        }


    }
}
