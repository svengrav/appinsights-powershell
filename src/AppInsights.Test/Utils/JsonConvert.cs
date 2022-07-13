using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace AppInsights.Test
{
    public static class JsonConvert
    {
        public static string ConvertToJson(object objectToConvert)
            => Newtonsoft.Json.JsonConvert.SerializeObject(objectToConvert, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                MaxDepth = 5
            });
    }
}
