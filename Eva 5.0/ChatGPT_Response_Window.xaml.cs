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

        private bool maximum_token_limit_exceeded;

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

            if (Animation_Timer != null)
            {
                Animation_Timer.Dispose();
            }
        }


        public async Task<bool> Update_Conversation(string input)
        {
            if(InputTextBox.Text.Length / 4 <= 1000)
            {
                if (WindowIsClosing == false)
                {

                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {

                        if (Application.Current.MainWindow != null)
                        {
                            Response_Loading = true;

                            Tuple<Type, string> result = await ChatGPT_API.Initiate_Chat_GPT(input);


                            await Application.Current.Dispatcher.Invoke(async () =>
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

                                        ResponseTextBox.Text += user_input.ToString();
                                        ResponseTextBox.Text += chat_gpt_input.ToString();

                                        user_input.Clear();
                                        chat_gpt_input.Clear();

                                        Get_Last_Response_Line();

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
                            });

                            Response_Loading = false;
                        }

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

        private async void Update_Conversation_And_UI()
        {
            if (InputTextBox.Text.Length / 4 <= 1000)
            {
                string input = InputTextBox.Text;
                InputTextBox.Text = String.Empty;
                await Update_Conversation(input);
                while (InputTextBox.Undo() == true) { }
                InputTextBox.Text = String.Empty;
            }
        }

        private void Text_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            switch(InputTextBox.Text.Length / 4 > 1000)
            {
                case true:
                    Warning_TextBlock.Visibility = Visibility.Visible;
                    break;

                case false:
                    Warning_TextBlock.Visibility = Visibility.Hidden;
                    break;
            }

            #pragma warning disable CS0618 // Type or member is obsolete
            FormattedText formattedText = new FormattedText(InputTextBox.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Consolas"), 16,Brushes.Black);
            #pragma warning restore CS0618 // Type or member is obsolete

            switch (formattedText.Width > InputTextBox.Width - 3)
            {
                case true:
                    this.Height = 500;
                    System.Windows.Controls.Grid.SetRow(ResponseTextBox, 0);

                    Window_Geometry.Rect = new Rect(0, 0, 450, 500);
                    System.Windows.Controls.Grid.SetRow(Input_Stackpanel, 6);

                    InputTextBox.Height = 120;
                    InputTextBox.Margin = new Thickness(0, 30, 0, 0);
                    InputTextBox_Geometry.Rect = new Rect(0, 0, 370, 120);

                    Input_Button.Margin = new Thickness(0, 30, 0, 0);
                    break;
                case false:
                    this.Height = 350;
                    System.Windows.Controls.Grid.SetRow(ResponseTextBox, 1);

                    Window_Geometry.Rect = new Rect(0, 0, 450, 350);
                    System.Windows.Controls.Grid.SetRow(Input_Stackpanel, 7);

                    InputTextBox.Height = 30;
                    InputTextBox.Margin = new Thickness(0, 10, 0, 0);
                    InputTextBox_Geometry.Rect = new Rect(0, 0, 370, 30);

                    Input_Button.Margin = new Thickness(10, 10, 0, 0);
                    break;
            }
        }

        private void Get_Last_Response_Line()
        {
            int detected_index = 0;
            for(int i = 0; i < ResponseTextBox.LineCount; i++)
            {
                if (ResponseTextBox.GetLineText(i).Contains("ChatGPT: "))
                {
                    detected_index = i;
                }
            }

            ResponseTextBox.ScrollToLine(detected_index);
        }
    }
}
