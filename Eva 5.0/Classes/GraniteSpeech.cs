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

#nullable enable
namespace Eva_5._0.Classes
{
    internal class GraniteSpeech
    {
        private enum MicState
        {
            Active,
            Mute
        }


        private StringBuilder builder = new StringBuilder();
        private MMDeviceEnumerator? enumerator;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly string enginePath = Path.Combine(Environment.CurrentDirectory, "python", "python.exe");
        private bool engineLoaded = false;
        private bool engineStarted = false;
        private bool dispatcherRunning = false;
        private Process? sttEngine;

        private MicState micState = MicState.Active;
        private DateTime time = DateTime.UtcNow;
        private const double RESULT_TIMEOUT = 3000;



        private ConcurrentQueue<Func<bool>> tasks = new ConcurrentQueue<Func<bool>>();


        private void ChangeMicVolume(MicState state)
        {
            try
            {
                if (sttEngine != null)
                {
                    if (enumerator != null && engineLoaded && !sttEngine.HasExited)
                    {
                        MMDevice? device = enumerator?.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);

                        if (device != null)
                        {
                            AudioSessionManager sessionManager = device.AudioSessionManager;
                            AudioSessionControl sessions = sessionManager.AudioSessionControl;

                            for (int i = 0; i < device.AudioSessionManager.Sessions.Count; i++)
                            {
                                var session = device.AudioSessionManager.Sessions[i];
                                if (session.GetProcessID == sttEngine?.Id)
                                {
                                    micState = state;
                                    session.SimpleAudioVolume.Mute = state == MicState.Mute;
                                }
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

            Thread execution_thread = new Thread(() =>
            {
                while (dispatcherRunning && !tokenSource.IsCancellationRequested)
                {
                    if ((DateTime.UtcNow - time).TotalMilliseconds >= RESULT_TIMEOUT)
                    {
                        builder.Clear();
                    }

                    Interlocked.MemoryBarrier();
                    Interlocked.SpeculationBarrier();
                    Func<bool>? command = null;
                    if (engineLoaded && sttEngine != null)
                    {
                        if (SpeechSynthesis.GetState() == SpeechSynthesis.State.Free)
                        {
                            if (tasks.TryDequeue(out command))
                            {
                                _ = command?.Invoke();
                            }

                            if (micState == MicState.Mute)
                                ChangeMicVolume(MicState.Active);
                        }
                        else
                        {
                            if (micState == MicState.Mute)
                                ChangeMicVolume(MicState.Mute);
                        }
                    }
                }
            });

            // Set the thread as a background thread, set the .COM thread resource sharing as self-contained,
            // and set the thread priority to the highest level. When the thread is set as a high-priority foreground thread,
            // it will have a higher chance of being scheduled for execution by the operating system, which can help reduce
            // latency and improve responsiveness, even during high CPU usage.
            execution_thread.IsBackground = true;
            execution_thread.SetApartmentState(ApartmentState.MTA);
            execution_thread.Priority = ThreadPriority.Highest;
            execution_thread.Start();
        }

        private void TaskScheduler(string text)
        {
            Task.Run(async () =>
            {
                bool initialised = false;
                int i = 0;

                string[] words = text.Split(' ');

                while (i < words.Length && engineStarted && !tokenSource.IsCancellationRequested)
                {
                    string current = String.Empty;

                    for (int j = i; j < words.Length; j++)
                    {
                        current += j != words.Length - 1 ? words[j] + " " : words[j] + String.Empty;
                    }

                    if (engineLoaded != true)
                    {
                        if (current.Contains("[ loaded ]"))
                        {
                            await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Sound_Player.Sounds.AppActivationSoundEffect);
                            Interlocked.MemoryBarrier();
                            Interlocked.SpeculationBarrier();
                            Interlocked.Exchange(ref App.stateMachine.Speech_Recogniser_Listening, 1);
                            engineLoaded = true;
                            return;
                        }
                    }
                    else
                    {
                        if (App.stateMachine.Speech_Recogniser_Listening == 1)
                        {
                            Func<bool> result = await App.stateMachine.nlu.PreProcessing(current);

                            if (result != null)
                            {
                                if (!initialised || SpeechSynthesis.GetState() == SpeechSynthesis.State.Free)
                                {
                                    initialised = true;
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

                    i++;
                }
            });
        }

        public void StartEngine()
        {
            if (!engineStarted)
            {
                enumerator = new MMDeviceEnumerator();
                tokenSource = new CancellationTokenSource();


                if (engineLoaded)
                    return;

                TaskDispatcher();

                sttEngine = new Process();
                sttEngine.StartInfo.FileName = enginePath;
                sttEngine.StartInfo.Arguments = $"./python/granite-speech.py";
                sttEngine.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                sttEngine.StartInfo.UseShellExecute = false;
                sttEngine.StartInfo.CreateNoWindow = true;
                sttEngine.StartInfo.RedirectStandardOutput = true;
                sttEngine.StartInfo.RedirectStandardError = true;
                sttEngine.StartInfo.RedirectStandardInput = false;
                sttEngine.ErrorDataReceived += ((obj, e) =>
                {
                    Debug.WriteLine(e.Data);
                });
                sttEngine.Exited += SttEngine_Exited;
                sttEngine.EnableRaisingEvents = true;


                string res_pattern = "[Result: ";

                sttEngine.OutputDataReceived += async (sender, e) =>
                {
                    if (!tokenSource.IsCancellationRequested && SpeechSynthesis.GetState() == SpeechSynthesis.State.Free && engineStarted)
                    {
                        if (e.Data != null)
                        {
                            int result_length = e.Data.Length - (res_pattern.Length + 1);
                            string result = (e.Data.IndexOf(res_pattern) == 0 && e.Data[e.Data.Length - 1] == ']' ? e.Data.Substring(res_pattern.Length, result_length) : String.Empty).ToLower();

                            ChangeMicVolume(MicState.Active);
                            Interlocked.MemoryBarrier();
                            Interlocked.SpeculationBarrier();


                            if (engineStarted)
                            {
                                if (App.stateMachine.gptProcess == 0)
                                {
                                    if (!string.IsNullOrWhiteSpace(result))
                                    {
                                        time = DateTime.UtcNow;
                                        switch (builder.Length > 0)
                                        {
                                            case true:
                                                builder.Append(' ').Append(result);
                                                break;

                                            case false:
                                                builder.Append(result);
                                                break;
                                        }

                                        string accumulated_result= builder.ToString();

                                        Debug.WriteLine($"Accumulated result: {accumulated_result}");
                                        TaskScheduler(accumulated_result);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ChangeMicVolume(MicState.Mute);
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
