using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
       
        public static bool SettingsWindowOpen;

        public static bool InstructionManualOpen;

        public static bool PermisissionWindowOpen;

        public static bool TimerWindowOpen;



        public static bool ErrorAppShutdown;

        public static bool InitiateErrorFunction;

        public static string ErrorFunction;




        public static bool StopRecognitionSession;





        public App()
        {
            // Construct the class and resources related to applications, processes and web-links
            // related to the Eva functions

            new Applications_And_Processes();
        }





        ~App()
        {
            StopRecognitionSession = true;
            ErrorFunction = null;

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
