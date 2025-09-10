using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for Commands_Customisation.xaml
    /// </summary>
    public partial class Commands_Customisation : Window
    {
        private class CommandsContent
        {
            public string command { get; set; }
            public string content { get; set; }
            public string type { get; set; }
        }

        private ConcurrentDictionary<string, string> clone = new ConcurrentDictionary<string, string>();

        private ConcurrentDictionary<Command, CommandsContent> commands_diff = new ConcurrentDictionary<Command, CommandsContent>();
        public ObservableCollection<Command> commands { get; private set; } = new ObservableCollection<Command>();

        private SettingsWindow.OpenSpeech openSpeech;

        private readonly StringBuilder search_filter = new StringBuilder();

        public enum Option
        {
            OpenApplications,
            CloseApplications,
            SearchContentOnWebApplications
        }

        private Option selected_option;

        public Commands_Customisation()
        {
            DataContext = this;
            InitializeComponent();
        }

        public Commands_Customisation(Option option, SettingsWindow.OpenSpeech openSpeech_)
        {
            DataContext = this;
            selected_option = option;
            InitializeComponent();
            this.openSpeech = openSpeech_;
        }

        private void Move_Window(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Application.Current != null)
                {
                    if (Application.Current.Dispatcher != null)
                    {
                        if (!Application.Current.Dispatcher.HasShutdownStarted)
                        {
                            this?.DragMove();
                        }
                    }
                }
            }
            catch { }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadContents();
        }

        private async Task LoadContents()
        {
            AddCommandButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
            SearchButton.IsEnabled = false;
            ClearFilterButton.IsEnabled = false;

            A_p_l____And____P_r_o_c.commands = await Command_Pallet.Get_Commands();

            await Application.Current.Dispatcher.InvokeAsync(() =>
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
            }, System.Windows.Threading.DispatcherPriority.Render);

            AddCommandButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            SearchButton.IsEnabled = true;
            ClearFilterButton.IsEnabled = true;
        }



        private void Load_Open_Applications(ConcurrentDictionary<string, string> selected_items)
        {
            commands.Clear();
            string filter = search_filter.ToString();

            for (int i = 0; i < selected_items.Keys.Count; i++)
            {
                string key = selected_items.Keys.ElementAt(i);
                bool item_valid = !String.IsNullOrEmpty(filter) ? (key.Contains(filter) ? true : false) : true;

                if (item_valid)
                {
                    string command_value = String.Empty;
                    selected_items.TryGetValue(key, out command_value);

                    clone.TryAdd(key, command_value);

                    string command_type = command_value.Substring(0, 3);
                    string command_content = command_value.Substring(6);

                    if (command_type != "APP")
                    {
                        Command.Type type_enum = Command.Type.CMD;
                        switch (command_type)
                        {
                            case "PRC":
                                type_enum = Command.Type.PRC;
                                break;
                            case "CMD":
                                type_enum = Command.Type.CMD;
                                break;
                            case "URI":
                                type_enum = Command.Type.URI;
                                break;
                        }

                        Command command = new Command(key, command_content, type_enum, true);
                        commands_diff.TryAdd(command, new CommandsContent()
                        {
                            command = key,
                            content = command_content,
                            type = command_type
                        });
                        commands.Add(command);
                    }
                }
            }
        }

        private void Load_Other(ConcurrentDictionary<string, string> selected_items)
        {
            commands.Clear();
            string filter = search_filter.ToString();

            for (int i = 0; i < selected_items.Keys.Count; i++)
            {
                string key = selected_items.Keys.ElementAt(i);
                bool item_valid = !String.IsNullOrEmpty(filter) ? (key.Contains(filter) ? true : false) : true;

                if (item_valid)
                {
                    string command_value = String.Empty;
                    selected_items.TryGetValue(key, out command_value);

                    clone.TryAdd(key, command_value);

                    Command command = new Command(key, command_value, Command.Type.NULL, false);
                    commands_diff.TryAdd(command, new CommandsContent()
                    {
                        command = key,
                        content = command_value,
                        type = "NULL"
                    });
                    commands.Add(command);
                }
            }
        }

        private async Task UpdateCommands()
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

            await LoadContents();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clone = null;
            GC.Collect(10);
        }

        private async Task UpdateClone(ConcurrentDictionary<string, string> clone_)
        {
            clone = clone_;

            await UpdateCommands();

            commands.Clear();

            await LoadContents();
            Main_Content.UpdateLayout();
        }

        private async void Reset_Commands(object sender, RoutedEventArgs e)
        {
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

            await UpdateCommands();
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            Command_Addition command_Addition = new Command_Addition(selected_option, clone, new Command_Addition.UpdateCallback(UpdateClone), openSpeech);
            command_Addition.ShowDialog();
        }

        private async void Search(object sender, RoutedEventArgs e)
        {
            search_filter.Clear();
            search_filter.Append(SearchTextBox.Text);
            await LoadContents();
        }

        private void Prev__Click(object sender, RoutedEventArgs e)
        {
            Button button = ((Button)sender);
            StackPanel parent = (StackPanel)button.Parent;
            TextBox type_display = (TextBox)parent.Children[1];

            if (type_display.Text == "CMD")
            {
                type_display.Text = "URI";
            }
            else if (type_display.Text == "PRC")
            {
                type_display.Text = "CMD";
            }
        }

        private void Next__Click(object sender, RoutedEventArgs e)
        {
            Button button = ((Button)sender);
            StackPanel parent = (StackPanel)button.Parent;
            TextBox type_display = (TextBox)parent.Children[1];

            if (type_display.Text == "URI")
            {
                type_display.Text = "CMD";
            }
            else if (type_display.Text == "CMD")
            {
                type_display.Text = "PRC";
            }
        }

        private void Reset_Command(object sender, RoutedEventArgs e)
        {
            Button button = ((Button)sender);
            Command command_instance = (Command)button.Tag;

            commands_diff.TryGetValue(command_instance, out CommandsContent command_diff);

            string previous_command = command_diff.command;
            string command_type = command_diff.type.ToString();
            string command_content = command_diff.content;

            Command selected_instance = commands.Single(element => element.Equals(command_instance));
            selected_instance.command = previous_command;
            selected_instance.content = command_content;

            if (selected_option == Option.OpenApplications)
            {
                switch (command_content)
                {
                    case "PRC":
                        selected_instance.type = Command.Type.PRC;
                        break;
                    case "CMD":
                        selected_instance.type = Command.Type.CMD;
                        break;
                    case "URI":
                        selected_instance.type = Command.Type.URI;
                        break;
                }
            }
        }

        private async void Save_Command(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Command command_instance = (Command)button.Tag;

            commands_diff.TryGetValue(command_instance, out CommandsContent command_diff);

            string command_content = String.Empty;
            clone.TryGetValue(command_instance.command, out command_content);

            StackPanel main_panel = button.Parent as StackPanel;
            TextBox command_box = main_panel.Children[1] as TextBox;
            TextBox content_box = main_panel.Children[3] as TextBox;

            StackPanel type_panel = main_panel.Children[5] as StackPanel;
            TextBox type_box = type_panel.Children[1] as TextBox;

            StringBuilder content_builder = new StringBuilder(type_box.Text);
            content_builder.Append(" = ");
            content_builder.Append(content_box.Text);

            if (command_box.Text == String.Empty)
            {
                command_instance.command = command_diff.command;
            }
            else
            {
                if (!String.IsNullOrEmpty(command_content) && content_box.Text == command_diff.content && type_box.Text == command_diff.type)
                {
                    MessageBox.Show("The key already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    bool changed = commands_diff.TryUpdate(command_instance, new CommandsContent()
                    {
                        command = command_box.Text,
                        content = content_box.Text,
                        type = type_box.Text
                    }, command_diff);

                    clone.TryUpdate(command_box.Text, content_builder.ToString(), command_content);
                }

            }

            await UpdateCommands();
        }

        private async void Delete_Command(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Command command_instance = (Command)button.Tag;

            commands_diff.TryRemove(command_instance, out CommandsContent command_diff);
            clone.TryRemove(command_instance.command, out string command_content);
            commands.Remove(command_instance);

            await UpdateCommands();
        }

        private async void ResetSeachFilter(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            search_filter.Clear();
            await LoadContents();
        }
    }
}
