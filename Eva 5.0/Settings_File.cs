using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Settings_File
    {
        //settings_File.Sound_On = true;
        //    settings_File.Open_AI_Chat_GPT_Key = String.Empty;
        //    settings_File.ModelTemperature = 5;
        //    settings_File.Vosk_Sensitivity = 8;
        public float Vosk_Sensitivity { get; set; } = 8.4f;
        public bool Sound_On { get; set; } = true;
        public bool Synthesis_On { get; set; } 
        public string Gpt_Model { get; set; }
        public int ModelTemperature { get; set; } = 5;
        public string Open_AI_Chat_GPT_Key { get; set; } = String.Empty;
        public string Online_Speech_Recognition_Language { get; set; } = "en-GB";
    }
}
