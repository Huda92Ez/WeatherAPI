using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.WeatherStatistics
{
    public class WeatherStatisticResponse
    {
        public decimal AverageTemperature { get; set; }
        public decimal HighestTemperature { get; set; }
        public decimal LowestTemperature { get; set; }
    }
}
