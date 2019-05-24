using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace WeatherMap.Helpers
{
    public class Json
    {
        public static async Task<T> DeserializeResource<T>(string fileName)
        {
            var assembly = typeof(Json).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}");
            using (var reader = new StreamReader(stream))
            {
                var jsonString = await reader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, Settings());
        }

        public static string Serialize<T>(T dataObject)
        {
            return JsonConvert.SerializeObject(dataObject, Settings());
        }

        private static JsonSerializerSettings Settings()
        {
            return new JsonSerializerSettings
            {
                Converters =
                {
                    new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" },
                },
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

    }
}
