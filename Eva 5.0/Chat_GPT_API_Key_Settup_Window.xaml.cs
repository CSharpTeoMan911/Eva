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
    /// Interaction logic for Chat_GPT_API_Key_Settup_Window.xaml
    /// </summary>
    public partial class Chat_GPT_API_Key_Settup_Window : Window
    {
        private System.Timers.Timer Animation_Timer;
        private bool WindowIsClosing;

        private bool SwitchOffset;
        private double OffsetArithmetic;


        public Chat_GPT_API_Key_Settup_Window()
        {
            InitializeComponent();
        }

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

        private async void Set_Chat_Gpt_Api_Key(object sender, RoutedEventArgs e)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        await Settings.Set_Chat_GPT_Api_Key(Chat_GPT_Api_Key_TextBox.Text);

                        // RETRIEVE THE CHATGPT API KEY INSERTED BY THE USER AND INITIATE A DUMMY QUERY IN ORDER TO TEST 
                        // THE VALIDITY OF THE API KEY.
                        //
                        // [ BEGIN ]

                        Tuple<Type, string> initiation_result = await ChatGPT_API.Initiate_Chat_GPT(String.Empty);

                        // [ END ]





                        if(initiation_result.Item1 == typeof(Exception))
                        {
                            // IF THE RETURN TYPE OF THE OPERATION IS AN EXCEPTION, THIS MEANS 
                            // THAT THE API KEY IS NOT CORRECT OR THAT ANOTHER ERROR OCCURRED
                            //
                            // [ BEIGN ]

                            if (App.PermisissionWindowOpen == false)
                            {
                                if(initiation_result.Item2 == "API authentification error")
                                {
                                    ErrorWindow errorWindow = new ErrorWindow("Invalid ChatGPT API key");
                                    errorWindow.Show();
                                }
                                else
                                {
                                    ErrorWindow errorWindow = new ErrorWindow("ChatGPT error");
                                    errorWindow.Show();
                                }
                            }

                            // [ END ]

                        }
                        else
                        {
                            // IF THE RETURN TYPE OF THE OPERATION IS NOT AN EXCEPTION
                            // THIS MEANS THAT THE API KEY IS VALID AND THE WIDOW IS
                            // CLOSED 

                            this.Close();
                        }
                    }

                }

            }
        }

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

        private void Animation_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {

                            switch (SwitchOffset)
                            {
                                case true:

                                    switch (OffsetArithmetic > 0)
                                    {
                                        case true:
                                            OffsetArithmetic--;
                                            CloseButtonOffset.Offset += 0.01;
                                            ChatGPTApiKeyOffset.Offset += 0.01;
                                            ChatGptApiTextBoxOffset.Offset += 0.01;
                                            WindowOffset.Offset += 0.01;
                                            SetChatGptApiButtonOffset.Offset += 0.01;
                                            break;

                                        case false:
                                            SwitchOffset = false;
                                            break;
                                    }

                                    break;

                                case false:

                                    switch (OffsetArithmetic < 88)
                                    {
                                        case true:
                                            OffsetArithmetic++;
                                            CloseButtonOffset.Offset -= 0.01;
                                            ChatGPTApiKeyOffset.Offset -= 0.01;
                                            ChatGptApiTextBoxOffset.Offset -= 0.01;
                                            WindowOffset.Offset -= 0.01;
                                            SetChatGptApiButtonOffset.Offset -= 0.01;
                                            break;

                                        case false:
                                            SwitchOffset = true;
                                            break;
                                    }

                                    break;
                            }

                        }
                    });

                }

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        if(Animation_Timer != null)
                        {
                            Animation_Timer.Dispose();
                        }

                    }

                }

            }
        }
    }
}
