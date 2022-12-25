using System;
using System.Collections.Generic;
using System.Linq;
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


    internal class Natural_Language_Processing
    {

        public static async Task<bool> PreProcessing(string Sentence)
        {

            Sentence = await Special_Character_Replacement.Remove_Special_Characters_Initiator(Sentence);

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
                    await Proc<string>.ProcInitialisation("Screen Capture Process", null, null);
                    break;

                case false:

                    switch (Sentence.IndexOf("take a screenshot") == 0)
                    {
                        case true:
                            await Proc<string>.ProcInitialisation("Screen Capture Process", null, null);
                            break;

                        case false:

                            switch (Sentence.IndexOf("take a screenshot please") == 0)
                            {
                                case true:
                                    await Proc<string>.ProcInitialisation("Screen Capture Process", null, null);
                                    break;

                                case false:

                                    switch (Sentence.IndexOf("please take a screenshot") == 0)
                                    {
                                        case true:
                                            await Proc<string>.ProcInitialisation("Screen Capture Process", null, null);
                                            break;

                                        case false:

                                            if (Sentence.IndexOf("screenshot") == 0)
                                            {
                                                await Proc<string>.ProcInitialisation("Screen Capture Process", null, null);
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
                    await Proc<string>.ProcInitialisation("System Process", Application, "open");
                    break;

                case "open [Application] please":

                    for (int Index = "open ".Length; Index <= Sentence.LastIndexOf(" please") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string>.ProcInitialisation("System Process", Application, "open");
                    break;

                case "open [Application] now":

                    for (int Index = "open ".Length; Index <= Sentence.LastIndexOf(" now") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string>.ProcInitialisation("System Process", Application, "open");
                    break;

                case "open [Application]":

                    for (int Index = "open ".Length ; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string>.ProcInitialisation("System Process", Application, "open");
                    break;




                case "please close [Application]":

                    for (int Index = "please close ".Length; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string>.ProcInitialisation("System Process", Application, "close");
                    break;

                case "close [Application] please":

                    for (int Index = "close ".Length; Index <= Sentence.LastIndexOf(" please") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string>.ProcInitialisation("System Process", Application, "close");
                    break;

                case "close [Application] now":

                    for (int Index = "close ".Length; Index <= Sentence.LastIndexOf(" now") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string>.ProcInitialisation("System Process", Application, "close");
                    break;

                case "close [Application]":

                    for (int Index = "close ".Length; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string>.ProcInitialisation("System Process", Application, "close");
                    break;




                case "please search [Content] on [Web Application Keyword]":

                    Sentence = Sentence + " ";

                    Application = await Web_Application_Selector(Sentence.LastIndexOf(" on ") + " on ".Length - 1, Sentence);

                    for (int Index = "please search".Length; Index < Sentence.LastIndexOf(" on "); Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index].ToString();
                    }

                    await Proc<string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;


                    

                case "please search on [Web Application Keyword] [Content]":

                    Application = await Web_Application_Selector("please search on ".Length - 1, Sentence);

                    for (int Index = "please search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index];
                    }

                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc<string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;




                case "search on [Web Application Keyword] [Content]":

                    Application = await Web_Application_Selector("search on ".Length - 1, Sentence);

                    for (int Index = "search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index];

                    }

                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc<string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;




                case "search [Content] on [Web Application Keyword]":

                    Sentence = Sentence + " ";

                    for (int Index = 7; Index < Sentence.LastIndexOf(" on "); Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index].ToString();
                    }

                    Application = await Web_Application_Selector(Sentence.LastIndexOf(" on ") + " on ".Length - 1, Sentence);
                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc<string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;



                case "set a [Timer Interval] timer":
                    await Timer_Time_Selector(Sentence, time_interval, "set a ".Length - 1);

                    await Proc<System.Collections.Concurrent.ConcurrentDictionary<string, int>>.ProcInitialisation("Timer Process", null, time_interval);
                    break;


                case "set a [Timer Interval] timer please":
                    await Timer_Time_Selector(Sentence, time_interval, "set a ".Length - 1);

                    await Proc<System.Collections.Concurrent.ConcurrentDictionary<string, int>>.ProcInitialisation("Timer Process", null, time_interval);
                    break;



                case "please set a [Timer Interval] timer":
                    await Timer_Time_Selector(Sentence, time_interval, "please set a ".Length - 1);

                    await Proc<System.Collections.Concurrent.ConcurrentDictionary<string, int>>.ProcInitialisation("Timer Process", null, time_interval);
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

                            if (Time_Unit_Received == false)
                            {
                                if (Word_Buffer == "one")
                                {
                                    Time_Unit_Value_Buffer = 1;

                                    Time_Unit_Received = true;
                                }

                            }

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
            string Web_Application = "google";

            switch (start_index == Sentence.IndexOf(" google images "))
            {
                case true:
                    Web_Application = "google images";
                    break;

                case false:
                    switch (start_index == Sentence.IndexOf(" google news "))
                    {
                        case true:
                            Web_Application = "google news";
                            break;

                        case false:

                            switch (start_index == Sentence.IndexOf(" google "))
                            {
                                case true:
                                    Web_Application = "google";
                                    break;

                                case false:

                                    switch (start_index == Sentence.IndexOf(" wikipedia "))
                                    {
                                        case true:
                                            Web_Application = "wikipedia";
                                            break;

                                        case false:

                                            switch (start_index == Sentence.IndexOf(" ebay "))
                                            {
                                                case true:
                                                    Web_Application = "ebay";
                                                    break;

                                                case false:

                                                    switch (start_index == Sentence.IndexOf(" netflix "))
                                                    {
                                                        case true:
                                                            Web_Application = "netflix";
                                                            break;

                                                        case false:

                                                            switch (start_index == Sentence.IndexOf(" youtube "))
                                                            {
                                                                case true:
                                                                    Web_Application = "youtube";
                                                                    break;

                                                                case false:

                                                                    if (start_index == Sentence.IndexOf(" amazon "))
                                                                    {
                                                                        Web_Application = "amazon";
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }

            return Task.FromResult(Web_Application);
        }
        ~Natural_Language_Processing()
        {
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(0, GCCollectionMode.Forced, true, true);
        }

    }
}
