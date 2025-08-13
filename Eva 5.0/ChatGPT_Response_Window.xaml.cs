using ModernWpf.Toolkit.UI.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Windows.ApplicationModel.Contacts;


namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for ChatGPT_Response_Window.xaml
    /// </summary>
    /// 

    public partial class ChatGPT_Response_Window : Window
    {
        private StringBuilder builder = new StringBuilder();
        private CancellationTokenSource tokenSource;

        private ChatGPT_API ChatGPT_API;
        private System.Timers.Timer Animation_Timer;

        private bool WindowIsClosing;
        private bool SwitchOffset;
        private double OffsetArithmetic;

        private bool processing;

        private bool isMenuPanelExpanded = false;

        public ObservableCollection<Message> messages { get; private set; } = new ObservableCollection<Message>();
        private Message last_gpt_message { get; set; }


        public ChatGPT_Response_Window()
        {
            ChatGPT_API = new ChatGPT_API(new ChatGPT_API.Callback(ApiResponseCallback));
            InitializeComponent();
            DataContext = this;
        }

        internal async void ApiResponseCallback(ChatGPT_API.ApiResponse response)
        {
            if (response.type == ChatGPT_API.ApiResponse.PayloadType.Payload)
            {
                if (!string.IsNullOrEmpty(response.response))
                {
                    if (last_gpt_message == null)
                    {
                        last_gpt_message = new Message(Message.MessageType.Assistant, response.response);
                        messages.Add(last_gpt_message);
                    }
                    else
                    {
                        last_gpt_message.UpdateMessage(response.response, true);
                    }

                    ConversationScrollViewer.ScrollToBottom();
                }
            }
            else if (response.type == ChatGPT_API.ApiResponse.PayloadType.Notification)
            {
                if (response.response == "Stream finished")
                {
                    Input_Button.Style = Application.Current.FindResource("SendGptQueryButtonStyle") as Style;
                    Input_Button.Content = "\xF5B0";
                    processing = false;
                    last_gpt_message = null;
                    await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTNotificationSoundEffect);
                }
            }
            else if (response.type == ChatGPT_API.ApiResponse.PayloadType.Exception)
            {
                if (App.PermisissionWindowOpen == false)
                {
                    if (response.response == "API authentification error")
                    {
                        ErrorWindow errorWindow = new ErrorWindow("Invalid ChatGPT API key");
                        errorWindow.Show();
                    }
                    else if (response.response == "Input exceeds the maximum number of tokens")
                    {
                        ErrorWindow errorWindow = new ErrorWindow("Maximum number of tokens exceeded");
                        errorWindow.Show();
                    }
                    else
                    {
                        ErrorWindow errorWindow = new ErrorWindow("ChatGPT error");
                        errorWindow.Show();
                    }
                }

            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ChatGPT_API.Get_Available_Gpt_Models();
            App.ChatGPTResponseWindowOpened = true;

            Animation_Timer = new System.Timers.Timer();
            Animation_Timer.Interval = 10;
            Animation_Timer.Elapsed += Animation_Timer_Elapsed;
            Animation_Timer.Start();

            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

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
                                        CloseButtonOffset.Offset += 0.003;
                                        ConversationScrollViewerOffset.Offset += 0.003;
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
                                        ConversationScrollViewerOffset.Offset += 0.003;
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
                                Animation_Timer?.Stop();
                            }
                        });

                    }
                    else
                    {
                        Animation_Timer?.Stop();
                    }

                }
                else
                {
                    Animation_Timer?.Stop();
                }
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowIsClosing = true;
            App.ChatGPTResponseWindowOpened = false;
            Animation_Timer?.Dispose();
        }


        public async void Update_Conversation(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    if (WindowIsClosing == false)
                    {
                        tokenSource = new CancellationTokenSource();
                        messages.Add(new Message(Message.MessageType.User, input));
                        processing = ChatGPT_API.Initiate_Chat_GPT(input, tokenSource.Token);

                        if (processing)
                        {
                            Input_Button.Style = Application.Current.FindResource("SendGptQueryButtonProcessingStyle") as Style;
                            Input_Button.Content = "\xF8AE";
                        }
                    }
                }, DispatcherPriority.Render);
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
            if (e.Key == Key.Return && !processing)
            {
                Update_Conversation_And_UI(true);
            }
        }

        private async void Update_Conversation_And_UI(bool carriageReturn = false)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                switch (processing)
                {
                    case false:
                        builder.Clear();
                        builder.Append(InputTextBox.Text);

                        if (carriageReturn)
                            builder.Remove(builder.Length - 2, 2).ToString();

                        string input = builder.ToString();

                        InputTextBox.IsEnabled = false;
                        InputTextBox.Clear();
                        Update_Conversation(input);
                        InputTextBox.IsEnabled = true;
                        InputTextBox.UpdateLayout();
                        break;

                    case true:
                        RemoveMessage();
                        Input_Button.Style = Application.Current.FindResource("SendGptQueryButtonStyle") as Style;
                        Input_Button.Content = "\xF5B0";
                        tokenSource?.Cancel();
                        processing = false;
                        break;
                }
            }, DispatcherPriority.Render);
        }

        private void RemoveMessage()
        {
            messages.RemoveAt(messages.Count - 1);
            ChatGPT_API.RemoveLastMessage();
        }

        private void NormaliseOrMaximiseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Normal:
                    this.WindowState = WindowState.Maximized;
                    NormaliseOrMaximiseTheWindowButton.Content = "\xEF2E";
                    break;
                case WindowState.Maximized:
                    this.WindowState = WindowState.Normal;
                    NormaliseOrMaximiseTheWindowButton.Content = "\xEF2F";
                    break;
            }

            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    this.Clip = new RectangleGeometry(new Rect(0, 0, e.NewSize.Width, e.NewSize.Height), 0, 0);
                    break;
                case WindowState.Normal:
                    this.Clip = Window_Geometry;
                    break;
            }

            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void RenderConversationScrollViewerOffset()
        {
            double new_height = (this.RenderSize.Height - WindowHandle.RenderSize.Height) - 25 - Input_Stackpanel.RenderSize.Height;
            ConversationScrollViewer.Height = new_height >= 0 ? new_height : 0;
            ConversationScrollViewer.UpdateLayout();
        }

        private void RenderInputTextbox()
        {
            if (InputTextBoxGrid.RenderSize.Width > 0)
            {
                if (isMenuPanelExpanded)
                {
                    double pane_width = this.RenderSize.Width * 25 / 100;
                    double content_width = this.RenderSize.Width * 75 / 100;
                    MenuPanel.Width = pane_width;
                    ContentPanel.Width = content_width;
                }
                else
                {
                    double content_width = this.RenderSize.Width;
                    MenuPanel.Width = 0;
                    ContentPanel.Width = content_width;
                }


                double columnWidth = ContentPanel.RenderSize.Width / 2;

                double ratio = columnWidth * 1.8 - 40;
                double width = this.RenderSize.Width;


                if (ratio > 0)
                {
                    if (width > 0)
                    {
                        InputTextBox.Width = ratio;

                        InputTextBox.Clip = new RectangleGeometry(new Rect(0, 0, ratio, InputTextBox.Height), 5, 5);

#pragma warning disable CS0618 // Type or member is obsolete
                        FormattedText formattedText = new FormattedText(InputTextBox.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Consolas"), 20, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete

                        double max_height = (this.RenderSize.Height - WindowHandle.RenderSize.Height - 25) / 2;

                        double standard_width = InputTextBox.RenderSize.Width;

                        double widths = formattedText.Width / standard_width;

                        double actual_height = (widths * formattedText.Height) + 33;

                        double height = actual_height >= max_height ? max_height : actual_height <= 36 ? 36 : actual_height;

                        InputTextBox.Height = height;
                    }
                }
            }
        }

        private void MessageGrid(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Grid messageGrid = (Grid)sender;
                StackPanel messageStackPanel = ((Border)(((Grid)sender)?.Children[0])).Child as StackPanel;

                if (messageGrid != null)
                {
                    double column_width = messageGrid.RenderSize.Width / 3;

                    if (column_width > 0 && messageStackPanel != null)
                    {
                        messageStackPanel.MaxWidth = column_width * 2;
                    }
                }

                RenderInputTextbox();
                RenderConversationScrollViewerOffset();
            }
            catch { }
        }

        private void MainGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void InputChanged(object sender, TextChangedEventArgs e)
        {
            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void InputTextChanged(object sender, TextCompositionEventArgs e)
        {
            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void InputSelection(object sender, RoutedEventArgs e)
        {
            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void Scrolled(object sender, MouseWheelEventArgs e)
        {
            ConversationScrollViewer.ScrollToVerticalOffset(ConversationScrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void BubbleDataChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = ((TextBox)sender);
            MarkdownTextBlock markdownTextBlock = (MarkdownTextBlock)((StackPanel)textBox.Parent).Children[0];

            if (markdownTextBlock != null)
            {
                if (markdownTextBlock.Visibility == Visibility.Collapsed)
                {
                    string text = textBox.Text;

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        string html = Markdig.Markdown.ToHtml(text);
                        // crude check: if HTML is significantly different from plain text

                        if (html.Contains("<"))
                        {
                            textBox.Visibility = Visibility.Collapsed;
                            markdownTextBlock.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void TextBlockMarkdownRendered(object sender, MarkdownRenderedEventArgs e)
        {
            MarkdownTextBlock markdownTextBlock = ((MarkdownTextBlock)sender);
            TextBox textBox = (TextBox)((StackPanel)markdownTextBlock.Parent).Children[1];

            if (markdownTextBlock != null)
            {
                if (markdownTextBlock.Visibility == Visibility.Collapsed)
                {
                    string text = markdownTextBlock.Text;

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        string html = Markdig.Markdown.ToHtml(text);
                        // crude check: if HTML is significantly different from plain text

                        if (html.Contains("<"))
                        {
                            textBox.Visibility = Visibility.Collapsed;
                            markdownTextBlock.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void ExpandOrContractConversationsMenu(object sender, RoutedEventArgs e)
        {
            isMenuPanelExpanded = !isMenuPanelExpanded;
            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void MenuPanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }

        private void ContentPanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderInputTextbox();
            RenderConversationScrollViewerOffset();
        }
    }
}
