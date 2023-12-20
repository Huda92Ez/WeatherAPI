using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using WeatherAPI.Models.BulkModels;
using WeatherAPI.Models.General;
using WeatherAPI.Models.Repo;
using WeatherAPI.Models.WeatherQueryResponse;
using WeatherAPI.Models.WeatherStatistics;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // the solution is maily stractured in 3 main layers
        // The models layer where domain models is existed.
        // The service layer that hold the buisness ligic
        // and the API layer (presentation layer)
        // This modular design is easier to understand, test, and extend.

        private readonly IWeatherAPIServices _weatherAPIServices;

        public WeatherController( IWeatherAPIServices weatherAPIServices)
        {
           
            _weatherAPIServices = weatherAPIServices ?? throw new ArgumentNullException();
        }


        /// <summary>
        /// Retrieves weather data for a specific city.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to get weather information for a specified city from the cach if it is found or using the Weather API.
        /// </remarks>
        /// <param name="city">The name of the city for which weather information is requested.</param>
        /// <returns>
        /// A <see cref="GenericResponse{CityWeatherQueryResponse}"/> containing the weather information for the specified city.
        /// </returns>
        /// <response code="200">Returns weather information for the specified city.</response>
        /// <response code="400">If the request is malformed or if the city parameter is missing or there is no location found matching parameter .</response>
        /// <response code="500">If there is an internal server error while processing the request.</response>


        [HttpGet("/weather/city")]
        public async Task<GenericResponse<CityWeatherQueryResponse>> GetWaetherDataOfCity([FromQuery] string city)
        {
            var response = await _weatherAPIServices.GetCityWeatherInfo(city);
            return response;
        }


        /// <summary>
        /// Retrieves weather data for multiple cities in bulk.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to get weather information for multiple cities simultaneously using the Weather API.
        /// </remarks>
        /// <param name="request">A <see cref="BulkRequest"/> object containing a list of cities for which weather information is requested.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the bulk weather information for the specified cities.
        /// </returns>
        /// <response code="200">Returns bulk weather information for the specified cities.</response>
        /// <response code="400">If the request is malformed or if the request body is missing or invalid.</response>
        /// <response code="500">If there is an internal server error while processing the request.</response>
         

        [HttpPost("/weather/bulk")]
        public async Task<GenericResponse<BulkCommandResponse>> GetWeathereDataOfCities([FromBody] BulkRequest request)
        {
            var response = await _weatherAPIServices.GetCitiesWeatherInfo(request);
            return response;
        }

        /// <summary>
        /// Retrieves weather statistics for a specific time period.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to get aggregated data like the average tempreture, highest temperature, and lowest temperature giving list of cities  using the Weather API.
        /// </remarks>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing weather statistics for the specified time period.
        /// </returns>
        /// <response code="200">Returns weather statistics for the specified time period.</response>
        
        /// <response code="500">If there is an internal server error while processing the request.</response>
        [HttpGet("/weather/statistics")]
        public async Task<GenericResponse<WeatherStatisticResponse>> GetWeatherStatistics()
        {

            var response = await _weatherAPIServices.GetWeatherStatistics();
            
            return response;
        }



    }
}
