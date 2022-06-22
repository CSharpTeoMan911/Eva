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
    public partial class App : Application
    {

        public static bool SettingsWindowOpen;

        public static bool InstructionManualOpen;

        public static bool PermisissionWindowOpen;

        public static bool ErrorAppShutdown;

        public static bool InitiateErrorFunction;

        public static string ErrorFunction;

        public static bool StopRecognitionSession;


        ~App()
        { }
    }
}
