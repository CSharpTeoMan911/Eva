using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class StringFormatting
    {
        public static string whitespace = " ";

        public static string Format(StringBuilder builder)
        {
            builder.Replace("\n", " new line ");
            List<char> processed = new List<char>();

            for (int i = 0; i < builder.Length; i++)
                if (char.IsLetter(builder[i]) == false)
                    if (char.IsNumber(builder[i]) == false)
                        if (char.IsWhiteSpace(builder[i]) == false)
                            if (processed.Contains(builder[i]) == false)
                            {
                                processed.Add(builder[i]);
                                string original = builder[i].ToString();
                                builder.Replace(original, new StringBuilder(whitespace).Append(original).ToString());
                                i += 2;
                            }


            return builder.ToString();
        }

        public static string UrlEncode(StringBuilder builder)
        {
            StringBuilder return_value = new StringBuilder();

            for (int i = 0; i < builder.Length; i++)
            {
                if (char.IsLetter(builder[i]) == false)
                {
                    if (char.IsNumber(builder[i]) == false)
                    {
                        if (char.IsWhiteSpace(builder[i]) == false)
                        {
                            return_value.Append(System.Web.HttpUtility.UrlEncode(builder[i].ToString()));
                        }
                        else
                        {
                            return_value.Append(builder[i]);
                        }
                    }
                    else
                    {
                        return_value.Append(builder[i]);
                    }
                }
                else
                {
                    return_value.Append(builder[i]);
                }
            }

            return return_value.ToString();
        }
    }
}
