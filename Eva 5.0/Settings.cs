using System;
using System.Text;
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


    internal class Settings
    {
        private static readonly string settings_file_name = "application_settings.json";


        private static void Ensure_Access_To_The_Settings_File()
        {
            if(System.IO.File.Exists(settings_file_name) == true)
            {
                System.Security.AccessControl.FileSecurity settings_file_security = System.IO.File.GetAccessControl(settings_file_name);
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Write, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Read, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Delete, System.Security.AccessControl.AccessControlType.Allow));

                System.IO.File.SetAccessControl(settings_file_name, settings_file_security);
            }
        }



        private static async Task<Settings_File> Load_Settings_File()
        {
            Ensure_Access_To_The_Settings_File(); 

            Settings_File settings_File = new Settings_File();
            settings_File.Sound_On = true;
            settings_File.Open_AI_Chat_GPT_Key = String.Empty;

            if (System.IO.File.Exists(settings_file_name) == true)
            {

                System.IO.FileStream settings_file_stream = System.IO.File.OpenRead(settings_file_name);

                try
                {
                    byte[] settings_file_binary = new byte[settings_file_stream.Length];
                    await settings_file_stream.ReadAsync(settings_file_binary, 0, settings_file_binary.Length);

                    settings_File = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings_File>(Encoding.UTF8.GetString(settings_file_binary));
                }
                catch
                {

                }
                finally
                {
                    if(settings_file_stream != null)
                    {
                        settings_file_stream.Close();
                        settings_file_stream.Dispose();
                    }
                }
            }
            else
            {
                await Create_Settings_File(settings_File);
            }

            return settings_File;
        }



        private static async Task<bool> Update_Settings_File(Settings_File settings_File)
        {
            Ensure_Access_To_The_Settings_File();

            if (System.IO.File.Exists(settings_file_name) == true)
            {
                System.IO.File.Delete(settings_file_name);
            }

            return await Create_Settings_File(settings_File);
        }



        private static async Task<bool> Create_Settings_File(Settings_File settings_File)
        {
            Ensure_Access_To_The_Settings_File();

            byte[] settings_file_binary = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(settings_File));

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
                if(settings_file_stream != null)
                {
                    settings_file_stream.Close();
                    settings_file_stream.Dispose();
                }
            }

            return true;
        }



        public static async Task<bool> Get_Sound_Settings()
        {
            return (await Load_Settings_File()).Sound_On;
        }


        public static async Task<bool> Set_Sound_Settings(bool Option)
        {
            Settings_File settings_File = await Load_Settings_File();
            settings_File.Sound_On = Option;

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


        ~Settings()
        { }
    }

}
