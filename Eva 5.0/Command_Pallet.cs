using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Command_Pallet
    {
        public static readonly string commands_file_name = "commands_pallet.json";


        public enum ResetType
        {
            Exe,
            Proc,
            WebApp
        }

        // THIS METHOD ENSURES THAT THE PERMISSIONS TO THE COMMANDS FILE FOR THE CURRENT
        // USER INCLUDE READ, WRITE, AND DELETE FUNCTIONALITIES TO THE SETTINGS FILE.
        // THE SETTINGS FILE MUST HAVE READ AND WRITE PERMISSIONS IN ORDER FOR THE 
        // APPLICATION TO ACCESS THE COMMANDS OTHERWISE THE APPLICATION CAN CRASH.
        private static Task Ensure_Access_To_The_Commands_File()
        {
            if (System.IO.File.Exists(commands_file_name) == true)
            {
                System.Security.AccessControl.FileSecurity settings_file_security = System.IO.File.GetAccessControl(commands_file_name);
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Write, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Read, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Delete, System.Security.AccessControl.AccessControlType.Allow));

                System.IO.File.SetAccessControl(commands_file_name, settings_file_security);
            }

            return Task.CompletedTask;
        }






        private static async Task<Command_Pallet_File> Load_Commands_File()
        {
            // THE REQUIRED FILE ACCESS METHODS FOR THE COMMANDS FILE
            // ARE ENSURED BEFORE ANY READ OPERATION IS PERFORMED
            // ON THE COMMANDS FILE TO AVOID FATAL ERRORS AND ALSO 
            // BE ABLE TO READ THE VALUES
            //
            // [ BEGIN ]

            await Ensure_Access_To_The_Commands_File();

            // [ END ]






            // A "Command_Pallet_File" CLASS OBJECT IS CREATED WITH SOME DEFAULT VALUES.
            // IF THE SETTINGS FILE DOES NOT EXIST, A SETTINGS FILE WILL BE CRATED
            // WITH THESE DEFAULT VALUES.
            Command_Pallet_File commands_File = new Command_Pallet_File();


            try
            {

                if (System.IO.File.Exists(commands_file_name) == true)
                {
                    // IF THE COMMANDS FILE EXISTS, A "FileStream" OBJECT IS CREATED
                    // IN ORDER TO OPEN THE FILE IN A BINARY DATA STREAM AND READ
                    // THE FILE'S BINARY CONTENTS
                    //
                    // [ BEGIN ]

                    using (System.IO.FileStream commands_file_stream = System.IO.File.OpenRead(commands_file_name))
                    {
                        // THE BINARY INFORMATION OF THE COMMANDS FILE IS READ IN A BUFFER
                        // AND THE BUFFER IS THEN CONVERTED IN A STRING IN ORDER TO BE 
                        // DE-SERIALIZED FROM THE JSON FILE FORMAT IN THE "Command_Pallet_File"
                        // FORMAT. 
                        byte[] commands_file_binary = new byte[commands_file_stream.Length];
                        await commands_file_stream.ReadAsync(commands_file_binary, 0, commands_file_binary.Length);
                        commands_File = JsonSerialisation.JsonDeserialiser<Command_Pallet_File>(Encoding.UTF8.GetString(commands_file_binary));
                    }
                    // [ END ]
                }
                else
                {
                    // IF THE COMMANDS FILE DOES NOT EXIST, PASS THE 
                    // "Command_Pallet_File" CLASS INSTANCE TO THIS 
                    // METHOD AND CREATE A SETTINGS FILE
                    // USING THE PRESET DEFAULT VALUES 
                    // OF THIS CLASS INSTANCE
                    //
                    // [ BEGIN ]

                    Generate_Initial_Commands(commands_File);
                    await Create_Commands_File(commands_File);

                    // [ END ]
                }
            }
            catch { }

            return commands_File;
        }


        public static void Generate_Initial_Commands(Command_Pallet_File commands)
        {
            Add_A_p_l_Name__And__A_p_l___E_x__Name(commands);

            Add_A_p_l_Name__And__A_p_l___P_r_o_c_Name(commands);

            Add_W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name(commands);

            App_A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k(commands);
        }


        private static void Add_A_p_l_Name__And__A_p_l___E_x__Name(Command_Pallet_File commands)
        {
            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("chrome", "PRC = chrome.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("firefox", "PRC = firefox.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("edge", "PRC = msedge.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("opera", "PRC = Opera.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("up the app", "PRC = Opera.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("up", "PRC = Opera.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("open app", "PRC = Opera.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("calculator", "PRC = calc.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("paint", "PRC = mspaint.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("file explorer", "PRC = explorer.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("gmail", "PRC = https://mail.google.com/mail/");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("skype", "PRC = skype.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("notepad", "PRC = notepad.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("up pad", "PRC = notepad.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("task manager", "PRC = Taskmgr.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("calendar", "URI = outlookcal:");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("weather", "URI = bingweather:");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("snip and sketch", "URI = ms-screensketch:");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("snipping sketch", "URI = ms-screensketch:");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("notepad sketch", "URI = ms-screensketch:");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("word", "PRC = https://www.office.com/launch/word");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("powerpoint", "PRC = https://www.office.com/launch/powerpoint");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("excel", "PRC = https://www.office.com/launch/excel");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("onedrive", "PRC = https://onedrive.live.com/");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("settings", "URI = ms-settings:");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("command prompt", "PRC = cmd.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("powershell", "PRC = powershell.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows terminal", @"PRC = C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\wt.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows 10", @"PRC = C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\wt.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows 10 mail", @"PRC = C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\wt.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("terminal", @"PRC = C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\wt.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("visual studio 2022", @"CMD = start """" devenv");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("visual studio 2019", @"CMD = start """" devenv");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("visual studio", @"CMD = start """" devenv");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("visual studio code", "CMD = code");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("disk cleanup", "PRC = cleanmgr.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("disc cleanup", "PRC = cleanmgr.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("control panel", "PRC = control.exe");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("temporary files", "CMD = START %TEMP%");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("recycle bin cleanup", "APP = recycle bin cleanup");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("recycle bin clean up", "APP = recycle bin cleanup");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("recycle bin cleaner", "APP = recycle bin cleanup");


            commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("chatgpt", "APP = chatgpt");






            ///// BEGIN ///////////// Settings //////////////


            if (App.Get_Windows_Version() == "Windows 10")
            {
                // Accounts Page

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("accounts settings", "URI = ms-settings:yourinfo");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("your info settings", "URI = ms-settings:yourinfo");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sign in options settings", "URI = ms-settings:signinoptions");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and accounts settings", "URI = ms-settings:emailandaccounts");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and account settings", "URI = ms-settings:emailandaccounts");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("access work or school settings", "URI = ms-settings:workplace");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other users settings", "URI = ms-settings:otherusers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other user settings", "URI = ms-settings:otherusers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sync your settings", "URI = ms-settings:sync");



                // Apps Page

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps settings", "URI = ms-settings:appsfeatures");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps and features settings", "URI = ms-settings:appsfeatures");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default apps settings", "URI = ms-settings:defaultapps");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("offline maps settings", "URI = ms-settings:maps");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for websites settings", "URI = ms-settings:appsforwebsites");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for website settings", "URI = ms-settings:appsforwebsites");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("video playback settings", "URI = ms-settings:videoplayback");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("startup apps settings", "URI = ms-settings:startupapps");



                // Search

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("search settings", "URI = ms-settings:search-permissions");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("permissions and history settings", "URI = ms-settings:search-permissions");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("searching windows settings", "URI = ms-settings:search-windowssearch");





                // Devices

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("devices settings", "URI = ms-settings:bluetooth");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("bluetooth and other devices settings", "URI = ms-settings:bluetooth");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanners settings", "URI = ms-settings:printers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanner settings", "URI = ms-settings:printers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "URI = ms-settings:mousetouchpad");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("typing settings", "URI = ms-settings:typing");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pen and windows ink settings", "URI = ms-settings:pen");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("autoplay settings", "URI = ms-settings:autoplay");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("usb settings", "URI = ms-settings:usb");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default web browser settings", "URI = ms-settings:defaultbrowsersettings");





                // Ease of Access

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("ease of access settings", "URI = ms-settings:easeofaccess-display");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("display settings", "URI = ms-settings:easeofaccess-display");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse pointer settings", "URI = ms-settings:easeofaccess-mousepointer");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("text cursor settings", "URI = ms-settings:easeofaccess-cursor");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("magnifier settings", "URI = ms-settings:easeofaccess-magnifier");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("color filters settings", "URI = ms-settings:easeofaccess-colorfilter");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("high contrast settings", "URI = ms-settings:easeofaccess-highcontrast");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("narrator settings", "URI = ms-settings:easeofaccess-narrator");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("audio settings", "URI = ms-settings:easeofaccess-audio");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("closed captions settings", "URI = ms-settings:easeofaccess-closedcaptioning");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("speech settings", "URI = ms-settings:easeofaccess-speechrecognition");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("keyboard settings", "URI = ms-settings:easeofaccess-keyboard");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "URI = ms-settings:easeofaccess-mouse");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("i control settings", "URI = ms-settings:easeofaccess-eyecontrol");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("eye control settings", "URI = ms-settings:easeofaccess-eyecontrol");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("icontrol settings", "URI = ms-settings:easeofaccess-eyecontrol");





                // Gaming

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("gaming settings", "URI = ms-settings:gaming-gamebar");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("xbox game bar settings", "URI = ms-settings:gaming-gamebar");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("game mode settings", "URI = ms-settings:gaming-gamemode");




                // Network and Internet

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("network and internet settings", "URI = ms-settings:network-wifi");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("wi-fi settings", "URI = ms-settings:network-wifi");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("ethernet settings", "URI = ms-settings:network-ethernet");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial up settings", "URI = ms-settings:network-dialup");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial app settings", "URI = ms-settings:network-dialup");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("VPN settings", "URI = ms-settings:network-vpn");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("airplane mode settings", "URI = ms-settings:network-airplanemode");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mobile hotspot settings", "URI = ms-settings:network-mobilehotspot");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("proxy settings", "URI = ms-settings:network-proxy");




                // Personalisation

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("personalization settings", "URI = ms-settings:personalization-background");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("background settings", "URI = ms-settings:personalization-background");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("colors settings", "URI = ms-settings:personalization-colors");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("lock screen settings", "URI = ms-settings:lockscreen");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("themes settings", "URI = ms-settings:themes");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("start settings", "URI = ms-settings:personalization-start");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("taskbar settings", "URI = ms-settings:taskbar");




                // Phone

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("phone settings", "URI = ms-settings:mobile-devices");




                // Privacy

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("privacy settings", "URI = ms-settings:privacy-general");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("inking and typing personalization settings", "URI = ms-settings:privacy-speechtyping");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("diagnostics and feedback settings", "URI = ms-settings:privacy-feedback");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activity history settings", "URI = ms-settings:privacy-activityhistory");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("location settings", "URI = ms-settings:privacy-location");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("camera settings", "URI = ms-settings:privacy-webcam");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("microphone settings", "URI = ms-settings:privacy-microphone");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("voice activation settings", "URI = ms-settings:privacy-voiceactivation");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("account info settings", "URI = ms-settings:privacy-accountinfo");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("contacts settings", "URI = ms-settings:privacy-contacts");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("phone calls settings", "URI = ms-settings:privacy-phonecalls");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail settings", "URI = ms-settings:privacy-email");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("messaging settings", "URI = ms-settings:privacy-messaging");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("radios settings", "URI = ms-settings:privacy-radios");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("other devices settings", "URI = ms-settings:privacy-customdevices");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("background apps settings", "URI = ms-settings:privacy-backgroundapps");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("app diagnostics settings", "URI = ms-settings:privacy-appdiagnostics");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("automatic file downloads settings", "URI = ms-settings:privacy-automaticfiledownloads");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("documents settings", "URI = ms-settings:privacy-documents");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pictures settings", "URI = ms-settings:privacy-pictures");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("videos settings", "URI = ms-settings:privacy-videos");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("file system settings", "URI = ms-settings:privacy-broadfilesystemaccess");




                // Time and Language

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("time and language settings", "URI = ms-settings:dateandtime");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("date and time settings", "URI = ms-settings:dateandtime");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("region settings", "URI = ms-settings:regionformatting");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("language settings", "URI = ms-settings:regionlanguage");





                // Update and Security

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("update and security settings", "URI = ms-settings:windowsupdate");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows update settings", "URI = ms-settings:windowsupdate");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("delivery optimization settings", "URI = ms-settings:delivery-optimization");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows security settings", "URI = ms-settings:windowsdefender");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("backup settings", "URI = ms-settings:backup");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("troubleshoot settings", "URI = ms-settings:troubleshoot");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("recovery settings", "URI = ms-settings:recovery");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activation settings", "URI = ms-settings:activation");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("find my device settings", "URI = ms-settings:findmydevice");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developers settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developer settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("for developer settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("for developers settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows insider program settings", "URI = ms-settings:windowsinsider");



                // System 

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("system settings", "URI = ms-settings:display");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sound settings", "URI = ms-settings:sound");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("notifications and actions settings", "URI = ms-settings:notifications");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("focus assist settings", "URI = ms-settings:quiethours");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("power and sleep settings", "URI = ms-settings:powersleep");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("storage settings", "URI = ms-settings:storagesense");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("tablet settings", "URI = ms-settings:tabletmode");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("multitasking settings", "URI = ms-settings:multitasking");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("projecting to this PC settings", "URI = ms-settings:project");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("shared experiences settings", "URI = ms-settings:crossdevice");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("clipboard settings", "URI = ms-settings:clipboard");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("remote desktop settings", "URI = ms-settings:remotedesktop");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("about settings", "URI = ms-settings:about");


            }
            else
            {
                // Accounts Page

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("accounts settings", "URI = ms-settings:yourinfo");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("your info settings", "URI = ms-settings:yourinfo");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sign in options settings", "URI = ms-settings:signinoptions");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and accounts settings", "URI = ms-settings:emailandaccounts");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and account settings", "URI = ms-settings:emailandaccounts");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("access work or school settings", "URI = ms-settings:workplace");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other users settings", "URI = ms-settings:otherusers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other user settings", "URI = ms-settings:otherusers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows backup settings", "URI = ms-settings:sync");



                // Apps Page

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps settings", "URI = ms-settings:appsfeatures");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("installed apps settings", "URI = ms-settings:appsfeatures");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default apps settings", "URI = ms-settings:defaultapps");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("offline maps settings", "URI = ms-settings:maps");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for websites settings", "URI = ms-settings:appsforwebsites");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for website settings", "URI = ms-settings:appsforwebsites");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("video playback settings", "URI = ms-settings:videoplayback");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("startup apps settings", "URI = ms-settings:startupapps");




                // Bluetooth and devices

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("bluetooth and devices settings", "URI = ms-settings:bluetooth");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanners settings", "URI = ms-settings:printers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanner settings", "URI = ms-settings:printers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "URI = ms-settings:mousetouchpad");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pen and windows ink settings", "URI = ms-settings:pen");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("autoplay settings", "URI = ms-settings:autoplay");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("usb settings", "URI = ms-settings:usb");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default apps settings", "URI = ms-settings:defaultbrowsersettings");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mobile devices settings", "URI = ms-settings:mobile-devices");





                // Accessibility

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("accessibility settings", "URI = ms-settings:easeofaccess-display");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("text size settings", "URI = ms-settings:easeofaccess-display");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse pointer and touch settings", "URI = ms-settings:easeofaccess-mousepointer");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("text cursor settings", "URI = ms-settings:easeofaccess-cursor");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("magnifier settings", "URI = ms-settings:easeofaccess-magnifier");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("color filters settings", "URI = ms-settings:easeofaccess-colorfilter");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("contrast themes settings", "URI = ms-settings:easeofaccess-highcontrast");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("narrator settings", "URI = ms-settings:easeofaccess-narrator");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("audio settings", "URI = ms-settings:easeofaccess-audio");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("captions settings", "URI = ms-settings:easeofaccess-closedcaptioning");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("speech settings", "URI = ms-settings:easeofaccess-speechrecognition");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("keyboard settings", "URI = ms-settings:easeofaccess-keyboard");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "URI = ms-settings:easeofaccess-mouse");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("i control settings", "URI = ms-settings:easeofaccess-eyecontrol");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("eye control settings", "URI = ms-settings:easeofaccess-eyecontrol");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("icontrol settings", "URI = ms-settings:easeofaccess-eyecontrol");





                // Gaming

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("gaming settings", "URI = ms-settings:gaming-gamebar");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("game bar settings", "URI = ms-settings:gaming-gamebar");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("game mode settings", "URI = ms-settings:gaming-gamemode");




                // Network and Internet

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("network and internet settings", "URI = ms-settings:network-wifi");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("wi-fi settings", "URI = ms-settings:network-wifi");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("ethernet settings", "URI = ms-settings:network-ethernet");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial up settings", "URI = ms-settings:network-dialup");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial app settings", "URI = ms-settings:network-dialup");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("VPN settings", "URI = ms-settings:network-vpn");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("airplane mode settings", "URI = ms-settings:network-airplanemode");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mobile hotspot settings", "URI = ms-settings:network-mobilehotspot");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("proxy settings", "URI = ms-settings:network-proxy");




                // Personalisation

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("personalization settings", "URI = ms-settings:personalization-background");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("background settings", "URI = ms-settings:personalization-background");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("colors settings", "URI = ms-settings:personalization-colors");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("colours settings", "URI = ms-settings:personalization-colors");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("lock screen settings", "URI = ms-settings:lockscreen");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("themes settings", "URI = ms-settings:themes");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("start settings", "URI = ms-settings:personalization-start");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("taskbar settings", "URI = ms-settings:taskbar");




                // Privacy

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("privacy and security settings", "URI = ms-settings:privacy-general");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("search permissions settings", "URI = ms-settings:search-permissions");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("inking and typing personalization settings", "URI = ms-settings:privacy-speechtyping");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("diagnostics and feedback settings", "URI = ms-settings:privacy-feedback");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activity history settings", "URI = ms-settings:privacy-activityhistory");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("location settings", "URI = ms-settings:privacy-location");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("camera settings", "URI = ms-settings:privacy-webcam");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("microphone settings", "URI = ms-settings:privacy-microphone");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("voice activation settings", "URI = ms-settings:privacy-voiceactivation");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("account info settings", "URI = ms-settings:privacy-accountinfo");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("contacts settings", "URI = ms-settings:privacy-contacts");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("phone calls settings", "URI = ms-settings:privacy-phonecalls");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail settings", "URI = ms-settings:privacy-email");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("messaging settings", "URI = ms-settings:privacy-messaging");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("radios settings", "URI = ms-settings:privacy-radios");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("other devices settings", "URI = ms-settings:privacy-customdevices");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("app diagnostics settings", "URI = ms-settings:privacy-appdiagnostics");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("automatic file downloads settings", "URI = ms-settings:privacy-automaticfiledownloads");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("documents settings", "URI = ms-settings:privacy-documents");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pictures settings", "URI = ms-settings:privacy-pictures");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("videos settings", "URI = ms-settings:privacy-videos");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("file system settings", "URI = ms-settings:privacy-broadfilesystemaccess");




                // Time and Language

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("time and language settings", "URI = ms-settings:dateandtime");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("typing settings", "URI = ms-settings:typing");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("date and time settings", "URI = ms-settings:dateandtime");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("language and region settings", "URI = ms-settings:regionlanguage");





                // Windows Update

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows update settings", "URI = ms-settings:windowsupdate");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("delivery optimization settings", "URI = ms-settings:delivery-optimization");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows security settings", "URI = ms-settings:windowsdefender");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("backup settings", "URI = ms-settings:backup");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("troubleshoot settings", "URI = ms-settings:troubleshoot");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("recovery settings", "URI = ms-settings:recovery");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activation settings", "URI = ms-settings:activation");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("find my device settings", "URI = ms-settings:findmydevice");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developers settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developer settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("for developer settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("for developers settings", "URI = ms-settings:developers");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows insider program settings", "URI = ms-settings:windowsinsider");



                // System 

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("system settings", "URI = ms-settings:display");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("display settings", "URI = ms-settings:display");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sound settings", "URI = ms-settings:sound");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("notifications settings", "URI = ms-settings:notifications");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("focus settings", "URI = ms-settings:quiethours");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("power settings", "URI = ms-settings:powersleep");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("storage settings", "URI = ms-settings:storagesense");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("multitasking settings", "URI = ms-settings:multitasking");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("projecting to this PC settings", "URI = ms-settings:project");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("nearby sharing settings", "URI = ms-settings:crossdevice");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("clipboard settings", "URI = ms-settings:clipboard");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("remote desktop settings", "URI = ms-settings:remotedesktop");

                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("about settings", "URI = ms-settings:about");

            }
            ///// END ///////////// Settings //////////////
        }


        private static void Add_A_p_l_Name__And__A_p_l___P_r_o_c_Name(Command_Pallet_File commands)
        {


            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("chrome", "chrome");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("firefox", "firefox");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("edge", "msedge");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("opera", "Opera");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("up app", "Opera");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("open app", "Opera");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("open", "Opera");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("up", "Opera");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("calculator", "CalculatorApp");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("paint", "mspaint");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("skype", "skype");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("notepad", "Notepad");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("up pad", "Notepad");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("task manager", "Taskmgr");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("calendar", "HxCalendarAppImm");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("weather", "Microsoft.Msn.Weather");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("snip and sketch", "ScreenSketch");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("snipping sketch", "ScreenSketch");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("notepad sketch", "ScreenSketch");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("settings", "SystemSettings");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("timer", "timer");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("command prompt", "cmd");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("powershell", "PowerShell");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("windows terminal", "WindowsTerminal");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("windows 10", "WindowsTerminal");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("terminal", "WindowsTerminal");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("visual studio 2022", "devenv");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("visual studio 2019", "devenv");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("visual studio", "devenv");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("visual studio code", "Code");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("disk cleanup", "cleanmgr");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("disc cleanup", "cleanmgr");

            commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("control panel", "Control Panel");
        }

        private static void Add_W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name(Command_Pallet_File commands)
        {
            // WEB APPLICATIONS
            //
            // BEGIN

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("youtube", "https://www.youtube.com/results?search_query=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("netflix", "https://www.netflix.com/search?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("wikipedia", "https://en.wikipedia.org/wiki/");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("google", "https://www.google.com/search?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("google news", "https://www.google.com/search?tbm=nws&q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("google scholar", "https://scholar.google.com/scholar?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("ebay", "https://www.ebay.co.uk/sch/i.html?_nkw=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("google images", "https://www.google.com/search?tbm=isch&q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("amazon", "https://www.amazon.co.uk/s?k=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("reddit", "https://www.reddit.com/search/?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("facebook", "https://www.facebook.com/search/top?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("instagram", "https://www.instagram.com/");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("gmail", "https://mail.google.com/mail/u/0/#search/");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("twitter", "https://twitter.com/search?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("pinterest", "https://www.pinterest.co.uk/search/pins/?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("linkedin", "https://www.linkedin.com/search/results/all/?keywords=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("github", "https://github.com/search?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("unsplash", "https://unsplash.com/s/photos/");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("slack overflow", "https://stackoverflow.com/search?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("stack overflow", "https://stackoverflow.com/search?q=");

            commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryAdd("stackoverflow", "https://stackoverflow.com/search?q=");

            // END
        }

        private static void App_A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k(Command_Pallet_File commands)
        {
            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("chrome", "https://www.google.co.uk/chrome/");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("firefox", "https://www.mozilla.org/en-GB/firefox/new/");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("edge", "https://www.microsoft.com/en-us/edge");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("opera", "https://www.opera.com/gx");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("up", "https://www.opera.com/gx");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("open app", "https://www.opera.com/gx");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("calculator", "https://www.microsoft.com/en-gb/p/windows-calculator/9wzdncrfhvn5?SilentAuth=1&wa=wsignin1.0&activetab=pivot:overviewtab");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("skype", "https://www.skype.com/en/get-skype/");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("notepad", "https://apps.microsoft.com/store/detail/notepad-for-windows-10/9NBLGGH4W20K?hl=en-us&gl=US");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("up pad", "https://apps.microsoft.com/store/detail/notepad-for-windows-10/9NBLGGH4W20K?hl=en-us&gl=US");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("calendar", "https://apps.microsoft.com/store/detail/mail-and-calendar/9WZDNCRFHVQM?hl=en-us&gl=US");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("weather", "https://apps.microsoft.com/store/detail/msn-weather/9WZDNCRFJ3Q2?hl=en-gb&gl=GB");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("snip and sketch", "https://apps.microsoft.com/store/detail/snipping-tool/9MZ95KL8MR0L?hl=en-gb&gl=GB");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("windows terminal", "https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701?hl=en-zw&gl=zw");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("windows 10", "https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701?hl=en-zw&gl=zw");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("terminal", "https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701?hl=en-zw&gl=zw");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("visual studio 2022", "https://visualstudio.microsoft.com/vs/");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("visual studio 2019", "https://visualstudio.microsoft.com/vs/older-downloads/");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("visual studio", "https://visualstudio.microsoft.com/vs/");

            commands.A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("visual studio code", "https://code.visualstudio.com/download");
        }

        private static async Task Update_Commands_File(Command_Pallet_File commands_File)
        {
            // THE REQUIRED FILE ACCESS METHODS FOR THE COMMANDS FILE
            // ARE ENSURED BEFORE ANY DELETE OPERATION IS
            // PERFORMED ON THE COMMANDS FILE TO AVOID FATAL
            // ERRORS AND ALSO BE ABLE TO READ THE VALUES
            //
            // [ BEGIN ]

            await Ensure_Access_To_The_Commands_File();

            // [ END ]

            try
            {
                // IF THE COMMANDS FILE EXIST, DELETE IT AND CREATE A
                // NEW COMMANDS FILE WITH THE SET VALUES
                //
                // [ BEGIN ]

                if (System.IO.File.Exists(commands_file_name) == true)
                {
                    System.IO.File.Delete(commands_file_name);
                }

                // [ END ]
            }
            catch { }
            await Create_Commands_File(commands_File);
        }



        private static async Task Create_Commands_File(Command_Pallet_File commands_File)
        {
            // SERIALIZE THE "Command_Pallet_File" OBJECT IN A JSON FILE FORMAT
            // AND CONVERT THE SERIALIZED INFORMATION IN A BINARY FORMAT.
            // WRITE THE BINARY INFORMATION USING A "FileStream" OBJECT
            // BY CREATING A COMMANDS FILE AND WRITING INTO IT USING 
            // THE STREAM.
            //
            // [ BEGIN ]

            try
            {
                byte[] commands_file_binary = Encoding.UTF8.GetBytes(await JsonSerialisation.JsonSerialiser(commands_File));

                using (System.IO.FileStream commands_file_stream = System.IO.File.Create(commands_file_name, commands_file_binary.Length, System.IO.FileOptions.Asynchronous))
                {
                    await commands_file_stream.WriteAsync(commands_file_binary, 0, commands_file_binary.Length);
                    await commands_file_stream.FlushAsync();
                }
            }
            catch { }

            // [ END ]







            // THE REQUIRED FILE ACCESS METHODS FOR THE COMMANDS FILE
            // ARE ENSURED BEFORE ANY WRITE OPERATION IS
            // PERFORMED ON THE COMMANDS FILE TO AVOID FATAL
            // ERRORS AND ALSO BE ABLE TO READ THE VALUES
            //
            // [ BEGIN ]

            await Ensure_Access_To_The_Commands_File();

            // [ END ]
        }



        // GETTER AND SETTER METHODS FOR THE APPLICATION'S COMMANDS FILE
        //
        // [ BEGIN ]

        // GET THE CURRENT COMMANDS FILE
        public static async Task<Command_Pallet_File> Get_Commands() => await Load_Commands_File();

        // UPDATE THE COMMANDS FILE
        public static async Task Set_Commands(Command_Pallet_File commands_File) => await Update_Commands_File(commands_File);

        // RESET THE COMMANDS FILE BY RESETING ONE OF THE COLLECTIONS TO THEIR DEFAULT VALUE AND UPDATE THE FILE
        public static async Task Reset_Commands(Commands_Customisation.Option type)
        {
            switch (type)
            {
                case Commands_Customisation.Option.OpenApplications:
                    A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___E_x__Name.Clear();
                    Add_A_p_l_Name__And__A_p_l___E_x__Name(A_p_l____And____P_r_o_c.commands);
                    break;
                case Commands_Customisation.Option.CloseApplications:
                    A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.Clear();
                    Add_A_p_l_Name__And__A_p_l___P_r_o_c_Name(A_p_l____And____P_r_o_c.commands);
                    break;
                case Commands_Customisation.Option.SearchContentOnWebApplications:
                    A_p_l____And____P_r_o_c.commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.Clear();
                    Add_W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name(A_p_l____And____P_r_o_c.commands);
                    break;
            }
            await Update_Commands_File(A_p_l____And____P_r_o_c.commands);
        }

        // [ END ]

    }
}
