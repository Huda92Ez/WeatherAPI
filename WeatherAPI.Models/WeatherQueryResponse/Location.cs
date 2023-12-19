using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.WeatherQueryResponse
{
    public class Location
    {

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Region
        /// </summary>
        //[DataMember(Name = "region", EmitDefaultValue = false)]
        [JsonProperty("region", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Region { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        //[DataMember(Name = "country", EmitDefaultValue = false)]
        [JsonProperty("country", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or Sets Lat
        /// </summary>
        //[DataMember(Name = "lat", EmitDefaultValue = false)]
        [JsonProperty("lat", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Lat { get; set; }

        /// <summary>
        /// Gets or Sets Lon
        /// </summary>
        //[DataMember(Name = "lon", EmitDefaultValue = false)]
        [JsonProperty("lon", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Lon { get; set; }

        /// <summary>
        /// Gets or Sets TzId
        /// </summary>
        [JsonProperty("tz_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TzId { get; set; }

        /// <summary>
        /// Gets or Sets LocaltimeEpoch
        /// </summary>
       // [DataMember(Name = "localtime_epoch", EmitDefaultValue = false)]
        [JsonProperty("localtime_epoch", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? LocaltimeEpoch { get; set; }

        /// <summary>
        /// Gets or Sets Localtime
        /// </summary>
        //[DataMember(Name = "localtime", EmitDefaultValue = false)]
        [JsonProperty("localtime", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Localtime { get; set; }
    }
}
