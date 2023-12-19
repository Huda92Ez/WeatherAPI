using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.WeatherQueryResponse
{
    public class CurrentAirQuality
    {
        /// <summary>
        /// Gets or Sets Co
        /// </summary>
        [DataMember(Name = "co", EmitDefaultValue = false)]
        public decimal? Co { get; set; }

        /// <summary>
        /// Gets or Sets No2
        /// </summary>
        [DataMember(Name = "no2", EmitDefaultValue = false)]
        public decimal? No2 { get; set; }

        /// <summary>
        /// Gets or Sets O3
        /// </summary>
        [DataMember(Name = "o3", EmitDefaultValue = false)]
        public decimal? O3 { get; set; }

        /// <summary>
        /// Gets or Sets So2
        /// </summary>
        [DataMember(Name = "so2", EmitDefaultValue = false)]
        public decimal? So2 { get; set; }

        /// <summary>
        /// Gets or Sets Pm25
        /// </summary>
        [DataMember(Name = "pm2_5", EmitDefaultValue = false)]
        public decimal? Pm25 { get; set; }

        /// <summary>
        /// Gets or Sets Pm10
        /// </summary>
        [DataMember(Name = "pm10", EmitDefaultValue = false)]
        public decimal? Pm10 { get; set; }

        /// <summary>
        /// Gets or Sets UsEpaIndex
        /// </summary>
        [DataMember(Name = "us-epa-index", EmitDefaultValue = false)]
        public int? UsEpaIndex { get; set; }

        /// <summary>
        /// Gets or Sets GbDefraIndex
        /// </summary>
        [DataMember(Name = "gb-defra-index", EmitDefaultValue = false)]
        public int? GbDefraIndex { get; set; }
    }
}
