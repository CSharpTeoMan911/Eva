using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Commands_Customisation.xaml
    /// </summary>
    public partial class Commands_Customisation : Window
    {
        private ConcurrentDictionary<string, string> clone = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<TextBox, string> controls_command = new ConcurrentDictionary<TextBox, string>();
        private DateTime timeout;

        private bool file_manipulation_init;

        private SettingsWindow.OpenSpeech openSpeech;

        public enum Option
        {
            OpenApplications,
            CloseApplications,
            SearchContentOnWebApplications
        }

        private Option selected_option;

        public Commands_Customisation()
        {
            InitializeComponent();
        }

        public Commands_Customisation(Option option, SettingsWindow.OpenSpeech openSpeech_)
        {
            timeout = DateTime.Now.AddMilliseconds(-2000);
            selected_option = option;
            InitializeComponent();
            this.openSpeech = openSpeech_;
        }

        private void Move_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadContents();
        }

        private async void LoadContents()
        {
            A_p_l____And____P_r_o_c.commands = await Command_Pallet.Get_Commands();

            Application.Current.Dispatcher.Invoke(() =>
            {
                switch (selected_option)
                {
                    case Option.OpenApplications:
                        Load_Open_Applications(A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___E_x__Name);
                        break;
                    case Option.CloseApplications:
                        Load_Other(A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name);
                        break;
                    case Option.SearchContentOnWebApplications:
                        Load_Other(A_p_l____And____P_r_o_c.commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name);
                        break;
                }
            });
        }

        private void Load_Open_Applications(ConcurrentDictionary<string, string> selected_items)
        {
            int index = 0;

            foreach (string key in selected_items.Keys)
            {
                string command_value = String.Empty;
                selected_items.TryGetValue(key, out command_value);

                clone.TryAdd(key, command_value);

                string command_type = command_value.Substring(0, 3);
                string command_content = command_value.Substring(6);

                if (command_type != "APP")
                {
                    Main_Content.BeginInit();

                    Border item_border = new Border();
                    item_border.BorderBrush = new SolidColorBrush(Colors.Black);
                    item_border.BorderThickness = new Thickness(1);


                    StackPanel current_item = new StackPanel();
                    current_item.Orientation = Orientation.Horizontal;
                    current_item.Width = Main_Content_ScrollViewer.ActualWidth;
                    current_item.Margin = new Thickness(10, 10, 0, 10);


                    TextBlock command_label = new TextBlock();
                    command_label.Text = "Command: ";
                    command_label.Margin = new Thickness(5, 0, 0, 0);

                    TextBox command = new TextBox();
                    command.Name = "TextBox_" + index;
                    command.Text = key;
                    command.Width = 100;
                    controls_command.TryAdd(command, key);

                    TextBlock content_label = new TextBlock();
                    content_label.Text = "Content: ";
                    content_label.Margin = new Thickness(20, 0, 0, 0);

                    TextBox content = new TextBox();
                    content.Text = command_content;
                    content.Width = 100;

                    TextBlock type_label = new TextBlock();
                    type_label.Text = "Type: ";
                    type_label.Margin = new Thickness(20, 0, 0, 0);


                    StackPanel type_selector = new StackPanel();
                    type_selector.Orientation = Orientation.Horizontal;

                    TextBox type_display = new TextBox();
                    type_display.Width = 40;
                    type_display.Text = command_type;
                    type_display.IsReadOnly = true;

                    Button prev_ = new Button();
                    prev_.Click += (object sender, RoutedEventArgs e) => Prev__Click(sender, e, type_display);
                    prev_.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    prev_.Content = "\xE016";

                    Button next_ = new Button();
                    next_.Click += (object sender, RoutedEventArgs e) => Next__Click(sender, e, type_display);
                    next_.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    next_.Content = "\xE017";

                    type_selector.Children.Add(prev_);
                    type_selector.Children.Add(type_display);
                    type_selector.Children.Add(next_);

                    Button reset_command = new Button();
                    reset_command.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    reset_command.Margin = new Thickness(30, 0, 0, 0);
                    reset_command.Content = "\xE149";
                    reset_command.Click += (object sender, RoutedEventArgs e) => Reset_command_Click(sender, e, command, content, type_display);

                    Button update_command = new Button();
                    update_command.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    update_command.Margin = new Thickness(30, 0, 0, 0);
                    update_command.Content = "\xE105";
                    update_command.Click += (object sender, RoutedEventArgs e) => Update_command_Click(sender, e, command, content, type_display);

                    Button remove = new Button();
                    remove.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    remove.Margin = new Thickness(30, 0, 0, 0);
                    remove.Click += (object sender, RoutedEventArgs e) => Remove_Click(sender, e, command, item_border);
                    remove.Content = "\xE107";

                    current_item.Children.Add(command_label);
                    current_item.Children.Add(command);
                    current_item.Children.Add(content_label);
                    current_item.Children.Add(content);
                    current_item.Children.Add(type_label);
                    current_item.Children.Add(type_selector);
                    current_item.Children.Add(reset_command);
                    current_item.Children.Add(update_command);
                    current_item.Children.Add(remove);


                    item_border.Child = current_item;
                    Main_Content.Children.Add(item_border);

                    Main_Content.EndInit();

                    index++;
                }
            }
        }

        private async void Remove_Click(object sender, RoutedEventArgs e, TextBox key, Border current_item)
        {
            if ((DateTime.Now - timeout).TotalMilliseconds >= 1000)
                if (file_manipulation_init == false)
                {
                    file_manipulation_init = true;

                    timeout = DateTime.Now;

                    string previous_command = String.Empty;
                    controls_command.TryGetValue(key, out previous_command);


                    string value = String.Empty;
                    clone.TryRemove(previous_command, out value);

                    foreach (UIElement element in Main_Content.Children)
                        if (((Border)element) == current_item)
                        {
                            Main_Content.BeginInit();
                            Main_Content.Children.Remove(element);
                            Main_Content.EndInit();
                            Main_Content.UpdateLayout();
                            break;
                        }
                    await UpdateCommands();
                    file_manipulation_init = false;
                }
        }

        private void Reset_command_Click(object sender, RoutedEventArgs e, TextBox key, TextBox value, TextBox type)
        {
            string previous_command = String.Empty;
            controls_command.TryGetValue(key, out previous_command);

            string content = String.Empty;
            clone.TryGetValue(previous_command, out content);

            string command_type = content.Substring(0, 3);
            string command_content = content.Substring(6);

            key.Text = previous_command;
            value.Text = command_content;
            type.Text = command_type;
        }

        private void Reset_command_Click_Other(object sender, RoutedEventArgs e, TextBox key, TextBox value)
        {
            string previous_command = String.Empty;
            controls_command.TryGetValue(key, out previous_command);

            string content = String.Empty;
            clone.TryGetValue(previous_command, out content);

            key.Text = previous_command;
            value.Text = content;
        }

        private async void Update_command_Click(object sender, RoutedEventArgs e, TextBox key, TextBox value, TextBox type)
        {
            key.Text = key.Text.ToLower().Trim();
            value.Text = value.Text.Trim();

            if ((DateTime.Now - timeout).TotalMilliseconds >= 1000)
                if (file_manipulation_init == false)
                {
                    file_manipulation_init = true;

                    string previous_value = String.Empty;
                    controls_command.TryGetValue(key, out previous_value);

                    string command_content = String.Empty;
                    clone.TryGetValue(key.Text, out command_content);

                    StringBuilder content_builder = new StringBuilder(type.Text);
                    content_builder.Append(" = ");
                    content_builder.Append(value.Text);

                    if (key.Text == String.Empty)
                    {
                        key.Text = previous_value;
                    }
                    else
                    {
                        if (key.Text == previous_value)
                        {
                            clone.TryUpdate(key.Text, content_builder.ToString(), command_content);
                        }
                        else
                        {
                            if (command_content != String.Empty && command_content != null)
                            {
                                key.Text = previous_value;
                                MessageBox.Show("The key already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                clone.TryRemove(previous_value, out command_content);
                                clone.TryAdd(key.Text, content_builder.ToString());
                                controls_command.TryUpdate(key, key.Text, previous_value);
                            }
                        }
                    }

                    await UpdateCommands();
                    file_manipulation_init = false;
                }
        }

        private async void Update_command_Click_Other(object sender, RoutedEventArgs e, TextBox key, TextBox value)
        {
            key.Text = key.Text.ToLower().Trim();
            value.Text = value.Text.Trim();

            if ((DateTime.Now - timeout).TotalMilliseconds >= 1000)
                if (file_manipulation_init == false)
                {
                    file_manipulation_init = true;

                    string previous_value = String.Empty;
                    controls_command.TryGetValue(key, out previous_value);

                    string command_content = String.Empty;
                    clone.TryGetValue(key.Text, out command_content);

                    if (key.Text == String.Empty)
                    {
                        key.Text = previous_value;
                    }
                    else
                    {
                        if (key.Text == previous_value)
                        {
                            clone.TryUpdate(key.Text, value.Text, command_content);
                        }
                        else
                        {
                            if (command_content != String.Empty && command_content != null)
                            {
                                key.Text = previous_value;
                                MessageBox.Show("The key already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                clone.TryRemove(previous_value, out command_content);
                                clone.TryAdd(key.Text, value.Text);
                                controls_command.TryUpdate(key, key.Text, previous_value);
                            }
                        }
                    }

                    await UpdateCommands();
                    file_manipulation_init = false;
                }
        }


        private void Load_Other(ConcurrentDictionary<string, string> selected_items)
        {
            int index = 0;

            foreach (string key in selected_items.Keys)
            {
                string command_value = String.Empty;
                selected_items.TryGetValue(key, out command_value);

                clone.TryAdd(key, command_value);

                Main_Content.BeginInit();

                Border item_border = new Border();
                item_border.BorderBrush = new SolidColorBrush(Colors.Black);
                item_border.BorderThickness = new Thickness(1);


                StackPanel current_item = new StackPanel();
                current_item.Orientation = Orientation.Horizontal;
                current_item.Width = Main_Content_ScrollViewer.ActualWidth;
                current_item.Margin = new Thickness(10, 10, 0, 10);


                TextBlock command_label = new TextBlock();
                command_label.Text = "Command: ";
                command_label.Margin = new Thickness(5, 0, 0, 0);

                TextBox command = new TextBox();
                command.Name = "TextBox_" + index;
                command.Text = key;
                command.Width = 100;
                controls_command.TryAdd(command, key);

                TextBlock content_label = new TextBlock();
                content_label.Text = "Content: ";
                content_label.Margin = new Thickness(20, 0, 0, 0);

                TextBox content = new TextBox();
                content.Text = command_value;
                content.Width = 100;


                Button reset_command = new Button();
                reset_command.FontFamily = new FontFamily("Segoe MDL2 Assets");
                reset_command.Margin = new Thickness(30, 0, 0, 0);
                reset_command.Content = "\xE149";
                reset_command.Click += (object sender, RoutedEventArgs e) => Reset_command_Click_Other(sender, e, command, content);

                Button update_command = new Button();
                update_command.FontFamily = new FontFamily("Segoe MDL2 Assets");
                update_command.Margin = new Thickness(30, 0, 0, 0);
                update_command.Content = "\xE105";
                update_command.Click += (object sender, RoutedEventArgs e) => Update_command_Click_Other(sender, e, command, content);

                Button remove = new Button();
                remove.FontFamily = new FontFamily("Segoe MDL2 Assets");
                remove.Margin = new Thickness(30, 0, 0, 0);
                remove.Click += (object sender, RoutedEventArgs e) => Remove_Click(sender, e, command, item_border);
                remove.Content = "\xE107";

                current_item.Children.Add(command_label);
                current_item.Children.Add(command);
                current_item.Children.Add(content_label);
                current_item.Children.Add(content);
                current_item.Children.Add(reset_command);
                current_item.Children.Add(update_command);
                current_item.Children.Add(remove);


                item_border.Child = current_item;
                Main_Content.Children.Add(item_border);

                Main_Content.EndInit();

                index++;
            }
        }


        private void Prev__Click(object sender, RoutedEventArgs e, TextBox type_display)
        {
            if (type_display.Text == "CMD")
            {
                type_display.Text = "URI";
            }
            else if (type_display.Text == "PRC")
            {
                type_display.Text = "CMD";
            }
        }

        private void Next__Click(object sender, RoutedEventArgs e, TextBox type_display)
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

        private async Task<bool> UpdateCommands()
        {
            switch (selected_option)
            {
                case Option.OpenApplications:
                    A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___E_x__Name = clone;
                    await Command_Pallet.Set_Commands(A_p_l____And____P_r_o_c.commands);
                    break;
                case Option.CloseApplications:
                    A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name = clone;
                    await Command_Pallet.Set_Commands(A_p_l____And____P_r_o_c.commands);
                    break;
                case Option.SearchContentOnWebApplications:
                    A_p_l____And____P_r_o_c.commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name = clone;
                    await Command_Pallet.Set_Commands(A_p_l____And____P_r_o_c.commands);
                    break;
            }

            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GC.Collect(10);
        }

        private async  void UpdateClone(ConcurrentDictionary<string, string> clone_)
        {
            clone = clone_;
            controls_command.Clear();

            await UpdateCommands();

            Main_Content.Children.Clear();

            LoadContents();
            Main_Content.UpdateLayout();
        }

        private async void Reset_Commands(object sender, RoutedEventArgs e)
        {
            if ((DateTime.Now - timeout).TotalMilliseconds >= 1000)
                if (file_manipulation_init == false)
                {
                    file_manipulation_init = true;

                    await Command_Pallet.Reset_Commands(selected_option);

                    switch (selected_option)
                    {
                        case Option.OpenApplications:
                            clone = A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___E_x__Name;
                            break;
                        case Option.CloseApplications:
                            clone = A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name;
                            break;
                        case Option.SearchContentOnWebApplications:
                            clone = A_p_l____And____P_r_o_c.commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name;
                            break;
                    }
                    timeout = DateTime.Now;
                    await UpdateCommands();

                    Main_Content.Children.Clear();

                    LoadContents();
                    Main_Content.UpdateLayout();

                    file_manipulation_init = false;
                }
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            Command_Addition command_Addition = new Command_Addition(selected_option, clone, new Command_Addition.UpdateCallback(UpdateClone), openSpeech);
            command_Addition.ShowDialog();
        }
    }
}
