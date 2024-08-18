using Eva_5._0.Properties;
using Newtonsoft.Json.Serialization;
using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private Sound_Player player = new Sound_Player();
        private System.Timers.Timer AnimationTimer;

        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;

        private bool SwitchSecondaryOffsets;
        private double GradientArithmeticSecodaryOffsets;

        private bool TimerDisposed;

        private byte NormalisedOrMaximised;

        private int ErrorPageTitleAnimation;

        public ErrorWindow(string ErrorType)
        {
            InitializeComponent();
            App.PermisissionWindowOpen = true;

            Application.Current.Dispatcher.Invoke(() =>
            {
                switch (ErrorType)
                {
                    case "Mircrophone Access Denied":
                        MicrophoneAccessDenied();
                        break;

                    case "Online Speech Recognition Access Denied":
                        OnlineSpeechRecognitionAccessDenied();
                        break;

                    case "Invalid ChatGPT API key":
                        InvalidChatGPTAPIKey();
                        break;

                    case "ChatGPT error":
                        ChatGPTError();
                        break;

                    case "Maximum number of tokens exceeded":
                        Token_Limit_Exceeded();
                        break;

                    case "Language not supported":
                        Language_Not_Supported();
                        break;

                    case "Unsupported Voice":
                        Unsupported_Voice();
                        break;
                }
            });
        }


        private async void MicrophoneAccessDenied()
        {
            App.Application_Error_Shutdown = true;
            await player.Play_Sound(Sound_Player.Sounds.ErrorSoundEffect);

            try
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-microphone"));

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (App.Get_Windows_Version() == "Windows 10")
                    {
                        ErrorContext.Text = "Go to Settings  ->  Privacy  ->  Microphone.\n\n\nUnder the  [Allow apps to access your microphone]\nsection, press the button associated with it, in order\nto enable it.";
                    }
                    else
                    {
                        ErrorContext.Text = "Go to Privacy & Security  ->  Microphone.\n\n\nUnder the  [Microphone access] section, press the\nbutton associated with it, in order to enable it.";
                    }
                });
            }
            catch { }
        }

        private async void OnlineSpeechRecognitionAccessDenied()
        {
            await player.Play_Sound(Sound_Player.Sounds.ErrorSoundEffect);

            try
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-speech"));

                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (App.Get_Windows_Version() == "Windows 10")
                    {
                        ErrorContext.Text = "Go to Settings  ->  Privacy  ->  Speech.\n\n\nUnder the  [Online speech recognition]  section,\npress the button associated with it,\nin order to enable it.";
                    }
                    else
                    {
                        ErrorContext.Text = "Go to Privacy & Security  ->  Speech.\n\n\nUnder the  [Online speech recognition]  section,\npress the button associated with it,\nin order to enable it.";
                    }
                });
            }
            catch { }
        }


        private async void InvalidChatGPTAPIKey()
        {
            await player.Play_Sound(Sound_Player.Sounds.ErrorSoundEffect);

            try
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Run open_ai_link = new Run("https://openai.com");
                    Run open_ai_api_link_1 = new Run("https://platform.openai.com/account/api-keys");
                    Run open_ai_api_link_2 = new Run("https://platform.openai.com/account/api-keys");


                    Hyperlink open_ai_link_hyperlink = new Hyperlink(open_ai_link)
                    {
                        NavigateUri = new Uri("https://openai.com")
                    };
                    open_ai_link_hyperlink.RequestNavigate += Hyperlink_RequestNavigate;


                    Hyperlink open_ai_api_link_hyperlink_1 = new Hyperlink(open_ai_api_link_1)
                    {
                        NavigateUri = new Uri("https://platform.openai.com/account/api-keys")
                    };
                    open_ai_api_link_hyperlink_1.RequestNavigate += Hyperlink_RequestNavigate;


                    Hyperlink open_ai_api_link_hyperlink_2 = new Hyperlink(open_ai_api_link_2)
                    {
                        NavigateUri = new Uri("https://platform.openai.com/account/api-keys")
                    };
                    open_ai_api_link_hyperlink_2.RequestNavigate += Hyperlink_RequestNavigate;


                    ErrorContext.Text = String.Empty;

                    ErrorContext.Inlines.Add("Go to ");
                    ErrorContext.Inlines.Add(open_ai_link_hyperlink);
                    ErrorContext.Inlines.Add(" and create an account, then");
                    ErrorContext.Inlines.Add("\n");
                    ErrorContext.Inlines.Add("go to ");
                    ErrorContext.Inlines.Add(open_ai_api_link_hyperlink_1);
                    ErrorContext.Inlines.Add(" to");
                    ErrorContext.Inlines.Add("\n");
                    ErrorContext.Inlines.Add("create an API key.");
                    ErrorContext.Inlines.Add("\n\n");
                    ErrorContext.Inlines.Add("If you already have an OpenAI account, go directly to");
                    ErrorContext.Inlines.Add("\n");
                    ErrorContext.Inlines.Add(open_ai_api_link_hyperlink_2);
                    ErrorContext.Inlines.Add(" and");
                    ErrorContext.Inlines.Add("\n");
                    ErrorContext.Inlines.Add("generate an API key.");

                });
            }
            catch { }
        }


        private async void ChatGPTError()
        {
            await player.Play_Sound(Sound_Player.Sounds.ErrorSoundEffect);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Run open_ai_link = new Run("https://platform.openai.com/account/usage");

                Hyperlink open_ai_link_hyperlink = new Hyperlink(open_ai_link)
                {
                    NavigateUri = new Uri("https://platform.openai.com/account/usage")
                };
                open_ai_link_hyperlink.RequestNavigate += Hyperlink_RequestNavigate;

                ErrorContext.Inlines.Add("An error occured. ");
                ErrorContext.Inlines.Add("Please check your available balance");
                ErrorContext.Inlines.Add("\n");
                ErrorContext.Inlines.Add("at ");
                ErrorContext.Inlines.Add(open_ai_link_hyperlink);
                ErrorContext.Inlines.Add("\n");
                ErrorContext.Inlines.Add("or your internet connection.");
            });
        }


        private async void Token_Limit_Exceeded()
        {
            await player.Play_Sound(Sound_Player.Sounds.ErrorSoundEffect);

            ErrorContext.Inlines.Add("Maximum number of tokens exceeded. ");
            ErrorContext.Inlines.Add("Please enter a query ");
            ErrorContext.Inlines.Add("\n");
            ErrorContext.Inlines.Add("that contains a lower number of characters.");
            ErrorContext.Inlines.Add("The maximum ");
            ErrorContext.Inlines.Add("\n");
            ErrorContext.Inlines.Add("number of characters in a query is ");
            ErrorContext.Inlines.Add("4000 characters.");
        }


        private async void Language_Not_Supported()
        {
            try
            {
                await player.Play_Sound(Sound_Player.Sounds.ErrorSoundEffect);

                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    if (App.Get_Windows_Version() == "Windows 10")
                    {
                        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:speech"));

                        ErrorContext.Inlines.Add("The English language pack is ");
                        ErrorContext.Inlines.Add("not detected on your system. \n");
                        ErrorContext.Inlines.Add("Go to Settings  ->  Time & Language  ->  Language.\n");
                        ErrorContext.Inlines.Add("Under the 'Preferred Languages' section press the \n");
                        ErrorContext.Inlines.Add("'Add a language' button and download the 'en-US'\n");
                        ErrorContext.Inlines.Add("or the 'en-GB' language pack. You must restart\n");
                        ErrorContext.Inlines.Add("your computer afterwards.");
                    }
                    else
                    {
                        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:regionlanguage"));

                        ErrorContext.Inlines.Add("The English language pack is ");
                        ErrorContext.Inlines.Add("not detected on your system. \n");
                        ErrorContext.Inlines.Add("Go to Settings  ->  Time & Language  ->  Language & Region.\n");
                        ErrorContext.Inlines.Add("Under the 'Preferred Languages' section press the \n");
                        ErrorContext.Inlines.Add("'Add a language' button and download the 'en-US'\n");
                        ErrorContext.Inlines.Add("or the 'en-GB' language pack. You must restart\n");
                        ErrorContext.Inlines.Add("your computer afterwards.");
                    }
                });

            }
            catch { }
        }


        private async void Unsupported_Voice()
        {
            try
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:speech"));

                await player.Play_Sound(Sound_Player.Sounds.ErrorSoundEffect);

                ErrorContext.Inlines.Add("The English voices language pack is ");
                ErrorContext.Inlines.Add("not detected on your system. \n");
                ErrorContext.Inlines.Add("Go to Settings  ->  Time & Language  ->  Speech.\n");
                ErrorContext.Inlines.Add("Under the 'Manage voices' section press the \n");
                ErrorContext.Inlines.Add("'Add voice' button and download the 'en-US'\n");
                ErrorContext.Inlines.Add("or the 'en-GB' voice language pack.");
            }
            catch { }
        }





        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process navigation_process = new System.Diagnostics.Process();
            navigation_process.StartInfo.FileName = ((Hyperlink)sender).NavigateUri.AbsoluteUri;
            navigation_process.Start();
        }


        private void MinimiseTheWindow(object sender, RoutedEventArgs e)
        {
            if (App.PermisissionWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        this.WindowState = WindowState.Minimized;

                    }

                }

            }
        }

        private void NormaliseOrMaximiseTheWindow(object sender, RoutedEventArgs e)
        {
            if (App.PermisissionWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

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

                    }

                }

            }
        }

        private void CloseTheWindow(object sender, RoutedEventArgs e)
        {
            if (App.PermisissionWindowOpen == true)
            {

                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {

                            if (Application.Current.MainWindow != null)
                            {

                                    this.Close();

                            }

                    }

            }
        }


        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            if (App.PermisissionWindowOpen == true)
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


                                        switch (SwitchSecondaryOffsets)
                                        {
                                            case false:

                                                switch (GradientArithmeticSecodaryOffsets <= 100)
                                                {
                                                    case true:
                                                        GradientArithmeticSecodaryOffsets++;
                                                        CloseButtonOffset.Offset += 0.025;
                                                        NormaliseOrMaximiseButtonOffset.Offset += 0.025;
                                                        MinimiseTheWindowOffset.Offset += 0.025;
                                                        ErrorPageTitleOffset.Offset += 0.025;
                                                        ErrorPageTitleContentOffset.Offset += 0.025;
                                                        ErrorPageContentOffset.Offset += 0.025;
                                                        break;
                                                    case false:
                                                        SwitchSecondaryOffsets = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticSecodaryOffsets > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticSecodaryOffsets--;
                                                        CloseButtonOffset.Offset -= 0.025;
                                                        NormaliseOrMaximiseButtonOffset.Offset -= 0.025;
                                                        MinimiseTheWindowOffset.Offset -= 0.025;
                                                        ErrorPageTitleOffset.Offset -= 0.025;
                                                        ErrorPageTitleContentOffset.Offset -= 0.025;
                                                        ErrorPageContentOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchSecondaryOffsets = false;
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

        private async void ErrorWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.PermisissionWindowOpen = false;

            if(App.Application_Error_Shutdown == true)
            {
                await Wake_Word_Engine.Stop_The_Wake_Word_Engine();
                Environment.Exit(0);
            }
        }


        private void Window_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            if (App.PermisissionWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        Rect geometry = new Rect();

                        geometry.Height = this.Height;
                        geometry.Width = this.Width;

                        Error_Window_Geometry.Rect = geometry;

                    }

                }

            }
        }


        ~ErrorWindow()
        {
            if (TimerDisposed == false)
            {
                AnimationTimer?.Dispose();
            }
        }
    }
}
