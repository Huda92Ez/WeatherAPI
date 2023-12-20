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
       
        private readonly IWeatherAPIServices _weatherAPIServices;

        public WeatherController( IWeatherAPIServices weatherAPIServices)
        {
           
            _weatherAPIServices = weatherAPIServices ?? throw new ArgumentNullException();
        }





        [HttpGet("/weather/city")]
        public async Task<GenericResponse<CityWeatherQueryResponse>> GetWaetherDataOfCity([FromQuery] string city)
        {
            var response = await _weatherAPIServices.GetCityWeatherInfo(city);
            return response;
        }

        [HttpPost("/weather/bulk")]
        public async Task<GenericResponse<BulkCommandResponse>> GetWeathereDataOfCities([FromBody] BulkRequest request)
        {
            var response = await _weatherAPIServices.GetCitiesWeatherInfo(request);
            return response;
        }


        [HttpGet("/weather/statistics")]
        public async Task<GenericResponse<WeatherStatisticResponse>> GetWeatherStatistics()
        {

            var response = await _weatherAPIServices.GetWeatherStatistics();
            
            return response;
        }



    }
}
