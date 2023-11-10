using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System
{
    public static class StringHelper
    {
        public static T? ToJson<T>(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(str)!;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        public static string ToJson(this object obj, bool isLowerCase = false)
        {
            try
            {
                if (isLowerCase)
                {
                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore

                    };
                    return JsonConvert.SerializeObject(obj, serializerSettings);
                }
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
