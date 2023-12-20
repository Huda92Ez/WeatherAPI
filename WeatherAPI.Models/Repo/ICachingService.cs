using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.WeatherQueryResponse;

namespace WeatherAPI.Models.Repo
{
    public interface ICachingService
    {
        Task<CityWeatherQueryResponse?> GetCachedWeatherAsync(string cityName);
        Task CacheWeatherAsync(string cityName, CityWeatherQueryResponse weatherData);
        Task<IEnumerable<string>> GetCachedCitiesAsync();
        Task<IEnumerable<string>> GetCacheValueAsync<T>(string cacheKey);

        Task CacheValueAsync<T>(string cacheKey, T data);
    }
}
