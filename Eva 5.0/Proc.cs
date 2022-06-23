using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Proc<ProcessType, Application, Content>
    {
        private System.Threading.Thread ParallelProcessing;




        public async Task<bool> ProcInitialisation(ProcessType process_type, Application application, Content content)
        {
            switch (process_type as string)
            {
                case "Online Process":
                    await OnlineProcesses(application as string, content as string);
                    break;

                case "System Process":
                    await SystemProcesses(application as string, content as string);
                    break;
            }

            return true;
        }


        private Task<bool> OnlineProcesses(string WebApplication, string SearchContent)
        {
            var AppExecutionSoundEffect = new System.Media.SoundPlayer("App execution.wav");

            ParallelProcessing = new System.Threading.Thread(() =>
            {
                try
                {
                    var settings = new Settings();
                    var SoundOrOff = settings.Get_Settings().Result;

                    switch (WebApplication)
                    {
                        case "youtube":

                            try
                            {

                                MainWindow.BeginExecutionAnimation = true;

                                using (var Youtube = new System.Diagnostics.Process())
                                {
                                    Youtube.StartInfo.FileName = "https://www.youtube.com/results?search_query=" + SearchContent;
                                    Youtube.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Youtube.StartInfo.UseShellExecute = true;
                                    Youtube.Start();
                                }



                                switch (SoundOrOff == "Enabled")
                                {
                                    case true:
                                        switch (System.IO.File.Exists(@"App execution.wav"))
                                        {
                                            case true:
                                                AppExecutionSoundEffect.Play();
                                                break;
                                        }
                                        break;
                                }

                                ParallelProcessing.Join();
                                ParallelProcessing.Abort();
                            }
                            catch { }
                            break;

                        case "netflix":

                            try
                            {
                                MainWindow.BeginExecutionAnimation = true;
                                using (var Netflix = new System.Diagnostics.Process())
                                {
                                    Netflix.StartInfo.FileName = "https://www.netflix.com/search?q=" + SearchContent;
                                    Netflix.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Netflix.StartInfo.UseShellExecute = true;
                                    Netflix.Start();
                                }

                                switch (SoundOrOff == "Enabled")
                                {
                                    case true:
                                        switch (System.IO.File.Exists(System.IO.Path.GetFullPath("App execution.wav")))
                                        {
                                            case true:
                                                AppExecutionSoundEffect.Play();
                                                break;
                                        }
                                        break;
                                }

                                ParallelProcessing.Join();
                                ParallelProcessing.Abort();
                            }
                            catch { }
                            break;

                        case "wikipedia":

                            try
                            {
                                MainWindow.BeginExecutionAnimation = true;
                                using (var Wikipedia = new System.Diagnostics.Process())
                                {
                                    Wikipedia.StartInfo.FileName = "https://en.wikipedia.org/wiki/" + SearchContent;
                                    Wikipedia.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Wikipedia.StartInfo.UseShellExecute = true;
                                    Wikipedia.Start();
                                }

                                switch (SoundOrOff == "Enabled")
                                {
                                    case true:
                                        switch (System.IO.File.Exists(@"App execution.wav"))
                                        {
                                            case true:
                                                AppExecutionSoundEffect.Play();
                                                break;
                                        }
                                        break;
                                }

                                ParallelProcessing.Join();
                                ParallelProcessing.Abort();
                            }
                            catch { }
                            break;

                        case "google":

                            try
                            {
                                MainWindow.BeginExecutionAnimation = true;
                                using (var Google = new System.Diagnostics.Process())
                                {
                                    Google.StartInfo.FileName = "https://www.google.com/search?q=" + SearchContent;
                                    Google.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Google.StartInfo.UseShellExecute = true;
                                    Google.Start();
                                }

                                switch (SoundOrOff == "Enabled")
                                {
                                    case true:
                                        switch (System.IO.File.Exists(@"App execution.wav"))
                                        {
                                            case true:
                                                AppExecutionSoundEffect.Play();
                                                break;
                                        }
                                        break;
                                }

                                ParallelProcessing.Join();
                                ParallelProcessing.Abort();
                            }
                            catch { }
                            break;

                        case "google news":

                            try
                            {
                                MainWindow.BeginExecutionAnimation = true;
                                using (var GoogleNews = new System.Diagnostics.Process())
                                {
                                    GoogleNews.StartInfo.FileName = "https://www.google.com/search?q=" + SearchContent + "&tbm=nws";
                                    GoogleNews.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    GoogleNews.StartInfo.UseShellExecute = true;
                                    GoogleNews.Start();
                                }

                                switch (SoundOrOff == "Enabled")
                                {
                                    case true:
                                        switch (System.IO.File.Exists(@"App execution.wav"))
                                        {
                                            case true:
                                                AppExecutionSoundEffect.Play();
                                                break;
                                        }
                                        break;
                                }

                                ParallelProcessing.Join();
                                ParallelProcessing.Abort();
                            }
                            catch { }
                            break;

                        case "ebay":

                            try
                            {
                                MainWindow.BeginExecutionAnimation = true;
                                using (var Ebay = new System.Diagnostics.Process())
                                {
                                    Ebay.StartInfo.FileName = "https://www.ebay.co.uk/sch/i.html?_nkw=" + SearchContent;
                                    Ebay.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                    Ebay.StartInfo.UseShellExecute = true;
                                    Ebay.Start();
                                }

                                switch (SoundOrOff == "Enabled")
                                {
                                    case true:
                                        switch (System.IO.File.Exists(@"App execution.wav"))
                                        {
                                            case true:
                                                AppExecutionSoundEffect.Play();
                                                break;
                                        }
                                        break;
                                }

                                ParallelProcessing.Join();
                                ParallelProcessing.Abort();
                            }
                            catch { }
                            break;
                    }
                }
                catch { }
            });
            ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
            ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
            ParallelProcessing.IsBackground = true;
            ParallelProcessing.Start();

            return Task.FromResult(true);
        }

        private Task<bool> SystemProcesses(string Application, string Process)
        {
            var AppExecutionSoundEffect = new System.Media.SoundPlayer("App execution.wav");
            var AppTerminationSoundEffect = new System.Media.SoundPlayer("App closing.wav");

            ParallelProcessing = new System.Threading.Thread(() =>
            {
                try
                {
                    var settings = new Settings();
                    var SoundOrOff = settings.Get_Settings().Result;

                    switch (Application)
                    {
                        case "chrome":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Chrome = new System.Diagnostics.Process())
                                        {
                                            Chrome.StartInfo.FileName = "chrome.exe";
                                            Chrome.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Chrome.StartInfo.UseShellExecute = true;
                                            Chrome.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var ChromeNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            ChromeNotDownloaded.StartInfo.FileName = "https://www.google.co.uk/chrome/";
                                            ChromeNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            ChromeNotDownloaded.StartInfo.UseShellExecute = true;
                                            ChromeNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("chrome"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "google chrome":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Chrome = new System.Diagnostics.Process())
                                        {
                                            Chrome.StartInfo.FileName = "chrome.exe";
                                            Chrome.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Chrome.StartInfo.UseShellExecute = true;
                                            Chrome.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var ChromeNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            ChromeNotDownloaded.StartInfo.FileName = "https://www.google.co.uk/chrome/";
                                            ChromeNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            ChromeNotDownloaded.StartInfo.UseShellExecute = true;
                                            ChromeNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("chrome"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "firefox":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Firefox = new System.Diagnostics.Process())
                                        {
                                            Firefox.StartInfo.FileName = "firefox.exe";
                                            Firefox.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Firefox.StartInfo.UseShellExecute = true;
                                            Firefox.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var FirefoxNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            FirefoxNotDownloaded.StartInfo.FileName = "https://www.mozilla.org/en-GB/firefox/new/";
                                            FirefoxNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            FirefoxNotDownloaded.StartInfo.UseShellExecute = true;
                                            FirefoxNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("firefox"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "mozila firefox":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Firefox = new System.Diagnostics.Process())
                                        {
                                            Firefox.StartInfo.FileName = "firefox.exe";
                                            Firefox.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Firefox.StartInfo.UseShellExecute = true;
                                            Firefox.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var FirefoxNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            FirefoxNotDownloaded.StartInfo.FileName = "https://www.mozilla.org/en-GB/firefox/new/";
                                            FirefoxNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            FirefoxNotDownloaded.StartInfo.UseShellExecute = true;
                                            FirefoxNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("firefox"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "edge":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Edge = new System.Diagnostics.Process())
                                        {
                                            Edge.StartInfo.FileName = "msedge.exe";
                                            Edge.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Edge.StartInfo.UseShellExecute = true;
                                            Edge.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var EdgeNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            EdgeNotDownloaded.StartInfo.FileName = "https://www.microsoft.com/en-us/edge";
                                            EdgeNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            EdgeNotDownloaded.StartInfo.UseShellExecute = true;
                                            EdgeNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("msedge"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "microsoft edge":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Edge = new System.Diagnostics.Process())
                                        {
                                            Edge.StartInfo.FileName = "msedge.exe";
                                            Edge.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Edge.StartInfo.UseShellExecute = true;
                                            Edge.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var EdgeNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            EdgeNotDownloaded.StartInfo.FileName = "https://www.microsoft.com/en-us/edge";
                                            EdgeNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            EdgeNotDownloaded.StartInfo.UseShellExecute = true;
                                            EdgeNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("msedge"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "opera":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Opera = new System.Diagnostics.Process())
                                        {
                                            Opera.StartInfo.FileName = "Opera.exe";
                                            Opera.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Opera.StartInfo.UseShellExecute = true;
                                            Opera.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var OperaNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            OperaNotDownloaded.StartInfo.FileName = "https://www.opera.com/gx";
                                            OperaNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            OperaNotDownloaded.StartInfo.UseShellExecute = true;
                                            OperaNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("Opera"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "calculator":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Calculator = new System.Diagnostics.Process())
                                        {
                                            Calculator.StartInfo.FileName = "calc.exe";
                                            Calculator.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Calculator.StartInfo.UseShellExecute = true;
                                            Calculator.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var CalculatorNotDownloaded = new System.Diagnostics.Process())
                                        {
                                            CalculatorNotDownloaded.StartInfo.FileName = "https://www.microsoft.com/en-gb/p/windows-calculator/9wzdncrfhvn5?SilentAuth=1&wa=wsignin1.0&activetab=pivot:overviewtab";
                                            CalculatorNotDownloaded.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            CalculatorNotDownloaded.StartInfo.UseShellExecute = true;
                                            CalculatorNotDownloaded.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("Calculator"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;



                        case "microsoft paint":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Paint = new System.Diagnostics.Process())
                                        {
                                            Paint.StartInfo.FileName = "mspaint.exe";
                                            Paint.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Paint.StartInfo.UseShellExecute = true;
                                            Paint.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("mspaint"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "paint":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Paint = new System.Diagnostics.Process())
                                        {
                                            Paint.StartInfo.FileName = "mspaint.exe";
                                            Paint.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Paint.StartInfo.UseShellExecute = true;
                                            Paint.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("mspaint"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "file explorer":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var FileExplorer = new System.Diagnostics.Process())
                                        {
                                            FileExplorer.StartInfo.FileName = "explorer.exe";
                                            FileExplorer.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            FileExplorer.StartInfo.UseShellExecute = true;
                                            FileExplorer.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "gmail":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Gmail = new System.Diagnostics.Process())
                                        {
                                            Gmail.StartInfo.FileName = "https://mail.google.com/mail/";
                                            Gmail.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Gmail.StartInfo.UseShellExecute = true;
                                            Gmail.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;

                        case "skype":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Skype = new System.Diagnostics.Process())
                                        {
                                            Skype.StartInfo.FileName = "skype.exe";
                                            Skype.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Skype.StartInfo.UseShellExecute = true;
                                            Skype.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("skype"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;


                        case "notepad":
                            switch (Process)
                            {
                                case "open":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        using (var Notepad = new System.Diagnostics.Process())
                                        {
                                            Notepad.StartInfo.FileName = "notepad.exe";
                                            Notepad.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                            Notepad.StartInfo.UseShellExecute = true;
                                            Notepad.Start();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App execution.wav"))
                                                {
                                                    case true:
                                                        AppExecutionSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }

                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;

                                case "close":

                                    try
                                    {
                                        MainWindow.BeginExecutionAnimation = true;
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("Notepad"))
                                        {
                                            p.Kill();
                                        }

                                        switch (SoundOrOff == "Enabled")
                                        {
                                            case true:
                                                switch (System.IO.File.Exists(@"App closing.wav"))
                                                {
                                                    case true:
                                                        AppTerminationSoundEffect.Play();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    catch { }
                                    ParallelProcessing.Join();
                                    ParallelProcessing.Abort();
                                    break;
                            }
                            break;
                    }
                }
                catch { }
            });

            ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
            ParallelProcessing.IsBackground = true;
            ParallelProcessing.Priority = System.Threading.ThreadPriority.Highest;
            ParallelProcessing.Start();

            return Task.FromResult(true);
        }

        ~Proc()
        {
            ParallelProcessing = null;

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(0, GCCollectionMode.Forced, true, true);
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(1, GCCollectionMode.Forced, true, true);
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}
