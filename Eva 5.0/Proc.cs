using Eva_5._0.Properties;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.WebUI;

namespace Eva_5._0
{
    internal class Proc:A_p_l____And____P_r_o_c
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


        /*
         * 
         * 
         * 
         * 
         * ALL THE PROCESSES (ONLINE OR SYSTEM RELATED) ARE MULTITHREADED WITHIN AN ASYNCHRONOUS CALL STACK.
         * 
         * 
         * 
         * THIS WAS DONE IN ORDER TO ENSURE A SMOOTH OPERABILITY AND ALSO TO AVOID THE THREAD OVERLOAD OF ANY CPU CORE.
         * 
         * 
         * 
         * THE ASYNCHRONOUS CALL STACK OPERATES WITHIN THE APPLICATION'S DEFAULT THREAD POOL, WHICH ARE THREADS
         * THAT DO NOT HAVE ANY PRESETTED VALUES OR PROPRIETIES, OCUPPY AN EQUAL AMMOUNT OF RESOURCES AND WHEN 
         * ALL ARE FULL THE REMAINING TASKS WILL WAIT FOR A BACKGROUND THREAD FROM THE THREADPOOL TO FREE.
         * 
         * 
         * THE SOUND EFFECTS OF THE APPLICATION'S RELATED SOUND EFFECTS FOR PROCESS EXECUTION AND/OR TERMINATION
         * AND APPLICATION'S OPERATING SYSTEM SPECIFIC ERRORS ARE INCLUDED IN THE APPLICATION'S SOLUTION. PLEASE 
         * COPY THEM IN THE APPLICATION'S DESIRED BIN FOLDER
         * 
         * 
         * 
         * 
         */

        // INT THAT IS MONITORING THE AMOUNT OF TASKS THAT ARE CURRENTLY RUNNING
        public static int tasks_running;

        protected static async Task<bool> ProcInitialisation<Content>(string process_type, string application, Content content)
        {
            // IF THE AMOUNT OF TASKS CURRENTLY RUNNING IS '0'
            if (tasks_running == 0)
            {
                // INCREMENT THE AMOUNT OF TASKS CURRENTLY RUNNING BY '1'
                Interlocked.Increment(ref tasks_running);

                switch (process_type)
                {
                    case "Online Process":
                        await OnlineProcesses(application, content as string);
                        break;

                    case "ChatGPT Process":
                        await ChatGPT_API_Interface(content as string);
                        break;

                    case "System Process":
                        await SystemProcesses(application, content as string);
                        break;

                    case "Timer Process":
                        await TimerProcess(content as System.Collections.Concurrent.ConcurrentDictionary<string, int>);
                        break;

                    case "Screen Capture Process":
                        await Screen_Capture();
                        break;
                }

                // DECREMENT THE AMOUNT OF TASKS CURRENTLY RUNNING BY '1'
                Interlocked.Decrement(ref tasks_running);
            }

            return true;
        }


        private static async Task<bool> OnlineProcesses(string WebApplication, string SearchContent)
        {
            string Process = String.Empty;

            try
            {
                _= await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Searching, SearchContent, WebApplication);

                commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryGetValue(WebApplication, out Process);

                StringBuilder Process_Builder = new StringBuilder(Process);
                Process_Builder.Append(await stringFormatting.UrlEncode(new StringBuilder(SearchContent)));
                string formated_process = Process_Builder.ToString();
                Process_Builder.Clear();

                Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                System.Diagnostics.Process Online_Process = new System.Diagnostics.Process();
                try
                {
                    Online_Process.StartInfo.FileName = formated_process;
                    Online_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    Online_Process.StartInfo.CreateNoWindow = true;
                    Online_Process.StartInfo.UseShellExecute = true;
                    Online_Process.Start();
                }
                catch
                {

                }
                finally
                {
                    Online_Process?.Dispose();
                }

                await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);

            }
            catch { }



            return true;
        }

        private static async Task<bool> SystemProcesses(string Application, string Process)
        {
            string application_executable_name = String.Empty;
            bool Application_Executable_Name_Retrieval_Result = commands.A_p_l_Name__And__A_p_l___E_x__Name.TryGetValue(Application, out application_executable_name);

            string application_process_name = String.Empty;
            bool Application_Process_Name_Retrieval_Result = commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryGetValue(Application, out application_process_name);

            string application_not_found_error_link = String.Empty;
            bool Application_Not_Found_Error_Download_Link_Result = commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryGetValue(Application, out application_not_found_error_link);

            switch (Process)
            {
                case "open":
                    try
                    {
                        if (Application_Executable_Name_Retrieval_Result == true)
                        {
                            string classifier = application_executable_name.Substring(0, 3);
                            string formatted_application_executable_name_string = application_executable_name.Substring(6);

                            await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Opening, null, Application);

                            System.Diagnostics.Process Application_Process = null;

                            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                            switch (classifier)
                            {
                                case "URI":
                                    Application_Process = new System.Diagnostics.Process();
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
                                    finally
                                    {
                                        Application_Process?.Dispose();
                                    }
                                    break;
                                case "CMD":
                                    Application_Process = new System.Diagnostics.Process();
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
                                    finally
                                    {
                                        Application_Process?.Dispose();
                                    }
                                    break;
                                case "APP":
                                    if (formatted_application_executable_name_string == "recycle bin cleanup")
                                    {
                                        await Eva_Functionalities.Recycle_Bine_Cleanup_Implementor.Empty_Recycle_Bin_Implementor();
                                    }
                                    break;
                                case "PRC":
                                    Application_Process = new System.Diagnostics.Process();
                                    try
                                    {
                                        Application_Process.StartInfo.WorkingDirectory = @"C:\Users\" + Environment.UserName + @"\Desktop";
                                        Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                        Application_Process.StartInfo.FileName = formatted_application_executable_name_string;
                                        Application_Process.StartInfo.UseShellExecute = true;
                                        Application_Process.Start();

                                    }
                                    catch(Exception E)
                                    {
                                        if (Application_Not_Found_Error_Download_Link_Result == true)
                                            MissingApplicationDownload(application_not_found_error_link);
                                    }
                                    finally
                                    {
                                        Application_Process?.Dispose();
                                    }
                                    break;
                            }

                            await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
                        }
                    }
                    catch
                    {
                        if (Application_Not_Found_Error_Download_Link_Result == true)
                            MissingApplicationDownload(application_not_found_error_link);
                    }
                    break;



                case "close":

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
                                await Timer_Interval.Cancel_Time_Interval();
                            }

                            await sound_player.Play_Sound(Sound_Player.Sounds.AppTerminationSoundEffect);
                        }
                        catch { }
                    }
                    break;
            }

            return true;
        }


        private static async void MissingApplicationDownload(string application_not_found_error_link)
        {
            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

            System.Diagnostics.Process Application_Not_Found_Download_Link_Process = new System.Diagnostics.Process();
            try
            {
                Application_Not_Found_Download_Link_Process.StartInfo.FileName = application_not_found_error_link;
                Application_Not_Found_Download_Link_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                Application_Not_Found_Download_Link_Process.StartInfo.UseShellExecute = true;
                Application_Not_Found_Download_Link_Process.Start();

            }
            catch
            {

            }
            finally
            {
                Application_Not_Found_Download_Link_Process?.Dispose();
            }

            await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
        }


        private async static Task<bool> TimerProcess(System.Collections.Concurrent.ConcurrentDictionary<string, int> Timer_Time_Intervals)
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




                await Timer_Interval.Set_Time_Interval(hours_interval, minutes_interval, seconds_interval);

                Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);

            }
            catch { }


           

            return true;
        }


        private static Task<bool> ChatGPT_API_Interface(string input)
        {
            
            Application.Current.Dispatcher.Invoke(async() =>
            {
                if (App.ChatGPTResponseWindowOpened == false)
                {
                    App.chatGPT_Response_Window = new ChatGPT_Response_Window();
                    App.chatGPT_Response_Window.Show();
                }
                await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
                await App.chatGPT_Response_Window.Update_Conversation(input);
            });
            

            return Task.FromResult(true);
        }


        private static async Task<bool> Screen_Capture()
        {
            await SpeechSynthesis.Synthesis(SpeechSynthesis.Action.Taking, null, "a screenshot");

            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();
            await sound_player.Play_Sound(Sound_Player.Sounds.ScreenshotExecutionSoundEffect);
            return await Eva_Functionalities.Screen_Capture_Mechanism_Mitigator.Screen_Capture_Initiator();
        }

        ~Proc()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
