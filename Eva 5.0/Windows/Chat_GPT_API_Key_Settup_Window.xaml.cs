using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using static Eva_5._0.ChatGPT_API;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for Chat_GPT_API_Key_Settup_Window.xaml
    /// </summary>
    public partial class Chat_GPT_API_Key_Settup_Window : Window
    {
        private bool keyValidated;

        private CancellationTokenSource tokenSource;
        private ChatGPT_API ChatGPT_API;

        private System.Timers.Timer Animation_Timer;
        private bool WindowIsClosing;

        private bool SwitchOffset;
        private double OffsetArithmetic;

        private delegate void ReloadCurrentModel();
        private ReloadCurrentModel reloadCurrentModel = new ReloadCurrentModel(SettingsWindow.ReloadCurrentModel);

        private string current_key;

        public Chat_GPT_API_Key_Settup_Window()
        {
            ChatGPT_API = new ChatGPT_API(new ChatGPT_API.Callback(ProcessGptResponse));
            InitializeComponent();
        }

        // METHOD THAT MOVES THE APPLICATION'S WINDOW AT THE CURRENT CURSOR LOCATION
        // WHEN THE MOUSE LEFT BUTTON IS PRESSED AND THE WINDOW'S HANDLE IS DRAGGED
        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            if (WindowIsClosing == false)
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

        // METHOD THAT CLOSES THE APPLICATION'S WINDOW 
        private void CloseTheWindow(object sender, RoutedEventArgs e)
        {
            if (WindowIsClosing == false)
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

        private async void ProcessGptResponse(ApiResponse response)
        {
            if (!tokenSource.IsCancellationRequested)
            {
                await Dispatcher.InvokeAsync(async () =>
                {
                    tokenSource?.Cancel();

                    if (!keyValidated)
                    {
                        if (response.type == ApiResponse.PayloadType.Payload)
                        {
                            await App.ChatGPT_API.Get_Available_Gpt_Models();
                            reloadCurrentModel.Invoke();
                            keyValidated = true;
                            this.Close();
                        }
                        else
                        {
                            // IF THE RETURN TYPE OF THE OPERATION IS AN EXCEPTION, THIS MEANS 
                            // THAT THE API KEY IS NOT CORRECT OR THAT ANOTHER ERROR OCCURRED
                            //
                            // [ BEIGN ]

                            await Settings.Set_Chat_GPT_Api_Key(current_key);

                            if (App.PermisissionWindowOpen == false)
                            {
                                if (response.response == "API authentification error")
                                {
                                    ErrorWindow errorWindow = new ErrorWindow("Invalid ChatGPT API key");
                                    errorWindow.Show();
                                }
                                else if (response.response == "Stream finished")
                                {
                                    await App.ChatGPT_API.Get_Available_Gpt_Models();
                                    reloadCurrentModel.Invoke();
                                    this.Close();
                                }
                                else
                                {
                                    ErrorWindow errorWindow = new ErrorWindow("ChatGPT error");
                                    errorWindow.Show();
                                }
                            }

                            // [ END ]

                        }
                    }
                });
            }

            MainStackPanel.Visibility = Visibility.Visible;
            LoadingIndicator.IsBusy = false;
        }


        // METHOD THAT SETS AND CACHES THE CHATGPT API KEY, IF THE KEY IS VALID
        private async void Set_Chat_Gpt_Api_Key(object sender, RoutedEventArgs e)
        {
            // IF THE WINDOW IS NOT CLOSING
            if (WindowIsClosing == false)
            {
                //IF THE SHUTDOWN OF THE UI THREAD DISPATHER HAS NOT STARTED
                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {
                    // IF THE MAIN WINDOW IS NOT NULL
                    if (Application.Current.MainWindow != null)
                    {
                        MainStackPanel.Visibility = Visibility.Collapsed;
                        LoadingIndicator.IsBusy = true;

                        tokenSource = new CancellationTokenSource();

                        current_key = await Settings.Get_Chat_GPT_Api_Key();

                        await Settings.Set_Chat_GPT_Api_Key(Chat_GPT_Api_Key_TextBox.Text);


                        // RETRIEVE THE CHATGPT API KEY INSERTED BY THE USER AND INITIATE A DUMMY QUERY IN ORDER TO TEST 
                        // THE VALIDITY OF THE API KEY.
                        //
                        // [ BEGIN ]

                        ChatGPT_API.Initiate_Chat_GPT(string.Empty, tokenSource.Token);

                        // [ END ]
                    }

                }

            }
        }

        // METHOD THAT IS CALLED WHEN THE CURRENT WINDOW LOADED
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {
                        Animation_Timer = new System.Timers.Timer();
                        Animation_Timer.Elapsed += Animation_Timer_Elapsed;
                        Animation_Timer.Interval = 40;
                        Animation_Timer.Start();

                    }

                }

            }
        }


        // ANIMATION TIMER
        private void Animation_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (WindowIsClosing == false)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (Application.Current.MainWindow != null)
                            {
                                if (App.Application_Error_Shutdown)
                                {
                                    Animation_Timer?.Stop();
                                    this.Close();
                                }

                                if (SwitchOffset == true)
                                {
                                    if (OffsetArithmetic > 0)
                                    {
                                        OffsetArithmetic--;
                                        CloseButtonOffset.Offset += 0.01;
                                        ChatGPTApiKeyOffset.Offset += 0.01;
                                        ChatGptApiTextBoxOffset.Offset += 0.01;
                                        WindowOffset.Offset += 0.01;
                                        SetChatGptApiButtonOffset.Offset += 0.01;
                                    }
                                    else
                                    {
                                        SwitchOffset = false;
                                    }
                                }
                                else
                                {
                                    if (OffsetArithmetic < 80)
                                    {
                                        OffsetArithmetic++;
                                        CloseButtonOffset.Offset -= 0.01;
                                        ChatGPTApiKeyOffset.Offset -= 0.01;
                                        ChatGptApiTextBoxOffset.Offset -= 0.01;
                                        WindowOffset.Offset -= 0.01;
                                        SetChatGptApiButtonOffset.Offset -= 0.01;
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

        // METHOD CALLED WHEN THE WINDOW IS CLOSING.
        // WHEN THE WINDOW IS CLOSING, THE ANIMATION
        // TIMER IS DISPOSED.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Animation_Timer?.Dispose();
            WindowIsClosing = true;
        }
    }
}
