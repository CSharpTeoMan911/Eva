using Microsoft.VisualBasic.Devices;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 


    /////////////////////////////////////////////////////////////////////////////
    ///                                                                       ///
    ///                   PRODUCT: EVA A.I. ASSISTANT                         ///
    ///                                                                       ///
    ///                   AUTHOR: TEODOR MIHAIL                               ///
    ///                                                                       ///
    ///                                                                       ///
    /// ANY UNAUTHORISED TRADEMARK USE OF THIS SOFTWARE IS PUNISHABLE BY LAW  ///
    ///                                                                       ///
    /// THE AUTHOR OF THIS SOFTWARE DOES NOT LET ANY PEOPLE PATENT OR USE     ///
    /// THIS PRODUCT'S TRADEMARK.                                             ///
    ///                                                                       ///
    /// DO NOT REMOVE THIS FILE HEADER                                        ///
    ///                                                                       ///
    /////////////////////////////////////////////////////////////////////////////


    public partial class App : Application
    {
       
        public static bool SettingsWindowOpen;

        public static bool InstructionManualOpen;

        public static bool PermisissionWindowOpen;

        public static bool TimerWindowOpen;

        public static bool ChatGPTResponseWindowOpened;

        public static bool Application_Error_Shutdown;

        public static ChatGPT_Response_Window chatGPT_Response_Window;

        private static string windows_version = String.Empty;

        private sealed class Wake_Word_Engine_Mitigator : Wake_Word_Engine
        {
            public static void Wake_Word_Engine_Stop()
            {
                Stop_The_Wake_Word_Engine();
            }
        }


        public App()
        {
            Set_Windows_Version();

            // Construct the class and resources related to applications, processes and web-links
            // related to the Eva functions
            new A_p_l____And____P_r_o_c();
        }

        public static string Get_Windows_Version()
        {
            return windows_version;
        }

        private static void Set_Windows_Version()
        {
            string main_version_section = "Windows";

            ComputerInfo computer = new ComputerInfo();

            int main_os_index = computer.OSFullName.IndexOf(main_version_section);

            if (main_os_index != -1)
            {
                StringBuilder os_builder = new StringBuilder(main_version_section);
                os_builder.Append(" ");
                os_builder.Append(computer.OSFullName.ElementAt(main_os_index + main_version_section.Length + 1));
                os_builder.Append(computer.OSFullName.ElementAt(main_os_index + main_version_section.Length + 2));

                if (Convert.ToInt32(os_builder.ToString(os_builder.Length - 2, 2)) >= 10)
                {
                    windows_version = os_builder.ToString();
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("The app requires that the minimum OS version is Windows 10", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                    Environment.Exit(1);
                }
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await ChatGPT_API.Get_Available_Gpt_Models();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);



            // Dispose static resources
            //
            // [ BEGIN ]

            await A_p_l____And____P_r_o_c.sound_player.Dispose_Sound_Effects();

            // [ END ]





            // ENSURE THAT ALL SUB-PROCESSES AND PROCESSES RELATED TO THIS APPLICATION ARE TERMINATED
            //
            // [ BEGIN ]

            Wake_Word_Engine_Mitigator.Wake_Word_Engine_Stop();

            // [ END ]
        }

    }
}
