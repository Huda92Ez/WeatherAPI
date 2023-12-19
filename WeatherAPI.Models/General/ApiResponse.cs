using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.General
{
    public class ApiResponse<T>
    {
        
        public int StatusCode { get; private set; }

        public IDictionary<string, string> Headers { get; private set; }

        public T Data { get; private set; }

        
        public ApiResponse(int statusCode, IDictionary<string, string> headers, T data)
        {
            this.StatusCode = statusCode;
            this.Headers = headers;
            this.Data = data;
        }

    }
}
