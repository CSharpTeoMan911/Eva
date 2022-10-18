using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class A_p_l____And____P_r_o_c
    {

        protected readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> A_p_l_Name__And__A_p_l___E_x__Name = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        protected readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> A_p_l_Name__And__A_p_l___P_r_o_c_Name = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        protected readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> A_p_l__Name__And__A_p_l__Not_Found_Error__L_n_k = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();




        public A_p_l____And____P_r_o_c()
        {
            A_p_l____And____P_r_o_c a_P_L____And____P_R_O_C = new A_p_l____And____P_r_o_c(0);
        }





        private A_p_l____And____P_r_o_c(int dif)
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


            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("settings", "ms-settings:");


            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("command prompt", "cmd.exe");


            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("powershell", "powershell.exe");









            ///// BEGIN ///////////// Settings //////////////


            // Accounts Page

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("accounts settings", "ms-settings:yourinfo");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("your info settings", "ms-settings:yourinfo");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sign in options settings", "ms-settings:signinoptions");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and accounts settings", "ms-settings:emailandaccounts");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail and account settings", "ms-settings:emailandaccounts");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("access work or school settings", "ms-settings:workplace");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other users settings", "ms-settings:otherusers");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("family and other user settings", "ms-settings:otherusers");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sync your settings", "ms-settings:sync");



            // Apps Page

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps settings", "ms-settings:appsfeatures");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps and features settings", "ms-settings:appsfeatures");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default apps settings", "ms-settings:defaultapps");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("offline maps settings", "ms-settings:maps");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for websites settings", "ms-settings:appsforwebsites");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("apps for website settings", "ms-settings:appsforwebsites");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("video playback settings", "ms-settings:videoplayback");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("startup apps settings", "ms-settings:startupapps");



            // Search

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("search settings", "ms-settings:search-permissions");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("permissions and history settings", "ms-settings:search-permissions");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("searching windows settings", "ms-settings:search-windowssearch");





            // Devices

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("devices settings", "ms-settings:bluetooth");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("bluetooth and other devices settings", "ms-settings:bluetooth");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanners settings", "ms-settings:printers");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("printers and scanner settings", "ms-settings:printers");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "ms-settings:mousetouchpad");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("typing settings", "ms-settings:typing");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pen and windows ink settings", "ms-settings:pen");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("autoplay settings", "ms-settings:autoplay");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("USB settings", "ms-settings:usb");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("default web browser settings", "ms-settings:defaultbrowsersettings");





            // Ease of Access

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("ease of access settings", "ms-settings:easeofaccess-display");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("display settings", "ms-settings:easeofaccess-display");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse pointer settings", "ms-settings:easeofaccess-mousepointer");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("text cursor settings", "ms-settings:easeofaccess-cursor");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("magnifier settings", "ms-settings:easeofaccess-magnifier");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("color filters settings", "ms-settings:easeofaccess-colorfilter");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("high contrast settings", "ms-settings:easeofaccess-highcontrast");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("narrator settings", "ms-settings:easeofaccess-narrator");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("audio settings", "ms-settings:easeofaccess-audio");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("closed captions settings", "ms-settings:easeofaccess-closedcaptioning");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("speech settings", "ms-settings:easeofaccess-speechrecognition");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("keyboard settings", "ms-settings:easeofaccess-keyboard");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mouse settings", "ms-settings:easeofaccess-mouse");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("i control settings", "ms-settings:easeofaccess-eyecontrol");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("icontrol settings", "ms-settings:easeofaccess-eyecontrol");





            // Gaming

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("gaming settings", "ms-settings:gaming-gamebar");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("xbox game bar settings", "ms-settings:gaming-gamebar");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("game mode settings", "ms-settings:gaming-gamemode");




            // Network and Internet

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("network and internet settings", "ms-settings:network-wifi");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("wi-fi settings", "ms-settings:network-wifi");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("ethernet settings", "ms-settings:network-ethernet");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial up settings", "ms-settings:network-dialup");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("dial app settings", "ms-settings:network-dialup");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("VPN settings", "ms-settings:network-vpn");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("airplane mode settings", "ms-settings:network-airplanemode");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("mobile hotspot settings", "ms-settings:network-mobilehotspot");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("proxy settings", "ms-settings:network-proxy");




            // Personalisation

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("personalization settings", "ms-settings:personalization-background");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("background settings", "ms-settings:personalization-background");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("colors settings", "ms-settings:personalization-colors");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("lock screen settings", "ms-settings:lockscreen");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("themes settings", "ms-settings:themes");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("start settings", "ms-settings:personalization-start");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("taskbar settings", "ms-settings:taskbar");




            // Phone

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("phone settings", "ms-settings:mobile-devices");




            // Privacy

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("privacy settings", "ms-settings:privacy-general");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("inking and typing personalization settings", "ms-settings:privacy-speechtyping");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("diagnostics and feedback settings", "ms-settings:privacy-feedback");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activity history settings", "ms-settings:privacy-activityhistory");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("location settings", "ms-settings:privacy-location");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("camera settings", "ms-settings:privacy-webcam");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("microphone settings", "ms-settings:privacy-microphone");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("voice activation settings", "ms-settings:privacy-voiceactivation");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("account info settings", "ms-settings:privacy-accountinfo");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("contacts settings", "ms-settings:privacy-contacts");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("phone calls settings", "ms-settings:privacy-phonecalls");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("e-mail settings", "ms-settings:privacy-email");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("messaging settings", "ms-settings:privacy-messaging");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("radios settings", "ms-settings:privacy-radios");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("other devices settings", "ms-settings:privacy-customdevices");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("background apps settings", "ms-settings:privacy-backgroundapps");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("app diagnostics settings", "ms-settings:privacy-appdiagnostics");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("automatic file downloads settings", "ms-settings:privacy-automaticfiledownloads");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("documents settings", "ms-settings:privacy-documents");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("pictures settings", "ms-settings:privacy-pictures");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("videos settings", "ms-settings:privacy-videos");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("file system settings", "ms-settings:privacy-broadfilesystemaccess");




            // Time and Language

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("time and language settings", "ms-settings:dateandtime");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("date and time settings", "ms-settings:dateandtime");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("region settings", "ms-settings:regionformatting");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("language settings", "ms-settings:regionlanguage");





            // Update and Security

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("update and security settings", "ms-settings:windowsupdate");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows update settings", "ms-settings:windowsupdate");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("delivery optimization settings", "ms-settings:delivery-optimization");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows security settings", "ms-settings:windowsdefender");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("backup settings", "ms-settings:backup");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("troubleshoot settings", "ms-settings:troubleshoot");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("recovery settings", "ms-settings:recovery");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("activation settings", "ms-settings:activation");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("find my device settings", "ms-settings:findmydevice");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developers settings", "ms-settings:developers");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("4 developer settings", "ms-settings:developers");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("windows insider program settings", "ms-settings:windowsinsider");



            // System 

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("system settings", "ms-settings:display");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("sound settings", "ms-settings:sound");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("notifications and actions settings", "ms-settings:notifications");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("focus assist settings", "ms-settings:quiethours");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("power and sleep settings", "ms-settings:powersleep");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("storage settings", "ms-settings:storagesense");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("tablet settings", "ms-settings:tabletmode");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("multitasking settings", "ms-settings:multitasking");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("projecting to this PC settings", "ms-settings:project");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("shared experiences settings", "ms-settings:crossdevice");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("clipboard settings", "ms-settings:clipboard");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("remote desktop settings", "ms-settings:remotedesktop");

            A_p_l_Name__And__A_p_l___E_x__Name.TryAdd("about settings", "ms-settings:about");



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
        }
    }
}



