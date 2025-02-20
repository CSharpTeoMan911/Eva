using System;
using System.Collections.Generic;
using System.Globalization;
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
using Windows.UI.Xaml.Controls;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for ChatGPT_Response_Window.xaml
    /// </summary>
    public partial class ChatGPT_Response_Window : Window
    {
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ChatGPT_API.Get_Available_Gpt_Models();
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
                            if (App.Application_Error_Shutdown)
                            {
                                try
                                {
                                    if (Animation_Timer != null)
                                    {
                                        Animation_Timer.Stop();
                                        this.Close();
                                    }
                                }
                                catch { }
                            }

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

                            if (SwitchOffset == true)
                            {
                                if (OffsetArithmetic > 0)
                                {
                                    OffsetArithmetic--;
                                    CloseButtonOffset.Offset += 0.003;
                                    ResponseTextBoxOffset.Offset += 0.003;
                                    WindowOffset.Offset += 0.003;
                                }
                                else
                                {
                                    SwitchOffset = false;
                                }
                            }
                            else
                            {
                                if (OffsetArithmetic < 300)
                                {
                                    OffsetArithmetic++;
                                    CloseButtonOffset.Offset -= 0.003;
                                    ResponseTextBoxOffset.Offset += 0.003;
                                    WindowOffset.Offset -= 0.003;
                                }
                                else
                                {
                                    SwitchOffset = true;
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
            Animation_Timer?.Dispose();
        }


        public async void Update_Conversation(string input)
        {
            if (WindowIsClosing == false)
            {
                Response_Loading = true;
                Tuple<Type, string> result = await ChatGPT_API.Initiate_Chat_GPT(input);

                await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            if (WindowIsClosing == false)
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

                                    ResponseTextBox.AppendText(user_input.ToString());

                                    Get_Last_Response_Line();

                                    ResponseTextBox.AppendText(chat_gpt_input.ToString());

                                    user_input.Clear();
                                    chat_gpt_input.Clear();

                                    await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTNotificationSoundEffect);
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
                                        else if (result.Item2 == "Input exceeds the maximum number of tokens")
                                        {
                                            ErrorWindow errorWindow = new ErrorWindow("Maximum number of tokens exceeded");
                                            errorWindow.Show();
                                        }
                                        else
                                        {
                                            if (result.Item2 != "Task cancelled")
                                            {
                                                ErrorWindow errorWindow = new ErrorWindow("ChatGPT error");
                                                errorWindow.Show();
                                            }
                                        }
                                    }
                                }
                            }

                            Response_Loading = false;
                        }
                    }
                });
            }
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
                        ChatGPT_API.Clear_Conversation_Cache();
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


        private void Send_Manual_GPT_Query(object sender, RoutedEventArgs e)
        {
            Update_Conversation_And_UI();
        }

        private void Detect_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Update_Conversation_And_UI();
            }
        }

        private void Update_Conversation_And_UI()
        {
            string input = InputTextBox.Text;
            InputTextBox.IsEnabled = false;
            InputTextBox.Clear();
            InputTextBox.Text = InputTextBox.Text.Remove(0, InputTextBox.MaxLength);
            Update_Conversation(input);
            InputTextBox.IsEnabled = true;
        }

        private void Text_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            #pragma warning disable CS0618 // Type or member is obsolete
            FormattedText formattedText = new FormattedText(InputTextBox.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Consolas"), 16,Brushes.Black);
            #pragma warning restore CS0618 // Type or member is obsolete

            if (formattedText.Width > InputTextBox.Width - 3)
            {
                this.Height = 500;
                System.Windows.Controls.Grid.SetRow(ResponseTextBox, 0);

                Window_Geometry.Rect = new Rect(0, 0, 450, 500);
                System.Windows.Controls.Grid.SetRow(Input_Stackpanel, 6);

                InputTextBox.Height = 120;
                InputTextBox.Margin = new Thickness(0, 30, 0, 0);
                InputTextBox_Geometry.Rect = new Rect(0, 0, 370, 120);

                Input_Button.Margin = new Thickness(0, 30, 0, 0);
            }
            else
            {
                this.Height = 350;
                System.Windows.Controls.Grid.SetRow(ResponseTextBox, 1);

                Window_Geometry.Rect = new Rect(0, 0, 450, 350);
                System.Windows.Controls.Grid.SetRow(Input_Stackpanel, 7);

                InputTextBox.Height = 30;
                InputTextBox.Margin = new Thickness(0, 10, 0, 0);
                InputTextBox_Geometry.Rect = new Rect(0, 0, 370, 30);

                Input_Button.Margin = new Thickness(10, 10, 0, 0);
            }
        }

        private void Get_Last_Response_Line() => ResponseTextBox.ScrollToLine(ResponseTextBox.LineCount - 1);
    }
}
