using System;
using System.CodeDom;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Documents;

namespace Eva_5._0
{

    /////////////////////////////////////////////////////////////////////////////
    ///                                                                       ///
    ///                   PRODUCT: EVA A.I. ASSISTANT                         ///
    ///                                                                       ///
    ///                   AUTHOR: TEODOR MIHAIL                               ///
    ///                                                                       ///
    ///                                                                       ///
    /// ANY UNAUTHORISED TRADEMARK USE OF THIS SOFTWARE IS PUNISHABLE BY LAW  ///
    ///                                                                       ///
    /// THE AUTHOR OF THIS SOFTWARE DOES NOT LET ANY PEOPLE PATENT OR USE     ///
    /// THIS PRODUCT'S TRADEMARK.                                             ///
    ///                                                                       ///
    /// DO NOT REMOVE THIS FILE HEADER                                        ///
    ///                                                                       ///
    /////////////////////////////////////////////////////////////////////////////




    //////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                                                                  //
    //       CONTEXTUAL NATURAL LANGUAGE PROCESSING ENGINE ( NATURAL LANGUAGE UNDERSTANDING ENGINE )    //
    //                                                                                                  //
    //////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                                                                  //
    //          TIME COMPLEXITIES:                                                                      //
    //                                                                                                  //
    //          WORST: O(N)                                                                             //
    //                                                                                                  //
    //          BEST: O N - (N - (ci + 1))     ===>  "ci = current index of the sentence where          //
    //                                          did not match with any command patern"                  //
    //                                                                                                  //
    //                                                                                                  //
    //////////////////////////////////////////////////////////////////////////////////////////////////////


    internal class Natural_Language_Processing:A_p_l____And____P_r_o_c
    {
        private static StringBuilder Sentence_StringBuilder = new StringBuilder();
        private static StringBuilder Application_StringBuilder = new StringBuilder();
        private static StringBuilder WebApplication_StringBuilder = new StringBuilder();
        private static StringBuilder WebApplicationSearchContent_StringBuilder = new StringBuilder();
        private static StringBuilder WordBuffer_StringBuilder = new StringBuilder();


        public static async Task<bool> PreProcessing(string Result)
        {
            Sentence_StringBuilder.Clear();
            Application_StringBuilder.Clear();
            WebApplication_StringBuilder.Clear();
            WebApplicationSearchContent_StringBuilder.Clear();
            WordBuffer_StringBuilder.Clear();
            Sentence_StringBuilder.Append(Result);

            display_recognition_result = Result;

            if (CommandTest == true)
                goto End;

            // THE FIRST TOKENIZATION IS INITIATED. THE FIRST TOKENIZATION IS RESPONSIBLE FOR PARAMETER ASSOCIATION WITH THEIR RESPECTIVE COMMAND FORMATS
            // FOR EXAMPLE IF YOU SAY "SEARCH ROBOTS ARE COOL ON YOUTUBE" THE FIRST TOKENIZATION WILL ASSOCIATE THE COMMAND WITH THE 
            // "SEARCH [ CONTENT ] ON [ WEB APPLICATION ] COMMAND FORMAT BASED ON THE POSITION OF THE KEYWORD "SEARCH" WITHIN THE
            // SENTENCE, AND IF SENTENCE CONTAINS THE KEYWORD "ON". IF THE TOKENIZATION SESSION WILL NOT FIND VALID COMMAND FORMATS
            // THE TOKENIZATION SESSION WILL DROP THE COMMAND.
            //
            // [ BEGIN ]


            if (Result == "invisible")
            {
                MainWindow.invisibility_mode = true;
                await sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTActivationSoundEffect);
                goto ChatGptMode;
            }
            else if (Result == "visible")
            {
                MainWindow.invisibility_mode = false;
                await sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTDeactivationSoundEffect);
                goto ChatGptMode;
            }
            else if (Result.IndexOf("activate c") == 0 && Result.IndexOf(" mode") == Result.Length - " mode".Length)
            {
                MainWindow.chatgpt_mode_enabled = true;
                await sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTActivationSoundEffect);
                goto ChatGptMode;
            }
            else if (Result.IndexOf("enable c") == 0 && Result.IndexOf(" mode") == Result.Length - " mode".Length)
            {
                MainWindow.chatgpt_mode_enabled = true;
                await sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTActivationSoundEffect);
                goto ChatGptMode;
            }
            else if(Result.IndexOf("deactivate c") == 0 && Result.IndexOf(" mode") == Result.Length - " mode".Length)
            {
                MainWindow.chatgpt_mode_enabled = false;
                await sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTDeactivationSoundEffect);
                goto ChatGptMode;
            }
            else if (Result.IndexOf("disable c") == 0 && Result.IndexOf(" mode") == Result.Length - " mode".Length)
            {
                MainWindow.chatgpt_mode_enabled = false;
                await sound_player.Play_Sound(Properties.Sound_Player.Sounds.ChatGPTDeactivationSoundEffect);
                goto ChatGptMode;
            }

            switch (MainWindow.chatgpt_mode_enabled)
            {
                case true:
                    await PostProcessing("chatgpt [ ChatGPT Query ]", Result);
                    break;

                case false:
                    switch (Result.IndexOf("please open ") == 0)
                    {
                        case true:

                            await PostProcessing("please open [Application]", Result);
                            break;

                        case false:

                            switch (Result.IndexOf("open ") == 0)
                            {
                                case true:

                                    switch (Result.LastIndexOf(" please") + " please".Length - 1 == Result.Length - 1)
                                    {
                                        case true:

                                            await PostProcessing("open [Application] please", Result);
                                            break;

                                        case false:

                                            switch (Result.LastIndexOf(" now") + " now".Length - 1 == Result.Length - 1)
                                            {
                                                case true:

                                                    await PostProcessing("open [Application] now", Result);
                                                    break;

                                                case false:

                                                    await PostProcessing("open [Application]", Result);
                                                    break;
                                            }

                                            break;
                                    }
                                    break;
                            }
                            break;
                    }



                    switch (Result.IndexOf("please close ") == 0)
                    {
                        case true:

                            await PostProcessing("please close [Application]", Result);
                            break;

                        case false:

                            switch (Result.IndexOf("close ") == 0)
                            {
                                case true:

                                    switch (Result.LastIndexOf(" please") + " please".Length - 1 == Result.Length - 1)
                                    {
                                        case true:

                                            await PostProcessing("close [Application] please", Result);
                                            break;

                                        case false:

                                            switch (Result.LastIndexOf(" now") + " now".Length - 1 == Result.Length - 1)
                                            {
                                                case true:

                                                    await PostProcessing("close [Application] now", Result);
                                                    break;

                                                case false:

                                                    await PostProcessing("close [Application]", Result);
                                                    break;
                                            }

                                            break;
                                    }
                                    break;
                            }
                            break;
                    }




                    switch (Result.IndexOf("please search on ") == 0)
                    {
                        case true:

                            await PostProcessing("please search on [Web Application Keyword] [Content]", Result);
                            break;

                        case false:

                            switch ((Result.IndexOf("please search ") == 0) && (Result.Contains(" on ") == true))
                            {
                                case true:

                                    await PostProcessing("please search [Content] on [Web Application Keyword]", Result);
                                    break;

                                case false:

                                    switch (Result.IndexOf("search on ") == 0)
                                    {
                                        case true:

                                            await PostProcessing("search on [Web Application Keyword] [Content]", Result);
                                            break;

                                        case false:

                                            if ((Result.IndexOf("search ") == 0) && (Result.Contains(" on ") == true))
                                            {
                                                await PostProcessing("search [Content] on [Web Application Keyword]", Result);
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }





                    switch (Result.IndexOf("set a ") == 0)
                    {
                        case true:

                            switch (Result.IndexOf(" timer") == Result.Length - 6)
                            {
                                case true:

                                    await PostProcessing("set a [Timer Interval] timer", Result);
                                    break;


                                case false:

                                    switch (Result.IndexOf(" please") == Result.Length - 7)
                                    {
                                        case true:

                                            if (Result.IndexOf(" timer ") == Result.Length - 13)
                                            {
                                                await PostProcessing("set a [Timer Interval] timer please", Result);
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;



                        case false:
                            switch (Result.IndexOf("please set a ") == 0)
                            {
                                case true:
                                    if (Result.IndexOf(" timer") == Result.Length - 6)
                                    {
                                        await PostProcessing("please set a [Timer Interval] timer", Result);
                                    }
                                    break;
                            }
                            break;
                    }



                    if (Result.IndexOf("set ") != 0)
                    {
                        if (Result.IndexOf(' ') == 3)
                        {
                            switch (Result.IndexOf('g') == 0)
                            {
                                case true:
                                    await PostProcessing("chatgpt [ ChatGPT Query ]", Result);
                                    break;


                                case false:
                                    switch (Result.IndexOf('p') == 1)
                                    {
                                        case true:
                                            await PostProcessing("chatgpt [ ChatGPT Query ]", Result);
                                            break;


                                        case false:
                                            if (Result.IndexOf('t') == 2)
                                            {
                                                await PostProcessing("chatgpt [ ChatGPT Query ]", Result);
                                            }
                                            break;
                                    }
                                    break;
                            }
                        }
                    }


                    // [ END ]






                    // COMMANDS THAT DO NOT REQUIRE A SECOND TOKENIZATION. THESE COMMANDS ARE COMMANDS THAT DO NOT HAVE
                    // EXTRA VARIABLES THAT REQUIRE CONTENT PROCESSING AND THE MEANING AND CONTENTS OF THE COMMAND
                    // ARE EXPLICIT
                    //
                    //[ BEGIN ]


                    // SCREENSHOT PROCESS
                    //[ BEGIN ]

                    switch (Result.IndexOf("take screenshot") == 0)
                    {
                        case true:
                            await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                            break;

                        case false:

                            switch (Result.IndexOf("take a screenshot") == 0)
                            {
                                case true:
                                    await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                                    break;

                                case false:

                                    switch (Result.IndexOf("take a screenshot please") == 0)
                                    {
                                        case true:
                                            await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                                            break;

                                        case false:

                                            switch (Result.IndexOf("please take a screenshot") == 0)
                                            {
                                                case true:
                                                    await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                                                    break;

                                                case false:

                                                    if (Result.IndexOf("screenshot") == 0)
                                                    {
                                                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }

                        //[ END ]


                    //[ END ]
                    break;
            }

 

        ChatGptMode:
        End:
            Sentence_StringBuilder.Clear();
            Application_StringBuilder.Clear();
            WebApplication_StringBuilder.Clear();
            WebApplicationSearchContent_StringBuilder.Clear();
            WordBuffer_StringBuilder.Clear();

            return true;
        }



        private static async Task<bool> PostProcessing(string Param, string Sentence)
        {
            // THE SECOND TOKENIZATION IS INITIATED. HERE THE CONTEXTUAL NATURAL LANGUAGE PROCESSING TAKES PLACE. THE KEYWORDS RELATED TO THE COMMAND FORMATS DETECTED ARE TOKENIZED.
            // THE KEYWORDS FOR OPERATIONS, APPLICATIONS AND THE CONTENT FOR WEB SEARCH FUNCTIONS OR THE TIMER FUNCTION ARE EXTRACTED FROM THE SENTENCE.
            // FOR EXAMPLE IF THE COMMAND FORMAT IS "SEARCH [ CONTENT ] ON [ WEB APPLICATION ]" THE CONTENT OF THE WEB APPLICATION WILL BE AFTER THE
            // FIRST OCCURENCE OF THE KEYWORD "SEARCH" AND BEFORE THE LAST OCCURENCE OF THE KEYWORD "ON", AND THE WEB APPLICATION WILL BE AFTER THE 
            // LAST OCCURENCE OF THE KEYWORD "ON". IF THE TOKENIZATION PROCEDURE DOES NOT FIND A CORRECT COMMAND FORMAT OR VALID COMMAD PARAMETERS,
            // THE TOKENIZATION SESSION WILL DROP THE COMMAND.
            //
            // [ BEGIN ]

            Application_StringBuilder.Clear();
            WebApplicationSearchContent_StringBuilder.Clear();


            string Application = String.Empty;
            string WebApplicationSearchContent = String.Empty;
            bool timer_set = false;

            System.Collections.Concurrent.ConcurrentDictionary<string, int> time_interval = new System.Collections.Concurrent.ConcurrentDictionary<string, int>();


            switch (Param)
            {
                case "please open [Application]":
                    Application = await System_Application_Selector("please open ".Length - 1, Sentence_StringBuilder.ToString());
                    if(Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    }
                    Application_StringBuilder.Clear();
                    break;

                case "open [Application] please":
                    Application = await System_Application_Selector("open ".Length - 1, Sentence_StringBuilder.ToString());
                    if(Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    }
                    Application_StringBuilder.Clear();
                    break;

                case "open [Application] now":
                    Application = await System_Application_Selector("open ".Length - 1, Sentence_StringBuilder.ToString());
                    if (Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    }
                    Application_StringBuilder.Clear();
                    break;

                case "open [Application]":
                    Application = await System_Application_Selector("open ".Length - 1, Sentence_StringBuilder.ToString());
                    if (Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    }
                    Application_StringBuilder.Clear();
                    break;




                case "please close [Application]":
                    Application = await System_Process_Selector("please close ".Length - 1, Sentence_StringBuilder.ToString());

                    if(Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    }
                    Application_StringBuilder.Clear();
                    break;

                case "close [Application] please":

                    Application = await System_Process_Selector("close ".Length - 1, Sentence_StringBuilder.ToString());

                    if (Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    }
                    Application_StringBuilder.Clear();
                    break;

                case "close [Application] now":

                    Application = await System_Process_Selector("close ".Length - 1, Sentence_StringBuilder.ToString());

                    if (Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    }
                    Application_StringBuilder.Clear();
                    break;

                case "close [Application]":

                    Application = await System_Process_Selector("close ".Length - 1, Sentence_StringBuilder.ToString());

                    if (Application != String.Empty)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    }
                    Application_StringBuilder.Clear();
                    break;




                case "please search [Content] on [Web Application Keyword]":

                    Application = await Web_Application_Selector(Sentence.LastIndexOf(" on ") + " on ".Length - 1, Sentence_StringBuilder.ToString());

                    if(Application != String.Empty)
                    {
                        for (int Index = "please search".Length; Index < Sentence.LastIndexOf(" on "); Index++)
                        {
                            WebApplicationSearchContent_StringBuilder.Append(Sentence[Index]);
                        }

                        WebApplicationSearchContent = WebApplicationSearchContent_StringBuilder.ToString();
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    }
                    break;

                case "please search on [Web Application Keyword] [Content]":

                    Application = await Web_Application_Selector(Sentence.LastIndexOf(" on ") + " on ".Length - 1, Sentence_StringBuilder.ToString());

                    if(Application != String.Empty)
                    {
                        for (int Index = "please search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                        {
                            WebApplicationSearchContent_StringBuilder.Append(Sentence[Index]);
                        }

                        WebApplicationSearchContent = WebApplicationSearchContent_StringBuilder.ToString().Trim();
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    }
                    break;

                case "search on [Web Application Keyword] [Content]":

                    Application = await Web_Application_Selector("search on ".Length - 1, Sentence);

                    if(Application != String.Empty)
                    {
                        for (int Index = "search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                        {
                            WebApplicationSearchContent_StringBuilder.Append(Sentence[Index]);
                        }

                        WebApplicationSearchContent = WebApplicationSearchContent_StringBuilder.ToString().Trim();
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    }
                    break;

                case "search [Content] on [Web Application Keyword]":

                    Application = await Web_Application_Selector(Sentence.LastIndexOf(" on ") + " on ".Length - 1, Sentence_StringBuilder.ToString());

                    if(Application != String.Empty)
                    {
                        for (int Index = 7; Index < Sentence.LastIndexOf(" on "); Index++)
                        {
                            WebApplicationSearchContent_StringBuilder.Append(Sentence[Index]);
                        }

                       
                        WebApplicationSearchContent = WebApplicationSearchContent_StringBuilder.ToString().Trim();
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    }
                    break;

                case "chatgpt [ ChatGPT Query ]":

                    switch(MainWindow.chatgpt_mode_enabled)
                    {
                        case true:
                            WebApplicationSearchContent = Sentence;
                            break;
                        case false:
                            for (int Index = "gpt".Length; Index < Sentence.Length; Index++)
                            {
                                WebApplicationSearchContent_StringBuilder.Append(Sentence[Index]);
                            }

                            WebApplicationSearchContent = WebApplicationSearchContent_StringBuilder.ToString();
                            break;
                    }
                    await Eva_Functionalities.Proc_Mitigator.Process_Initialisation<string>("ChatGPT Process", Application, WebApplicationSearchContent);
                    break;

                case "set a [Timer Interval] timer":
                    timer_set = await Timer_Time_Selector(Sentence, time_interval, "set a ".Length - 1);

                    if(timer_set == true)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation("Timer Process", null, time_interval);
                    }
                    break;


                case "set a [Timer Interval] timer please":
                    timer_set = await Timer_Time_Selector(Sentence, time_interval, "set a ".Length - 1);

                    if (timer_set == true)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation("Timer Process", null, time_interval);
                    }
                    break;

                case "please set a [Timer Interval] timer":
                    timer_set = await Timer_Time_Selector(Sentence, time_interval, "please set a ".Length - 1);

                    if (timer_set == true)
                    {
                        await Eva_Functionalities.Proc_Mitigator.Process_Initialisation("Timer Process", null, time_interval);
                    }
                    break;
            }

            // [ END ]

            return true;
        }




        private static Task<bool> Timer_Time_Selector(string Sentence, System.Collections.Concurrent.ConcurrentDictionary<string, int> time_interval, int start_index)
        {
            // WHEN A TIMER IS SET THE TIME VARIABLES FROM THE SENTECE MUST BE EXTRACTED. THE SECOND TOKENIZATION WILL EXTRACTS WHERE
            // THE POSITION OF THESE TIME VARIALBLES WILL BE WITHIN THE SENTENCE AND PASS THE SENTENCE AND INDEX WHERE THESE TIME
            // VARIABLES ARE LOCATED WITHIN THE SENTENCE. IF THE COMMAND GIVEN IS "SET A ONE HOUR, 45 MINUTES, AND 10 SECONDS TIMER",
            // THE LOCATION OF THE TIME VARIABLES STARTS AFTER THE "SET" AND "A" KEYWORDS. AFTERWARDS, IF A WORD THAT REPRESENTS A 
            // NUMERICAL VALUE IS DETECTED ( e.g. "ONE" ) OR A NUMERICAL VALUE IS DETECTED, THEN THIS METHOD WILL EXTRACT THE TIME UNIT
            // RELATED TO THIS NUMERICAL VALUE ( e.g. HOUR/HOURS, MINUTE/MINUTES, SECOND/SECONDS ) IS EXPECTED TO EXIST AFTER THE NUMERICAL
            // VALUE EXTRACTED IN ORDER TO BE ASSOCIATED. IF NO ACCCEPTED TIME UNIT IS FOUND AFTER THE NUMERICAL VALUE, THIS TOKENIZATION
            // SESSION WILL DROP THE COMMAND, OTHERWISE THE NUMERICAL VALUE IS SAVED WITHIN A THREAD SAFE DICTIONARY ( HASHMAP ) IN ORDER
            // TO BE PROCESSED LATER. THIS TOKENIZATION PROCEDURE EXPECTS 1, 2, OR 3 DIFFERENT TIME UNITS WITH NUMERIACL VALUES ASSOCIATED
            // WITH THEM. IF TIME UNITS REPEAT, THE TOKENIZATION SESSION WILL DROP THE COMMAND (e.g. "SET A 1 HOUR AND 2 HOURS TIMER" ).
            //
            // [ BEGIN ]

            bool Time_Unit_Received = false;

            int Time_Unit_Value_Buffer = 0;

            string Word_Buffer = String.Empty;

            int buffer = 0;



            for (int Index = start_index; Index <= Sentence.Length - 6; Index++)
            {

                if (Sentence[Index] == ' ')
                {
                    Word_Buffer = WordBuffer_StringBuilder.ToString();
                    WordBuffer_StringBuilder.Clear();

                    if (Time_Unit_Received == true)
                    {

                        if ((Word_Buffer == "hour") || (Word_Buffer == "hours"))
                        {

                            bool retrieval_result = time_interval.TryGetValue("hours", out buffer);

                            if (retrieval_result == false)
                            {
                                time_interval.TryAdd("hours", Time_Unit_Value_Buffer);

                                Time_Unit_Value_Buffer = 0;

                                Time_Unit_Received = false;
                            }
                            else
                            {
                                break;
                            }

                        }
                        else if ((Word_Buffer.Trim() == "minute") || (Word_Buffer.Trim() == "minutes"))
                        {

                            bool retrieval_result = time_interval.TryGetValue("minutes", out buffer);

                            if (retrieval_result == false)
                            {
                                time_interval.TryAdd("minutes", Time_Unit_Value_Buffer);

                                Time_Unit_Value_Buffer = 0;

                                Time_Unit_Received = false;
                            }
                            else
                            {
                                break;
                            }

                        }
                        else if ((Word_Buffer == "second") || (Word_Buffer == "seconds"))
                        {

                            bool retrieval_result = time_interval.TryGetValue("seconds", out buffer);

                            if (retrieval_result == false)
                            {
                                time_interval.TryAdd("seconds", Time_Unit_Value_Buffer);

                                Time_Unit_Value_Buffer = 0;

                                Time_Unit_Received = false;
                            }
                            else
                            {
                                break;
                            }

                        }

                    }
                    else
                    {
                        try
                        {

                            double Number_Verification_Buffer = Convert.ToDouble(Word_Buffer);

                            Time_Unit_Value_Buffer = Convert.ToInt32(Word_Buffer);

                            Time_Unit_Received = true;

                        }
                        catch
                        {
                            // CHANGE NON NUMBERIC REPRESENTATIONS OF NUMBERS WITH NUMERIC REPRESENTATIONS
                            //
                            // [ BEGIN ]

                            if (Time_Unit_Received == false)
                            {
                                switch (Word_Buffer)
                                {
                                    case "one":
                                        Time_Unit_Value_Buffer = 1;

                                        Time_Unit_Received = true;
                                        break;

                                    case "two":
                                        Time_Unit_Value_Buffer = 2;

                                        Time_Unit_Received = true;
                                        break;

                                    case "three":
                                        Time_Unit_Value_Buffer = 3;

                                        Time_Unit_Received = true;
                                        break;

                                    case "four":
                                        Time_Unit_Value_Buffer = 4;

                                        Time_Unit_Received = true;
                                        break;

                                    case "five":
                                        Time_Unit_Value_Buffer = 5;

                                        Time_Unit_Received = true;
                                        break;

                                    case "six":
                                        Time_Unit_Value_Buffer = 6;

                                        Time_Unit_Received = true;
                                        break;

                                    case "seven":
                                        Time_Unit_Value_Buffer = 7;

                                        Time_Unit_Received = true;
                                        break;

                                    case "eight":
                                        Time_Unit_Value_Buffer = 8;

                                        Time_Unit_Received = true;
                                        break;

                                    case "nine":
                                        Time_Unit_Value_Buffer = 9;

                                        Time_Unit_Received = true;
                                        break;
                                }

                            }

                            // [ END ]
                        }
                    }


                }
                else
                {
                    WordBuffer_StringBuilder.Append(Sentence[Index]);
                }
            }

            // [ END ]

            return Task.FromResult(true);
        }

        private static Task<string> System_Application_Selector(int start_index, string Sentence)
        {
            System.Collections.Generic.Stack<string> Token_Buffer_List = new System.Collections.Generic.Stack<string>();
            string Token_Buffer;
            string app = String.Empty;
            int index = start_index + 1;


            Application_StringBuilder.Clear();

            while (index < Sentence.Length)
            {
                Application_StringBuilder.Append(Sentence[index]);

                app = Application_StringBuilder.ToString();
                commands.A_p_l_Name__And__A_p_l___E_x__Name.TryGetValue(app, out Token_Buffer);


                if (Token_Buffer != null)
                {
                    Token_Buffer_List.Push(app);
                    Token_Buffer = null;
                }

                index++;
            }
            Application_StringBuilder.Clear();

            if(Token_Buffer_List.Count > 0)
            {
                return Task.FromResult(Token_Buffer_List.Pop());
            }
            else
            {
                return Task.FromResult(String.Empty);
            }
        }

        private static Task<string> System_Process_Selector(int start_index, string Sentence)
        {
            System.Collections.Generic.Stack<string> Token_Buffer_List = new System.Collections.Generic.Stack<string>();
            string Token_Buffer;
            string process = String.Empty;
            int index = start_index + 1;


            Application_StringBuilder.Clear();

            while (index < Sentence.Length)
            {
                Application_StringBuilder.Append(Sentence[index]);

                process = Application_StringBuilder.ToString();
                commands.A_p_l_Name__And__A_p_l___P_r_o_c_Name.TryGetValue(process, out Token_Buffer);


                if (Token_Buffer != null)
                {
                    Token_Buffer_List.Push(process);
                    Token_Buffer = null;
                }

                index++;
            }
            Application_StringBuilder.Clear();

            if (Token_Buffer_List.Count > 0)
            {
                return Task.FromResult(Token_Buffer_List.Pop());
            }
            else
            {
                return Task.FromResult(String.Empty);
            }
        }

        private static Task<string> Web_Application_Selector(int start_index, string Sentence)
        {
            // THE SECOND TOKENIZATION IDENTIFIES WHERE THE POSITION OF THE WEB APPLICATION KEYWORD IS WITHIN THE SENTENCE
            // PASSES THE SENTENCE AND THE WEB APPLICATION'S DETECTED KEYWORD INDEX TO THIS METHOD. THIS METHOD WILL THEN 
            // COMPARE THE DETECTED KEYWORD WITH THE KEYS VALUE WITHIN THE LIST OF KEYWORD-APPLICATION PAIRS STORED
            // WITHIN A DICTIONARY ( HASHMAP ) WITHIN THE "A_p_l____And____P_r_o_c" CLASS.THIS IS DONE TO ENSURE A O(1)
            // TIME COMPLEXITY FOR THE KEYWORD-APPLICATION SELECTION AND COMPARTION TIME. IF THE TOKENIZATION PROCEDURE
            // DOES NOT FIND A VALID WEB APPLICATION KEYWORD, THE THE TOKENIZATION SESSION WILL DROP THE COMMAND.
            //
            // [ BEGIN ]


            System.Collections.Generic.Stack<string> Token_Buffer_List = new System.Collections.Generic.Stack<string>();
            string Token_Buffer;
            string web_app = String.Empty;
            int index = start_index + 1;





            WebApplicationSearchContent_StringBuilder.Clear();

            while(index < Sentence.Length)
            {
                WebApplication_StringBuilder.Append(Sentence[index]);

                web_app = WebApplication_StringBuilder.ToString();
                commands.W_e_b__A_p_l_Name__And__W_e_b__A_p_l___P_r_o_c_Name.TryGetValue(web_app, out Token_Buffer);


                if(Token_Buffer != null)
                {
                    Token_Buffer_List.Push(web_app);
                    Token_Buffer = null;
                }

                index++;
            }

            WebApplicationSearchContent_StringBuilder.Clear();



            // [ END ]

            if (Token_Buffer_List.Count > 0)
            {
                return Task.FromResult(Token_Buffer_List.Pop());
            }
            else
            {
                return Task.FromResult(String.Empty);
            }
        }

        ~Natural_Language_Processing()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(0, GCCollectionMode.Forced, true, true);
        }

    }
}
