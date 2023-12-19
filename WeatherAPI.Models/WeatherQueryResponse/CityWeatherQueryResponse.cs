using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.WeatherQueryResponse
{
    public class CityWeatherQueryResponse
    {

        public Location location {  get; set; }

        public Current Current { get; set; }
    }
}
