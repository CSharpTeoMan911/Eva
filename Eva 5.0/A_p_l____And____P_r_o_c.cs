using Eva_5._0.Properties;
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Devices.Display.Core;
using Windows.UI.Input.Inking;

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


    internal class A_p_l____And____P_r_o_c
    {

        // THIS IS THE CLASS RESPONSIBLE FOR STORING THE SYSTEM PROCESSES, WEB APPLICATIONS, AND OS URI LINKS IN KEY-VALUE
        // PAIR FORMAT USIING "ConcurrentDictionary" CLASS OBJECTS, WHICH ARE THREAD SAFE DICTIONARIES ( HASHMAPS ). THIS
        // IS DONE TO ENSURE A O(1) TIME COMPLEXITY FOR SEARCH AND COMPARISON OPERATIONS. 
        //
        //
        //
        //
        //
        // - "A_p_l_Name__And__A_p_l___E_x__Name" OBJECT STORES KEY-VALUE PAIRS OF EXECUTABLES AND OS URIs, AND THEIR 
        //    RESPECTIVE KEY VALUES. THIS OBJECT STORES VALUES THAT ARE RELATED TO PROCESS EXECUTION.
        //
        //
        //    * EXECUTABLES ARE STORED IN THE ( "APPLICATION IDENTIFIER", "EXECUTABLE.exe" ) KEY VALUE FORMAT
        //
        //
        //    * URIs ARE STORED IN THE ( "URI INDENTIFIER", "URI = URI LINK" ) KEY VALUE FORMAT
        //
        //
        //    * SHELL COMMANDS IN THE ( "COMMAND IDENTIFIER", "CMD = COMMAND") KEY VALUE FORMAT
        //
        //
        //    * FUNCTIONS WITHIN CLASSES WITHIN THE EVA APP IN THE ("FUNCTION IDENTIFIER", "FUNCTION IDENTIFIER") KEY VALUE FORMAT
        //
        //
        //
        //
        //
        // - "W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name" OBJECT STORES KEY-VALUE PAIRS OF WEB-APPLICATION LINKS AND THEIR 
        //    RESPECTIVE KEY VALUES. THIS OBJECT STORES VALUES THAT ARE RELATED TO WEB APPLICATION CONTENT SEARCH.
        //
        //
        //    * WEB APPLICATION LINKS ARE STORED IN THE ( "WEB APPLICATION LINK IDENTIFIER", "www.WEB APPLICATION LINK.com" ) KEY VALUE FORMAT
        //
        //
        //
        //
        //
        //
        // - "A_p_l_Name__And__A_p_l___P_r_o_c_Name" OBJECT STORES KEY-VALUE PAIRS OF THE NAMES OF THE PROCESSES' EXECUTABLES . THIS OBJECT
        //    STORES VALUES THAT ARE RELATED TO THE PROCESSES TO BE SHUT DOWN
        //
        //
        //    * PROCESS NAMES ARE STORED IN THE ( "PROCESS NAME IDENTIFIER", "PROCESS NAME" )
        //
        //
        //
        //
        //
        // - "A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k" OBJECT STORES KEY-VALUE PAIRS OF THE LINKS TO APPLICATIONS THAT ARE NOT FOUND.
        //    THIS OBJECT STORES THE VALUES OF THE LINKS THAT MUST BE ACCESSED IN ORDER FOR THE USER TO DOWNLOAD THE APPLICATION THAT IS 
        //    PRESENT IN THE "A_p_l_Name__And__A_p_l___E_x__Name" BUT THEY ARE NOT PRESENT ON THE USER'S OS.
        //
        //    * LINKS THAT MUST BE ACCESSED IN ORDER FOR THE USER TO DOWNLOAD THE APPLICATION ARE STORED IN ( "APPLICATION DOWNLOAD LINK", "www.APPLICATION DOWNLOAD LINK.com" ) KEY VALUE FORMAT

        public static bool CommandTest;

        public static string display_recognition_result = String.Empty;

        public static Command_Pallet_File commands = new Command_Pallet_File();

        public readonly static Sound_Player sound_player = new Sound_Player();

        public static StringFormatting stringFormatting = new StringFormatting();

        protected class Eva_Functionalities
        {
            public class Recycle_Bine_Cleanup_Implementor : Recycle_Bine_Cleanup
            {
                internal static void Empty_Recycle_Bin_Implementor() => Empty_Recycle_Bin();
            }

            public class Begin_Application_Execution_Animation : MainWindow
            {
                internal static void Start_The_Application_Execution_Animation()
                {
                    lock (BeginExecutionAnimation)
                    {
                        BeginExecutionAnimation = "true";
                    }
                }
            }

            public class Screen_Capture_Mechanism_Mitigator : Screen_Capture_Mechanism
            {
                internal static async Task Screen_Capture_Initiator() => await Screen_Capture();
            }


            public class Proc_Mitigator : Proc
            {
                internal static void Process_Initialisation<Content>(string process_type, string application, Content content) => ProcInitialisation<Content>(process_type, application, content);
            }
        }




        public A_p_l____And____P_r_o_c()
        {
            new A_p_l____And____P_r_o_c(true);
        }

        public enum SpeechRecognitionOperation
        {
            FormFilling,
            Dictation,
            WebSearch
        }

        private A_p_l____And____P_r_o_c(bool initiate)
        {
            if(initiate == true)
                InitiateCommands();
        }

        public void InitiateCommands() => Task.Run(async() => { commands = await Command_Pallet.Get_Commands();});
    }
}



