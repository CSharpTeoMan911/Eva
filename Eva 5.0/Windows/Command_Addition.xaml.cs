using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for Command_Addition.xaml
    /// </summary>
    public partial class Command_Addition
    {

        private readonly Commands_Customisation.Option selected_option;
        private ConcurrentDictionary<string, string> clone = new ConcurrentDictionary<string, string>();
        public delegate Task UpdateCallback(ConcurrentDictionary<string, string> clone);
        private UpdateCallback callback;
        private bool WindowIsClosing;
        public ObservableCollection<CommandCharacteristics> command_format { get; private set; } = new ObservableCollection<CommandCharacteristics>();

        private SettingsWindow.OpenSpeech openSpeech;

        public Command_Addition()
        {
            InitializeComponent();
        }

        public Command_Addition(Commands_Customisation.Option option, ConcurrentDictionary<string, string> clone_, UpdateCallback callback_, SettingsWindow.OpenSpeech openSpeech_)
        {
            selected_option = option;
            clone = clone_;
            callback = callback_;
            this.openSpeech = openSpeech_;

            this.DataContext = this;
            command_format.Add(new CommandCharacteristics(option == Commands_Customisation.Option.OpenApplications));
            InitializeComponent();
        }


        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            A_p_l____And____P_r_o_c.CommandTest = true;
            A_p_l____And____P_r_o_c.display_recognition_result = String.Empty;
            openSpeech.Invoke();

            System.Timers.Timer recognition_checkup = new System.Timers.Timer();
            recognition_checkup.Elapsed += Recognition_checkup_Elapsed;
            recognition_checkup.Interval = 100;
            recognition_checkup.Start();
        }

        private void Recognition_checkup_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer)sender;

            if (App.Current != null)
            {
                if (Application.Current != null)
                {
                    if (Application.Current.Dispatcher != null)
                    {
                        if (Application.Current.Dispatcher.HasShutdownStarted == false)
                        {
                            if (WindowIsClosing == false)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    if (A_p_l____And____P_r_o_c.display_recognition_result != String.Empty)
                                    {
                                        Eva_Command.Text = A_p_l____And____P_r_o_c.display_recognition_result;
                                        A_p_l____And____P_r_o_c.display_recognition_result = String.Empty;
                                    }
                                });
                            }
                            else
                            {
                                timer?.Close();
                            }
                        }
                        else
                        {
                            timer?.Close();
                        }
                    }
                    else
                    {
                        timer?.Close();
                    }
                }
                else
                {
                    timer?.Close();
                }
            }
            else
            {
                timer?.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            A_p_l____And____P_r_o_c.CommandTest = false;
            WindowIsClosing = true;
            GC.Collect(10);
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

        private void CloseWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void PreviousType(object sender, RoutedEventArgs e)
        {
            CommandCharacteristics command = command_format.ElementAt(0);

            if (command.type == CommandCharacteristics.Type.URI)
            {
                command.type = CommandCharacteristics.Type.CMD;
            }
            else if (command.type == CommandCharacteristics.Type.CMD)
            {
                command.type = CommandCharacteristics.Type.PRC;
            }
        }

        private void NextType(object sender, RoutedEventArgs e)
        {
            CommandCharacteristics command = command_format.ElementAt(0);

            if (command.type == CommandCharacteristics.Type.PRC)
            {
                command.type = CommandCharacteristics.Type.CMD;
            }
            else if (command.type == CommandCharacteristics.Type.CMD)
            {
                command.type = CommandCharacteristics.Type.URI;
            }
        }

        private async void AddCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                CommandCharacteristics command = command_format.ElementAt(0);
                clone.TryGetValue(command.command, out string command_content);

                if (command_content != String.Empty && command_content != null)
                {
                    MessageBox.Show("The key already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    StringBuilder command_builder = new StringBuilder();

                    if (selected_option == Commands_Customisation.Option.OpenApplications)
                    {
                        command_builder.Append(command.type.ToString())
                                       .Append(" = ")
                                       .Append(command.content);
                    }
                    else
                    {
                        command_builder.Append(command.content);
                    }

                    clone.TryAdd(command.command, command_builder.ToString());
                    await callback.Invoke(clone);
                    this.Close();
                }
            }
            catch { }
        }
    }
}
