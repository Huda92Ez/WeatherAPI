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
    }
}
