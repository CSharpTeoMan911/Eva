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
        public readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> Application_Name__And__Application_Executable_Name = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        public readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> Application_Name__And__Application_Process_Name = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        public readonly static System.Collections.Concurrent.ConcurrentDictionary<string, string> Application_Name__And__Application_Not_Found_Error_Download_Link = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();




        public static bool SettingsWindowOpen;

        public static bool InstructionManualOpen;

        public static bool PermisissionWindowOpen;

        public static bool ErrorAppShutdown;

        public static bool InitiateErrorFunction;

        public static string ErrorFunction;




        public static bool StopRecognitionSession;





        public App()
        {
            // Construct the class and resources related to applications, processes and web-links
            // related to the Eva functions


        

            Application_Name__And__Application_Executable_Name.TryAdd("chrome", "chrome.exe");

            
            Application_Name__And__Application_Executable_Name.TryAdd("firefox", "firefox.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("edge", "msedge.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("opera", "Opera.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("calculator", "calc.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("paint", "mspaint.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("file explorer", "explorer.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("gmail", "https://mail.google.com/mail/");


            Application_Name__And__Application_Executable_Name.TryAdd("skype", "skype.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("notepad", "notepad.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("task manager", "Taskmgr.exe");


            Application_Name__And__Application_Executable_Name.TryAdd("calendar", "URI = outlookcal:");


            Application_Name__And__Application_Executable_Name.TryAdd("weather", "URI = bingweather:");


            Application_Name__And__Application_Executable_Name.TryAdd("snip and sketch", "URI = ms-screensketch:");


            Application_Name__And__Application_Executable_Name.TryAdd("word", "https://www.office.com/launch/word");


            Application_Name__And__Application_Executable_Name.TryAdd("powerpoint", "https://www.office.com/launch/powerpoint");


            Application_Name__And__Application_Executable_Name.TryAdd("excel", "https://www.office.com/launch/excel");


            Application_Name__And__Application_Executable_Name.TryAdd("onedrive", "https://onedrive.live.com/");
            

            Application_Name__And__Application_Executable_Name.TryAdd("settings", "ms-settings:");









            ///// BEGIN ///////////// Settings //////////////


            // Accounts Page

            Application_Name__And__Application_Executable_Name.TryAdd("accounts settings", "ms-settings:yourinfo");

            Application_Name__And__Application_Executable_Name.TryAdd("your info settings", "ms-settings:yourinfo");

            Application_Name__And__Application_Executable_Name.TryAdd("sign in options settings", "ms-settings:signinoptions");

            Application_Name__And__Application_Executable_Name.TryAdd("e-mail and accounts settings", "ms-settings:emailandaccounts");

            Application_Name__And__Application_Executable_Name.TryAdd("e-mail and account settings", "ms-settings:emailandaccounts");

            Application_Name__And__Application_Executable_Name.TryAdd("access work or school settings", "ms-settings:workplace");

            Application_Name__And__Application_Executable_Name.TryAdd("family and other users settings", "ms-settings:otherusers");

            Application_Name__And__Application_Executable_Name.TryAdd("family and other user settings", "ms-settings:otherusers");

            Application_Name__And__Application_Executable_Name.TryAdd("sync your settings", "ms-settings:sync");



            // Apps Page

            Application_Name__And__Application_Executable_Name.TryAdd("apps settings", "ms-settings:appsfeatures");

            Application_Name__And__Application_Executable_Name.TryAdd("apps and features settings", "ms-settings:appsfeatures");

            Application_Name__And__Application_Executable_Name.TryAdd("default apps settings", "ms-settings:defaultapps");

            Application_Name__And__Application_Executable_Name.TryAdd("offline maps settings", "ms-settings:maps");

            Application_Name__And__Application_Executable_Name.TryAdd("apps for websites settings", "ms-settings:appsforwebsites");

            Application_Name__And__Application_Executable_Name.TryAdd("apps for website settings", "ms-settings:appsforwebsites");

            Application_Name__And__Application_Executable_Name.TryAdd("video playback settings", "ms-settings:videoplayback");

            Application_Name__And__Application_Executable_Name.TryAdd("startup apps settings", "ms-settings:startupapps");



            // Search

            Application_Name__And__Application_Executable_Name.TryAdd("search settings", "ms-settings:search-permissions");

            Application_Name__And__Application_Executable_Name.TryAdd("permissions and history settings", "ms-settings:search-permissions");

            Application_Name__And__Application_Executable_Name.TryAdd("searching windows settings", "ms-settings:search-windowssearch");





            // Devices

            Application_Name__And__Application_Executable_Name.TryAdd("devices settings", "ms-settings:bluetooth");

            Application_Name__And__Application_Executable_Name.TryAdd("bluetooth and other devices settings", "ms-settings:bluetooth");

            Application_Name__And__Application_Executable_Name.TryAdd("printers and scanners settings", "ms-settings:printers");

            Application_Name__And__Application_Executable_Name.TryAdd("printers and scanner settings", "ms-settings:printers");

            Application_Name__And__Application_Executable_Name.TryAdd("mouse settings", "ms-settings:mousetouchpad");

            Application_Name__And__Application_Executable_Name.TryAdd("typing settings", "ms-settings:typing");

            Application_Name__And__Application_Executable_Name.TryAdd("pen and windows ink settings", "ms-settings:pen");

            Application_Name__And__Application_Executable_Name.TryAdd("autoplay settings", "ms-settings:autoplay");

            Application_Name__And__Application_Executable_Name.TryAdd("USB settings", "ms-settings:usb");

            Application_Name__And__Application_Executable_Name.TryAdd("default web browser settings", "ms-settings:defaultbrowsersettings");





            // Ease of Access

            Application_Name__And__Application_Executable_Name.TryAdd("ease of access settings", "ms-settings:easeofaccess-display");

            Application_Name__And__Application_Executable_Name.TryAdd("display settings", "ms-settings:easeofaccess-display");

            Application_Name__And__Application_Executable_Name.TryAdd("mouse pointer settings", "ms-settings:easeofaccess-mousepointer");

            Application_Name__And__Application_Executable_Name.TryAdd("text cursor settings", "ms-settings:easeofaccess-cursor");

            Application_Name__And__Application_Executable_Name.TryAdd("magnifier settings", "ms-settings:easeofaccess-magnifier");

            Application_Name__And__Application_Executable_Name.TryAdd("color filters settings", "ms-settings:easeofaccess-colorfilter");

            Application_Name__And__Application_Executable_Name.TryAdd("high contrast settings", "ms-settings:easeofaccess-highcontrast");

            Application_Name__And__Application_Executable_Name.TryAdd("narrator settings", "ms-settings:easeofaccess-narrator");

            Application_Name__And__Application_Executable_Name.TryAdd("audio settings", "ms-settings:easeofaccess-audio");

            Application_Name__And__Application_Executable_Name.TryAdd("closed captions settings", "ms-settings:easeofaccess-closedcaptioning");

            Application_Name__And__Application_Executable_Name.TryAdd("speech settings", "ms-settings:easeofaccess-speechrecognition");

            Application_Name__And__Application_Executable_Name.TryAdd("keyboard settings", "ms-settings:easeofaccess-keyboard");

            Application_Name__And__Application_Executable_Name.TryAdd("mouse settings", "ms-settings:easeofaccess-mouse");

            Application_Name__And__Application_Executable_Name.TryAdd("i control settings", "ms-settings:easeofaccess-eyecontrol");

            Application_Name__And__Application_Executable_Name.TryAdd("icontrol settings", "ms-settings:easeofaccess-eyecontrol");





            // Gaming

            Application_Name__And__Application_Executable_Name.TryAdd("gaming settings", "ms-settings:gaming-gamebar");

            Application_Name__And__Application_Executable_Name.TryAdd("xbox game bar settings", "ms-settings:gaming-gamebar");

            Application_Name__And__Application_Executable_Name.TryAdd("game mode settings", "ms-settings:gaming-gamemode");




            // Network and Internet

            Application_Name__And__Application_Executable_Name.TryAdd("network and internet settings", "ms-settings:network-wifi");

            Application_Name__And__Application_Executable_Name.TryAdd("wi-fi settings", "ms-settings:network-wifi");

            Application_Name__And__Application_Executable_Name.TryAdd("ethernet settings", "ms-settings:network-ethernet");

            Application_Name__And__Application_Executable_Name.TryAdd("dial up settings", "ms-settings:network-dialup");

            Application_Name__And__Application_Executable_Name.TryAdd("dial app settings", "ms-settings:network-dialup");

            Application_Name__And__Application_Executable_Name.TryAdd("VPN settings", "ms-settings:network-vpn");

            Application_Name__And__Application_Executable_Name.TryAdd("airplane mode settings", "ms-settings:network-airplanemode");

            Application_Name__And__Application_Executable_Name.TryAdd("mobile hotspot settings", "ms-settings:network-mobilehotspot");

            Application_Name__And__Application_Executable_Name.TryAdd("proxy settings", "ms-settings:network-proxy");




            // Personalisation

            Application_Name__And__Application_Executable_Name.TryAdd("personalization settings", "ms-settings:personalization-background");

            Application_Name__And__Application_Executable_Name.TryAdd("background settings", "ms-settings:personalization-background");

            Application_Name__And__Application_Executable_Name.TryAdd("colors settings", "ms-settings:personalization-colors");

            Application_Name__And__Application_Executable_Name.TryAdd("lock screen settings", "ms-settings:lockscreen");

            Application_Name__And__Application_Executable_Name.TryAdd("themes settings", "ms-settings:themes");

            Application_Name__And__Application_Executable_Name.TryAdd("start settings", "ms-settings:personalization-start");

            Application_Name__And__Application_Executable_Name.TryAdd("taskbar settings", "ms-settings:taskbar");




            // Phone

            Application_Name__And__Application_Executable_Name.TryAdd("phone settings", "ms-settings:mobile-devices");




            // Privacy

            Application_Name__And__Application_Executable_Name.TryAdd("privacy settings", "ms-settings:privacy-general");

            Application_Name__And__Application_Executable_Name.TryAdd("inking and typing personalization settings", "ms-settings:privacy-speechtyping");

            Application_Name__And__Application_Executable_Name.TryAdd("diagnostics and feedback settings", "ms-settings:privacy-feedback");

            Application_Name__And__Application_Executable_Name.TryAdd("activity history settings", "ms-settings:privacy-activityhistory");

            Application_Name__And__Application_Executable_Name.TryAdd("location settings", "ms-settings:privacy-location");

            Application_Name__And__Application_Executable_Name.TryAdd("camera settings", "ms-settings:privacy-webcam");

            Application_Name__And__Application_Executable_Name.TryAdd("microphone settings", "ms-settings:privacy-microphone");

            Application_Name__And__Application_Executable_Name.TryAdd("voice activation settings", "ms-settings:privacy-voiceactivation");

            Application_Name__And__Application_Executable_Name.TryAdd("account info settings", "ms-settings:privacy-accountinfo");

            Application_Name__And__Application_Executable_Name.TryAdd("contacts settings", "ms-settings:privacy-contacts");

            Application_Name__And__Application_Executable_Name.TryAdd("phone calls settings", "ms-settings:privacy-phonecalls");

            Application_Name__And__Application_Executable_Name.TryAdd("e-mail settings", "ms-settings:privacy-email");

            Application_Name__And__Application_Executable_Name.TryAdd("messaging settings", "ms-settings:privacy-messaging");

            Application_Name__And__Application_Executable_Name.TryAdd("radios settings", "ms-settings:privacy-radios");

            Application_Name__And__Application_Executable_Name.TryAdd("other devices settings", "ms-settings:privacy-customdevices");

            Application_Name__And__Application_Executable_Name.TryAdd("background apps settings", "ms-settings:privacy-backgroundapps");

            Application_Name__And__Application_Executable_Name.TryAdd("app diagnostics settings", "ms-settings:privacy-appdiagnostics");

            Application_Name__And__Application_Executable_Name.TryAdd("automatic file downloads settings", "ms-settings:privacy-automaticfiledownloads");

            Application_Name__And__Application_Executable_Name.TryAdd("documents settings", "ms-settings:privacy-documents");

            Application_Name__And__Application_Executable_Name.TryAdd("pictures settings", "ms-settings:privacy-pictures");

            Application_Name__And__Application_Executable_Name.TryAdd("videos settings", "ms-settings:privacy-videos");

            Application_Name__And__Application_Executable_Name.TryAdd("file system settings", "ms-settings:privacy-broadfilesystemaccess");




            // Time and Language

            Application_Name__And__Application_Executable_Name.TryAdd("time and language settings", "ms-settings:dateandtime");

            Application_Name__And__Application_Executable_Name.TryAdd("date and time settings", "ms-settings:dateandtime");

            Application_Name__And__Application_Executable_Name.TryAdd("region settings", "ms-settings:regionformatting");

            Application_Name__And__Application_Executable_Name.TryAdd("language settings", "ms-settings:regionlanguage");





            // Update and Security

            Application_Name__And__Application_Executable_Name.TryAdd("update and security settings", "ms-settings:windowsupdate");

            Application_Name__And__Application_Executable_Name.TryAdd("windows update settings", "ms-settings:windowsupdate");

            Application_Name__And__Application_Executable_Name.TryAdd("delivery optimization settings", "ms-settings:delivery-optimization");

            Application_Name__And__Application_Executable_Name.TryAdd("windows security settings", "ms-settings:windowsdefender");

            Application_Name__And__Application_Executable_Name.TryAdd("backup settings", "ms-settings:backup");

            Application_Name__And__Application_Executable_Name.TryAdd("troubleshoot settings", "ms-settings:troubleshoot");

            Application_Name__And__Application_Executable_Name.TryAdd("recovery settings", "ms-settings:recovery");

            Application_Name__And__Application_Executable_Name.TryAdd("activation settings", "ms-settings:activation");

            Application_Name__And__Application_Executable_Name.TryAdd("find my device settings", "ms-settings:findmydevice");

            Application_Name__And__Application_Executable_Name.TryAdd("4 developers settings", "ms-settings:developers");

            Application_Name__And__Application_Executable_Name.TryAdd("4 developer settings", "ms-settings:developers");

            Application_Name__And__Application_Executable_Name.TryAdd("windows insider program settings", "ms-settings:windowsinsider");



            // System 

            Application_Name__And__Application_Executable_Name.TryAdd("system settings", "ms-settings:display");

            Application_Name__And__Application_Executable_Name.TryAdd("sound settings", "ms-settings:sound");

            Application_Name__And__Application_Executable_Name.TryAdd("notifications and actions settings", "ms-settings:notifications");

            Application_Name__And__Application_Executable_Name.TryAdd("focus assist settings", "ms-settings:quiethours");

            Application_Name__And__Application_Executable_Name.TryAdd("power and sleep settings", "ms-settings:powersleep");

            Application_Name__And__Application_Executable_Name.TryAdd("storage settings", "ms-settings:storagesense");

            Application_Name__And__Application_Executable_Name.TryAdd("tablet settings", "ms-settings:tabletmode");

            Application_Name__And__Application_Executable_Name.TryAdd("multitasking settings", "ms-settings:multitasking");

            Application_Name__And__Application_Executable_Name.TryAdd("projecting to this PC settings", "ms-settings:project");

            Application_Name__And__Application_Executable_Name.TryAdd("shared experiences settings", "ms-settings:crossdevice");

            Application_Name__And__Application_Executable_Name.TryAdd("clipboard settings", "ms-settings:clipboard");

            Application_Name__And__Application_Executable_Name.TryAdd("remote desktop settings", "ms-settings:remotedesktop");

            Application_Name__And__Application_Executable_Name.TryAdd("about settings", "ms-settings:about");



            ///// END ///////////// Settings //////////////






            Application_Name__And__Application_Process_Name.TryAdd("chrome", "chrome");

            Application_Name__And__Application_Process_Name.TryAdd("firefox", "firefox");

            Application_Name__And__Application_Process_Name.TryAdd("edge", "msedge");

            Application_Name__And__Application_Process_Name.TryAdd("opera", "Opera");

            Application_Name__And__Application_Process_Name.TryAdd("calculator", "Calculator");

            Application_Name__And__Application_Process_Name.TryAdd("paint", "mspaint");

            Application_Name__And__Application_Process_Name.TryAdd("skype", "skype");

            Application_Name__And__Application_Process_Name.TryAdd("notepad", "Notepad");

            Application_Name__And__Application_Process_Name.TryAdd("task manager", "Taskmgr");

            Application_Name__And__Application_Process_Name.TryAdd("calendar", "HxCalendarAppImm");

            Application_Name__And__Application_Process_Name.TryAdd("weather", "Microsoft.Msn.Weather");

            Application_Name__And__Application_Process_Name.TryAdd("snip and sketch", "ScreenSketch");

            Application_Name__And__Application_Process_Name.TryAdd("settings", "SystemSettings");









            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("chrome", "https://www.google.co.uk/chrome/");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("firefox", "https://www.mozilla.org/en-GB/firefox/new/");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("edge", "https://www.microsoft.com/en-us/edge");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("opera", "https://www.opera.com/gx");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("calculator", "https://www.microsoft.com/en-gb/p/windows-calculator/9wzdncrfhvn5?SilentAuth=1&wa=wsignin1.0&activetab=pivot:overviewtab");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("skype", "https://www.skype.com/en/get-skype/");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("notepad", "https://apps.microsoft.com/store/detail/notepad-for-windows-10/9NBLGGH4W20K?hl=en-us&gl=US");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("calendar", "https://apps.microsoft.com/store/detail/mail-and-calendar/9WZDNCRFHVQM?hl=en-us&gl=US");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("weather", "https://apps.microsoft.com/store/detail/msn-weather/9WZDNCRFJ3Q2?hl=en-gb&gl=GB");

            Application_Name__And__Application_Not_Found_Error_Download_Link.TryAdd("snip and sketch", "https://apps.microsoft.com/store/detail/snipping-tool/9MZ95KL8MR0L?hl=en-gb&gl=GB");
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
