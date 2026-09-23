using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace Eva_5._0
{
    internal class SpeechSynthesis
    {
        public enum State
        {
            Free,
            Processing
        }

        public enum Action
        {
            Opening,
            Closing,
            Searching,
            Setting,
            Taking
        }

        private static Process? synthesiser;
        private static int processing = (int)State.Free;

        public static State GetState(int val) => (State)val;
        public static State GetState() => (State)processing;
        public static int StateToInt(State state) => (int)state;

        private static bool engineLoaded;
        private static readonly string PythonPath = Path.Combine(Environment.CurrentDirectory, "python", "python.exe");
        private static readonly string PiperPath = Path.Combine(Environment.CurrentDirectory, "python", "PiperController.py");
        public static async void StartSynthesiser()
        {
            Interlocked.MemoryBarrier();
            Interlocked.SpeculationBarrier();

            if (GetState() == State.Free && !engineLoaded)
            {
                if (await Settings.Get_Synthesis_Settings())
                {
                    Interlocked.Exchange(ref processing, StateToInt(State.Processing));

                    synthesiser = new Process();
                    synthesiser.StartInfo.FileName = PythonPath;
                    synthesiser.StartInfo.Arguments = $"\"{PiperPath}\"";
                    synthesiser.StartInfo.UseShellExecute = false;
                    synthesiser.StartInfo.RedirectStandardInput = true;
                    synthesiser.StartInfo.RedirectStandardOutput = true;
                    synthesiser.StartInfo.RedirectStandardError = true;
                    synthesiser.StartInfo.CreateNoWindow = true;
                    synthesiser.Exited += Synthesiser_Exited;
                    synthesiser.OutputDataReceived += Synthesiser_OutputDataReceived;
                    synthesiser.ErrorDataReceived += Synthesiser_ErrorDataReceived;
                    synthesiser.Start();

                    synthesiser.BeginOutputReadLine();
                    synthesiser.BeginErrorReadLine();

                    DateTime start = DateTime.UtcNow;
                    while ((DateTime.UtcNow - start).TotalMinutes < 5)
                    {
                        if (engineLoaded)
                        {
                            Interlocked.Exchange(ref processing, StateToInt(State.Free));
                            return;
                        }
                    }

                    throw new Exception("Synthesiser fatal error");
                }
            }
        }

        private static void Synthesiser_Exited(object sender, EventArgs e) => StopSynthesiser();

        private static void Synthesiser_ErrorDataReceived(object sender, DataReceivedEventArgs e) => StopSynthesiser();

        public static void StopSynthesiser()
        {
            try
            {
                if (engineLoaded)
                {
                    Interlocked.MemoryBarrier();
                    Interlocked.SpeculationBarrier();
                    Interlocked.Exchange(ref processing, StateToInt(State.Free));
                    engineLoaded = false;

                    if(synthesiser != null)
                        if(synthesiser.HasExited)
                            synthesiser.Kill();
                }
            }
            catch { }
        }

        private static void Synthesiser_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Interlocked.MemoryBarrier();
            Interlocked.SpeculationBarrier();

            string result = e.Data;
            if (!engineLoaded && result == "[ loaded ]")
            {
                engineLoaded = true;
            }
            else
            {
                if (engineLoaded)
                {
                    if (result == "[ Synthesis finished ]")
                    {
                        Interlocked.Exchange(ref processing, StateToInt(State.Free));
                    }
                }
            }
        }

        public static async Task Synthesis(Action action, string content, string app)
        {
            try
            {
                Interlocked.MemoryBarrier();
                Interlocked.SpeculationBarrier();

                if (GetState() == State.Free && synthesiser != null && engineLoaded == true)
                {
                    if (await Settings.Get_Synthesis_Settings())
                    {
                        Interlocked.Exchange(ref processing, StateToInt(State.Processing));

                        StringBuilder synthesis_builder = new StringBuilder();

                        if (content != null)
                        {
                            synthesis_builder.Append(action.ToString());
                            synthesis_builder.Append(content);
                            synthesis_builder.Append(" on ");
                            synthesis_builder.Append(app);
                        }
                        else
                        {
                            synthesis_builder.Append(action.ToString());
                            synthesis_builder.Append(" ");
                            synthesis_builder.Append(app);
                        }

                        await synthesiser.StandardInput.WriteLineAsync(synthesis_builder.ToString());

                        DateTime time = DateTime.UtcNow;
                        while ((DateTime.UtcNow - time).TotalMinutes < 3 && processing == StateToInt(State.Processing));
                    }
                }
            }
            catch 
            {
                StopSynthesiser();
            }
        }
    }
}