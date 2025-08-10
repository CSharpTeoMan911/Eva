using System.Text;

namespace Eva_5._0
{
    internal class StringFormatting
    {
        public static string RemoveNewLine(StringBuilder builder) => builder.Replace("\n", " new line ").ToString();

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
