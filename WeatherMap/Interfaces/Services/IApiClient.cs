using System;
using System.Threading.Tasks;
using WeatherMap.Infrastucture.DataTransfer;

namespace WeatherWeathers.Domain.Services
{
    public interface IApiClient<TResult> : IDisposable
    {
        Task<TResult> GetAsync(string apiRoute, Action<TResult> callback = null, params Parameter[] parameters);
        IApiClient<TResult> UseSufix(string urlSufix);
        Task<TResult> PostResultAsync(string apiRoute, object body = null, Action<TResult> callback = null);
        Task<TResult> PostAsync(string apiRoute, object body = null, Action<TResult> callback = null);
    }
}
