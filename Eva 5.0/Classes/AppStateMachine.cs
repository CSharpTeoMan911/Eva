using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0.Classes
{
    internal class AppStateMachine
    {
        public bool Cropped = true;
        public bool invisibility_mode;
        public bool bring_to_top;
        public DateTime? Speech_Recogniser_Activation_Delay_Detector = null;
        public readonly double Speech_Recogniser_Activation_Delay = 0.2;
        public bool chatgpt_mode_enabled = false;
        public long Speech_Recogniser_Listening;
        public long BeginExecutionAnimation;
        public long Speech_Detected;
        public long Window_Minimised;
        public long Online_Speech_Recogniser_Disabled;
        public Windows.Media.SpeechRecognition.SpeechRecognizerState Online_Speech_Recogniser_State = Windows.Media.SpeechRecognition.SpeechRecognizerState.Idle;
        public long Wake_Word_Detected;
        public DateTime? speech_recognition_timeout;
        public bool MainWindowIsClosing;
        public long OnOff;
        public Proc proc = new Proc();
        public Wake_Word_Engine wakeWordEngine = new Wake_Word_Engine();
        public Natural_Language_Processing nlp = new Natural_Language_Processing();
        public MoonshineASR moonshineASR = new MoonshineASR();
    }
}
