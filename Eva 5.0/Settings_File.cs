﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Settings_File
    {
        public bool Sound_On { get; set; }
        public bool Synthesis_On { get; set; }
        public string Gpt_Model { get; set; }
        public int ModelTemperature { get; set; }
        public string Open_AI_Chat_GPT_Key { get; set; }
        public string Online_Speech_Recognition_Language { get; set; } = "en-GB";
    }
}
