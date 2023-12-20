﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.Repo;
using WeatherAPI.Models.WeatherQueryResponse;

namespace WatherAPI.Services
{
    public class WeatherPollingService : BackgroundService, IWeatherPollingService
    {

        private readonly IWeatherAPIServices _weatherService;
        private readonly ILogger<IWeatherPollingService> _logger;
        private readonly TimeSpan _pollingInterval = TimeSpan.FromHours(1);
        private readonly ICachingService _cachingService;
        private static readonly List<string> ListOfCities = new List<string>
        {
        "Makkah",
        "Madina",
        "Riyadh",
        "Jeddah",
        "Taif",
        "Dammam",
        "Abha",
        "Jazan"
        };
        public WeatherPollingService(IWeatherAPIServices weatherService, ILogger<IWeatherPollingService> logger, ICachingService cachingService)
        {
            _weatherService = weatherService;
            _logger = logger;
            _cachingService = cachingService;
        }
        protected  override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Trigger the weather data update
                    await UpdateWeatherData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during weather data update.");
                }

                // Wait for the specified interval before the next update
                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }



        public async Task UpdateWeatherData()
        {
            
            foreach (var city in ListOfCities)
            {
                var response = await _weatherService.GetCityWeatherInfo(city);
                if (response.IsSuccess)
                {
                    await UpdateCityWeatherData(city, response.Data!);
                    //string lowerCaseCity = city.ToLower();
                    // Update cache with the latest weather data
                    //await _cachingService.CacheWeatherAsync(lowerCaseCity, response.Data!);
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
