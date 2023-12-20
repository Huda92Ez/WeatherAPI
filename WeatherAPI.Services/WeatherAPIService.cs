using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.BulkModels;
using WeatherAPI.Models.General;
using WeatherAPI.Models.Repo;
using WeatherAPI.Models.WeatherQueryResponse;
using WeatherAPI.Models.WeatherStatistics;

namespace WatherAPI.Services
{
    public class WeatherAPIService : IWeatherAPIServices
    {
        private readonly IWeatherAPIClient _client;
        private readonly ICachingService _cachingService;

        public WeatherAPIService(IWeatherAPIClient client, ICachingService cachingService)
        {
            _client = client;
            _cachingService = cachingService;
        }

       

        public async Task<GenericResponse<BulkCommandResponse>> GetCitiesWeatherInfo(BulkRequest request)
        {
            try
            {
                string url = $"/current.json?q=bulk";
                var BulkRequestJSON = JsonConvert.SerializeObject(request);
                var response = await _client.PerformRequest(BulkRequestJSON, url).ConfigureAwait(false);
                return response;
            }
            catch (HttpRequestException ex)
            {
                // Handle general HTTP request exceptions
                return new GenericResponse<BulkCommandResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);

            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                return new GenericResponse<BulkCommandResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);
            }
           
            
        }

       

        public async Task<GenericResponse<CityWeatherQueryResponse>> GetCityWeatherInfo(string cityName)
        {
            try
            {
                
                var cachedData = await _cachingService.GetCachedWeatherAsync(cityName);
                if (cachedData != null)
                {
                    return new GenericResponse<CityWeatherQueryResponse>(true, (int)HttpStatusCode.OK, cachedData);
                    
                }

                string url = $"/current.json?q={cityName}";
                var response = await _client.PerformRequest(url).ConfigureAwait(false);
                await _cachingService.CacheWeatherAsync(cityName,response.Data!);
                return response;
            }
            catch (HttpRequestException ex)
            {
                // Handle general HTTP request exceptions
                return new GenericResponse<CityWeatherQueryResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);
                
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                return new GenericResponse<CityWeatherQueryResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        public async Task<GenericResponse<WeatherStatisticResponse>> GetWeatherStatistics()
        {
            try
            {
                // Fetch weather data for all cities
                var cities = new List<string> { "Makkah", "Madina", "Riyadh", "Jeddah", "Taif", "Dammam", "Abha", "Jazan" };
                var bulkRequest = MapCitiesToBulkRequest(cities);
                var bulkResponse = await GetCitiesWeatherInfo(bulkRequest).ConfigureAwait(false);
                if (!bulkResponse.IsSuccess)
                {
                    
                    

                    return new GenericResponse<WeatherStatisticResponse>(false, bulkResponse.StatusCode, bulkResponse.ErrorMessage!);
                    
                }

                // Calculate aggregated statistics
                var statisticsResponse = CalculateWeatherStatistics(bulkResponse.Data!);

                return new GenericResponse<WeatherStatisticResponse>(true, bulkResponse.StatusCode, statisticsResponse);
                
            }
            catch (HttpRequestException ex)
            {
                // Handle general HTTP request exceptions
                return new GenericResponse<WeatherStatisticResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                return new GenericResponse<WeatherStatisticResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private WeatherStatisticResponse CalculateWeatherStatistics(BulkCommandResponse bulkResponse)
        {
            // Extract temperatures from the bulk response
            var temperatures = bulkResponse.Bulk.Select(item => item.Query.Current?.TempC).Where(temp => temp.HasValue).Select(temp => temp!.Value).ToList();

            // Calculate aggregated statistics
            var averageTemperature = temperatures.Average();
            var highestTemperature = temperatures.Max();
            var lowestTemperature = temperatures.Min();

            // Create a response object with the calculated statistics
            var statisticsResponse = new WeatherStatisticResponse
            {
                AverageTemperature = averageTemperature,
                HighestTemperature = highestTemperature,
                LowestTemperature = lowestTemperature
            };

            return statisticsResponse;
        }

        

        private BulkRequest MapCitiesToBulkRequest(List<string> cityNames)
        {
            var bulkRequest = new BulkRequest
            {
                Locations = new List<BulkRequestLocation>()
            };

            foreach (var cityName in cityNames)
            {
                var location = new BulkRequestLocation
                {
                    q = cityName,
                    custom_id = cityName + "-"
                };

                bulkRequest.Locations.Add(location);
            }

            return bulkRequest;
        }
    }
}




