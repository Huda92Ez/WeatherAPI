using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.WeatherQueryResponse;

namespace WeatherAPI.Models.BulkModels
{
    public class BulkResponse
    {
        [DataMember(Name = "Custom-Id", EmitDefaultValue = false)]
        public string? CustomId { get; set; }
        public string? Q { get; set; }
        public Location? Location { get; set; }
        public Current? Current { get; set; }
    }
}
