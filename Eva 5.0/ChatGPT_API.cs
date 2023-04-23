using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class ChatGPT_API
    {
        public static async Task<Tuple<Type, string>> Initiate_Chat_GPT(string input)
        {
            string result = null;
            Type return_type = null;

            try
            {
                // IF THE OPERATION COMPLETES WITHOUT ANY ERRORS
                // THE SET TYPE VALUE WITHIN THE TUPLE IS A
                // STRING AND THE STRING VALUE IS THE
                // CHATGPT RESPONSE
                //
                // [ BEIGN ]

                string api_key = await Settings.Get_Chat_GPT_Api_Key();

                OpenAI_API.OpenAIAPI api = new OpenAI_API.OpenAIAPI(api_key);
                OpenAI_API.Chat.Conversation chat = api.Chat.CreateConversation();

                chat.AppendUserInput(input);

                result = await chat.GetResponseFromChatbotAsync();
                return_type = typeof(string);

                // [ END ]
            }
            catch(Exception E)
            {
                // IF AN EXCEPTION OCCURS, THEN THE OPERATION
                // IS NOT SUCCESSFUL AND THE SET TYPE VALUE
                // WITHIN THE TUPLE IS AN EXCEPTION AND THE
                // STRING AND THE STRING VALUE IS THE
                // ERROR MESSAGE
                //
                // [ BEGIN ]

                if (E.Message.Contains("OpenAI rejected your authorization, most likely due to an invalid API Key.") == true)
                {
                    return_type = typeof(Exception);
                    result = "API authentification error";
                }
                else
                {
                    return_type = typeof(Exception);
                    result = "An error occured";
                }

                // [ END ]
            }

            return new Tuple<Type, string>(return_type, result);
        }
    }
}
