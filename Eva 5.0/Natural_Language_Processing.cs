using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Natural_Language_Processing
    {
        private Proc<string, string, string> proc;

        public static async Task<bool> PreProcessing<Sentence_Parameter> (Sentence_Parameter sentence_parameter)
        {
            string Sentence = sentence_parameter as string;

            switch (Sentence.IndexOf("please open ") == 0)
            {
                case true:

                    await PostProcessing<string, string>("please open [Application]", Sentence);
                    break;

                case false:

                    switch (Sentence.IndexOf("open ") == 0)
                    {
                        case true:

                            switch (Sentence.LastIndexOf(" please") + 6 == Sentence.Length - 1)
                            {
                                case true:

                                    await PostProcessing<string, string>("open [Application] please", Sentence);
                                    break;

                                case false:

                                    switch (Sentence.LastIndexOf(" now") + 3 == Sentence.Length - 1)
                                    {
                                        case true:

                                            await PostProcessing<string, string>("open [Application] now", Sentence);
                                            break;

                                        case false:

                                            await PostProcessing<string, string>("open [Application]", Sentence);
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

                    await PostProcessing<string, string>("please close [Application]", Sentence);
                    break;

                case false:

                    switch (Sentence.IndexOf("close ") == 0)
                    {
                        case true:

                            switch (Sentence.LastIndexOf(" please") + 6 == Sentence.Length - 1)
                            {
                                case true:

                                    await PostProcessing<string, string>("close [Application] please", Sentence);
                                    break;

                                case false:

                                    switch (Sentence.LastIndexOf(" now") + 3 == Sentence.Length - 1)
                                    {
                                        case true:

                                            await PostProcessing<string, string>("close [Application] now", Sentence);
                                            break;

                                        case false:

                                            await PostProcessing<string, string>("close [Application]", Sentence);
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

                    await PostProcessing<string, string>("please search on [Web Application Keyword] [Content]", Sentence);
                    break;

                case false:

                    switch ((Sentence.IndexOf("please search ") == 0) && (Sentence.Contains(" on ") == true))
                    {
                        case true:

                            await PostProcessing<string, string>("please search [Content] on [Web Application Keyword]", Sentence);
                            break;

                        case false:

                            switch (Sentence.IndexOf("search on ") == 0)
                            {
                                case true:

                                    await PostProcessing<string, string>("search on [Web Application Keyword] [Content]", Sentence);
                                    break;

                                case false:

                                    switch ((Sentence.IndexOf("search ") == 0) && (Sentence.Contains(" on ") == true))
                                    {
                                        case true:

                                            await PostProcessing<string, string>("search [Content] on [Web Application Keyword]", Sentence);
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }

            return true;
        }



        private static async Task<bool> PostProcessing<Parameter, Sentence_Parameter>(Parameter parameter, Sentence_Parameter sentence)
        {

            string Application = String.Empty;
            string WebApplicationSearchContent = String.Empty;

            string Sentence = sentence as string;
            string Param = parameter as string;


            switch (Param)
            {
                case "please open [Application]":

                    for (int Index = 12; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }


                    
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "open");
                    break;

                case "open [Application] please":

                    for (int Index = 5; Index <= Sentence.LastIndexOf(" please") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "open");
                    break;

                case "open [Application] now":

                    for (int Index = 5; Index <= Sentence.LastIndexOf(" now") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "open");
                    break;

                case "open [Application]":

                    for (int Index = 5; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "open");
                    break;




                case "please close [Application]":

                    for (int Index = 13; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "close");
                    break;

                case "close [Application] please":

                    for (int Index = 6; Index <= Sentence.LastIndexOf(" please") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "close");
                    break;

                case "close [Application] now":

                    for (int Index = 6; Index <= Sentence.LastIndexOf(" now") - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "close");
                    break;

                case "close [Application]":

                    for (int Index = 6; Index <= Sentence.Length - 1; Index++)
                    {
                        Application += Sentence[Index];

                    }
                    await Proc<string, string, string>.ProcInitialisation("System Process", Application, "close");
                    break;




                case "please search [Content] on [Web Application Keyword]":

                    for (int Index = "please search".Length; Index <= Sentence.Length - 1; Index++)
                    {
                        switch (Index <= Sentence.LastIndexOf(" on ") - 1)
                        {
                            case true:
                                WebApplicationSearchContent += Sentence[Index].ToString();
                                break;
                        }

                        

                        switch (Index >= Sentence.LastIndexOf(" on ") + 4)
                        {
                            case true:

                                Application += Sentence[Index].ToString();

                                break;
                        }
                    }

                   

                    Application = Application.Trim();
                    await Proc<string, string, string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;


                    

                case "please search on [Web Application Keyword] [Content]":

                    switch (16 == Sentence.IndexOf(" google images "))
                    {
                        case true:
                            Application = "google images";
                            break;

                        case false:
                            switch (16 == Sentence.IndexOf(" google news "))
                            {
                                case true:
                                    Application = "google news";
                                    break;

                                case false:

                                    switch (16 == Sentence.IndexOf(" google "))
                                    {
                                        case true:
                                            Application = "google";
                                            break;

                                        case false:

                                            switch (16 == Sentence.IndexOf(" wikipedia "))
                                            {
                                                case true:
                                                    Application = "wikipedia";
                                                    break;

                                                case false:

                                                    switch (16 == Sentence.IndexOf(" ebay "))
                                                    {
                                                        case true:
                                                            Application = "ebay";
                                                            break;

                                                        case false:

                                                            switch (16 == Sentence.IndexOf(" netflix "))
                                                            {
                                                                case true:
                                                                    Application = "netflix";
                                                                    break;

                                                                case false:

                                                                    switch (16 == Sentence.IndexOf(" youtube "))
                                                                    {
                                                                        case true:
                                                                            Application = "youtube";
                                                                            break;

                                                                        case false:

                                                                            switch (16 == Sentence.IndexOf(" amazon "))
                                                                            {
                                                                                case true:
                                                                                    Application = "amazon";
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
                            break;
                    }


                    for (int Index = "please search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index];
                    }

                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();



                    await Proc<string, string, string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;





                    

                case "search on [Web Application Keyword] [Content]":


                    switch (9 == Sentence.IndexOf(" google images "))
                    {
                        case true:
                            Application = "google images";
                            break;

                        case false:

                            switch (9 == Sentence.IndexOf(" google news "))
                            {
                                case true:
                                    Application = "google news";
                                    break;

                                case false:

                                    switch (9 == Sentence.IndexOf(" google "))
                                    {
                                        case true:
                                            Application = "google";
                                            break;

                                        case false:

                                            switch (9 == Sentence.IndexOf(" wikipedia "))
                                            {
                                                case true:
                                                    Application = "wikipedia";
                                                    break;

                                                case false:

                                                    switch (9 == Sentence.IndexOf(" ebay "))
                                                    {
                                                        case true:
                                                            Application = "ebay";
                                                            break;

                                                        case false:

                                                            switch (9 == Sentence.IndexOf(" netflix "))
                                                            {
                                                                case true:
                                                                    Application = "netflix";
                                                                    break;

                                                                case false:

                                                                    switch (9 == Sentence.IndexOf(" youtube "))
                                                                    {
                                                                        case true:
                                                                            Application = "youtube";
                                                                            break;

                                                                        case false:

                                                                            switch (9 == Sentence.IndexOf(" amazon "))
                                                                            {
                                                                                case true:
                                                                                    Application = "amazon";
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
                            break;
                    }


                    

                    for (int Index = "search on ".Length + Application.Length; Index <= Sentence.Length - 1; Index++)
                    {
                        WebApplicationSearchContent += Sentence[Index];

                    }

                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc<string, string, string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;




                case "search [Content] on [Web Application Keyword]":

                    for (int Index = 7; Index <= Sentence.Length - 1; Index++)
                    {
                        switch (Index <= Sentence.LastIndexOf(" on ") - 1)
                        {
                            case true:
                                WebApplicationSearchContent += Sentence[Index].ToString();
                                break;
                        }

                        switch (Index >= Sentence.LastIndexOf(" on ") + 4)
                        {
                            case true:

                                Application += Sentence[Index].ToString();

                                break;
                        }
                    }

                    Application = Application.Trim();
                    WebApplicationSearchContent = WebApplicationSearchContent.Trim();

                    await Proc<string, string, string>.ProcInitialisation("Online Process", Application, WebApplicationSearchContent);
                    break;
            }


            return true;
        }


        ~Natural_Language_Processing()
        {
            proc = null;

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(0, GCCollectionMode.Forced, true, true);
        }

    }
}
