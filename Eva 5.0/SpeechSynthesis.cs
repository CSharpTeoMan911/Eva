using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace Eva_5._0
{
    internal class SpeechSynthesis
    {
        private static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public enum Action
        {
            Opening,
            Closing,
            Searching,
            Setting,
            Taking
        }

        public static async Task Synthesis(Action action, string content, string app)
        {
            SpeechSynthesisStream stream = null;

            try
            {
                foreach (VoiceInformation voice in SpeechSynthesizer.AllVoices)
                    if (voice.Language == "en-GB" || voice.Language == "en-us")
                        if (voice.Gender == VoiceGender.Female)
                        {
                            synthesizer.Voice = voice;
                            break;
                        }

                if (synthesizer.Voice.Gender != VoiceGender.Female)
                {
                    ErrorWindow OpenPermissionDeclinedWindow = new ErrorWindow("Unsupported Voice");
                    OpenPermissionDeclinedWindow.Show();
                }
                else
                {
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


                    stream = await synthesizer.SynthesizeTextToStreamAsync(synthesis_builder.ToString());
                    await A_p_l____And____P_r_o_c.sound_player.Play_Synthesis(stream.AsStream());
                }
            }
            catch
            {

            }
            finally
            {
                stream?.Dispose();
            }
        }
    }
}
