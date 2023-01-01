using System;
using System.Threading.Tasks;

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


    internal class Proc<Content>:A_p_l____And____P_r_o_c
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
         * THIS WAS DONE IN ORDER TO ENSURE A SMOOTH OPERABILITY AND ALSO TO AVOID THE THREAD OVERLOAD OF ANY CORE.
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


        private sealed class Process_Execution_Mitigator:MainWindow
        {
            public static Task<bool> Process_Execution_Mitigator_Task()
            {

                lock (Online_Speech_Recogniser_Listening)
                {
                    Online_Speech_Recogniser_Listening = "false";
                }

                return Task.FromResult(true);
            }

        }


        private sealed class Recycle_Bine_Cleanup_Implementor:Recycle_Bine_Cleanup
        {
            public async static Task<bool> Empty_Recycle_Bin_Implementor()
            {
                return await Empty_Recycle_Bin();
            }
        }


        public static async Task<bool> ProcInitialisation(string process_type, string application, Content content)
        {

            switch (process_type)
            {
                case "Online Process":
                    await OnlineProcesses(application, content as string);
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

            await Process_Execution_Mitigator.Process_Execution_Mitigator_Task();

            return true;
        }


        private static async Task<bool> OnlineProcesses(string WebApplication, string SearchContent)
        {
           
            System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("App execution.wav");


            string Process = String.Empty;


            try
            {
                bool SoundOrOff = await Settings.Get_Settings();

                W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryGetValue(WebApplication, out Process);


                MainWindow.BeginExecutionAnimation = true;
                using (System.Diagnostics.Process Online_Process = new System.Diagnostics.Process())
                {
                    Online_Process.StartInfo.FileName = await Special_Character_Replacement.Remove_Special_Characters_Initiator(Process) + await Special_Character_Replacement.Remove_Special_Characters_Initiator(SearchContent);
                    Online_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    Online_Process.StartInfo.UseShellExecute = true;
                    Online_Process.Start();

                    new Set_Process_As_Foreground(Online_Process.MainWindowHandle);
                }

                try
                {

                    if (SoundOrOff == true)
                    {
                        if (System.IO.File.Exists(@"App execution.wav"))
                        {
                            AppExecutionSoundEffect.Play();
                        }
                    }

                }
                catch { }

            }
            catch { }



            return true;
        }

        private static async Task<bool> SystemProcesses(string Application, string Process)
        {

            System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("App execution.wav");
            System.Media.SoundPlayer AppTerminationSoundEffect = new System.Media.SoundPlayer("App closing.wav");




            bool SoundOrOff = await Settings.Get_Settings();



            string application_executable_name = String.Empty;

            bool Application_Executable_Name_Retrieval_Result = A_p_l____And____P_r_o_c.A_p_l_Name__And__A_p_l___E_x__Name.TryGetValue(Application, out application_executable_name);



            string application_process_name = String.Empty;

            bool Application_Process_Name_Retrieval_Result = A_p_l____And____P_r_o_c.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryGetValue(Application, out application_process_name);



            string application_not_found_error_link = String.Empty;

            bool Application_Not_Found_Error_Download_Link_Result = A_p_l____And____P_r_o_c.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryGetValue(Application, out application_not_found_error_link);




            switch (Process)
            {
                case "open":
                    try
                    {

                        if (Application_Executable_Name_Retrieval_Result == true)
                        {
                            switch (application_executable_name[0].ToString() + application_executable_name[1].ToString() + application_executable_name[2].ToString() == "URI")
                            {
                                case true:
                                    MainWindow.BeginExecutionAnimation = true;

                                    await Windows.System.Launcher.LaunchUriAsync(new Uri(application_executable_name.Remove(0, 6)));
                                    break;



                                case false:
                                    switch(application_executable_name[0].ToString() + application_executable_name[1].ToString() + application_executable_name[2].ToString() == "CMD")
                                    {
                                        case true:
                                            MainWindow.BeginExecutionAnimation = true;

                                            using (System.Diagnostics.Process Application_Process = new System.Diagnostics.Process())
                                            {
                                                Application_Process.StartInfo.FileName = "cmd";
                                                Application_Process.StartInfo.Arguments = "/k " + application_executable_name.Remove(0, 6);
                                                Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                                Application_Process.StartInfo.CreateNoWindow = true;
                                                Application_Process.StartInfo.UseShellExecute = true;
                                                Application_Process.Start();
                                            }
                                            break;



                                        case false:
                                            MainWindow.BeginExecutionAnimation = true;

                                            switch (application_executable_name[0].ToString() + application_executable_name[1].ToString() + application_executable_name[2].ToString() == "APP")
                                            {
                                                case true:

                                                    switch(application_executable_name.Remove(0, 6))
                                                    {
                                                        case "recycle bin cleanup":
                                                            await Recycle_Bine_Cleanup_Implementor.Empty_Recycle_Bin_Implementor();
                                                            break;
                                                    }
                                                    break;



                                                case false:
                                                    using (System.Diagnostics.Process Application_Process = new System.Diagnostics.Process())
                                                    {
                                                        Application_Process.StartInfo.FileName = await Special_Character_Replacement.Remove_Special_Characters_Initiator(application_executable_name);
                                                        Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                                        Application_Process.StartInfo.UseShellExecute = true;
                                                        Application_Process.Start();

                                                        new Set_Process_As_Foreground(Application_Process.MainWindowHandle);
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }


                            try
                            {
                                if (SoundOrOff == true)
                                {
                                    if (System.IO.File.Exists(@"App execution.wav"))
                                    {
                                        AppExecutionSoundEffect.Play();
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    catch
                    {
                        try
                        {
                            if (Application_Not_Found_Error_Download_Link_Result == true)
                            {

                                MainWindow.BeginExecutionAnimation = true;

                                using (System.Diagnostics.Process Application_Not_Found_Downdload_Link_Process = new System.Diagnostics.Process())
                                {
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.FileName = await Special_Character_Replacement.Remove_Special_Characters_Initiator(application_not_found_error_link); ;
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.UseShellExecute = true;
                                    Application_Not_Found_Downdload_Link_Process.Start();

                                    new Set_Process_As_Foreground(Application_Not_Found_Downdload_Link_Process.MainWindowHandle);
                                }


                                try
                                {

                                    if (SoundOrOff == true)
                                    {
                                        if (System.IO.File.Exists(@"App execution.wav"))
                                        {
                                            AppExecutionSoundEffect.Play();
                                        }
                                    }

                                }
                                catch { }

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

                            MainWindow.BeginExecutionAnimation = true;

                            if (application_process_name != "timer")
                            {
                                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName(await Special_Character_Replacement.Remove_Special_Characters_Initiator(application_process_name)))
                                {
                                    p.Kill();
                                }
                            }
                            else
                            {
                                await Timer_Interval.Cancel_Time_Interval();
                            }

                            try
                            {
                                if (SoundOrOff == true)
                                {
                                    if (System.IO.File.Exists(@"App closing.wav"))
                                    {
                                        AppExecutionSoundEffect.Play();
                                    }
                                }
                            }
                            catch { }
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

                bool SoundOrOff = await Settings.Get_Settings();

                System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("App execution.wav");




                int hours_interval = 0;

                Timer_Time_Intervals.TryGetValue("hours", out hours_interval);



                int minutes_interval = 0;

                Timer_Time_Intervals.TryGetValue("minutes", out minutes_interval);



                int seconds_interval = 0;

                Timer_Time_Intervals.TryGetValue("seconds", out seconds_interval);




                await Timer_Interval.Set_Time_Interval(hours_interval, minutes_interval, seconds_interval);

                MainWindow.BeginExecutionAnimation = true;





                try
                {

                    if (SoundOrOff == true)
                    {
                        if (System.IO.File.Exists(@"App execution.wav"))
                        {
                            AppExecutionSoundEffect.Play();
                        }
                    }

                }
                catch { }

            }
            catch { }


           

            return true;
        }


        private static async Task<bool> Screen_Capture()
        {
            bool SoundOrOff = await Settings.Get_Settings();

            System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("Screenshot_Sound_Effect.wav");

            try
            {

                MainWindow.BeginExecutionAnimation = true;

                if (SoundOrOff == true)
                {
                    if (System.IO.File.Exists(@"Screenshot_Sound_Effect.wav"))
                    {
                        AppExecutionSoundEffect.Play();
                    }
                }

            }
            catch
            {

            }

            return await Screen_Capture_Mechanism.Screen_Capture_Initiator();
        }



        ~Proc()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
