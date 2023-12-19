using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.WeatherQueryResponse
{
    public class CurrentCondition
    {
        /// <summary>
        /// Gets or Sets Text
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// Gets or Sets Icon
        /// </summary>
        [DataMember(Name = "icon", EmitDefaultValue = false)]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public int? Code { get; set; }

    }
}
