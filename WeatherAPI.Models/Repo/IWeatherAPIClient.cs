using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.BulkModels;
using WeatherAPI.Models.General;
using WeatherAPI.Models.WeatherQueryResponse;

namespace WeatherAPI.Models.Repo
{
    public interface IWeatherAPIClient
    {
        public Task<GenericResponse<BulkCommandResponse>> PerformRequest(string requestJSON, string url);

        public Task<GenericResponse<CityWeatherQueryResponse>> PerformRequest(string requestUrl);
    }
}
