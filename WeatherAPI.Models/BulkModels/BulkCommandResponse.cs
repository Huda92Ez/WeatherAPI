using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.BulkModels
{
    public class BulkCommandResponse
    {

        public List<BulkItemResponse> Bulk { set; get; }
    }
}
