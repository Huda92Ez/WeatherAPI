using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.WeatherQueryResponse
{
    public class Current
    {
        /// <summary>
        /// Gets or Sets LastUpdatedEpoch
        /// </summary>
        //[DataMember(Name = "last_updated_epoch", EmitDefaultValue = false)]
        [JsonProperty("last_updated_epoch", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? LastUpdatedEpoch { get; set; }

        /// <summary>
        /// Gets or Sets LastUpdated
        /// </summary>
        //[DataMember(Name = "last_updated", EmitDefaultValue = false)]
        [JsonProperty("last_updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LastUpdated { get; set; }

        /// <summary>
        /// Gets or Sets TempC
        /// </summary>
        //[DataMember(Name = "temp_c", EmitDefaultValue = false)]
        [JsonProperty("temp_c", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? TempC { get; set; }

        /// <summary>
        /// Gets or Sets TempF
        /// </summary>
        //[DataMember(Name = "temp_f", EmitDefaultValue = false)]
        [JsonProperty("temp_f", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? TempF { get; set; }

        /// <summary>
        /// Gets or Sets IsDay
        /// </summary>
        //[DataMember(Name = "is_day", EmitDefaultValue = false)]
        [JsonProperty("is_day", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? IsDay { get; set; }

        /// <summary>
        /// Gets or Sets Condition
        /// </summary>
        //[DataMember(Name = "condition", EmitDefaultValue = false)]
        [JsonProperty("condition", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CurrentCondition Condition { get; set; }

        /// <summary>
        /// Gets or Sets WindMph
        /// </summary>
       // [DataMember(Name = "wind_mph", EmitDefaultValue = false)]
        [JsonProperty("wind_mph", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? WindMph { get; set; }

        /// <summary>
        /// Gets or Sets WindKph
        /// </summary>
        //[DataMember(Name = "wind_kph", EmitDefaultValue = false)]
        [JsonProperty("wind_kph", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? WindKph { get; set; }

        /// <summary>
        /// Gets or Sets WindDegree
        /// </summary>
        //[DataMember(Name = "wind_degree", EmitDefaultValue = false)]
        [JsonProperty("wind_degree", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? WindDegree { get; set; }

        /// <summary>
        /// Gets or Sets WindDir
        /// </summary>
        //[DataMember(Name = "wind_dir", EmitDefaultValue = false)]
        [JsonProperty("wind_dir", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string WindDir { get; set; }

        /// <summary>
        /// Gets or Sets PressureMb
        /// </summary>
        //[DataMember(Name = "pressure_mb", EmitDefaultValue = false)]
        [JsonProperty("pressure_mb", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? PressureMb { get; set; }

        /// <summary>
        /// Gets or Sets PressureIn
        /// </summary>
        //[DataMember(Name = "pressure_in", EmitDefaultValue = false)]
        [JsonProperty("pressure_in", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? PressureIn { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMm
        /// </summary>
        //[DataMember(Name = "precip_mm", EmitDefaultValue = false)]
        [JsonProperty("precip_mm", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? PrecipMm { get; set; }

        /// <summary>
        /// Gets or Sets PrecipIn
        /// </summary>
        //[DataMember(Name = "precip_in", EmitDefaultValue = false)]
        [JsonProperty("precip_in", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? PrecipIn { get; set; }

        /// <summary>
        /// Gets or Sets Humidity
        /// </summary>
        //[DataMember(Name = "humidity", EmitDefaultValue = false)]
        [JsonProperty("humidity", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Humidity { get; set; }

        /// <summary>
        /// Gets or Sets Cloud
        /// </summary>
        //[DataMember(Name = "cloud", EmitDefaultValue = false)]
        [JsonProperty("cloud", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Cloud { get; set; }

        /// <summary>
        /// Gets or Sets FeelslikeC
        /// </summary>
        //[DataMember(Name = "feelslike_c", EmitDefaultValue = false)]
        [JsonProperty("feelslike_c", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? FeelslikeC { get; set; }

        /// <summary>
        /// Gets or Sets FeelslikeF
        /// </summary>
        //[DataMember(Name = "feelslike_f", EmitDefaultValue = false)]
        [JsonProperty("feelslike_f", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? FeelslikeF { get; set; }

        /// <summary>
        /// Gets or Sets VisKm
        /// </summary>
        //[DataMember(Name = "vis_km", EmitDefaultValue = false)]
        [JsonProperty("vis_km", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? VisKm { get; set; }

        /// <summary>
        /// Gets or Sets VisMiles
        /// </summary>
        //[DataMember(Name = "vis_miles", EmitDefaultValue = false)]
        [JsonProperty("vis_miles", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? VisMiles { get; set; }

        /// <summary>
        /// Gets or Sets Uv
        /// </summary>
        //[DataMember(Name = "uv", EmitDefaultValue = false)]
        [JsonProperty("uv", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double? Uv { get; set; }

        /// <summary>
        /// Gets or Sets GustMph
        /// </summary>
        //[DataMember(Name = "gust_mph", EmitDefaultValue = false)]
        [JsonProperty("gust_mph", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? GustMph { get; set; }

        /// <summary>
        /// Gets or Sets GustKph
        /// </summary>
        //[DataMember(Name = "gust_kph", EmitDefaultValue = false)]
        [JsonProperty("gust_kph", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? GustKph { get; set; }

        /// <summary>
        /// Gets or Sets AirQuality
        /// </summary>
        //[DataMember(Name = "air_quality", EmitDefaultValue = false)]
        [JsonProperty("air_quality", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CurrentAirQuality AirQuality { get; set; }
    }
}
