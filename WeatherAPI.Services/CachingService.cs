using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.Repo;
using WeatherAPI.Models.WeatherQueryResponse;

namespace WatherAPI.Services
{
    public class CachingService: ICachingService
    {
        private readonly Dictionary<string, CityWeatherQueryResponse> _cache;

        public CachingService()
        {
            _cache = new Dictionary<string, CityWeatherQueryResponse>();
        }

        public  Task<CityWeatherQueryResponse?> GetCachedWeatherAsync(string cityName)
        {
            string city = cityName.ToLower();
            if (_cache.TryGetValue(city, out var cachedData))
            {
                return Task.FromResult(cachedData)!;
            }

            return Task.FromResult<CityWeatherQueryResponse?>(null);
        }

        public Task CacheWeatherAsync(string cityName, CityWeatherQueryResponse weatherData)
        {
            string city = cityName.ToLower();
            _cache[city] = weatherData;
            return Task.CompletedTask;
        }
    }
}
