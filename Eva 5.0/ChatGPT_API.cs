using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eva_5._0
{
    internal class ChatGPT_API
    {
        private static List<messages> cached_conversation = new List<messages>();

        // CLASS THAT IS SERIALIZED IN A JSON FILE FORMAT
        // TO SEND REQUEST TO CHATGPT OVER THE API
        internal class request
        {
            public string model;
            public messages[] messages;
            public double temperature = 0.5;
        }


        // CLASS THAT IS SERIALIZED WITHIN THE
        // "request" CLASS AS THE MESSAGE
        // CONTENT
        internal class messages
        {
            public string role;
            public string content;
        }


        public static void Clear_Conversation_Cache()
        {
            cached_conversation.Clear();
        }


        public static async Task<Tuple<Type, string>> Initiate_Chat_GPT(string input)
        {
            string result = null;
            Type return_type = null;

            // 'HttpClient' OBJECT NEEDED TO SEND HTTP REQUESTS TO THE OPENAI SERVER.
            System.Net.Http.HttpClient api_client = new System.Net.Http.HttpClient();

            try
            {
                // IF THE OPERATION COMPLETES WITHOUT ANY ERRORS
                // THE SET TYPE VALUE WITHIN THE TUPLE IS A
                // STRING AND THE STRING VALUE IS THE
                // CHATGPT RESPONSE
                //
                // [ BEIGN ]

                StringBuilder api_key_StringBuilder = new StringBuilder("Bearer");
                api_key_StringBuilder.Append(" ");
                api_key_StringBuilder.Append(await Settings.Get_Chat_GPT_Api_Key());


                api_client.DefaultRequestHeaders.Add("Authorization", api_key_StringBuilder.ToString());


                messages messages = new messages();
                messages.role = "user";
                messages.content = input;

                cached_conversation.Add(messages);

                request request = new request();
                request.model = "gpt-3.5-turbo";
                request.messages = cached_conversation.ToArray();
                request.temperature = 0.5;




                System.Net.Http.StringContent message_content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                try
                {
                    System.Net.Http.HttpResponseMessage response = await api_client.PostAsync("https://api.openai.com/v1/chat/completions", message_content);

                    System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());

                    try
                    {
                        JObject json_response = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

                        Tuple<Type, string> payload_processing_result = API_Payload_Processing(json_response);

                        return_type = payload_processing_result.Item1;
                        result = payload_processing_result.Item2;
                    }
                    catch
                    {
                        // IF AN EXCEPTION OCCURS, THEN THE OPERATION
                        // IS NOT SUCCESSFUL AND THE SET TYPE VALUE
                        // WITHIN THE TUPLE IS AN EXCEPTION AND THE
                        // STRING AND THE STRING VALUE IS THE
                        // ERROR MESSAGE

                        return_type = typeof(Exception);
                        result = "An error occured";
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Dispose();
                        }
                    }
                }
                catch
                {
                    // IF AN EXCEPTION OCCURS, THEN THE OPERATION
                    // IS NOT SUCCESSFUL AND THE SET TYPE VALUE
                    // WITHIN THE TUPLE IS AN EXCEPTION AND THE
                    // STRING AND THE STRING VALUE IS THE
                    // ERROR MESSAGE

                    return_type = typeof(Exception);
                    result = "An error occured";
                }
                finally
                {
                    if(message_content != null)
                    {
                        message_content.Dispose();
                    }
                }


                // [ END ]
            }
            catch
            {
                // IF AN EXCEPTION OCCURS, THEN THE OPERATION
                // IS NOT SUCCESSFUL AND THE SET TYPE VALUE
                // WITHIN THE TUPLE IS AN EXCEPTION AND THE
                // STRING AND THE STRING VALUE IS THE
                // ERROR MESSAGE

                return_type = typeof(Exception);
                result = "An error occured";
            }
            finally
            {
                if (api_client != null)
                {
                    api_client.Dispose();
                }
            }

            return new Tuple<Type, string>(return_type, result);
        }






        private static Tuple<Type, string> API_Payload_Processing(JObject json_response)
        {

            // DECONSTRUCT THE JSON PAYLOAD INTO ELEMENTS.
            // IF THE API RESPONSE STRUCTURE CORRESPONDS
            // TO AN ERROR MESSAGE, SET THE RETURN VALUE
            // TUPLE TYPE VALUE AS EXCEPTION AND THE 
            // STRING VALUE AS THE EXCEPTION MESSAGE,
            // ELSE SET THE RETURN VALUE TUPLE TYPE
            // AS STRING AND THE RETURN VALUE AS THE
            // RESPONSE MESSAGE.
            //
            //
            // [ BEGIN ]

            JToken error = json_response["error"];

            if (error != null)
            {
                JToken message = error["message"];

                if (message != null)
                {

                    string error_message = message.ToString();

                    if (error_message.Contains("You didn't provide an API key") == true)
                    {
                        return new Tuple<Type, string>(typeof(Exception), "API authentification error");
                    }
                    else if (error_message.Contains("Incorrect API key provided") == true)
                    {
                        return new Tuple<Type, string>(typeof(Exception), "API authentification error");
                    }
                    else
                    {
                        return new Tuple<Type, string>(typeof(Exception), "An error occured");
                    }

                }
                else
                {
                    return new Tuple<Type, string>(typeof(Exception), "An error occured");
                }
            }
            else
            {
                JToken choices = json_response["choices"];

                if (choices != null)
                {
                    JToken message = choices[0]["message"];

                    if (choices != null)
                    {
                        JToken content = message["content"];

                        if (content != null)
                        {
                            string content_message = content.ToString();

                            messages messages = new messages();
                            messages.role = "assistant";
                            messages.content = content_message;

                            cached_conversation.Add(messages);

                            return new Tuple<Type, string>(typeof(string), content_message);
                        }
                        else
                        {
                            return new Tuple<Type, string>(typeof(Exception), "An error occured");
                        }
                    }
                    else
                    {
                        return new Tuple<Type, string>(typeof(Exception), "An error occured");
                    }
                }
                else
                {
                    return new Tuple<Type, string>(typeof(Exception), "An error occured");
                }
            }

            // [ END ]
        }

    }
}
