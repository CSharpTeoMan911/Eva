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

        public SpeechClassification()
        {
            
        }

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
            "screenshot",
            "mode"
        };

        private readonly HashSet<string> commandFormats = new HashSet<string>()
        {
            "visible",
            "invisible",
            "activate gpt mode",
            "enable gpt mode",
            "deactivate gpt mode",
            "disable gpt mode",
            "open [Application] now",
            "open [Application]",
            "close [Application] now",
            "close [Application]",
            "search on [WebApplication] [Content]",
            "search [Content] on [WebApplication]",
            "set a [Content] timer",
            "set an [Content] timer",
            "gpt [Content]",
            "take screenshot",
            "take a screenshot",
            "take a screenshot please",
            "please take a screenshot",
            "screenshot"
        };


        private readonly ConcurrentDictionary<string, Dictionary<int, string>> linkTree = new ConcurrentDictionary<string, Dictionary<int, string>>();


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
