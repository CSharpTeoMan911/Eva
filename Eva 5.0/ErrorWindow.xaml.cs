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
using System.Windows.Shapes;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private System.Timers.Timer AnimationTimer;

        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;

        private bool SwitchOffsetCloseButtonOffset;
        private double GradientArithmeticCloseButtonOffset;

        private bool SwitchOffsetNormaliseOrMaximiseButtonOffset;
        private double GradientArithmeticNormaliseOrMaximiseButtonOffset;

        private bool SwitchOffsetMinimiseTheWindowOffset;
        private double GradientArithmeticMinimiseTheWindowOffset;

        private bool SwitchOffsetErrorPageTitleOffset;
        private double GradientArithmeticErrorPageTitleOffset;

        private bool SwitchOffsetErrorPageTitleContentOffset;
        private double GradientArithmeticErrorPageTitleContentOffset;

        private bool SwitchOffsetErrorPageContentOffset;
        private double GradientArithmeticErrorPageContentOffset;

        private bool TimerDisposed;

        private byte NormalisedOrMaximised;

        private int ErrorPageTitleAnimation;

        public ErrorWindow(string ErrorType)
        {
            App.PermisissionWindowOpen = true;
            InitializeComponent();

            switch (ErrorType)
            {
                case "Mircrophone Access Denied":

                    Task.Run(async () =>
                    {
                        await MicrophoneAccessDenied();
                    });

                    break;

                case "Online Speech Recognition Access Denied":

                    Task.Run(async () =>
                    {
                        await OnlineSpeechRecognitionAccessDenied();
                    });

                    break;
            }
        }


        private async Task<bool> MicrophoneAccessDenied()
        {

            try
            {
                var ErrorSoundEffect = new System.Media.SoundPlayer("Privacy statement declined or mic not available.wav");

                switch (await Settings.Get_Settings() == true)
                {
                    case true:
                        ErrorSoundEffect.Play();
                        break;
                }
            }
            catch { }

            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-microphone"));

            Application.Current.Dispatcher.Invoke(() =>
            {
                ErrorContext.Text = "Go to Settings  ->  Privacy  ->  Microphone.\n\n\nUnder the  [Allow apps to access your microphone]\nsection, press the button associated with it, in order\nto enable it.";
            });

            return false;
        }

        private async Task<bool> OnlineSpeechRecognitionAccessDenied()
        {
            try
            {
                var ErrorSoundEffect = new System.Media.SoundPlayer("Privacy statement declined or mic not available.wav");

                switch (await Settings.Get_Settings() == true)
                {
                    case true:
                        ErrorSoundEffect.Play();
                        break;
                }
            }
            catch { }

            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-speech"));

            Application.Current.Dispatcher.Invoke(() =>
            {
                ErrorContext.Text = "Go to Settings  ->  Privacy  ->  Speech.\n\n\nUnder the  [Online speech recognition]  section,\npress the button associated with it,\nin order to enable it.";
            });

            return false;
        }

        private void MinimiseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (App.PermisissionWindowOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:


                            switch (Application.Current.MainWindow == null)
                            {
                                case false:
                                    this.WindowState = WindowState.Minimized;
                                    break;
                            }


                            break;
                    }
                    break;
            }
        }

        private void NormaliseOrMaximiseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (App.PermisissionWindowOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:


                            switch (Application.Current.MainWindow == null)
                            {
                                case false:

                                    NormalisedOrMaximised++;

                                    switch (NormalisedOrMaximised)
                                    {
                                        case 1:
                                            this.WindowState = WindowState.Maximized;
                                            break;

                                        case 2:
                                            this.WindowState = WindowState.Normal;
                                            NormalisedOrMaximised = 0;
                                            break;
                                    }

                                    break;
                            }


                            break;
                    }
                    break;
            }
        }

        private void CloseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (App.PermisissionWindowOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:


                            switch (Application.Current.MainWindow == null)
                            {
                                case false:
                                    this.Close();
                                    break;
                            }


                            break;
                    }
                    break;
            }
        }


        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            switch (App.PermisissionWindowOpen)
            {
                case true:

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


        private void ErrorWindowLoaded(object sender, RoutedEventArgs e)
        {
            AnimationTimer = new System.Timers.Timer();
            AnimationTimer.Disposed += AnimationTimer_Disposed;
            AnimationTimer.Elapsed += AnimationTimer_Elapsed;
            AnimationTimer.Interval = 10;
            AnimationTimer.Start();
        }

        private void AnimationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {


            switch (App.PermisissionWindowOpen)
            {
                case false:
                    try
                    {
                        AnimationTimer.Stop();
                    }
                    catch { }
                    break;

                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case true:
                            try
                            {
                                AnimationTimer.Stop();
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
                                            AnimationTimer.Stop();
                                        }
                                        catch { }
                                        break;

                                    case false:

                                        switch (SwitchWindowOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticWindowOffset <= 300)
                                                {
                                                    case true:
                                                        GradientArithmeticWindowOffset++;
                                                        WindowOffset.Offset -= 0.0025;
                                                        break;

                                                    case false:
                                                        SwitchWindowOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticWindowOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticWindowOffset--;
                                                        WindowOffset.Offset += 0.0025;
                                                        break;

                                                    case false:
                                                        SwitchWindowOffset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetCloseButtonOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticCloseButtonOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticCloseButtonOffset++;
                                                        CloseButtonOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCloseButtonOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticCloseButtonOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticCloseButtonOffset--;
                                                        CloseButtonOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCloseButtonOffset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetNormaliseOrMaximiseButtonOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticNormaliseOrMaximiseButtonOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticNormaliseOrMaximiseButtonOffset++;
                                                        NormaliseOrMaximiseButtonOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetNormaliseOrMaximiseButtonOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticNormaliseOrMaximiseButtonOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticNormaliseOrMaximiseButtonOffset--;
                                                        NormaliseOrMaximiseButtonOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetNormaliseOrMaximiseButtonOffset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        switch (SwitchOffsetMinimiseTheWindowOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticMinimiseTheWindowOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticMinimiseTheWindowOffset++;
                                                        MinimiseTheWindowOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMinimiseTheWindowOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticMinimiseTheWindowOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticMinimiseTheWindowOffset--;
                                                        MinimiseTheWindowOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMinimiseTheWindowOffset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetErrorPageTitleOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticErrorPageTitleOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticErrorPageTitleOffset++;
                                                        ErrorPageTitleOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetErrorPageTitleOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticErrorPageTitleOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticErrorPageTitleOffset--;
                                                        ErrorPageTitleOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetErrorPageTitleOffset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetErrorPageTitleContentOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticErrorPageTitleContentOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticErrorPageTitleContentOffset++;
                                                        ErrorPageTitleContentOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetErrorPageTitleContentOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticErrorPageTitleContentOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticErrorPageTitleContentOffset--;
                                                        ErrorPageTitleContentOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetErrorPageTitleContentOffset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        switch (SwitchOffsetErrorPageContentOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticErrorPageContentOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticErrorPageContentOffset++;
                                                        ErrorPageContentOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetErrorPageContentOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticErrorPageContentOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticErrorPageContentOffset--;
                                                        ErrorPageContentOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetErrorPageContentOffset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        ErrorPageTitleAnimation++;

                                        switch (ErrorPageTitleAnimation)
                                        {


                                            case 25:
                                                ErrorPageTitleContent.Text = "E";
                                                break;

                                            case 50:
                                                ErrorPageTitleContent.Text = "Er";
                                                break;

                                            case 75:
                                                ErrorPageTitleContent.Text = "Err";
                                                break;

                                            case 100:
                                                ErrorPageTitleContent.Text = "Erro";
                                                break;

                                            case 125:
                                                ErrorPageTitleContent.Text = "Error";
                                                break;

                                            case 150:
                                                ErrorPageTitleContent.Text = "Error_";
                                                break;

                                            case 175:
                                                ErrorPageTitleContent.Text = "Error";
                                                break;

                                            case 200:
                                                ErrorPageTitleContent.Text = "Error_";
                                                break;

                                            case 225:
                                                ErrorPageTitleContent.Text = "Error";
                                                break;

                                            case 250:
                                                ErrorPageTitleContent.Text = String.Empty;
                                                ErrorPageTitleAnimation = 0;
                                                break;
                                        }


                                        switch (App.InitiateErrorFunction)
                                        {
                                            case true:

                                                App.InitiateErrorFunction = false;

                                                switch (App.ErrorFunction)
                                                {
                                                    case "Mircrophone Access Denied":

                                                        Task.Run(async () =>
                                                        {
                                                            await MicrophoneAccessDenied();
                                                        });

                                                        break;

                                                    case "Online Speech Recognition Access Denied":

                                                        Task.Run(async () =>
                                                        {
                                                            await OnlineSpeechRecognitionAccessDenied();
                                                        });

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

        private void AnimationTimer_Disposed(object sender, EventArgs e)
        {
            TimerDisposed = true;
        }

        private void ErrorWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (App.ErrorAppShutdown)
            {
                case true:
                    Application.Current.MainWindow.Close();
                    break;
            }


            App.PermisissionWindowOpen = false;
        }

        ~ErrorWindow()
        {
            switch (TimerDisposed)
            {
                case false:

                    switch (AnimationTimer == null)
                    {

                        case false:

                            AnimationTimer.Dispose();
                            break;
                    }
                    break;
            }
        }
    }
}
