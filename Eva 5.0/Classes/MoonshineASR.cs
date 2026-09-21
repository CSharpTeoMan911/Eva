#nullable enable

using Eva_5._0.Properties;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eva_5._0.Classes
{
    internal class MoonshineASR
    {
        private enum MicState
        {
            Active,
            Mute
        }

        private MMDeviceEnumerator? enumerator;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly string enginePath = Path.Combine(Environment.CurrentDirectory, "python", "python.exe");
        private bool engineLoaded = false;
        private bool engineStarted = false;
        private bool dispatcherRunning = false;
        private Process? sttEngine;

        private ConcurrentQueue<Func<bool>> tasks = new ConcurrentQueue<Func<bool>>();

        public MoonshineASR()
        {

        }


        private void ChangeMicVolume(MicState state)
        {
            try
            {
                if (enumerator != null)
                {
                    MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);

                    if (device != null)
                    {
                        AudioSessionManager sessionManager = device.AudioSessionManager;
                        AudioSessionControl sessions = sessionManager.AudioSessionControl;

                        for (int i = 0; i < device.AudioSessionManager.Sessions.Count; i++)
                        {
                            var session = device.AudioSessionManager.Sessions[i];
                            if (session.GetProcessID == sttEngine?.Id)
                            {
                                session.SimpleAudioVolume.Mute = state == MicState.Mute;
                            }
                        }
                    }
                }
            }
            catch { }
        }


        private void TaskDispatcher()
        {
            dispatcherRunning = true;

            Task.Run(() =>
            {
                while (dispatcherRunning && !tokenSource.IsCancellationRequested)
                {
                    Interlocked.MemoryBarrier();
                    Interlocked.SpeculationBarrier();

                    Func<bool>? task = null;
                    if (engineLoaded && sttEngine != null)
                    {
                        if (SpeechSynthesis.GetState() == SpeechSynthesis.State.Free)
                        {
                            if (tasks.TryDequeue(out task))
                            {
                                _ = task?.Invoke();
                            }

                            ChangeMicVolume(MicState.Active);
                        }
                        else
                        {
                            ChangeMicVolume(MicState.Mute);
                        }
                    }
                }
            });
        }

        private void TaskScheduler(string text)
        {
            Task.Run(async() =>
            {
                DateTime time = DateTime.UtcNow;
                bool initialised = false;

                string[] words = text.Split(' ');
                int index = 0;
                int i = 0;

                while (i < text.Length && index < words.Length && engineStarted && !tokenSource.IsCancellationRequested)
                {
                    Interlocked.MemoryBarrier();
                    Interlocked.SpeculationBarrier();

                    string word = words[index];
                    string current = text.Substring(i);

                    if (engineLoaded != true)
                    {
                        if (current == "[ loaded ]")
                        {
                            await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Sound_Player.Sounds.AppActivationSoundEffect);
                            Interlocked.Exchange(ref App.stateMachine.Speech_Recogniser_Listening, 1);
                            engineLoaded = true;
                            return;
                        }
                    }
                    else
                    {
                        if (App.stateMachine.Speech_Recogniser_Listening == 1)
                        {
                            Func<bool> result = await App.stateMachine.nlp.PreProcessing(current);

                            if (result != null)
                            {
                                if (!initialised || (DateTime.UtcNow - time).TotalMilliseconds > 100 && SpeechSynthesis.GetState() == SpeechSynthesis.State.Free)
                                {
                                    initialised = true;
                                    time = DateTime.UtcNow;
                                    tasks.Enqueue(result);
                                }
                                return;
                            }
                        }
                        else
                        {
                            StopEngine();
                            return;
                        }
                    }

                    i += word.Length + 1;
                    index++;
                }
            });
        }


        public void StartEngine()
        {
            if (!engineStarted)
            {
                enumerator = new MMDeviceEnumerator();
                tokenSource = new CancellationTokenSource();

                List<string> procs = A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.Keys.ToList();
                List<string> exes = A_p_l____And____P_r_o_c.commands.A_p_l_Name__And__A_p_l___E_x__Name.Keys.ToList();
                List<string> web_exes = A_p_l____And____P_r_o_c.commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.Keys.ToList();


                StringBuilder s = new StringBuilder();
                string[] context = procs.Concat(exes).Concat(web_exes).ToArray();
                for (int i = 0; i < context.Length; i++)
                {
                    if (i != context.Length)
                        s.Append(context[i]).Append(',');
                    else
                        s.Append(context[i]);
                }



                if (engineLoaded)
                    return;

                TaskDispatcher();

                sttEngine = new Process();
                sttEngine.StartInfo.FileName = enginePath;
                sttEngine.StartInfo.Arguments = $"./python/Controller.py";
                sttEngine.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                sttEngine.StartInfo.UseShellExecute = false;
                sttEngine.StartInfo.CreateNoWindow = true;
                sttEngine.StartInfo.RedirectStandardOutput = true;
                sttEngine.StartInfo.RedirectStandardError = true;
                sttEngine.StartInfo.RedirectStandardInput = false;

                sttEngine.Exited += SttEngine_Exited;

                sttEngine.OutputDataReceived += async(sender, e) =>
                {
                    if (!tokenSource.IsCancellationRequested && SpeechSynthesis.GetState() == SpeechSynthesis.State.Free)
                    {
                        ChangeMicVolume(MicState.Active);
                        Interlocked.MemoryBarrier();
                        Interlocked.SpeculationBarrier();

                        if (engineStarted)
                        {
                            if (!string.IsNullOrWhiteSpace(e.Data))
                            {
                                TaskScheduler(e.Data);
                            }
                        }
                    }
                    else
                    {
                        ChangeMicVolume(MicState.Mute);
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

                engineStarted = true;
            }
        }

        private void SttEngine_Exited(object sender, EventArgs e) => StopEngine();

        public void StopEngine()
        {
            if (engineStarted)
            {
                try
                {
                    Interlocked.MemoryBarrier();
                    Interlocked.SpeculationBarrier();
                    Interlocked.Exchange(ref App.stateMachine.Speech_Recogniser_Listening, 0);

                    enumerator?.Dispose();
                    tokenSource.Cancel();
                    engineStarted = false;
                    engineLoaded = false;
                    dispatcherRunning = false;

                    if (sttEngine != null)
                        if (!sttEngine.HasExited)
                            sttEngine.Kill();
                }
                catch { }
            }
        }
    }
}
