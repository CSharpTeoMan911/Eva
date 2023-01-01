using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Special_Character_Replacement
    {
        public static async Task<string> Remove_Special_Characters_Initiator(string Sentence)
        {
            return await Remove_Special_Characters(Sentence);
        }

        private static Task<string> Remove_Special_Characters(string Sentence)
        {
            // [ BEGIN ] REMOVE SPECIAL CHARACTERS 
            //
            //            [ NOTE ] :
            //
            //            THE INPUT COULD TAKE SOME WORDS OR SEQUENCES OF WORDS SUCH AS [ NEW LINE ] 
            //            AND PARSE THEM INTO SPECIAL CHARACTERS, IN THIS EXAMPLE THIS SPECIAL CHARACTER WILL
            //            BE [ \n ], THUS TAKING THE INPUT WRONGLY. THE INPUT COULD ALSO HAVE SPECIAL
            //            CHARACTERS THAT POSSES A RISK TO SECURITY BY ALLOWING ATTACKERS TO INJECT
            //            COMMANDS INTO THE SHELL OF THE OS WHEN A PROCESS IS EXECUTED.


            if (Sentence.Contains("\n") == true)
            {
                Sentence.Replace("\n", " new line ");
            }

            if (Sentence.Contains("#") == true)
            {
                Sentence.Replace("#", "%23");
            }

            if (Sentence.Contains("&") == true)
            {
                Sentence.Replace("&", " and ");
            }

            if (Sentence.Contains("|") == true)
            {
                Sentence.Replace("|", " pipe ");
            }

            if (Sentence.Contains(">") == true)
            {
                Sentence.Replace(">", " greater than ");
            }

            if (Sentence.Contains("<") == true)
            {
                Sentence.Replace("<", " less than ");
            }

            if (Sentence.Contains("^") == true)
            {
                Sentence.Replace("^", " raised to ");
            }

            if (Sentence.Contains("%") == true)
            {
                Sentence.Replace("%", " percent ");
            }

            if (Sentence.Contains(";") == true)
            {
                Sentence.Replace(";", " semicolon ");
            }

            if (Sentence.Contains("\"") == true)
            {
                Sentence.Replace("\"", " double quote ");
            }

            if (Sentence.Contains("'") == true)
            {
                Sentence.Replace("'", " quote ");
            }

            if (Sentence.Contains("\\") == true)
            {
                Sentence.Replace("\\", " back slash ");
            }

            if (Sentence.Contains("/") == true)
            {
                Sentence.Replace("/", " forward slash ");
            }

            // [ END ] REMOVE SPECIAL CHARACTERS 


            return Task.FromResult(Sentence);
        }
    }
}
