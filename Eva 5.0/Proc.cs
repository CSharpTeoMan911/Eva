using Eva_5._0.Properties;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;

namespace Eva_5._0
{
    internal class Proc : A_p_l____And____P_r_o_c
    {


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


        // INT THAT IS MONITORING THE AMOUNT OF TASKS THAT ARE CURRENTLY RUNNING
        public static int tasks_running;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void ProcInitialisation<Content>(string process_type, string application, Content content)
        {
            // IF THE AMOUNT OF TASKS CURRENTLY RUNNING IS '0'
            if (tasks_running == 0)
            {
                // INCREMENT THE AMOUNT OF TASKS CURRENTLY RUNNING BY '1'
                Interlocked.Increment(ref tasks_running);

                switch (process_type)
                {
                    case "Online Process":
                        OnlineProcesses(application, content as string);
                        break;

                    case "ChatGPT Process":
                        ChatGPT_API_Interface(content as string);
                        break;

                    case "System Process":
                        SystemProcesses(application, content as string);
                        break;

                    case "Timer Process":
                        TimerProcess(content as System.Collections.Concurrent.ConcurrentDictionary<string, int>);
                        break;

                    case "Screen Capture Process":
                        Screen_Capture();
                        break;
                }

                // DECREMENT THE AMOUNT OF TASKS CURRENTLY RUNNING BY '1'
                Interlocked.Decrement(ref tasks_running);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async void OnlineProcesses(string WebApplication, string SearchContent)
        {
            string Process = String.Empty;


            using (System.Diagnostics.Process Online_Process = new System.Diagnostics.Process())
            {
                try
                {
                    await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Searching, SearchContent, WebApplication);

                    commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryGetValue(WebApplication, out Process);

                    StringBuilder Process_Builder = new StringBuilder(Process);
                    Process_Builder.Append(StringFormatting.UrlEncode(new StringBuilder(SearchContent)));
                    string formated_process = Process_Builder.ToString();
                    Process_Builder.Clear();

                    Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                    Online_Process.StartInfo.FileName = formated_process;
                    Online_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    Online_Process.StartInfo.CreateNoWindow = true;
                    Online_Process.StartInfo.UseShellExecute = true;
                    Online_Process.Start();

                    await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
                }
                catch { }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async void SystemProcesses(string Application, string Process)
        {
            string application_executable_name = String.Empty;
            bool Application_Executable_Name_Retrieval_Result = commands.A_p_l_Name__And__A_p_l___E_x__Name.TryGetValue(Application, out application_executable_name);

            string application_process_name = String.Empty;
            bool Application_Process_Name_Retrieval_Result = commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryGetValue(Application, out application_process_name);

            string application_not_found_error_link = String.Empty;
            bool Application_Not_Found_Error_Download_Link_Result = commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryGetValue(Application, out application_not_found_error_link);

            if (Process == "open")
            {
                try
                {
                    if (Application_Executable_Name_Retrieval_Result == true)
                    {
                        string classifier = application_executable_name.Substring(0, 3);
                        string formatted_application_executable_name_string = application_executable_name.Substring(6);

                        await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Opening, null, Application);

                        using (System.Diagnostics.Process Application_Process = new System.Diagnostics.Process())
                        {
                            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                            switch (classifier)
                            {
                                case "URI":
                                    try
                                    {
                                        Application_Process.StartInfo.FileName = formatted_application_executable_name_string;
                                        Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                        Application_Process.StartInfo.UseShellExecute = true;
                                        Application_Process.Start();
                                    }
                                    catch
                                    {
                                        if (Application_Not_Found_Error_Download_Link_Result == true)
                                            MissingApplicationDownload(application_not_found_error_link);
                                    }
                                    break;
                                case "CMD":
                                    try
                                    {

                                        Application_Process.StartInfo.WorkingDirectory = @"C:\Users\" + Environment.UserName + @"\Desktop";
                                        Application_Process.StartInfo.Arguments = "/k \"" + formatted_application_executable_name_string + "\"";
                                        Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                        Application_Process.StartInfo.UseShellExecute = true;
                                        Application_Process.StartInfo.CreateNoWindow = true;
                                        Application_Process.StartInfo.FileName = "cmd";
                                        Application_Process.Start();
                                    }
                                    catch
                                    {
                                        if (Application_Not_Found_Error_Download_Link_Result == true)
                                            MissingApplicationDownload(application_not_found_error_link);
                                    }
                                    break;
                                case "APP":
                                    if (formatted_application_executable_name_string == "recycle bin cleanup")
                                    {
                                        Eva_Functionalities.Recycle_Bine_Cleanup_Implementor.Empty_Recycle_Bin_Implementor();
                                    }
                                    break;
                                case "PRC":
                                    try
                                    {
                                        Application_Process.StartInfo.WorkingDirectory = @"C:\Users\" + Environment.UserName + @"\Desktop";
                                        Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                        Application_Process.StartInfo.FileName = formatted_application_executable_name_string;
                                        Application_Process.StartInfo.UseShellExecute = true;
                                        Application_Process.Start();

                                    }
                                    catch
                                    {
                                        if (Application_Not_Found_Error_Download_Link_Result == true)
                                            MissingApplicationDownload(application_not_found_error_link);
                                    }
                                    break;
                            }

                            await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
                        }
                    }
                }
                catch
                {
                    if (Application_Not_Found_Error_Download_Link_Result == true)
                        MissingApplicationDownload(application_not_found_error_link);
                }
            }
            else
            {
                if (Application_Process_Name_Retrieval_Result == true)
                {
                    try
                    {
                        await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Closing, null, Application);

                        Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                        if (application_process_name != "timer")
                        {
                            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName(application_process_name))
                            {
                                p?.Kill();
                            }
                        }
                        else
                        {
                            Timer_Interval.Cancel_Time_Interval();
                        }

                        await sound_player.Play_Sound(Sound_Player.Sounds.AppTerminationSoundEffect);
                    }
                    catch { }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async void MissingApplicationDownload(string application_not_found_error_link)
        {
            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

            using (System.Diagnostics.Process Application_Not_Found_Download_Link_Process = new System.Diagnostics.Process())
            {
                try
                {
                    Application_Not_Found_Download_Link_Process.StartInfo.FileName = application_not_found_error_link;
                    Application_Not_Found_Download_Link_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    Application_Not_Found_Download_Link_Process.StartInfo.UseShellExecute = true;
                    Application_Not_Found_Download_Link_Process.Start();
                }
                catch { }
            }

            await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async static void TimerProcess(System.Collections.Concurrent.ConcurrentDictionary<string, int> Timer_Time_Intervals)
        {
            try
            {
                await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Setting, null, "a timer");

                int hours_interval = 0;
                Timer_Time_Intervals.TryGetValue("hours", out hours_interval);

                int minutes_interval = 0;
                Timer_Time_Intervals.TryGetValue("minutes", out minutes_interval);

                int seconds_interval = 0;
                Timer_Time_Intervals.TryGetValue("seconds", out seconds_interval);

                Timer_Interval.Set_Time_Interval(hours_interval, minutes_interval, seconds_interval);
                Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);

            }
            catch { }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async void ChatGPT_API_Interface(string input)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (App.ChatGPTResponseWindowOpened == false)
                {
                    App.chatGPT_Response_Window = new ChatGPT_Response_Window();
                    App.chatGPT_Response_Window.Show();
                }
            });

            await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
            App.chatGPT_Response_Window.Update_Conversation(input);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async void Screen_Capture()
        {
            await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Taking, null, "a screenshot");

            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();
            await sound_player.Play_Sound(Sound_Player.Sounds.ScreenshotExecutionSoundEffect);

            await Eva_Functionalities.Screen_Capture_Mechanism_Mitigator.Screen_Capture_Initiator();
        }

        public static void NavigateToLink(string link)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = link;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
            catch { }
        }

        ~Proc()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
