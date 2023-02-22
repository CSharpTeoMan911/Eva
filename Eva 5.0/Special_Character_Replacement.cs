using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Special_Character_Replacement
    {
        protected static Task<string> Remove_Special_Characters(string Sentence)
        {
            // [ BEGIN ] REMOVE SPECIAL CHARACTERS 
            //
            //            [ NOTE ] :
            //
            //            THE INPUT COULD TAKE SOME WORDS OR SEQUENCES OF WORDS SUCH AS [ NEW LINE ] AND 
            //            PARSE THEM INTO SPECIAL CHARACTERS, IN THIS EXAMPLE THIS SPECIAL CHARACTER WILL
            //            BE [ \n ], THUS TAKING THE INPUT WRONGLY. THE INPUT COULD ALSO HAVE SPECIAL
            //            CHARACTERS THAT POSE A RISK TO THE SECURITY BY ALLOWING ATTACKERS TO INJECT
            //            COMMANDS INTO THE SHELL OF THE OS WHEN A PROCESS IS EXECUTED.


            if (Sentence != null)
            {
                if (Sentence.Contains("\n") == true)
                {
                    Sentence = Sentence.Replace("\n", " new line ");
                }

                if (Sentence.Contains("#") == true)
                {
                    Sentence = Sentence.Replace("#", "%23");
                }

                if (Sentence.Contains("&") == true)
                {
                    Sentence = Sentence.Replace("&", " and ");
                }

                if (Sentence.Contains("|") == true)
                {
                    Sentence = Sentence.Replace("|", " pipe ");
                }

                if (Sentence.Contains(">") == true)
                {
                    Sentence = Sentence.Replace(">", " greater than ");
                }

                if (Sentence.Contains("<") == true)
                {
                    Sentence = Sentence.Replace("<", " less than ");
                }

                if (Sentence.Contains("^") == true)
                {
                    Sentence = Sentence.Replace("^", " raised to ");
                }

                if (Sentence.Contains(";") == true)
                {
                    Sentence = Sentence.Replace(";", " semicolon ");
                }

                if (Sentence.Contains("\"") == true)
                {
                    Sentence = Sentence.Replace("\"", " double quote ");
                }

                if (Sentence.Contains("'") == true)
                {
                    Sentence = Sentence.Replace("'", " quote ");
                }

                if (Sentence.Contains("\\") == true)
                {
                    Sentence = Sentence.Replace("\\", " backslash ");
                }

                if (Sentence.Contains("/") == true)
                {
                    Sentence = Sentence.Replace("/", " forwardslash ");
                }
            }

            // [ END ] REMOVE SPECIAL CHARACTERS 


            return Task.FromResult(Sentence);
        }
    }
}
