using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.Repo;
using WeatherAPI.Models.WeatherQueryResponse;
using WeatherAPI.Services;

namespace WatherAPI.Services
{
    public class WeatherPollingService : BackgroundService, IWeatherPollingService
    {

        private readonly IWeatherAPIServices _weatherService;
        private readonly ILogger<IWeatherPollingService> _logger;
        private readonly TimeSpan _pollingInterval = TimeSpan.FromHours(1);
        private readonly ICachingService _cachingService;
        private readonly ICitySrvice _cityService;
        //private static readonly List<string> ListOfCities = new List<string>
        //{
        //"Makkah",
        //"Madina",
        //"Riyadh",
        //"Jeddah",
        //"Taif",
        //"Dammam",
        //"Abha",
        //"Jazan"
        //};
        public WeatherPollingService(IWeatherAPIServices weatherService, ILogger<IWeatherPollingService> logger, ICachingService cachingService, ICitySrvice cityService)
        {
            _weatherService = weatherService;
            _logger = logger;
            _cachingService = cachingService;
            _cityService = cityService;
        }
        protected  override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Trigger the weather data update
                    var cities = await _cityService.GetCitiesAsync();
                    await UpdateWeatherData(cities);
                    //await UpdateWeatherData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during weather data update.");
                }

                // Wait for the specified interval before the next update
                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }



        public async Task UpdateWeatherData(IEnumerable<string> cities)
        {
            
            foreach (var city in cities)
            {
                var response = await _weatherService.GetCityWeatherInfo(city);
                if (response.IsSuccess)
                {
                    
                    await UpdateCityWeatherData(city, response.Data!);
                    
                }
                else
                {
                    _logger.LogError($"Failed to update weather data for {city}. Error: {response.ErrorMessage}");
                }
            }
        }


        public async Task UpdateCityWeatherData(string cityName,CityWeatherQueryResponse res)
        {
            string lowerCaseCity = cityName.ToLower();
            // Update cache with the latest weather data
            await _cachingService.CacheWeatherAsync(lowerCaseCity, res!);

        }
    }
}
