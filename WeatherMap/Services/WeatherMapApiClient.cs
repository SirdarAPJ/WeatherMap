using System.Threading.Tasks;
using WeatherMap.Infraestructure.DataTransfer;
using WeatherMap.Infrastucture.DataTransfer;
using WeatherMap.Interfaces.Services;

namespace WeatherMap.Services
{
    public class WeatherMapApiClient : ApiClient<WeatherResult>, IWeatherMapApiClient
    {
        public async Task<WeatherResult> GetWeatherMap(string cityId, string appId, string language, string units)
        {
            Parameter[] parameters =
                {
                    new Parameter { Name = "id", Value = cityId.ToString() },
                    new Parameter { Name = "appid", Value = appId.ToString() },
                    new Parameter { Name = "lang", Value = language.ToString() },
                    new Parameter { Name = "units", Value = units.ToString() },
                };

            return await GetAsync("weather", null, parameters);
        }
    }
}
