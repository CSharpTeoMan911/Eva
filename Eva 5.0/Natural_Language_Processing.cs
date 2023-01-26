using System;
using System.CodeDom;
using System.Text;
using System.Threading.Tasks;

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
    //          BEST: O(N - ci)     ===>  "ci = current index of the sentence where                     //
    //                                          did not match with any command patern"                  //
    //                                                                                                  //
    //                                                                                                  //
    //////////////////////////////////////////////////////////////////////////////////////////////////////


    internal class Natural_Language_Processing:A_p_l____And____P_r_o_c
    {

        // COMPONENTS THAT INTERACT WITH SECURTY SENSITIVE FEATURES ARE CONTAINED INSIDE PRIVATE SEALED CLASSES FOR EXTRA PROTECTION
        //
        // [ BEGIN ]

        private sealed class Special_Character_Replacement_Implementor:Special_Character_Replacement
        {
            internal async static Task<string> Remove_Special_Characters_Procedure(string Sentence)
            {
                return await Remove_Special_Characters(Sentence);
            }
        }


        private sealed class Proc_Mitigator : Proc
        {
            internal static async Task<bool> Process_Initialisation<Content>(string process_type, string application, Content content)
            {
                return await ProcInitialisation<Content> (process_type, application, content);
            }
        }

        // [ END ]


        protected static async Task<bool> PreProcessing(string Result)
        {
            string Sentence = await Special_Character_Replacement_Implementor.Remove_Special_Characters_Procedure(Result);

            // THE FIRST TOKENIZATION IS INITIATED. THE FIRST TOKENIZATION IS RESPONSIBLE FOR PARAMETER ASSOCIATION WITH THEIR RESPECTIVE COMMAND FORMATS
            // [ BEGIN ]

            switch (Sentence.IndexOf("please open ") == 0)
            {
                case true:

                    await PostProcessing("please open [Application]", Sentence);
                    break;

                case false:

                    switch (Sentence.IndexOf("open ") == 0)
                    {
                        case true:

                            switch (Sentence.LastIndexOf(" please") + " please".Length - 1 == Sentence.Length - 1)
                            {
                                case true:

                                    await PostProcessing("open [Application] please", Sentence);
                                    break;

                                case false:

                                    switch (Sentence.LastIndexOf(" now") + " now".Length - 1 == Sentence.Length - 1)
                                    {
                                        case true:

                                            await PostProcessing("open [Application] now", Sentence);
                                            break;

                                        case false:

                                            await PostProcessing("open [Application]", Sentence);
                                            break;
                                    }

                                    break;
                            }
                            break;
                    }
                    break;
            }



            switch (Sentence.IndexOf("please close ") == 0)
            {
                case true:

                    await PostProcessing("please close [Application]", Sentence);
                    break;

                case false:

                    switch (Sentence.IndexOf("close ") == 0)
                    {
                        case true:

                            switch (Sentence.LastIndexOf(" please") + " please".Length - 1 == Sentence.Length - 1)
                            {
                                case true:

                                    await PostProcessing("close [Application] please", Sentence);
                                    break;

                                case false:

                                    switch (Sentence.LastIndexOf(" now") + " now".Length - 1 == Sentence.Length - 1)
                                    {
                                        case true:

                                            await PostProcessing("close [Application] now", Sentence);
                                            break;

                                        case false:

                                            await PostProcessing("close [Application]", Sentence);
                                            break;
                                    }

                                    break;
                            }
                            break;
                    }
                    break;
            }




            switch (Sentence.IndexOf("please search on ") == 0)
            {
                case true:

                    await PostProcessing("please search on [Web Application Keyword] [Content]", Sentence);
                    break;

                case false:

                    switch ((Sentence.IndexOf("please search ") == 0) && (Sentence.Contains(" on ") == true))
                    {
                        case true:

                            await PostProcessing("please search [Content] on [Web Application Keyword]", Sentence);
                            break;

                        case false:

                            switch (Sentence.IndexOf("search on ") == 0)
                            {
                                case true:

                                    await PostProcessing("search on [Web Application Keyword] [Content]", Sentence);
                                    break;

                                case false:

                                    if ((Sentence.IndexOf("search ") == 0) && (Sentence.Contains(" on ") == true))
                                    {
                                        await PostProcessing("search [Content] on [Web Application Keyword]", Sentence);
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }





            switch (Sentence.IndexOf("set a ") == 0)
            {
                case true:

                    switch (Sentence.IndexOf(" timer") == Sentence.Length - 6)
                    {
                        case true:

                            await PostProcessing("set a [Timer Interval] timer", Sentence);
                            break;


                        case false:

                            switch (Sentence.IndexOf(" please") == Sentence.Length - 7)
                            {
                                case true:

                                    if (Sentence.IndexOf(" timer ") == Sentence.Length - 13)
                                    {
                                        await PostProcessing("set a [Timer Interval] timer please", Sentence);
                                    }
                                    break;
                            }
                            break;
                    }
                    break;



                case false:

                    switch (Sentence.IndexOf("please set a ") == 0)
                    {
                        case true:

                            if (Sentence.IndexOf(" timer") == Sentence.Length - 6)
                            {
                                await PostProcessing("please set a [Timer Interval] timer", Sentence);
                            }
                            break;
                    }
                    break;
            }

            // [ END ]




            // PROCESSES THAT DO NOT NEED NATURAL LANGUAGE TEXT PROCESSING REQUIRE PROCESSING AND THEY ONLY REQUIRE THE FIRST TOKENIZATION
            //[ BEGIN ]


                    // SCREENSHOT PROCESS
                    //[ BEGIN ]

            switch (Sentence.IndexOf("take screenshot") == 0)
            {
                case true:
                    await Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                    break;

                case false:

                    switch (Sentence.IndexOf("take a screenshot") == 0)
                    {
                        case true:
                            await Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                            break;

                        case false:

                            switch (Sentence.IndexOf("take a screenshot please") == 0)
                            {
                                case true:
                                    await Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                                    break;

                                case false:

                                    switch (Sentence.IndexOf("please take a screenshot") == 0)
                                    {
                                        case true:
                                            await Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
                                            break;

                                        case false:

                                            if (Sentence.IndexOf("screenshot") == 0)
                                            {
                                                await Proc_Mitigator.Process_Initialisation<string>("Screen Capture Process", null, null);
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

            return true;
        }



        private static async Task<bool> PostProcessing(string Param, string Sentence)
        {
            // THE SECOND TOKENIZATION IS INITIATED. HERE THE CONTEXTUAL NATURAL LANGUAGE PROCESSING TAKES PLACE. THE KEYWORDS RELATED TO THE COMMAND FORMATS DETECTED ARE TOKENIZED.
            // THE KEYWORDS FOR OPERATIONS, APPLICATIONS AND THE CONTENT FOR WEB SEARCH FUNCTIONS OR THE TIMER FUNCTION ARE EXTRACTED FROM THE SENTENCE.

            // [ BEGIN ]

            string Application = String.Empty;
            string WebApplicationSearchContent = String.Empty;

            System.Collections.Concurrent.ConcurrentDictionary<string, int> time_interval = new System.Collections.Concurrent.ConcurrentDictionary<string, int>();


            switch (Param)
            {
                case "please open [Application]":

                    for (int Index = "please open ".Length; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    break;

                case "open [Application] please":

                    for (int Index = "open ".Length; Index <= Sentence.LastIndexOf(" please") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    break;

                case "open [Application] now":

                    for (int Index = "open ".Length; Index <= Sentence.LastIndexOf(" now") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    break;

                case "open [Application]":

                    for (int Index = "open ".Length ; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "open");
                    break;




                case "please close [Application]":

                    for (int Index = "please close ".Length; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    break;

                case "close [Application] please":

                    for (int Index = "close ".Length; Index <= Sentence.LastIndexOf(" please") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    break;

                case "close [Application] now":

                    for (int Index = "close ".Length; Index <= Sentence.LastIndexOf(" now") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    break;

                case "close [Application]":

                    for (int Index = "close ".Length; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc_Mitigator.Process_Initialisation<string>("System Process", Application, "close");
                    break;




                case "please search [Content] on [Web Application Keyword]":

                    Sentence = Sentence + " ";

                    Application = await Web_Application_Selector(Sentence.LastIndexOf(" on ") + " on ".Length - 1, Sentence);

                    for (int Index = "please search".Length; Index < Sentence.LastIndexOf(" on "); Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index].ToString();
                    }

                    await Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    break;


                    

                case "please search on [Web Application Keyword] [Content]":

                    Application = await Web_Application_Selector("please search on ".Length - 1, Sentence);

                    for (int Index = "please search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index];
                    }

                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    break;




                case "search on [Web Application Keyword] [Content]":

                    Application = await Web_Application_Selector("search on ".Length - 1, Sentence);

                    for (int Index = "search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index];

                    }

                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    break;




                case "search [Content] on [Web Application Keyword]":

                    Sentence = Sentence + " ";

                    for (int Index = 7; Index < Sentence.LastIndexOf(" on "); Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index].ToString();
                    }

                    Application = await Web_Application_Selector(Sentence.LastIndexOf(" on ") + " on ".Length - 1, Sentence);
                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc_Mitigator.Process_Initialisation<string>("Online Process", Application, WebApplicationSearchContent);
                    break;



                case "set a [Timer Interval] timer":
                    await Timer_Time_Selector(Sentence, time_interval, "set a ".Length - 1);

                    await Proc_Mitigator.Process_Initialisation<System.Collections.Concurrent.ConcurrentDictionary<string, int>>("Timer Process", null, time_interval);
                    break;


                case "set a [Timer Interval] timer please":
                    await Timer_Time_Selector(Sentence, time_interval, "set a ".Length - 1);

                    await Proc_Mitigator.Process_Initialisation<System.Collections.Concurrent.ConcurrentDictionary<string, int>>("Timer Process", null, time_interval);
                    break;



                case "please set a [Timer Interval] timer":
                    await Timer_Time_Selector(Sentence, time_interval, "please set a ".Length - 1);

                    await Proc_Mitigator.Process_Initialisation<System.Collections.Concurrent.ConcurrentDictionary<string, int>>("Timer Process", null, time_interval);
                    break;
            }

            // [ END ]

            return true;
        }

        private static Task<bool> Timer_Time_Selector(string Sentence, System.Collections.Concurrent.ConcurrentDictionary<string, int> time_interval, int start_index)
        {

            bool Time_Unit_Received = false;

            int Time_Unit_Value_Buffer = 0;

            string Word_Buffer = String.Empty;

            int buffer = 0;



            for (int Index = start_index; Index <= Sentence.Length - 6; Index++)
            {

                if (Sentence[Index].ToString() == " ")
                {

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
                            // BEGIN

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

                            // END
                        }
                    }


                    Word_Buffer = String.Empty;
                }
                else
                {
                    Word_Buffer += Sentence[Index].ToString();
                }
            }

            return Task.FromResult(true);
        }

        private static Task<string> Web_Application_Selector(int start_index, string Sentence)
        {
            int web_app_key = 0;
            int index = start_index;
            string web_app = String.Empty;

            while(index < Sentence.Length)
            {
                web_app += Sentence[index].ToString();
                index++;
            }

            while(web_app_key < 8)
            {
                string Token_Buffer = String.Empty;
                W_e_b__A_p_l_Name__Tokens.TryGetValue(web_app_key, out Token_Buffer);

                if (Token_Buffer == web_app)
                {
                    StringBuilder Token_Buffer_String_Builder = new StringBuilder(Token_Buffer);
                    Token_Buffer_String_Builder.Remove(Token_Buffer_String_Builder.Length - 1, 1);
                    Token_Buffer_String_Builder.Remove(0, 1);

                    return Task.FromResult(Token_Buffer_String_Builder.ToString());
                }

                web_app_key++;
            }

            return Task.FromResult(String.Empty);
        }

        ~Natural_Language_Processing()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(0, GCCollectionMode.Forced, true, true);
        }

    }
}
