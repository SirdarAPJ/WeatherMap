using System.Threading.Tasks;
using WeatherMap.Infraestructure.DataTransfer;

namespace WeatherMap.Interfaces.Services
{
    public interface IWeatherMapApiClient
    {
        Task<WeatherResult> GetWeatherMap(string cityId, string appId, string language, string units);
    }
}
