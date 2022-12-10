using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Content_Tokenizer:A_p_l____And____P_r_o_c
    {
        public static async Task<Tuple<string, int>> Application_Executable_Content_Extraction_Initiator(bool web_application, int start_index, string Sentence)
        {
            return await Application_Executable_Content_Extraction(web_application, start_index, Sentence);
        }


        private static Task<Tuple<string, int>> Application_Executable_Content_Extraction(bool web_application, int start_index, string Sentence)
        {
            string extracted_application = String.Empty;

            if (web_application == true)
            {
                extracted_application = "google";
            }


            int current_char_index = 0;
            string application_buffer = String.Empty;



            for (int index = start_index; index < Sentence.Length; index++)
            {

                char value = '*';

                Dictionary<char, char> current_index_char_tokens = new Dictionary<char, char>();


                switch(web_application)
                {
                    case true:
                        Web_A_p_l_Name__And__A_p_l___E_x__Name_Tokens.TryGetValue(current_char_index, out current_index_char_tokens);
                        break;

                    case false:
                        A_p_l_Name__And__A_p_l___E_x__Name_Tokens.TryGetValue(current_char_index, out current_index_char_tokens);
                        break;
                }


                current_index_char_tokens.TryGetValue(Sentence[index], out value);


                if (Sentence[index] != value)
                {
                    break;
                }


                application_buffer += value;
                current_char_index++;
            }







            string retrieved_application_key_value = String.Empty;


            switch(web_application)
            {
                case true:
                    Web_A_p_l_Name__And__A_p_l___E_x__Name.TryGetValue(application_buffer, out retrieved_application_key_value);
                    break;

                case false:
                    A_p_l_Name__And__A_p_l___E_x__Name.TryGetValue(application_buffer, out retrieved_application_key_value);
                    break;
            }


            if(retrieved_application_key_value == String.Empty)
            {
                application_buffer = String.Empty;
            }

            





            extracted_application = application_buffer;


            return Task.FromResult(new Tuple<string, int>(extracted_application, current_char_index));
        }
    }
}
