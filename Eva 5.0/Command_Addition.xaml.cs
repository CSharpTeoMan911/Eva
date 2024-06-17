using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for Command_Addition.xaml
    /// </summary>
    public partial class Command_Addition : Window
    {

        private Commands_Customisation.Option selected_option;
        private ConcurrentDictionary<string, string> clone = new ConcurrentDictionary<string, string>();
        public delegate void UpdateCallback(ConcurrentDictionary<string, string> clone);
        private UpdateCallback callback;


        public Command_Addition()
        {
            InitializeComponent();
        }

        public Command_Addition(Commands_Customisation.Option option, ConcurrentDictionary<string, string> clone_, UpdateCallback callback_)
        {
            selected_option = option;
            clone = clone_;
            callback = callback_;
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Contents();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Load_Contents()
        {
            switch (selected_option)
            {
                case Commands_Customisation.Option.OpenApplications:
                    Render();
                    break;
                case Commands_Customisation.Option.CloseApplications:
                    OtherRender();
                    break;
                case Commands_Customisation.Option.SearchContentOnWebApplications:
                    OtherRender();
                    break;
            }
        }

        private void Render()
        {
            Main_Content.BeginInit();

            StackPanel current_item = new StackPanel();
            current_item.Orientation = System.Windows.Controls.Orientation.Horizontal;
            current_item.Width = Main_Content.ActualWidth;
            current_item.Margin = new Thickness(10, 20, 0, 10);


            TextBlock command_label = new TextBlock();
            command_label.Text = "Command: ";
            command_label.Margin = new Thickness(5, 0, 0, 0);
            command_label.FontSize = 16;

            System.Windows.Controls.TextBox command = new System.Windows.Controls.TextBox();
            command.Width = 120;

            TextBlock content_label = new TextBlock();
            content_label.Text = "Content: ";
            content_label.Margin = new Thickness(20, 0, 0, 0);
            content_label.FontSize = 16;

            System.Windows.Controls.TextBox content = new System.Windows.Controls.TextBox();
            content.Width = 120;

            TextBlock type_label = new TextBlock();
            type_label.Text = "Type: ";
            type_label.Margin = new Thickness(20, 0, 0, 0);
            type_label.FontSize = 16;


            StackPanel type_selector = new StackPanel();
            type_selector.Orientation = System.Windows.Controls.Orientation.Horizontal;

            System.Windows.Controls.TextBox type_display = new System.Windows.Controls.TextBox();
            type_display.Width = 45;
            type_display.IsReadOnly = true;
            type_display.Text = "PRC";
            type_display.FontSize = 16;

            System.Windows.Controls.Button prev_ = new System.Windows.Controls.Button();
            prev_.Click += (object sender, RoutedEventArgs e) => Prev__Click (sender, e, type_display);
            prev_.FontFamily = new FontFamily("Segoe MDL2 Assets");
            prev_.Content = "\xE016";
            prev_.FontSize = 16;

            System.Windows.Controls.Button next_ = new System.Windows.Controls.Button();
            next_.Click += (object sender, RoutedEventArgs e) => Next__Click(sender, e, type_display);
            next_.FontFamily = new FontFamily("Segoe MDL2 Assets");
            next_.Content = "\xE017";
            next_.FontSize = 16;

            type_selector.Children.Add(prev_);
            type_selector.Children.Add(type_display);
            type_selector.Children.Add(next_);


            System.Windows.Controls.Button update_command = new System.Windows.Controls.Button();
            update_command.FontFamily = new FontFamily("Segoe MDL2 Assets");
            update_command.Margin = new Thickness(30, 0, 0, 0);
            update_command.Content = "\xE105";
            update_command.Click += (object sender, RoutedEventArgs e) => Update_command_Click(sender, e, command, content, type_display);
            update_command.FontSize = 16;

            current_item.Children.Add(command_label);
            current_item.Children.Add(command);
            current_item.Children.Add(content_label);
            current_item.Children.Add(content);
            current_item.Children.Add(type_label);
            current_item.Children.Add(type_selector);
            current_item.Children.Add(update_command);
            Main_Content.Children.Add(current_item);

            Main_Content.EndInit();
        }

        private void OtherRender()
        {
            Main_Content.BeginInit();

            StackPanel current_item = new StackPanel();
            current_item.Orientation = System.Windows.Controls.Orientation.Horizontal;
            current_item.Width = Main_Content.ActualWidth;
            current_item.Margin = new Thickness(70, 20, 0, 10);


            TextBlock command_label = new TextBlock();
            command_label.Text = "Command: ";
            command_label.Margin = new Thickness(5, 0, 0, 0);
            command_label.FontSize = 16;

            System.Windows.Controls.TextBox command = new System.Windows.Controls.TextBox();
            command.Width = 120;

            TextBlock content_label = new TextBlock();
            content_label.Text = "Content: ";
            content_label.Margin = new Thickness(20, 0, 0, 0);
            content_label.FontSize = 16;

            System.Windows.Controls.TextBox content = new System.Windows.Controls.TextBox();
            content.Width = 120;


            System.Windows.Controls.Button update_command = new System.Windows.Controls.Button();
            update_command.FontFamily = new FontFamily("Segoe MDL2 Assets");
            update_command.Margin = new Thickness(30, 0, 0, 0);
            update_command.Content = "\xE105";
            update_command.Click += (object sender, RoutedEventArgs e) => Update_command_Click_Other(sender, e, command, content);
            update_command.FontSize = 16;

            current_item.Children.Add(command_label);
            current_item.Children.Add(command);
            current_item.Children.Add(content_label);
            current_item.Children.Add(content);
            current_item.Children.Add(update_command);
            Main_Content.Children.Add(current_item);

            Main_Content.EndInit();
        }

        private void Update_command_Click(object sender, RoutedEventArgs e, System.Windows.Controls.TextBox key, System.Windows.Controls.TextBox value, System.Windows.Controls.TextBox type)
        {
            key.Text = key.Text.ToLower().Trim();
            value.Text = value.Text.Trim();

            string command_content = String.Empty;
            clone.TryGetValue(key.Text, out command_content);

            if (command_content != String.Empty && command_content != null)
            {
                System.Windows.MessageBox.Show("The key already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                StringBuilder content_builder = new StringBuilder(type.Text);
                content_builder.Append(" = ");
                content_builder.Append(value.Text);

                clone.TryAdd(key.Text, content_builder.ToString());
                callback.Invoke(clone);
                this.Close();
            }
        }

        private void Update_command_Click_Other(object sender, RoutedEventArgs e, System.Windows.Controls.TextBox key, System.Windows.Controls.TextBox value)
        {
            key.Text = key.Text.ToLower().Trim();
            value.Text = value.Text.Trim();

            string command_content = String.Empty;
            clone.TryGetValue(key.Text, out command_content);

            if (command_content != String.Empty && command_content != null)
            {
                System.Windows.MessageBox.Show("The key already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                clone.TryAdd(key.Text, value.Text);
                callback.Invoke(clone);
                this.Close();
            }
        }


        private void Prev__Click(object sender, RoutedEventArgs e, System.Windows.Controls.TextBox type_display)
        {
            if (type_display.Text == "URI")
            {
                type_display.Text = "CMD";
            }
            else if (type_display.Text == "CMD")
            {
                type_display.Text = "PRC";
            }
        }

        private void Next__Click(object sender, RoutedEventArgs e, System.Windows.Controls.TextBox type_display)
        {
            if (type_display.Text == "PRC")
            {
                type_display.Text = "CMD";
            }
            else if (type_display.Text == "CMD")
            {
                type_display.Text = "URI";
            }
        }


        private void Move_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
