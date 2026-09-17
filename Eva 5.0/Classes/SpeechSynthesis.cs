using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        private static int processing = (int)State.Free;

        public static State GetState(int val) => (State)val;
        public static State GetState() => (State)processing;
        public static int StateToInt(State state) => (int)state;

        private static readonly string PiperPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                         "python", "piper", "piper.exe");

        private static readonly string ModelPath =
            Path.Combine(Environment.CurrentDirectory,
                         "python", "eva_voice",
                         "en_GB-cori-high.onnx");

        public static async Task Synthesis(Action action, string content, string app)
        {
            if (GetState() == State.Free)
            {
                if (await Settings.Get_Synthesis_Settings())
                {
                    Interlocked.MemoryBarrier();
                    Interlocked.Exchange(ref processing, StateToInt(State.Processing));

                    string outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".wav");
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = PiperPath,
                        Arguments = $"--model \"{ModelPath}\" --output_file \"{outputPath}\"",

                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    using Process process = new Process
                    {
                        StartInfo = psi
                    };

                    process.Start();

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

                    await process.StandardInput.WriteAsync(synthesis_builder.ToString());
                    process.StandardInput.Close();

                    string error = await process.StandardError.ReadToEndAsync();
                    process.WaitForExit();


                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Piper failed: {error}");
                    }

                    if (!File.Exists(outputPath) || new FileInfo(outputPath).Length == 0)
                        throw new Exception("Piper produced no audio.");

                    using (var audio = new FileStream(outputPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (var reader = new NAudio.Wave.WaveFileReader(audio))
                    using (var waveOut = new NAudio.Wave.WaveOutEvent())
                    {
                        waveOut.Init(reader);
                        waveOut.Play();

                        while (waveOut.PlaybackState == NAudio.Wave.PlaybackState.Playing) ;
                    }

                    File.Delete(outputPath);
                    Interlocked.Exchange(ref processing, StateToInt(State.Free));
                }
            }
        }
    }
}