using System;

namespace Eva_5._0
{
    internal class Settings_File
    {
        public float Vosk_Sensitivity { get; set; } = 8.2f;
        public bool Sound_On { get; set; } = true;
        public bool Synthesis_On { get; set; }
        public string Gpt_Model { get; set; }
        public int ModelTemperature { get; set; } = 5;
        public string Open_AI_Chat_GPT_Key { get; set; } = String.Empty;
    }
}
