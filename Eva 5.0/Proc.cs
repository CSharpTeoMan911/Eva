﻿using Eva_5._0.Properties;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eva_5._0
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



        protected static async Task<bool> ProcInitialisation<Content>(string process_type, string application, Content content)
        {
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

            return true;
        }


        private static async Task<bool> OnlineProcesses(string WebApplication, string SearchContent)
        {
            string Process = String.Empty;

            try
            {
                W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryGetValue(WebApplication, out Process);

                StringBuilder Process_Builder = new StringBuilder(Process);
                Process_Builder.Append(SearchContent);
                string formated_process = Process_Builder.ToString();
                Process_Builder.Clear();

                Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                using (System.Diagnostics.Process Online_Process = new System.Diagnostics.Process())
                {
                    Online_Process.StartInfo.FileName = formated_process;
                    Online_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    Online_Process.StartInfo.CreateNoWindow = true;
                    Online_Process.StartInfo.UseShellExecute = true;
                    Online_Process.Start();
                    
                    SetForegroundWindowInitiator(Online_Process.MainWindowHandle);
                }

                await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);

            }
            catch { }



            return true;
        }

        private static async Task<bool> SystemProcesses(string Application, string Process)
        {
            string application_executable_name = String.Empty;

            bool Application_Executable_Name_Retrieval_Result = A_p_l_Name__And__A_p_l___E_x__Name.TryGetValue(Application, out application_executable_name);



            string application_process_name = String.Empty;

            bool Application_Process_Name_Retrieval_Result = A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryGetValue(Application, out application_process_name);



            string application_not_found_error_link = String.Empty;

            bool Application_Not_Found_Error_Download_Link_Result = A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryGetValue(Application, out application_not_found_error_link);



            StringBuilder formated_application_executable_name = new StringBuilder(application_executable_name);
            if(formated_application_executable_name.Length > 6)
            {
                formated_application_executable_name.Remove(0, 6);
            }

            string formated_application_executable_name_string = formated_application_executable_name.ToString();
            formated_application_executable_name.Clear();


            switch (Process)
            {
                case "open":
                    try
                    {

                        if (Application_Executable_Name_Retrieval_Result == true)
                        {
                            switch (application_executable_name[0] == 'U' && application_executable_name[1] == 'R' && application_executable_name[2] == 'I')
                            {
                                case true:
                                    Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                                    using (System.Diagnostics.Process Application_Process = new System.Diagnostics.Process())
                                    {
                                        Application_Process.StartInfo.FileName = formated_application_executable_name_string;
                                        Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                        Application_Process.StartInfo.UseShellExecute = true;
                                        Application_Process.Start();
                                    }
                                    break;



                                case false:
                                    switch (application_executable_name[0] == 'C' && application_executable_name[1] == 'M' && application_executable_name[2] == 'D')
                                    {
                                        case true:
                                            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                                            using (System.Diagnostics.Process Application_Process = new System.Diagnostics.Process())
                                            {
                                                Application_Process.StartInfo.WorkingDirectory = @"C:\Users\" + Environment.UserName + @"\Desktop";
                                                Application_Process.StartInfo.Arguments = "/k " + formated_application_executable_name_string;
                                                Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                                Application_Process.StartInfo.UseShellExecute = true;
                                                Application_Process.StartInfo.CreateNoWindow = true;
                                                Application_Process.StartInfo.FileName = "cmd";
                                                Application_Process.Start();
                                            }
                                            break;



                                        case false:
                                            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                                            switch (application_executable_name[0] == 'A' && application_executable_name[1] == 'P' && application_executable_name[2] == 'P')
                                            {
                                                case true:

                                                    string application_name = formated_application_executable_name_string;

                                                    if (application_name == "recycle bin cleanup")
                                                    {
                                                        await Eva_Functionalities.Recycle_Bine_Cleanup_Implementor.Empty_Recycle_Bin_Implementor();
                                                    }
                                                    break;



                                                case false:
                                                    using (System.Diagnostics.Process Application_Process = new System.Diagnostics.Process())
                                                    {
                                                        Application_Process.StartInfo.WorkingDirectory = @"C:\Users\" + Environment.UserName + @"\Desktop";
                                                        Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                                        Application_Process.StartInfo.FileName = application_executable_name;
                                                        Application_Process.StartInfo.UseShellExecute = true;
                                                        Application_Process.Start();

                                                        SetForegroundWindowInitiator(Application_Process.MainWindowHandle);
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }


                            await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);
                        }
                    }
                    catch
                    {
                        try
                        {
                            if (Application_Not_Found_Error_Download_Link_Result == true)
                            {
                                Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                                using (System.Diagnostics.Process Application_Not_Found_Downdload_Link_Process = new System.Diagnostics.Process())
                                {
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.FileName = application_not_found_error_link;
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.UseShellExecute = true;
                                    Application_Not_Found_Downdload_Link_Process.Start();

                                    SetForegroundWindowInitiator(Application_Not_Found_Downdload_Link_Process.MainWindowHandle);
                                }


                                await sound_player.Play_Sound(Sound_Player.Sounds.AppExecutionSoundEffect);

                            }
                        }
                        catch { }

                    }
                    break;



                case "close":

                    if (Application_Process_Name_Retrieval_Result == true)
                    {
                        try
                        {
                            Eva_Functionalities.Begin_Application_Execution_Animation.Start_The_Application_Execution_Animation();

                            if (application_process_name != "timer")
                            {
                                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName(application_process_name))
                                {
                                    p.Kill();
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



        private async static Task<bool> TimerProcess(System.Collections.Concurrent.ConcurrentDictionary<string, int> Timer_Time_Intervals)
        {
            try
            {
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
