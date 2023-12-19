using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.General
{
    public class GenericResponse<T>
    {

        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public GenericResponse(bool isSuccess,int statusCode, string error)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            ErrorMessage = error;
        }

        public GenericResponse(bool isSuccess, int statusCode,T data)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Data = data;
        }
    }

    public class ErrorResponseModel
    {
        [JsonProperty("error")]
        public APIError Error { set; get; }
    }

    public class APIError
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

    }



}