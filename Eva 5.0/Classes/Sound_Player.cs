using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Buffers;
using System.IO;
using System.Threading.Tasks;

namespace Eva_5._0.Properties
{
    public class Sound_Player
    {
        private MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

        private WaveOutEvent wave_player;

        private System.Timers.Timer timer;

        private string deviceID;

        private System.Media.SoundPlayer SpeechSynthesisStream = new System.Media.SoundPlayer();
        private System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("Sounds/App execution.wav");
        private System.Media.SoundPlayer AppTerminationSoundEffect = new System.Media.SoundPlayer("Sounds/App closing.wav");
        private System.Media.SoundPlayer ScreenshotExecutionSoundEffect = new System.Media.SoundPlayer("Sounds/Screenshot_Sound_Effect.wav");
        private System.Media.SoundPlayer Alarm_Sound_Effect = new System.Media.SoundPlayer("Sounds/AlarmSound.wav");
        private System.Media.SoundPlayer ErrorSoundEffect = new System.Media.SoundPlayer("Sounds/Privacy statement declined or mic not available.wav");
        private System.Media.SoundPlayer AppActivationSoundEffect = new System.Media.SoundPlayer("Sounds/Listen.wav");
        private System.Media.SoundPlayer ChatGPTNotificationSoundEffect = new System.Media.SoundPlayer("Sounds/Chat_GPT_Notification.wav");
        private System.Media.SoundPlayer ChatGPTActivationSoundEffect = new System.Media.SoundPlayer("Sounds/ChatGpt Activated.wav");
        private System.Media.SoundPlayer ChatGPTDeactivationSoundEffect = new System.Media.SoundPlayer("Sounds/ChatGpt Deactivation.wav");

        public enum Sounds
        {
            AppExecutionSoundEffect,
            AppTerminationSoundEffect,
            ScreenshotExecutionSoundEffect,
            Alarm_Sound_Effect,
            ErrorSoundEffect,
            AppActivationSoundEffect,
            ChatGPTNotificationSoundEffect,
            ChatGPTActivationSoundEffect,
            ChatGPTDeactivationSoundEffect
        }


        public void PlayBackgroundNoise()
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer(100);
                timer.Elapsed += PlugAndPlayDetection;
                timer.Start();
            }
        }

        private void PlugAndPlayDetection(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                MMDeviceCollection devices = GetAudioDevices();
                if (devices.Count > 0)
                {
                    MMDevice current_device = devices[devices.Count - 1];
                    WaveFormat audioFormat = current_device.AudioClient.MixFormat;

                    if (current_device.ID != deviceID)
                    {
                        deviceID = current_device.ID;

                        if (wave_player?.PlaybackState == PlaybackState.Playing)
                        {
                            wave_player?.Stop();
                        }

                        wave_player?.Dispose();

                        PlayWhiteNoise(audioFormat);
                    }
                }
            }
            catch { }
        }

        private void PlayWhiteNoise(WaveFormat wave_format)
        {
            try
            {
                wave_player = new WaveOutEvent();

                using (MemoryPool<byte> memoryPool = MemoryPool<byte>.Shared)
                {
                    byte[] audio = memoryPool.Rent(wave_format.AverageBytesPerSecond).Memory.ToArray();
                    BufferedWaveProvider bufferedWave = new BufferedWaveProvider(wave_format);
                    bufferedWave.AddSamples(audio, 0, audio.Length);
                    wave_player.DeviceNumber = -1;
                    wave_player.Init(bufferedWave);
                    wave_player.Play();
                }
            }
            catch { }
        }

        private MMDeviceCollection GetAudioDevices() => enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);


        public async Task Play_Sound(Sounds sound)
        {
            bool SoundOrOff = await Eva_5._0.Settings.Get_Sound_Settings();

            if (SoundOrOff == true)
            {
                switch (sound)
                {
                    case Sounds.AppExecutionSoundEffect:
                        if (File.Exists(@"Sounds/App execution.wav"))
                        {
                            AppExecutionSoundEffect.Play();
                        }
                        break;
                    case Sounds.AppTerminationSoundEffect:
                        if (File.Exists(@"Sounds/App closing.wav"))
                        {
                            AppTerminationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ScreenshotExecutionSoundEffect:
                        if (File.Exists(@"Sounds/Screenshot_Sound_Effect.wav"))
                        {
                            ScreenshotExecutionSoundEffect.Play();
                        }
                        break;
                    case Sounds.Alarm_Sound_Effect:
                        if (File.Exists(@"Sounds/AlarmSound.wav"))
                        {
                            Alarm_Sound_Effect.PlayLooping();
                        }
                        break;
                    case Sounds.ErrorSoundEffect:
                        if (File.Exists(@"Sounds/Privacy statement declined or mic not available.wav"))
                        {
                            ErrorSoundEffect.Play();
                        }
                        break;
                    case Sounds.AppActivationSoundEffect:
                        if (File.Exists(@"Sounds/Listen.wav"))
                        {
                            AppActivationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ChatGPTNotificationSoundEffect:
                        if (File.Exists(@"Sounds/Chat_GPT_Notification.wav"))
                        {
                            ChatGPTNotificationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ChatGPTActivationSoundEffect:
                        if (File.Exists(@"Sounds/ChatGpt Activated.wav"))
                        {
                            ChatGPTActivationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ChatGPTDeactivationSoundEffect:
                        if (File.Exists(@"Sounds/ChatGpt Deactivation.wav"))
                        {
                            ChatGPTDeactivationSoundEffect.Play();
                        }
                        break;
                }
            }
        }

        public async Task Play_Synthesis(Stream stream)
        {
            bool SoundOrOff = await Eva_5._0.Settings.Get_Sound_Settings();
            bool Sythesis_On = await Eva_5._0.Settings.Get_Synthesis_Settings();

            if (SoundOrOff == true)
            {
                if (Sythesis_On == true)
                {
                    SpeechSynthesisStream.Stream = stream;
                    SpeechSynthesisStream.PlaySync();
                }
            }
        }

        public void Stop_Alarm()
        {
            try
            {
                Alarm_Sound_Effect?.Stop();
            }
            catch { }
        }


        public void Dispose_Sound_Effects()
        {
            try
            {
                wave_player?.Stop();
                wave_player?.Dispose();
                timer?.Stop();
                timer?.Dispose();
                enumerator?.Dispose();
                AppExecutionSoundEffect?.Dispose();
                AppTerminationSoundEffect?.Dispose();
                ScreenshotExecutionSoundEffect?.Dispose();
                Alarm_Sound_Effect?.Dispose();
                ErrorSoundEffect?.Dispose();
                AppActivationSoundEffect?.Dispose();
                ChatGPTNotificationSoundEffect?.Dispose();
            }
            catch { }
        }
    }
}
