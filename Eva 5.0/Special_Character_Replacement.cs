using System;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Special_Character_Replacement
    {
        private static StringBuilder SentenceStringBuilder = new StringBuilder();

        protected static Task<StringBuilder> Remove_Special_Characters(string Sentence)
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

            SentenceStringBuilder.Clear();
            SentenceStringBuilder.Append(Sentence);

            try
            {

                if (Sentence != null)
                {
                    if (Sentence.Contains("\n") == true)
                    {
                        SentenceStringBuilder.Replace("\n", " new line ");
                    }

                    if (Sentence.Contains(".") == true)
                    {
                        SentenceStringBuilder.Replace(".", " dot ");
                    }

                    if (Sentence.Contains("#") == true)
                    {
                        SentenceStringBuilder.Replace("#", " sharp ");
                    }

                    if (Sentence.Contains("&") == true)
                    {
                        SentenceStringBuilder.Replace("&", " and ");
                    }

                    if (Sentence.Contains("|") == true)
                    {
                        SentenceStringBuilder.Replace("|", " pipe ");
                    }

                    if (Sentence.Contains(">") == true)
                    {
                        SentenceStringBuilder.Replace(">", " greater than ");
                    }

                    if (Sentence.Contains("<") == true)
                    {
                        SentenceStringBuilder.Replace("<", " less than ");
                    }

                    if (Sentence.Contains("^") == true)
                    {
                        SentenceStringBuilder.Replace("^", " raised to ");
                    }

                    if (Sentence.Contains(";") == true)
                    {
                        SentenceStringBuilder.Replace(";", " semicolon ");
                    }

                    if (Sentence.Contains("\"") == true)
                    {
                        SentenceStringBuilder.Replace("\"", String.Empty);
                    }

                    if (Sentence.Contains("'") == true)
                    {
                        SentenceStringBuilder.Replace("'", String.Empty);
                    }

                    if (Sentence.Contains("\\") == true)
                    {
                        SentenceStringBuilder.Replace("\\", " backslash ");
                    }

                    if (Sentence.Contains("/") == true)
                    {
                        SentenceStringBuilder.Replace("/", " forwardslash ");
                    }

                    if (Sentence.Contains("c++") == true)
                    {
                        SentenceStringBuilder.Replace("c++", "cpp");
                    }

                    if (Sentence.Contains("+") == true)
                    {
                        SentenceStringBuilder.Replace("+", " plus ");
                    }
                }
            }
            catch
            {
            }


            // [ END ] REMOVE SPECIAL CHARACTERS 


            return Task.FromResult(SentenceStringBuilder);
        }
    }
}
