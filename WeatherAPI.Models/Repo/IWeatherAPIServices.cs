using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.BulkModels;
using WeatherAPI.Models.General;
using WeatherAPI.Models.WeatherQueryResponse;
using WeatherAPI.Models.WeatherStatistics;

namespace WeatherAPI.Models.Repo
{
    public interface IWeatherAPIServices
    {

        Task<GenericResponse<CityWeatherQueryResponse>> GetCityWeatherInfo (string cityName);

        Task<GenericResponse<BulkCommandResponse>> GetCitiesWeatherInfo(BulkRequest request);
        Task<GenericResponse<WeatherStatisticResponse>> GetWeatherStatistics();

        //Task<WeatherStatisticResponse> CalculateWeatherStatistics(BulkCommandResponse bulkResponse);
    }
}
