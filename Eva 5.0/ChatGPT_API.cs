using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpToken;


namespace Eva_5._0
{
    internal class ChatGPT_API
    {
        private static List<messages> cached_conversation = new List<messages>();
        private static int total_tokens;
        public static List<string> gpt_models = new List<string>();
        public static string gpt_model_buffer;

        // CLASS THAT IS SERIALIZED IN A JSON FILE FORMAT
        // TO SEND REQUEST TO CHATGPT OVER THE API
        internal class request
        {
            public string model;
            public messages[] messages;
            public double temperature;
            public int max_tokens;
        }


        // CLASS THAT IS SERIALIZED WITHIN THE
        // "request" CLASS AS THE MESSAGE
        // CONTENT
        internal class messages
        {
            public string role;
            public string content;
        }


        // CLASS THAT CONTAINS THE CHARACTERISTICS
        // OF EVERY GPT MODEL
        internal class model
        {
            public string id { get; set; }
            public string @object { get; set; }
            public int created { get; set; }
            public string owned_by { get; set; }
        }


        // CLASS THAT CONTAINS THE PAYLOAD OF THE
        // API THAT IS RESPONSIBLE FOR FETCHING
        // THE AVAILABLE GPT MODELS
        internal class models
        {
            public string @object { get; set; }
            public List<model> data { get; set; } 
        }

        public static void Clear_Conversation_Cache()
        {
            cached_conversation.Clear();
        }


        public static async Task<bool> Get_Available_Gpt_Models()
        {
            // 'HttpClient' OBJECT NEEDED TO SEND HTTP REQUESTS TO THE OPENAI SERVER.
            System.Net.Http.HttpClient api_client = new System.Net.Http.HttpClient();

            // 'HttpResponseMessage' OBJECT NEEDED TO GET THE RESPONSE OF THE HTTP GET QUERY
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                // 'StringBuilder' OBJECT USED TO CREATE THE HTTP HEADER AUTHORISATION STRING.
                // 'StringBuilder' IS A MUTABLE OBJECT, THUS ELIMINATING THE NEED FOR 
                // MEMORY ALLOCATIONS FOR EACH STRING MANIPULATION
                StringBuilder api_key_StringBuilder = new StringBuilder("Bearer");
                api_key_StringBuilder.Append(" ");
                api_key_StringBuilder.Append(await Settings.Get_Chat_GPT_Api_Key());

                // ADD THE AUTHORISATION HEADER FROM THE 'StringBuilder'
                api_client.DefaultRequestHeaders.Add("Authorization", api_key_StringBuilder.ToString());

                // INITIATE AN HTTP GET QUERY TO THE 'https://api.openai.com/v1/models' API ENDPOINT
                // IN ORDER TO GET A LIST OF AVAILABLE GPT MODELS
                response = await api_client.GetAsync("https://api.openai.com/v1/models");

                // READ THE JSON RESPONSE AND STORE IT INSIDE A STRING
                string response_string = await response.Content.ReadAsStringAsync();

                // DESERIALISE AND STORE THE JSON RESULT INSIDE AN OBJECT

                models models = await JsonSerialisation.JsonDeserialiser<models>(response_string);


                // CLEAN THE DATA WITHIN THE EXTRACTED HTTP PAYLOAD BY ELIMINATING NON-GPT MODELS
                // AND VISION BASED MODELS

                if(models.data != null)
                    for (int i = 0; i < models.data.Count; i++)
                    {
                        string current_model = models.data.ElementAt(i).id;

                        if (current_model.Contains("gpt") == true)
                        {
                            if (current_model.Contains("vision") == false)
                            {
                                gpt_models.Add(current_model);
                            }
                        }
                    }
            }
            catch
            {

            }
            finally
            {
                api_client?.Dispose();
                response?.Dispose();
            }

            return true;
        }

        public static async Task<Tuple<Type, string>> Initiate_Chat_GPT(string input)
        {
            string result = null;
            Type return_type = null;

            string selected_model = await Settings.Get_Current_Chat_GPT__Model();
            double temp = await Settings.Get_Current_Model_Temperature();
            int tokens = await GetGptModelEncoding(input);

            if (tokens < 4096)
            {
                Remove_Superflous_Tokens(tokens);

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

                    switch(selected_model == null)
                    {
                        case true:
                            request.model = "gpt-3.5-turbo";
                            await Get_Available_Gpt_Models();
                            break;
                        case false:
                            request.model = selected_model;
                            break;
                    }

                    request.messages = cached_conversation.ToArray();

                    temp = temp / 10;
                    request.temperature = temp;
                    request.max_tokens = 4096;



                    System.Net.Http.StringContent message_content = new System.Net.Http.StringContent(await JsonSerialisation.JsonSerialiser(request), Encoding.UTF8, "application/json");

                    try
                    {
                        System.Net.Http.HttpResponseMessage response = await api_client.PostAsync("https://api.openai.com/v1/chat/completions", message_content);

                        try
                        {
                            JObject json_response = await JsonSerialisation.JsonDeserialiser<JObject>(await response.Content.ReadAsStringAsync());

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
                            response?.Dispose();
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
                        message_content?.Dispose();
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
                    api_client?.Dispose();
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


        private static async void Remove_Superflous_Tokens(int tokens)
        {
            // IF THE NUMBERS OF EXTRACTED GPT MODELS IS GREATER THAN 0
            if (gpt_models.Count > 0)
            {
                // IF THE TOTAL NUMBER OF TOKENS IS LESS THAN OR EQUAL WITH 4096
                if (total_tokens + tokens <= 4096)
                {
                    total_tokens += tokens;
                }
                // IF THE TOTAL NUMBER OF TOKENS IS MORE THAN 4096
                else
                {
                    // ITERATE EACH MESSAGE WITHIN THE CACHED CONVERSATION
                    for (int i = 0; i < cached_conversation.Count; i++)
                    {
                        // GET THE CONTENT OF THE CURRENT ITEM
                        string message = cached_conversation.ElementAt(i).content;

                        // GET THE NUMBER OF TOKENS OF THE CURRENT ITEM'S CONTENT
                        // AND SUBSTRACT THEM FROM THE TOTAL TOKENS
                        total_tokens -= await GetGptModelEncoding(message);

                        // REMOVE THE CURRENT ITEM FROM THE CONVERSATION  CACHE
                        cached_conversation.RemoveAt(i);

                        // IF THE TOTAL NUMBER OF TOKENS IS LESS 4096
                        // STOP THE ITERATION
                        if (total_tokens <= 4096)
                        {
                            break;
                        }
                    }
                }
            }
        }


        public static async Task<int> GetGptModelEncoding(string input)
        {
            int tokens = 0;

            // IF THE NUMBERS OF EXTRACTED GPT MODELS IS GREATER THAN 0
            if (gpt_models.Count > 0)
            {
                // GET THE CURRENT SELECTED MODEL WITHI THE APPLICATION'S SETTINGS
                string current_model = await Settings.Get_Current_Chat_GPT__Model();

                // IF THE CURRENT MODEL IS NULL
                if (current_model == null)
                {
                    // GET THE FIRST MODEL WITHIN THE LIST OF MODELS
                    current_model = gpt_models.First();

                    // SET THE FIRST MODEL WITHIN THE LIST OF MODELS
                    // AS THE SELECTED MODEL WITHIN TE APPLICATION'S
                    // SETTINGS FILE
                    await Settings.Set_Current_Chat_GPT__Model(current_model);
                }

                // IF ANY MODEL WAS SELECTED UNTIL NOW
                if (gpt_model_buffer != null)
                {
                    // IF THE MODEL SELECTED UNTIL NOW
                    // DOES NOT MATCH THE ONE IN THE
                    // SETTINGS FILE
                    if (gpt_model_buffer != current_model)
                    {
                        // CLEAR THE CONVERSATION CACHE
                        Clear_Conversation_Cache();
                        // RESET THE TOTAL NUMBER OF TOKENS TO 0
                        total_tokens = 0;
                    }
                }
                // IF NO MODEL WAS SELECTED UNTIL NOW
                else
                {
                    gpt_model_buffer = current_model;
                }

                // GET THE ENCODING THAT THE SELECTED MODEL USES
                GptEncoding encoding = GptEncoding.GetEncodingForModel(current_model);

                // GET THE NUMBER OF TOKENS THAT THE INPUT CONTAINS
                // IN ACCORDANCE WITH THE SELECTED MODEL AND ITS
                // ENCODING FORMAT
                tokens = encoding.Encode(input).Count;
            }

            return tokens;
        }

    }
}
