using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.AllJoyn;
using Windows.Graphics.Printing;
using static Eva_5._0.Commands_Customisation;

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


    internal class Settings
    {
        private static readonly string settings_file_name = "application_settings.json";

        public enum SpeechLanguage
        {
            en_US,
            en_GB
        }



        // THIS METHOD ENSURES THAT THE PERMISSIONS TO THE SETTINGS FILE FOR THE CURRENT
        // USER INCLUDE READ, WRITE, AND DELETE FUNCTIONALITIES TO THE SETTINGS FILE.
        // THE SETTINGS FILE MUST HAVE READ AND WRITE PERMISSIONS IN ORDER FOR THE 
        // APPLICATION TO ACCESS THE CHATGPT API KEY AND THE SET VOLUME SETTINGS,
        // OTHERWISE THE APPLICATION CAN CRASH.
        private static void Ensure_Access_To_The_Settings_File()
        {
            string full_path = new StringBuilder(Environment.CurrentDirectory).Append("\\").Append(settings_file_name).ToString();

            if(System.IO.File.Exists(full_path) == true)
            {
                System.Security.AccessControl.FileSecurity settings_file_security = System.IO.File.GetAccessControl(settings_file_name);

                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Write, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Read, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Delete, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.WriteData, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.ReadData, System.Security.AccessControl.AccessControlType.Allow));

                System.IO.File.SetAccessControl(full_path, settings_file_security);
            }
        }


        public static async Task<string> GetSettingsFilePath()
        {
            if (! System.IO.File.Exists(settings_file_name) == true)
                await Create_Settings_File(new Settings_File());
            return new StringBuilder("\"").Append(Environment.CurrentDirectory).Append("\\").Append(settings_file_name).Append("\"").ToString();
        }



        
        private static async Task<Settings_File> Load_Settings_File()
        {
            // THE REQUIRED FILE ACCESS METHODS FOR THE SETTINGS FILE
            // ARE ENSURED BEFORE ANY READ OPERATION IS PERFORMED
            // ON THE SETTINGS FILE TO AVOID FATAL ERRORS AND ALSO 
            // BE ABLE TO READ THE VALUES
            //
            // [ BEGIN ]

            Ensure_Access_To_The_Settings_File();

            // [ END ]






            // A "Settings_File" CLASS OBJECT IS CREATED WITH SOME DEFAULT VALUES.
            // IF THE SETTINGS FILE DOES NOT EXIST, A SETTINGS FILE WILL BE CRATED
            // WITH THESE DEFAULT VALUES.
            //
            // [ BEGIN ]

            Settings_File settings_File = new Settings_File();

            // [ END ]


            try
            {

                if (System.IO.File.Exists(settings_file_name) == true)
                {
                    // IF THE SETTINGS FILE EXISTS, A "FileStream" OBJECT IS CRATED
                    // IN ORDER TO OPEN THE FILE IN A BINARY DATA STREAM AND READ
                    // THE FILE'S BINARY CONTENTS
                    //
                    // [ BEGIN ]


                    System.IO.FileStream settings_file_stream = System.IO.File.OpenRead(settings_file_name);

                    try
                    {
                        // THE BINARY INFORMATION OF THE SETTINGS FILE IS READ IN A BUFFER
                        // AND THE BUFFER IS THEN CONVERTED IN A STRING IN ORDER TO BE 
                        // DE-SERIALIZED FROM THE JSON FILE FORMAT IN THE "Settings_File"
                        // FORMAT. 
                        //
                        // [ BEGIN ]

                        byte[] settings_file_binary = new byte[settings_file_stream.Length];
                        await settings_file_stream.ReadAsync(settings_file_binary, 0, settings_file_binary.Length);

                        settings_File = await JsonSerialisation.JsonDeserialiser<Settings_File>(Encoding.UTF8.GetString(settings_file_binary));

                        // [ END ]
                    }
                    catch
                    {

                    }
                    finally
                    {
                        settings_file_stream?.Close();
                        settings_file_stream?.Dispose();
                    }

                    // [ END ]
                }
                else
                {
                    // IF THE SETTINGS FILE DOES NOT EXIST, PASS THE 
                    // "Settings_File" CLASS INSTANCE TO THIS 
                    // METHOD AND CREATE A SETTINGS FILE
                    // USING THE PRESET DEFAULT VALUES 
                    // OF THIS CLASS INSTANCE
                    //
                    // [ BEGIN ]

                    await Create_Settings_File(settings_File);

                    // [ END ]
                }
            }
            catch { }

            return settings_File;
        }



        private static async Task<bool> Update_Settings_File(Settings_File settings_File)
        {
            // THE REQUIRED FILE ACCESS METHODS FOR THE SETTINGS FILE
            // ARE ENSURED BEFORE ANY DELETE OPERATION IS
            // PERFORMED ON THE SETTINGS FILE TO AVOID FATAL
            // ERRORS AND ALSO BE ABLE TO READ THE VALUES
            //
            // [ BEGIN ]

            Ensure_Access_To_The_Settings_File();

            // [ END ]

            try
            {
                // IF THE SETTINGS FILE EXIST, DELETE IT AND CREATE A
                // NEW SETTINGS FILE WITH THE SET VALUES
                //
                // [ BEGIN ]

                if (System.IO.File.Exists(settings_file_name) == true)
                {
                    System.IO.File.Delete(settings_file_name);
                }

                // [ END ]

            }
            catch { }
            return await Create_Settings_File(settings_File);
        }



        private static async Task<bool> Create_Settings_File(Settings_File settings_File)
        {
            // SERIALIZE THE "Settings_File" OBJECT IN A JSON FILE FORMAT
            // AND CONVERT THE SERIALIZED INFORMATION IN A BINARY FORMAT.
            // WRITE THE BINARY INFORMATION USING A "FileStream" OBJECT
            // BY CREATING A SETTINGS FILE AND WRITING INTO IT USING 
            // THE STREAM.
            //
            // [ BEGIN ]

            try
            {
                byte[] settings_file_binary = Encoding.UTF8.GetBytes(await JsonSerialisation.JsonSerialiser(settings_File));

                System.IO.FileStream settings_file_stream = System.IO.File.Create(settings_file_name, settings_file_binary.Length, System.IO.FileOptions.Asynchronous);

                try
                {
                    await settings_file_stream.WriteAsync(settings_file_binary, 0, settings_file_binary.Length);
                    await settings_file_stream.FlushAsync();
                }
                catch
                {

                }
                finally
                {
                    settings_file_stream?.Close();
                    settings_file_stream?.Dispose();
                }
            }
            catch { }

            // [ END ]







            // THE REQUIRED FILE ACCESS METHODS FOR THE SETTINGS FILE
            // ARE ENSURED BEFORE ANY WRITE OPERATION IS
            // PERFORMED ON THE SETTINGS FILE TO AVOID FATAL
            // ERRORS AND ALSO BE ABLE TO READ THE VALUES
            //
            // [ BEGIN ]

            Ensure_Access_To_The_Settings_File();

            // [ END ]



            return true;
        }







        // GETTER AND SETTER METHODS FOR THE APPLICATION'S SETTINGS FILE
        //
        // [ BEGIN ]

        public static async Task<float> Get_Vosk_Sensitivity_Settings()
        {
            return (await Load_Settings_File()).Vosk_Sensitivity;
        }

        public static async Task<bool> Get_Sound_Settings()
        {
            return (await Load_Settings_File()).Sound_On;
        }


        public static async Task<bool> Get_Synthesis_Settings()
        {
            return (await Load_Settings_File()).Synthesis_On;
        }

        public static async Task<string> Get_Speech_Language_Settings()
        {
            return (await Load_Settings_File()).Online_Speech_Recognition_Language;
        }

        public static async Task<bool> Set_Vosk_Sensitivity_Settings(float sensitivity)
        {
            Settings_File settings_File = await Load_Settings_File();
            settings_File.Vosk_Sensitivity = sensitivity;

            return (await Update_Settings_File(settings_File));
        }

        public static async Task<bool> Set_Sound_Settings(bool Option)
        {
            Settings_File settings_File = await Load_Settings_File();
            settings_File.Sound_On = Option;

            return (await Update_Settings_File(settings_File));
        }

        public static async Task<bool> Set_Synthesis_Settings(bool Option)
        {
            Settings_File settings_File = await Load_Settings_File();
            settings_File.Synthesis_On = Option;

            return (await Update_Settings_File(settings_File));
        }

        public static async Task<bool> Set_Speech_Language_Settings(SpeechLanguage language)
        {
            Settings_File settings_File = await Load_Settings_File();

            switch (language)
            {
                case SpeechLanguage.en_US:
                    settings_File.Online_Speech_Recognition_Language = "en-US";
                    break;
                case SpeechLanguage.en_GB:
                    settings_File.Online_Speech_Recognition_Language = "en-GB";
                    break;
            }

            return (await Update_Settings_File(settings_File));
        }

        public static async Task<string> Get_Chat_GPT_Api_Key()
        {
            return (await Load_Settings_File()).Open_AI_Chat_GPT_Key;
        }


        public static async Task<bool> Set_Chat_GPT_Api_Key(string api_key)
        {
            Settings_File settings_File = await Load_Settings_File();
            settings_File.Open_AI_Chat_GPT_Key = api_key;

            return (await Update_Settings_File(settings_File));
        }


        public static async Task<string> Get_Current_Chat_GPT__Model()
        {
            return (await Load_Settings_File()).Gpt_Model;
        }


        public static async Task<bool> Set_Current_Chat_GPT__Model(string gpt_model)
        {
            Settings_File settings_File = await Load_Settings_File();
            settings_File.Gpt_Model = gpt_model;

            return (await Update_Settings_File(settings_File));
        }

        public static async Task<int> Get_Current_Model_Temperature()
        {
            return (await Load_Settings_File()).ModelTemperature;
        }

        public static async Task<bool> Set_Current_Model_Temperature(int temp)
        {
            Settings_File settings_File = await Load_Settings_File();
            settings_File.ModelTemperature = temp;

            return (await Update_Settings_File(settings_File));
        }

        // [ END ]


        ~Settings()
        { }
    }

}
