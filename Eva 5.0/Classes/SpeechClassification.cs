using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0.Classes
{
    internal class SpeechClassification
    {
        /*
        
        Action,
        Keyword,
        Application,
        WebApplication,
        Content

         */

        public enum SegmentType
        {
            Action,
            Keyword,
            Application,
            WebApplication,
            Content
        }

        private readonly HashSet<string> actions = new HashSet<string>() {
            "open",
            "close",
            "set",
            "search",
            "activate",
            "deactivate",
            "enable",
            "disable",
            "invisible",
            "visible",
            "take"
        };

        private readonly HashSet<string> keywords = new HashSet<string> {
            "on",
            "a",
            "an",
            "please",
            "timer",
            "gpt",
            "screenshot"
        };


        private void GetSegmentType(string segment, int index)
        {
            if(actions.Contains(segment))
            {

            }
            else if(keywords.Contains(segment))
            {

            }
            else
            {
                
            }
        }
    }
}
