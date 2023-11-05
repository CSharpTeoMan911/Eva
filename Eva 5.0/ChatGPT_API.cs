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
            public int max_tokens = 1000;
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

            if (input.Length / 4 <= 1000)
            {

                Remove_Superflous_Tokens(input.Length);

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
                        if (message_content != null)
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
            }
            else
            {
                return_type = typeof(Exception);
                result = "Input exceeds the maximum number of tokens";
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




        /// <summary>
        /// 
        /// METHOD THAT REMOVES THE NUMBER OF TOKENS ABOVE THE ACCEPTED LIMIT BY OPENAI'S CHATGPT API.
        /// THE ALGORITHM HAS AN O(N) TIME COMPLEXITY AND A O(1) SPACE COMPLEXITY
        /// 
        /// </summary>
        private static void Remove_Superflous_Tokens(int input_length)
        {

            // IF THE OBJECT THAT HOLDS THE CONVERATION CACHE HAS A NUMBER OF ELEMENTS ABOVE ZERO START THE SUPERFLOUS TOKEN
            // REMOVAL PROCESS.
            if (cached_conversation.Count > 0)
            {
                // VARIABLE THAT HOLDS THE TOTAL DETECTED TOKENS IN THE CACHED CONVERSATION
                int total_tokens = 0;

                // VARIABLE THAT HOLDS THE MAXIMUM LIMIT OF TOKENS THAT CAN BE USED IN A CONVERSATION
                int limit = 2148;


                // ALGORITHM FOR THE REMOVAL OF SUPERFLOUS TOKENS
                //
                // [ START ]

                // GET THE NUMBER OF TOKENS IN THE CHACHED CONVERSATION DEEP COPY OBJECT
                for (int i = 0; i < cached_conversation.Count; i++)
                {
                    // DIVIDE NUMBER OF STRINGS OF THE CURRENT MESSAGE BY 4 TO GET THE NUMBER OF TOKENS 
                    // FOR THE CURRENT MESSAGE ( 1 TOKEN = 4 ENGLISH CHARACTERS), AND INCREMENT THE
                    // 'total_tokens' VARIABLE BY THE NUMBER OF TOKENS.
                    total_tokens += cached_conversation[i].content.Length / 4;
                }

                // THE NUMBER OF TOKENS CURRENTLY PRESENT IN THE CONVERSATION, THE TOTAL NUMBER OF TOKENS WITHIN THE INPUT TO BE SENT WITHIN THE API REQUEST,
                // AND THE MAXIMUM NUMBER OF TOKENS THAT CHATGPT CAN GIVE AS A RESPONSE THROUGH THE API (WHICH IN THIS CASE IS 1000) REPRESENT THE TOTAL
                // NUMBER OF TOKENS.
                total_tokens += 1000 + input_length;

                // IF THE CURRENT NUMBER OF TOKENS EXCEEDS THE TOKEN LIMIT REMOVE MESSAGES FROM THE CONVERSATION TO SATISFY THE LIMIT REQUIREMENT
                if (total_tokens >= limit)
                {
                    for (int i = 0; i < cached_conversation.Count - 3; i++)
                    {
                        // GET THE CONTENT OF THE MESSAGE AT THE CURRENT INDEX
                        string item = cached_conversation[i].content;

                        // DIVIDE NUMBER OF STRINGS OF THE CURRENT MESSAGE BY 4 TO GET THE NUMBER OF TOKENS
                        // FOR THE CURRENT MESSAGE ( 1 TOKEN = 4 ENGLISH CHARACTERS).
                        int item_tokens = item.Length / 4;

                        // REMOVE THE MESSAGE FROM THE ORIGINAL OBJECT THAT STORES THE CACHED CONVERSATION
                        cached_conversation.RemoveAt(i);

                        // SUSTRACT THE NUMBER OF TOKENS FROM THE VARIABLE 'total_tokens'
                        total_tokens -= item_tokens;

                        // IF THE NUMBER OF TOKENS THAT ARE CURENTLY IN THE CONVERSATION ARE BELOW THE TOKEN LIMIT,
                        // INTERRUPT THE ALGORITHM.
                        if (total_tokens < limit)
                        {
                            break;
                        }
                    }
                }

                // IF THE TOTAL AMOUT OF TOKENS AFTER THE SUPERFLOUS TOKEN REMOVAL
                // STILL EXCEEDS THE LIMIT, THAN THE WHOLE CONVERSATION CACHE MUST
                // BE CLEARED IN ORDER TO FOR THE API TO BE ABLE TO PROCESS 
                // QUERIES.
                if(total_tokens > limit)
                {
                    cached_conversation.Clear();
                }

                // [ END ]
            }

        }

    }
}
