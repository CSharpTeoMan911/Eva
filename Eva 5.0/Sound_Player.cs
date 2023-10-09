using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0.Properties
{
    public class Sound_Player
    {
        private System.Media.SoundPlayer AppExecutionSoundEffect = new System.Media.SoundPlayer("Sounds/App execution.wav");
        private System.Media.SoundPlayer AppTerminationSoundEffect = new System.Media.SoundPlayer("Sounds/App closing.wav");
        private System.Media.SoundPlayer ScreenshotExecutionSoundEffect = new System.Media.SoundPlayer("Sounds/Screenshot_Sound_Effect.wav");
        private System.Media.SoundPlayer Alarm_Sound_Effect = new System.Media.SoundPlayer("Sounds/AlarmSound.wav");
        private System.Media.SoundPlayer ErrorSoundEffect = new System.Media.SoundPlayer("Sounds/Privacy statement declined or mic not available.wav");
        private System.Media.SoundPlayer AppActivationSoundEffect = new System.Media.SoundPlayer("Sounds/Listen.wav");
        private System.Media.SoundPlayer ChatGPTNotificationSoundEffect = new System.Media.SoundPlayer("Sounds/Chat_GPT_Notification.wav");

        public enum Sounds
        {
            AppExecutionSoundEffect,
            AppTerminationSoundEffect,
            ScreenshotExecutionSoundEffect,
            Alarm_Sound_Effect,
            ErrorSoundEffect,
            AppActivationSoundEffect,
            ChatGPTNotificationSoundEffect
        }

        public async Task<bool> Play_Sound(Sounds sound)
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
                }
            }

            return true;
        }

        public Task<bool> Stop_Alarm()
        {
            try
            {
                Alarm_Sound_Effect.Stop();
            }
            catch { }

            return Task.FromResult(true);
        }


        public Task<bool> Dispose_Sound_Effects()
        {
            AppExecutionSoundEffect.Dispose();
            AppTerminationSoundEffect.Dispose();
            ScreenshotExecutionSoundEffect.Dispose();
            Alarm_Sound_Effect.Dispose();
            ErrorSoundEffect.Dispose();
            AppActivationSoundEffect.Dispose();
            ChatGPTNotificationSoundEffect.Dispose();

            return Task.FromResult(true);
        }
    }
}
