using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.BulkModels;
using WeatherAPI.Models.General;
using WeatherAPI.Models.Repo;
using WeatherAPI.Models.WeatherQueryResponse;

namespace WatherAPI.Services
{
    public class WeatherAPIClient: IWeatherAPIClient
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClient;

        private string? _apiKey;
        private string? _apiUrl;

        public WeatherAPIClient(IConfiguration config, IHttpClientFactory httpClient)
        {
            _config = config;
            _httpClient = httpClient;
            _apiKey = _config[$"WeatherApiKey"];
            _apiUrl = _config[$"WeatherApiUrl"];
        }

        


        public async Task <GenericResponse<BulkCommandResponse>> PerformRequest(string requestJSON, string url)
        {
            try
            {
                string fullUrl = $"{_apiUrl}{url}&key={_apiKey}";
                var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");
                var client = _httpClient.CreateClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await client.PostAsync(fullUrl, httpContent).ConfigureAwait(false);
                string response = string.Empty;
                if (!responseMessage.IsSuccessStatusCode)
                {
                    // Handle API-specific errors
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(await responseMessage.Content.ReadAsStringAsync());

                    return new GenericResponse<BulkCommandResponse>(false, (int)responseMessage.StatusCode, errorResponse?.Error.Message);
                }
                else
                {
                    response = await responseMessage.Content.ReadAsStringAsync();
                    BulkCommandResponse weatherData = JsonConvert.DeserializeObject<BulkCommandResponse>(response);
                    return new GenericResponse<BulkCommandResponse>(true, (int)responseMessage.StatusCode, weatherData);
                }

            }
            catch (HttpRequestException ex)
            {
                // Handle general HTTP request exceptions

                return new GenericResponse<BulkCommandResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);

            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions

                return new GenericResponse<BulkCommandResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);

            }
            
        }







        public async Task<GenericResponse<CityWeatherQueryResponse>> PerformRequest(string url)
        {
            string fullUrl = $"{_apiUrl}{url}&key={_apiKey}";

            try
            {
                var client = _httpClient.CreateClient();
                var responseMessage = await client.GetAsync(fullUrl).ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                    CityWeatherQueryResponse weatherData = JsonConvert.DeserializeObject<CityWeatherQueryResponse>(jsonResponse);
                    return new GenericResponse<CityWeatherQueryResponse>(true, (int)responseMessage.StatusCode, weatherData);
                    
                }
                else
                {
                    // Handle API-specific errors
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(await responseMessage.Content.ReadAsStringAsync());

                    return new GenericResponse<CityWeatherQueryResponse>(false, (int)responseMessage.StatusCode, errorResponse?.Error.Message);
                    
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle general HTTP request exceptions

                return new GenericResponse<CityWeatherQueryResponse>(false, (int)HttpStatusCode.InternalServerError,  ex.Message);
                
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions

                return new GenericResponse<CityWeatherQueryResponse>(false, (int)HttpStatusCode.InternalServerError, ex.Message);
               
            }
        }
    }
}

