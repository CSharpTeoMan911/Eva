using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Eva_5._0.Classes
{
    internal class MoonshineASR
    {
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly string enginePath = Path.Combine(Environment.CurrentDirectory, "python", "python.exe");
        private bool engineLoaded = false;
        private Process sttEngine;

        public MoonshineASR()
        {

        }

        private void TaskScheduler(string text)
        {
            Task.Run(async() =>
            {
                string[] words = text.Split(' ');
                int index = 0;
                int i = 0;

                while (i < text.Length && index < words.Length && !tokenSource.IsCancellationRequested)
                {
                    Interlocked.MemoryBarrier();

                    if (App.stateMachine.Speech_Recogniser_Listening == 1)
                    {
                        string word = words[index];
                        string current = text.Substring(i);

                        if (await App.stateMachine.nlp.PreProcessing(current))
                            break;


                        i += word.Length + 1;
                        index++;
                    }
                    else
                    {
                        StopEngine();
                        break;
                    }
                }
            });
        }


        public void StartEngine()
        {
            tokenSource = new CancellationTokenSource();

            List<string> procs = A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.Keys.ToList();
            List<string> exes = A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___E_x__Name.Keys.ToList();
            List<string> web_exes = A_p_l____And____P_r_o_c.commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.Keys.ToList();


            StringBuilder s = new StringBuilder();
            string[] context = procs.Concat(exes).Concat(web_exes).ToArray();
            for (int i = 0; i < context.Length; i++)
            {
                if(i != context.Length)
                    s.Append(context[i]).Append(' ');
                else
                    s.Append(context[i]);
            }



            if (engineLoaded)
                return;

            sttEngine = new Process();
            sttEngine.StartInfo.FileName = enginePath;
            sttEngine.StartInfo.Arguments = $"./python/Controller.py -c {s.ToString()}";
            sttEngine.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            sttEngine.StartInfo.UseShellExecute = false;
            sttEngine.StartInfo.CreateNoWindow = true;
            sttEngine.StartInfo.RedirectStandardOutput = true;
            sttEngine.StartInfo.RedirectStandardError = true;

            sttEngine.OutputDataReceived += (sender, e) =>
            {
                if (!tokenSource.IsCancellationRequested)
                {
                    Interlocked.MemoryBarrier();

                    if (App.stateMachine.Speech_Recogniser_Listening == 1)
                    {
                        if (!string.IsNullOrWhiteSpace(e.Data))
                        {

                            if (App.stateMachine.Speech_Recogniser_Listening == 0)
                            {
                                Interlocked.Exchange(ref App.stateMachine.Speech_Recogniser_Listening, 1);
                            }
                            else
                            {
                                TaskScheduler(e.Data);
                            }
                        }
                    }
                    else
                    {
                        if (engineLoaded)
                            StopEngine();
                    }
                }
            };

            sttEngine.ErrorDataReceived += (sender, e) =>
            {
                if (!tokenSource.IsCancellationRequested)
                {
                    StopEngine();
                }
            };

            sttEngine.Start();
            sttEngine.BeginOutputReadLine();
            sttEngine.BeginErrorReadLine();

            engineLoaded = true;
        }


        public void StopEngine()
        {
            try
            {
                Interlocked.MemoryBarrier();
                Interlocked.Exchange(ref App.stateMachine.Speech_Recogniser_Listening, 0);

                tokenSource.Cancel();
                engineLoaded = false;

                if (!sttEngine.HasExited)
                    sttEngine.Kill();
            }
            catch { }
        }
    }
}
