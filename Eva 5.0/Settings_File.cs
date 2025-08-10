using System;

namespace Eva_5._0
{
    internal class Settings_File
    {
        public int Timeout { get; set; } = 4;
        public float Vosk_Sensitivity { get; set; } = 8.3f;
        public A_p_l____And____P_r_o_c.SpeechRecognitionOperation Operation { get; set; } = A_p_l____And____P_r_o_c.SpeechRecognitionOperation.FormFilling;
        public bool Sound_On { get; set; } = true;
        public bool Synthesis_On { get; set; }
        public string Gpt_Model { get; set; }
        public int ModelTemperature { get; set; } = 5;
        public string Open_AI_Chat_GPT_Key { get; set; } = String.Empty;
        public string Online_Speech_Recognition_Language { get; set; } = "en-GB";
        public int SpoolingTime { get; set; } = 550;
    }
}
