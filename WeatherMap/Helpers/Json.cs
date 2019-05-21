using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;

namespace WeatherMap.Helpers
{
    public class Json
    {
        public static T DeserializeFromFile<T>(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T)serializer.Deserialize(file, typeof(T));
            }
        }

        public static T DeserializeResource<T>(string fileName)
        {
            var assembly = typeof(Json).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}");
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

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
