using Newtonsoft.Json.Linq;
using SharpToken;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace Eva_5._0
{
    internal class ChatGPT_API
    {


        private System.Timers.Timer dispatcherTimer;
        private ConcurrentQueue<ApiResponse> responseQueue = new ConcurrentQueue<ApiResponse>();

        private StringBuilder parse_builder = new StringBuilder();
        private StringBuilder content_builder = new StringBuilder();

        private List<messages> cached_conversation = new List<messages>();
        private int total_tokens;
        public List<string> gpt_models = new List<string>();
        public string gpt_model_buffer;
        private string data_label = "data: ";

        public delegate void Callback(ApiResponse response);
        private Callback callback;

        public ChatGPT_API()
        {

        }
        public ChatGPT_API(Callback callback)
        {
            dispatcherTimer = new System.Timers.Timer();
            dispatcherTimer.Elapsed += DispatcherTimer_Elapsed;
            dispatcherTimer.Interval = 100;

            this.callback = callback;
        }

        private async void DispatcherTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (responseQueue.Count > 0)
            {
                if (responseQueue.TryDequeue(out ApiResponse response))
                {
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        callback.Invoke(response);
                    });

                    if (response.isFinished)
                    {
                        dispatcherTimer.Stop();
                    }
                }
            }
        }


        public class ApiResponse
        {
            public enum PayloadType
            {
                Payload,
                Exception,
                Notification
            }
            public PayloadType type { get; set; }
            public string response { get; set; }
            public bool isFinished;
        }


        // CLASS THAT IS SERIALIZED IN A JSON FILE FORMAT
        // TO SEND REQUEST TO CHATGPT OVER THE API
        internal class request
        {
            public string model;
            public messages[] messages;
            public double temperature;
            public bool stream = true;
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

        public void Clear_Conversation_Cache()
        {
            parse_builder.Clear();
            cached_conversation.Clear();
            gpt_models.Clear();
        }


        public async Task Get_Available_Gpt_Models()
        {
            try
            {
                // 'HttpClient' OBJECT NEEDED TO SEND HTTP REQUESTS TO THE OPENAI SERVER.
                using (HttpClient api_client = new HttpClient())
                {
                    api_client.Timeout = TimeSpan.FromSeconds(10);

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
                    using (HttpResponseMessage response = await api_client.GetAsync("https://api.openai.com/v1/models"))
                    {
                        // READ THE JSON RESPONSE AND STORE IT INSIDE A STRING
                        string response_string = await response.Content.ReadAsStringAsync();

                        // DESERIALISE AND STORE THE JSON RESULT INSIDE AN OBJECT

                        models models = JsonSerialisation.JsonDeserialiser<models>(response_string);

                        // CLEAN THE DATA WITHIN THE EXTRACTED HTTP PAYLOAD BY ELIMINATING NON-GPT MODELS
                        // AND VISION BASED MODELS

                        if (models.data != null)
                        {
                            gpt_models.Clear();
                            for (int i = 0; i < models.data.Count; i++)
                            {
                                string current_model = models.data.ElementAt(i).id;

                                if (current_model.Contains("gpt") == true)
                                    if (current_model.Contains("voice") == false)
                                        if (current_model.Contains("vision") == false)
                                            if (current_model.Contains("audio") == false)
                                                if (current_model.Contains("realtime") == false)
                                                    if (current_model.Contains("tts") == false)
                                                        if (current_model.Contains("transcribe") == false)
                                                            if (current_model.Contains("image") == false)
                                                                if (current_model.Contains("search") == false)
                                                                    gpt_models.Add(current_model);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public void RemoveLastMessage()
        {
            if (cached_conversation?.Count > 0)
                cached_conversation.RemoveAt(cached_conversation.Count - 1);
        }

        public bool Initiate_Chat_GPT(string input, CancellationToken cancellation)
        {
            Task.Run(async () =>
            {
                dispatcherTimer.Start();
                if (!cancellation.IsCancellationRequested)
                {
                    string selected_model = await Settings.Get_Current_Chat_GPT__Model();
                    double temp = await Settings.Get_Current_Model_Temperature();
                    int tokens = await GetGptModelEncoding(input);
                    int max_tokens = GetModelContextWindow(selected_model);

                    if (tokens < max_tokens)
                    {
                        try
                        {
                            Remove_Superflous_Tokens(tokens, selected_model);

                            // 'HttpClient' OBJECT NEEDED TO SEND HTTP REQUESTS TO THE OPENAI SERVER.
                            using (HttpClient api_client = new HttpClient())
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

                                if (selected_model == null)
                                {
                                    request.model = "gpt-3.5-turbo";
                                    await Get_Available_Gpt_Models();
                                }
                                else
                                {
                                    request.model = selected_model;
                                }

                                request.messages = cached_conversation.ToArray();

                                temp = temp / 10;
                                request.temperature = temp;


                                if (!cancellation.IsCancellationRequested)
                                {
                                    using (System.Net.Http.StringContent message_content = new System.Net.Http.StringContent(await JsonSerialisation.JsonSerialiser(request), Encoding.UTF8, "application/json"))
                                    {
                                        if (!cancellation.IsCancellationRequested)
                                        {
                                            using (System.Net.Http.HttpResponseMessage response = await api_client.PostAsync("https://api.openai.com/v1/chat/completions", message_content))
                                            {
                                                if (response.IsSuccessStatusCode)
                                                {
                                                    if (!cancellation.IsCancellationRequested)
                                                    {
                                                        using (StreamReader response_stream = new StreamReader(await response.Content.ReadAsStreamAsync(), new UTF8Encoding()))
                                                        {
                                                            while (!response_stream.EndOfStream && !cancellation.IsCancellationRequested)
                                                            {
                                                                string resposeJson = await response_stream.ReadLineAsync();

                                                                parse_builder.Clear();
                                                                parse_builder.Append(resposeJson);

                                                                if (parse_builder.Length >= data_label.Length)
                                                                {
                                                                    string chunk = parse_builder.Remove(0, data_label.Length).ToString();

                                                                    if (!String.IsNullOrEmpty(chunk))
                                                                    {
                                                                        if (chunk != "[DONE]")
                                                                        {
                                                                            JObject data = JsonSerialisation.JsonDeserialiser<JObject>(chunk);

                                                                            if (data != null)
                                                                            {
                                                                                JToken choices = data["choices"];

                                                                                if (choices != null)
                                                                                {
                                                                                    JToken delta = choices[0]["delta"];

                                                                                    if (data != null)
                                                                                    {
                                                                                        JToken content = delta["content"];

                                                                                        if (content != null)
                                                                                        {
                                                                                            string content_string = content.ToString();

                                                                                            if (content_string != String.Empty)
                                                                                            {
                                                                                                content_builder.Append(content.ToString());

                                                                                                if (content_builder.Length >= 250)
                                                                                                {
                                                                                                    responseQueue.Enqueue(new ApiResponse()
                                                                                                    {
                                                                                                        type = ApiResponse.PayloadType.Payload,
                                                                                                        response = content_builder.ToString()
                                                                                                    });

                                                                                                    content_builder.Clear();
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        api_client.CancelPendingRequests();
                                                    }
                                                }
                                                else
                                                {
                                                    await Dispatch(new ApiResponse()
                                                    {
                                                        type = ApiResponse.PayloadType.Exception,
                                                        response = "An error occured",
                                                        isFinished = true
                                                    });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            api_client.CancelPendingRequests();
                                        }
                                    }
                                }
                                else
                                {
                                    api_client.CancelPendingRequests();
                                }

                                // [ END ]
                            }


                            if (content_builder.Length >= 0)
                            {
                                string last_content = content_builder.ToString();

                                if (!string.IsNullOrEmpty(last_content))
                                {
                                    responseQueue.Enqueue(new ApiResponse()
                                    {
                                        type = ApiResponse.PayloadType.Payload,
                                        response = content_builder.ToString()
                                    });
                                }

                                content_builder.Clear();
                            }

                            responseQueue.Enqueue(new ApiResponse()
                            {
                                type = ApiResponse.PayloadType.Notification,
                                response = "Stream finished",
                                isFinished = true
                            });
                        }
                        catch
                        {
                            // IF AN EXCEPTION OCCURS, THEN THE OPERATION
                            // IS NOT SUCCESSFUL AND THE SET TYPE VALUE
                            // WITHIN THE TUPLE IS AN EXCEPTION AND THE
                            // STRING AND THE STRING VALUE IS THE
                            // ERROR MESSAGE

                            await Dispatch(new ApiResponse()
                            {
                                type = ApiResponse.PayloadType.Exception,
                                response = "An error occured",
                                isFinished = true
                            });
                        }
                    }
                    else
                    {
                        await Dispatch(new ApiResponse()
                        {
                            type = ApiResponse.PayloadType.Exception,
                            response = "Input exceeds the maximum number of tokens",
                            isFinished = true
                        });
                    }
                }
                else
                {
                    RemoveLastMessage();
                }
            });

            return true;
        }

        private async Task Dispatch(ApiResponse payload)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                callback.Invoke(payload);
            });
        }

        private int GetModelContextWindow(string model)
        {
            return model switch
            {
                // GPT-3.5 family
                "gpt-3.5-turbo-16k" => 16385, // 16,385 tokens max
                "gpt-3.5-turbo-instruct" => 4096, // 4,096 tokens max
                "gpt-3.5-turbo-instruct-0914" => 4096, // 4,096 tokens max
                "gpt-3.5-turbo-1106" => 16385, // 16,385 tokens max
                "gpt-3.5-turbo-0125" => 16385, // 16,385 tokens max
                "gpt-3.5-turbo" => 4096, // legacy / 4,096 tokens max

                // GPT-4o models
                "gpt-4o-mini" => 128000, // 128,000 tokens max
                "gpt-4o" => 128000, // 128,000 tokens max

                // GPT-4.1 models
                "gpt-4.1-nano" => 1047576, // 1,047,576 tokens max
                "gpt-4.1-mini" => 1047576, // 1,047,576 tokens max
                "gpt-4.1" => 1047576, // 1,047,576 tokens max

                // GPT-4 turbo models
                "gpt-4-turbo-preview" => 128000, // 128,000 tokens max
                "gpt-4-turbo-2024-04-09" => 128000, // 128,000 tokens max
                "gpt-4-turbo" => 128000, // 128,000 tokens max

                // GPT-4 preview models
                "gpt-4-1106-preview" => 128000, // 128,000 tokens max
                "gpt-4-0125-preview" => 128000, // 128,000 tokens max

                // GPT-4 standard models
                "gpt-4-32k" => 32768, // 32,768 tokens max
                "gpt-4-0613" => 8192, // 8,192 tokens max
                "gpt-4" => 8192, // 8,192 tokens max

                // GPT-5 models
                "gpt-5-nano" => 400000, // 8,192 tokens max
                "gpt-5-mini" => 400000, // 32,768 tokens max
                "gpt-5" => 400000, // 400,000 tokens max

                // Fallback for unknown models
                _ => 4096 // default
            };
        }


        private string GetModelTokenizer(string model)
        {
            return model switch
            {
                // GPT-3.5 family (use cl100k_base)
                "gpt-3.5-turbo-16k" => "cl100k_base",
                "gpt-3.5-turbo-instruct" => "cl100k_base",
                "gpt-3.5-turbo-instruct-0914" => "cl100k_base",
                "gpt-3.5-turbo-1106" => "cl100k_base",
                "gpt-3.5-turbo-0125" => "cl100k_base",
                "gpt-3.5-turbo" => "cl100k_base",

                // GPT-4o models (use cl100k_base)
                "gpt-4o-mini" => "cl100k_base",
                "gpt-4o" => "cl100k_base",

                // GPT-4.1 models (use cl100k_base)
                "gpt-4.1-nano" => "cl100k_base",
                "gpt-4.1-mini" => "cl100k_base",
                "gpt-4.1" => "cl100k_base",

                // GPT-4 turbo models (use cl100k_base)
                "gpt-4-turbo-preview" => "cl100k_base",
                "gpt-4-turbo-2024-04-09" => "cl100k_base",
                "gpt-4-turbo" => "cl100k_base",

                // GPT-4 preview models (use cl100k_base)
                "gpt-4-1106-preview" => "cl100k_base",
                "gpt-4-0125-preview" => "cl100k_base",

                // GPT-4 standard models (use cl100k_base)
                "gpt-4-32k" => "cl100k_base",
                "gpt-4-0613" => "cl100k_base",
                "gpt-4" => "cl100k_base",

                // GPT-5 models (assuming cl100k_base)
                "gpt-5-nano" => "cl100k_base",
                "gpt-5-mini" => "cl100k_base",
                "gpt-5" => "cl100k_base",

                // Fallback for unknown models: assume r50k_base (legacy GPT-3)
                _ => "r50k_base"
            };
        }

        private async void Remove_Superflous_Tokens(int tokens, string model)
        {
            // IF THE NUMBERS OF EXTRACTED GPT MODELS IS GREATER THAN 0
            if (gpt_models.Count > 0)
            {
                int max_tokens = GetModelContextWindow(model);

                // IF THE TOTAL NUMBER OF TOKENS IS LESS THAN OR EQUAL WITH max_tokens
                if (total_tokens + tokens <= max_tokens)
                {
                    total_tokens += tokens;
                }
                // IF THE TOTAL NUMBER OF TOKENS IS MORE THAN max_tokens
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

                        // IF THE TOTAL NUMBER OF TOKENS IS LESS max_tokens
                        // STOP THE ITERATION
                        if (total_tokens <= max_tokens)
                        {
                            break;
                        }
                    }
                }
            }
        }


        public async Task<int> GetGptModelEncoding(string input)
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


                GptEncoding encoding = GptEncoding.GetEncoding(GetModelTokenizer(current_model));

                // GET THE NUMBER OF TOKENS THAT THE INPUT CONTAINS
                // IN ACCORDANCE WITH THE SELECTED MODEL AND ITS
                // ENCODING FORMAT
                tokens = encoding.Encode(input).Count;
            }

            return tokens;
        }

    }
}
