using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.Repo;

namespace WeatherAPI.Services
{
    public class CityService:ICitySrvice
    {

        private readonly ICachingService _cachingService;
        private readonly string _citiesCacheKey = "ListOfCities";

        public CityService(ICachingService cachingService)
        {
            _cachingService = cachingService;
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            var cachedCities = await _cachingService.GetCacheValueAsync<IEnumerable<string>>(_citiesCacheKey);
            return cachedCities ?? Enumerable.Empty<string>();
        }

        public async Task UpdateCitiesAsync(IEnumerable<string> cities)
        {
            await _cachingService.CacheValueAsync(_citiesCacheKey, cities);
        }
    }
}
