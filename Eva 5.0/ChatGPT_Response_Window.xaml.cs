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
    /// Interaction logic for ChatGPT_Response_Window.xaml
    /// </summary>
    public partial class ChatGPT_Response_Window : Window
    {
        private static System.Media.SoundPlayer ChatGPTNotificationSoundEffect = new System.Media.SoundPlayer("Chat_GPT_Notification.wav");
        private static RotateTransform Rotate = new RotateTransform();
        private System.Timers.Timer Animation_Timer;


        private static StringBuilder user_input = new StringBuilder();
        private static StringBuilder chat_gpt_input = new StringBuilder();

        private bool WindowIsClosing;
        private bool Response_Loading;
        private double RotationValue;

        private bool SwitchOffset;
        private double OffsetArithmetic;

        public ChatGPT_Response_Window()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.ChatGPTResponseWindowOpened = true;

            Animation_Timer = new System.Timers.Timer();
            Animation_Timer.Interval = 10;
            Animation_Timer.Elapsed += Animation_Timer_Elapsed;
            Animation_Timer.Start();
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
                            if (Response_Loading == true)
                            {
                                Loading_Background.Height = ResponseTextBox.Height;
                                Loading_Background.Width = ResponseTextBox.Width;

                                Loading_TextBlock.Height = 60;
                                Loading_TextBlock.Width = 60;

                                RotationValue += 4;

                                if (RotationValue == 360)
                                {
                                    RotationValue = 0;
                                }

                                Rotate.Angle = RotationValue;
                                Loading_TextBlock.RenderTransform = Rotate;
                            }
                            else
                            {
                                Loading_Background.Height = 0;
                                Loading_Background.Width = 0;

                                Loading_TextBlock.Height = 0;
                                Loading_TextBlock.Width = 0;

                                RotationValue = 0;
                                Rotate.Angle = 0;

                                Loading_TextBlock.RenderTransform = Rotate;
                            }


                            switch (SwitchOffset)
                            {
                                case true:

                                    switch (OffsetArithmetic > 0)
                                    {
                                        case true:
                                            OffsetArithmetic--;
                                            CloseButtonOffset.Offset += 0.003;
                                            ResponseTextBoxOffset.Offset += 0.003;
                                            WindowOffset.Offset += 0.003;
                                            break;

                                        case false:
                                            SwitchOffset = false;
                                            break;
                                    }

                                    break;

                                case false:

                                    switch (OffsetArithmetic < 300)
                                    {
                                        case true:
                                            OffsetArithmetic++;
                                            CloseButtonOffset.Offset -= 0.003;
                                            ResponseTextBoxOffset.Offset += 0.003;
                                            WindowOffset.Offset -= 0.003;
                                            break;

                                        case false:
                                            SwitchOffset = true;
                                            break;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            if(Animation_Timer != null)
                            {
                                Animation_Timer.Stop();
                            }
                        }
                    });

                }
                else
                {
                    if (Animation_Timer != null)
                    {
                        Animation_Timer.Stop();
                    }
                }

            }
            else
            {
                if (Animation_Timer != null)
                {
                    Animation_Timer.Stop();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowIsClosing = true;
            App.ChatGPTResponseWindowOpened = false;

            if (Animation_Timer != null)
            {
                Animation_Timer.Dispose();
            }
        }


        public async Task<bool> Update_Conversation(string input)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {
                        Response_Loading = true;

                        Tuple<Type, string> result = await ChatGPT_API.Initiate_Chat_GPT(input);


                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if(WindowIsClosing == false)
                            {
                                if (result.Item1 == typeof(string))
                                {
                                    user_input.Clear();
                                    chat_gpt_input.Clear();

                                    user_input.Append("User: ");
                                    user_input.Append(input);
                                    user_input.Append("\n\n");


                                    chat_gpt_input.Append("ChatGPT: ");
                                    chat_gpt_input.Append(result.Item2);
                                    chat_gpt_input.Append("\n\n");

                                    ResponseTextBox.Text += user_input.ToString();
                                    ResponseTextBox.Text += chat_gpt_input.ToString();

                                    user_input.Clear();
                                    chat_gpt_input.Clear();
                                }
                                else
                                {
                                    if (App.PermisissionWindowOpen == false)
                                    {
                                        if (result.Item2 == "API authentification error")
                                        {
                                            ErrorWindow errorWindow = new ErrorWindow("Invalid ChatGPT API key");
                                            errorWindow.Show();
                                        }
                                        else
                                        {
                                            if(result.Item2 != "Task cancelled")
                                            {
                                                ErrorWindow errorWindow = new ErrorWindow("ChatGPT error");
                                                errorWindow.Show();
                                            }
                                        }
                                    }
                                }
                            }
                        });

                        if (await Settings.Get_Sound_Settings() == true)
                        {
                            if (System.IO.File.Exists(@"Chat_GPT_Notification.wav"))
                            {
                                if(WindowIsClosing == false)
                                {
                                    ChatGPTNotificationSoundEffect.Play();
                                }
                            }
                        }

                        Response_Loading = false;
                    }

                }

            }

            return true;
        }



        private void MinimiseTheWindow(object sender, RoutedEventArgs e)
        {
            if (WindowIsClosing == false)
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

        private void Move_The_Window(object sender, MouseButtonEventArgs e)
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

        public static Task<bool> Dispose_Sound_Effects()
        {
            if(ChatGPTNotificationSoundEffect != null)
            {
                ChatGPTNotificationSoundEffect.Dispose();
            }

            return Task.FromResult(true);
        }



        private async void Send_Manual_GPT_Query(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            InputTextBox.Text = String.Empty;
            await Update_Conversation(input);
        }
    }
}
