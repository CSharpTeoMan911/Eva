using System;
using System.Collections.Generic;
using System.Linq;
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
    
    
    internal class A_p_l____And____P_r_o_c
    {

        protected readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> A_p_l_Name__And__A_p_l___E_x__Name = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        protected readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> A_p_l_Name__And__A_p_l___P_r_o_c_Name = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        protected readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();




        private readonly System.Threading.Thread ParallelProcessing;




        public A_p_l____And____P_r_o_c()
        {
            new A_p_l____And____P_r_o_c(true);
        }





        private A_p_l____And____P_r_o_c(bool initiate)
        {

            if(initiate == true)
            {
                ParallelProcessing = new System.Threading.Thread(() =>
                {

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("chrome", "chrome.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("firefox", "firefox.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("edge", "msedge.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("opera", "Opera.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("calculator", "calc.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("paint", "mspaint.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("file explorer", "explorer.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("gmail", "https://mail.google.com/mail/");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("skype", "skype.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("notepad", "notepad.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("task manager", "Taskmgr.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("calendar", "URI = outlookcal:");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("weather", "URI = bingweather:");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("snip and sketch", "URI = ms-screensketch:");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("word", "https://www.office.com/launch/word");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("powerpoint", "https://www.office.com/launch/powerpoint");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("excel", "https://www.office.com/launch/excel");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("onedrive", "https://onedrive.live.com/");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("settings", "URI = ms-settings:");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("command prompt", "cmd.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("powershell", "powershell.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows terminal", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\wt.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows 10", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\wt.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("terminal", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\wt.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("visual studio 2022", @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("visual studio 2019", @"C:\Program Files (x86)\Microsoft Visual Studio\2019\\Community\Common7\IDE\devenv.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("visual studio code", @"C:\Users\Teodor Mihail\AppData\Local\Programs\Microsoft VS Code\Code.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("disk cleanup", "cleanmgr.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("disc cleanup", "cleanmgr.exe");


                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("control panel", "control.exe");









                    ///// BEGIN ///////////// Settings //////////////


                    // Accounts Page

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("accounts settings", "URI = ms-settings:yourinfo");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("your info settings", "URI = ms-settings:yourinfo");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sign in options settings", "URI = ms-settings:signinoptions");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and accounts settings", "URI = ms-settings:emailandaccounts");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and account settings", "URI = ms-settings:emailandaccounts");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("access work or school settings", "URI = ms-settings:workplace");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other users settings", "URI = ms-settings:otherusers");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other user settings", "URI = ms-settings:otherusers");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sync your settings", "URI = ms-settings:sync");



                    // Apps Page

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps settings", "URI = ms-settings:appsfeatures");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps and features settings", "URI = ms-settings:appsfeatures");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default apps settings", "URI = ms-settings:defaultapps");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("offline maps settings", "URI = ms-settings:maps");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for websites settings", "URI = ms-settings:appsforwebsites");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for website settings", "URI = ms-settings:appsforwebsites");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("video playback settings", "URI = ms-settings:videoplayback");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("startup apps settings", "URI = ms-settings:startupapps");



                    // Search

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("search settings", "URI = ms-settings:search-permissions");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("permissions and history settings", "URI = ms-settings:search-permissions");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("searching windows settings", "URI = ms-settings:search-windowssearch");





                    // Devices

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("devices settings", "URI = ms-settings:bluetooth");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("bluetooth and other devices settings", "URI = ms-settings:bluetooth");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanners settings", "URI = ms-settings:printers");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanner settings", "URI = ms-settings:printers");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "URI = ms-settings:mousetouchpad");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("typing settings", "URI = ms-settings:typing");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pen and windows ink settings", "URI = ms-settings:pen");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("autoplay settings", "URI = ms-settings:autoplay");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("USB settings", "URI = ms-settings:usb");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default web browser settings", "URI = ms-settings:defaultbrowsersettings");





                    // Ease of Access

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("ease of access settings", "URI = ms-settings:easeofaccess-display");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("display settings", "URI = ms-settings:easeofaccess-display");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse pointer settings", "URI = ms-settings:easeofaccess-mousepointer");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("text cursor settings", "URI = ms-settings:easeofaccess-cursor");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("magnifier settings", "URI = ms-settings:easeofaccess-magnifier");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("color filters settings", "URI = ms-settings:easeofaccess-colorfilter");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("high contrast settings", "URI = ms-settings:easeofaccess-highcontrast");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("narrator settings", "URI = ms-settings:easeofaccess-narrator");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("audio settings", "URI = ms-settings:easeofaccess-audio");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("closed captions settings", "URI = ms-settings:easeofaccess-closedcaptioning");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("speech settings", "URI = ms-settings:easeofaccess-speechrecognition");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("keyboard settings", "URI = ms-settings:easeofaccess-keyboard");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "URI = ms-settings:easeofaccess-mouse");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("i control settings", "URI = ms-settings:easeofaccess-eyecontrol");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("icontrol settings", "URI = ms-settings:easeofaccess-eyecontrol");





                    // Gaming

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("gaming settings", "URI = ms-settings:gaming-gamebar");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("xbox game bar settings", "URI = ms-settings:gaming-gamebar");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("game mode settings", "URI = ms-settings:gaming-gamemode");




                    // Network and Internet

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("network and internet settings", "URI = ms-settings:network-wifi");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("wi-fi settings", "URI = ms-settings:network-wifi");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("ethernet settings", "URI = ms-settings:network-ethernet");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial up settings", "URI = ms-settings:network-dialup");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial app settings", "URI = ms-settings:network-dialup");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("VPN settings", "URI = ms-settings:network-vpn");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("airplane mode settings", "URI = ms-settings:network-airplanemode");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mobile hotspot settings", "URI = ms-settings:network-mobilehotspot");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("proxy settings", "URI = ms-settings:network-proxy");




                    // Personalisation

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("personalization settings", "URI = ms-settings:personalization-background");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("background settings", "URI = ms-settings:personalization-background");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("colors settings", "URI = ms-settings:personalization-colors");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("lock screen settings", "URI = ms-settings:lockscreen");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("themes settings", "URI = ms-settings:themes");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("start settings", "URI = ms-settings:personalization-start");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("taskbar settings", "URI = ms-settings:taskbar");




                    // Phone

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("phone settings", "URI = ms-settings:mobile-devices");




                    // Privacy

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("privacy settings", "URI = ms-settings:privacy-general");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("inking and typing personalization settings", "URI = ms-settings:privacy-speechtyping");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("diagnostics and feedback settings", "URI = ms-settings:privacy-feedback");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activity history settings", "URI = ms-settings:privacy-activityhistory");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("location settings", "URI = ms-settings:privacy-location");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("camera settings", "URI = ms-settings:privacy-webcam");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("microphone settings", "URI = ms-settings:privacy-microphone");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("voice activation settings", "URI = ms-settings:privacy-voiceactivation");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("account info settings", "URI = ms-settings:privacy-accountinfo");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("contacts settings", "URI = -settings:privacy-contacts");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("phone calls settings", "URI = ms-settings:privacy-phonecalls");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail settings", "URI = ms-settings:privacy-email");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("messaging settings", "URI = ms-settings:privacy-messaging");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("radios settings", "URI = ms-settings:privacy-radios");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("other devices settings", "URI = ms-settings:privacy-customdevices");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("background apps settings", "URI = ms-settings:privacy-backgroundapps");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("app diagnostics settings", "URI = ms-settings:privacy-appdiagnostics");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("automatic file downloads settings", "URI = ms-settings:privacy-automaticfiledownloads");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("documents settings", "URI = ms-settings:privacy-documents");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pictures settings", "URI = ms-settings:privacy-pictures");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("videos settings", "URI = ms-settings:privacy-videos");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("file system settings", "URI = ms-settings:privacy-broadfilesystemaccess");




                    // Time and Language

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("time and language settings", "URI = ms-settings:dateandtime");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("date and time settings", "URI = ms-settings:dateandtime");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("region settings", "URI = ms-settings:regionformatting");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("language settings", "URI = ms-settings:regionlanguage");





                    // Update and Security

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("update and security settings", "URI = ms-settings:windowsupdate");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows update settings", "URI = ms-settings:windowsupdate");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("delivery optimization settings", "URI = ms-settings:delivery-optimization");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows security settings", "URI = ms-settings:windowsdefender");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("backup settings", "URI = ms-settings:backup");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("troubleshoot settings", "URI = ms-settings:troubleshoot");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("recovery settings", "URI = ms-settings:recovery");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activation settings", "URI = ms-settings:activation");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("find my device settings", "URI = ms-settings:findmydevice");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developers settings", "URI = ms-settings:developers");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developer settings", "URI = ms-settings:developers");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows insider program settings", "URI = ms-settings:windowsinsider");



                    // System 

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("system settings", "URI = ms-settings:display");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sound settings", "URI = ms-settings:sound");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("notifications and actions settings", "URI = ms-settings:notifications");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("focus assist settings", "URI = ms-settings:quiethours");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("power and sleep settings", "URI = ms-settings:powersleep");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("storage settings", "URI = ms-settings:storagesense");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("tablet settings", "URI = ms-settings:tabletmode");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("multitasking settings", "URI = ms-settings:multitasking");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("projecting to this PC settings", "URI = ms-settings:project");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("shared experiences settings", "URI = ms-settings:crossdevice");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("clipboard settings", "URI = ms-settings:clipboard");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("remote desktop settings", "URI = ms-settings:remotedesktop");

                    A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("about settings", "URI = ms-settings:about");



                    ///// END ///////////// Settings //////////////






                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("chrome", "chrome");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("firefox", "firefox");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("edge", "msedge");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("opera", "Opera");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("calculator", "Calculator");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("paint", "mspaint");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("skype", "skype");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("notepad", "Notepad");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("task manager", "Taskmgr");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("calendar", "HxCalendarAppImm");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("weather", "Microsoft.Msn.Weather");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("snip and sketch", "ScreenSketch");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("settings", "SystemSettings");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("timer", "timer");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("command prompt", "cmd");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("powershell", "PowerShell");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("windows terminal", "WindowsTerminal");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("windows 10", "WindowsTerminal");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("terminal", "WindowsTerminal");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("visual studio 2022", "devenv");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("visual studio 2019", "devenv");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("visual studio code", "Code");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("disk cleanup", "cleanmgr");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("disc cleanup", "cleanmgr");

                    A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryAdd("control panel", "Control Panel");






                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("chrome", "https://www.google.co.uk/chrome/");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("firefox", "https://www.mozilla.org/en-GB/firefox/new/");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("edge", "https://www.microsoft.com/en-us/edge");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("opera", "https://www.opera.com/gx");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("calculator", "https://www.microsoft.com/en-gb/p/windows-calculator/9wzdncrfhvn5?SilentAuth=1&wa=wsignin1.0&activetab=pivot:overviewtab");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("skype", "https://www.skype.com/en/get-skype/");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("notepad", "https://apps.microsoft.com/store/detail/notepad-for-windows-10/9NBLGGH4W20K?hl=en-us&gl=US");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("calendar", "https://apps.microsoft.com/store/detail/mail-and-calendar/9WZDNCRFHVQM?hl=en-us&gl=US");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("weather", "https://apps.microsoft.com/store/detail/msn-weather/9WZDNCRFJ3Q2?hl=en-gb&gl=GB");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("snip and sketch", "https://apps.microsoft.com/store/detail/snipping-tool/9MZ95KL8MR0L?hl=en-gb&gl=GB");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("windows terminal", "https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701?hl=en-zw&gl=zw");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("windows 10", "https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701?hl=en-zw&gl=zw");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("terminal", "https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701?hl=en-zw&gl=zw");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("visual studio 2022", "https://visualstudio.microsoft.com/vs/");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("visual studio 2019", "https://visualstudio.microsoft.com/vs/older-downloads/");

                    A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k.TryAdd("visual studio code", "https://code.visualstudio.com/download");

                });

                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
                ParallelProcessing.IsBackground = false;
                ParallelProcessing.Start();
            }

        }
    }
}



