using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0.Properties
{
    public class Sound_Player
    {
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

        public async Task Play_Sound(Sounds sound)
        {
            bool SoundOrOff = await Eva_5._0.Settings.Get_Sound_Settings();

            if (SoundOrOff == true)
            {
                switch(sound)
                {
                    case Sounds.AppExecutionSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/App execution.wav"))
                        {
                            AppExecutionSoundEffect.Play();
                        }
                        break;
                    case Sounds.AppTerminationSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/App closing.wav"))
                        {
                            AppTerminationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ScreenshotExecutionSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/Screenshot_Sound_Effect.wav"))
                        {
                            ScreenshotExecutionSoundEffect.Play();
                        }
                        break;
                    case Sounds.Alarm_Sound_Effect:
                        if (System.IO.File.Exists(@"Sounds/AlarmSound.wav"))
                        {
                            Alarm_Sound_Effect.PlayLooping();
                        }
                        break;
                    case Sounds.ErrorSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/Privacy statement declined or mic not available.wav"))
                        {
                            ErrorSoundEffect.Play();
                        }
                        break;
                    case Sounds.AppActivationSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/Listen.wav"))
                        {
                            AppActivationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ChatGPTNotificationSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/Chat_GPT_Notification.wav"))
                        {
                            ChatGPTNotificationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ChatGPTActivationSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/ChatGpt Activated.wav"))
                        {
                            ChatGPTActivationSoundEffect.Play();
                        }
                        break;
                    case Sounds.ChatGPTDeactivationSoundEffect:
                        if (System.IO.File.Exists(@"Sounds/ChatGpt Deactivation.wav"))
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
