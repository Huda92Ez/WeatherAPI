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
        private readonly HashSet<string> _citiesCache;

        public CachingService()
        {
            _cache = new Dictionary<string, CityWeatherQueryResponse>();
            _citiesCache = new HashSet<string>();
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
            _citiesCache.Add(city);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetCachedCitiesAsync()
        {
            // Return the list of cached cities
            return Task.FromResult<IEnumerable<string>>(_citiesCache);
        }

        public  async Task<IEnumerable<string>> GetCacheValueAsync<T>(string cacheKey)
        {
            if (cacheKey == "_citiesCacheKey")
            {
                // Return the HashSet<string>
                await Task.FromResult<IEnumerable<string>>(_citiesCache);
            }

            return Enumerable.Empty<string>();

           
        }

        public Task CacheValueAsync<T>(string cacheKey, T data)
        {
            if (cacheKey == "_citiesCacheKey" && typeof(T) == typeof(IEnumerable<string>))
            {
                // Update the HashSet<string> with the new data
                _citiesCache.Clear();
                _citiesCache.UnionWith(data as IEnumerable<string>);
            }

            return Task.CompletedTask;
        }
    }
}
