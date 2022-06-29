using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Proc<ProcessType, Application, Content>
    {
        private static System.Threading.Thread ParallelProcessing;


        public static async Task<bool> ProcInitialisation(ProcessType process_type, Application application, Content content)
        {

            switch (process_type as string)
            {
                case "Online Process":
                    await OnlineProcesses(application as string, content as string);
                    break;

                case "System Process":
                    await SystemProcesses(application as string, content as string);
                    break;
            }

            return true;
        }


        private static Task<bool> OnlineProcesses(string WebApplication, string SearchContent)
        {
           
            System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("App execution.wav");


            string Process = String.Empty;


            ParallelProcessing = new System.Threading.Thread(async () =>
            {
                try
                {
                    bool SoundOrOff = await Settings.Get_Settings();

                    switch (WebApplication)
                    {
                        case "youtube":

                            Process = "https://www.youtube.com/results?search_query=";

                            break;

                        case "netflix":

                            Process = "https://www.netflix.com/search?q=";

                            break;

                        case "wikipedia":

                            Process = "https://en.wikipedia.org/wiki/";

                            break;

                        case "google":

                            Process = "https://www.google.com/search?q=";

                            break;

                        case "google news":

                            Process = "https://www.google.com/search?tbm=nws&q=";

                            break;

                        case "ebay":

                            Process = "https://www.ebay.co.uk/sch/i.html?_nkw=";

                            break;


                        case "google images":

                            Process = "https://www.google.com/search?tbm=isch&q=";

                            break;


                        case "amazon":

                            Process = "https://www.amazon.co.uk/s?k=";

                            break;
                    }


                    try
                    {
                        MainWindow.BeginExecutionAnimation = true;
                        using (System.Diagnostics.Process Online_Process = new System.Diagnostics.Process())
                        {
                            Online_Process.StartInfo.FileName = Process + SearchContent;
                            Online_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                            Online_Process.StartInfo.UseShellExecute = true;
                            Online_Process.Start();
                        }

                        try
                        {
                            switch (SoundOrOff == true)
                            {
                                case true:
                                    switch (System.IO.File.Exists(System.IO.Path.GetFullPath("App execution.wav")))
                                    {
                                        case true:
                                            AppExecutionSoundEffect.Play();
                                            break;
                                    }
                                    break;
                            }
                        }
                        catch { }

                        ParallelProcessing.Join();
                        ParallelProcessing.Abort();
                    }
                    catch 
                    {
                       
                    }
                }
                catch
                {

                }

            });
            ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
            ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
            ParallelProcessing.IsBackground = true;
            ParallelProcessing.Start();

            return Task.FromResult(true);
        }

        private static Task<bool> SystemProcesses(string Application, string Process)
        {

            System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("App execution.wav");
            System.Media.SoundPlayer AppTerminationSoundEffect = new System.Media.SoundPlayer("App closing.wav");




            ParallelProcessing = new System.Threading.Thread(async () =>
            {
                bool SoundOrOff = await Settings.Get_Settings();



                string application_executable_name = String.Empty;

                bool Application_Executable_Name_Retrieval_Result = App.Application_Name__And__Application_Executable_Name.TryGetValue(Application, out application_executable_name);



                string application_process_name = String.Empty;

                bool Application_Process_Name_Retrieval_Result = App.Application_Name__And__Application_Process_Name.TryGetValue(Application, out application_process_name);



                string application_not_found_error_link = String.Empty;

                bool Application_Not_Found_Error_Download_Link_Result = App.Application_Name__And__Application_Not_Found_Error_Download_Link.TryGetValue(Application, out application_not_found_error_link);


                

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

                                        await Windows.System.Launcher.LaunchUriAsync(new Uri(application_executable_name.Remove(0, 6)));
                                        break;

                                    case false:

                                        MainWindow.BeginExecutionAnimation = true;

                                        using (System.Diagnostics.Process Application_Process = new System.Diagnostics.Process())
                                        {
                                            Application_Process.StartInfo.FileName = application_executable_name;
                                            Application_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Application_Process.StartInfo.UseShellExecute = true;
                                            Application_Process.Start();
                                        }
                                        break;
                                }


                                try
                                {
                                    switch (SoundOrOff == true)
                                    {
                                        case true:
                                            switch (System.IO.File.Exists(@"App execution.wav"))
                                            {
                                                case true:
                                                    AppExecutionSoundEffect.Play();
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                catch { }
                            }
                        }
                        catch
                        {

                            if (Application_Not_Found_Error_Download_Link_Result == true)
                            {

                                MainWindow.BeginExecutionAnimation = true;

                                using (System.Diagnostics.Process Application_Not_Found_Downdload_Link_Process = new System.Diagnostics.Process())
                                {
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.FileName = application_not_found_error_link;
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Application_Not_Found_Downdload_Link_Process.StartInfo.UseShellExecute = true;
                                    Application_Not_Found_Downdload_Link_Process.Start();
                                }

                                switch (SoundOrOff == true)
                                {
                                    case true:
                                        switch (System.IO.File.Exists(@"App execution.wav"))
                                        {
                                            case true:
                                                AppExecutionSoundEffect.Play();
                                                break;
                                        }
                                        break;
                                }
                            }
                        }
                        ParallelProcessing.Join();
                        ParallelProcessing.Abort();


                        break;










                    case "close":



                        if (Application_Process_Name_Retrieval_Result == true)
                        {
                            try
                            {
                                MainWindow.BeginExecutionAnimation = true;

                                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName(application_process_name))
                                {
                                    p.Kill();
                                }

                                try
                                {
                                    switch (SoundOrOff == true)
                                    {
                                        case true:
                                            switch (System.IO.File.Exists(@"App closing.wav"))
                                            {
                                                case true:
                                                    AppTerminationSoundEffect.Play();
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                catch { }
                            }
                            catch { }
                        }
                        break;
                }

            });

            ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
            ParallelProcessing.IsBackground = true;
            ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
            ParallelProcessing.Start();



            return Task.FromResult(true);
        }



        ~Proc()
        {
            ParallelProcessing = null;

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
