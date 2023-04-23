﻿using System;
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


        private sealed class Wake_Word_Engine_Mitigator : Wake_Word_Engine
        {
            public static async Task<bool> Wake_Word_Engine_Stop()
            {
                return await Stop_The_Wake_Word_Engine();
            }
        }


        public App()
        {
            // Construct the class and resources related to applications, processes and web-links
            // related to the Eva functions

            new A_p_l____And____P_r_o_c();
        }



        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);



            // Dispose static resources
            //
            // [ BEGIN ]

            await Proc.Dispose_Sound_Effects();
            await ChatGPT_Response_Window.Dispose_Sound_Effects();
            await Timer_Window.Dispose_Sound_Effects();

            // [ END ]





            // ENSURE THAT ALL SUB-PROCESSES AND PROCESSES RELATED TO THIS APPLICATION ARE TERMINATED
            //
            // [ BEGIN ]

            foreach (System.Diagnostics.Process instance in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
            {
                await Wake_Word_Engine_Mitigator.Wake_Word_Engine_Stop();
                instance.Kill();
            }
            // [ END ]
        }

    }
}
