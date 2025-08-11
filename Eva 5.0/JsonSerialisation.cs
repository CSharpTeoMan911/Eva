using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class JsonSerialisation
    {
        private static readonly DefaultContractResolver contractResolver = new DefaultContractResolver();
        private static readonly JsonSerializer serializer = new JsonSerializer()
        {
            ContractResolver = contractResolver
        };

        public static ReturnType JsonDeserialiser<ReturnType>(string json)
        {
            ReturnType return_item = default;

            try
            {
                using (StringReader sr = new StringReader(json))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        return_item = serializer.Deserialize<ReturnType>(reader);
                    }
                }
            }
            catch { }

            return (ReturnType)(object)return_item;
        }


        public static Task<string> JsonSerialiser<ReturnType>(ReturnType item)
        {
            string return_item = null;

            try
            {
                using (StringWriter tw = new StringWriter())
                {
                    using (JsonWriter writer = new JsonTextWriter(tw))
                    {
                        writer.Formatting = Formatting.Indented;
                        serializer.Serialize(writer, item);
                        return_item = tw.ToString();
                    }
                }
            }
            catch { }

            return Task.FromResult(return_item);
        }
    }
}
