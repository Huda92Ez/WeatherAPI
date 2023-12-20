using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Net;
using System.Runtime.InteropServices;
using WatherAPI.Services;
using WeatherAPI.Controllers;
using WeatherAPI.Models.BulkModels;
using WeatherAPI.Models.General;
using WeatherAPI.Models.Repo;
using WeatherAPI.Models.WeatherQueryResponse;
using WeatherAPI.Services;

namespace TestWeatherAPI
{
    public class WeatherAPIServiceTests
    {
        [Fact]
        public async Task GetCityWeatherInfoTest_AddCachedData_andReturnFromCache()
        {
            // Arrange
            var cityName = "Makkah";
            var cachedData = new CityWeatherQueryResponse
            {
                location = new Location
                {
                    Name = "Makkah",
                    Region= "Makkah",
                    Country= "Saudi Arabia",
                    Lat= (decimal?)21.43,
                    Lon= (decimal?)39.83,
                    TzId= "Asia/Riyadh",
                    LocaltimeEpoch= 1703026236,
                    Localtime = "2023-12-20 1:50"
                },
                Current = new Current
                {
                   LastUpdatedEpoch= 1703025900,
                    LastUpdated= "2023-12-20 01:45",
                    TempC = (decimal?)23.4,
                    TempF= 74,
                    IsDay=0,
                    Condition= new CurrentCondition { Text= "Clear", Icon= "//cdn.weatherapi.com/weather/64x64/night/113.png", Code = 1000},
                    WindMph= (decimal?)3.8,
                    WindKph= (decimal?)6.1,
                    WindDegree= 343,
                    WindDir= "NNW",
                    PressureMb= 1014,
                    PressureIn= (decimal?)29.95,
                    PrecipMm=0,
                    PrecipIn=0,
                    Humidity =60,
                    Cloud =4,
                    FeelslikeC = (decimal?)25.1,
                    FeelslikeF= (decimal?)77.1,
                    VisKm = 10,
                    VisMiles= 6,
                    Uv=1,
                    GustMph= (decimal?)5.7,
                    GustKph= (decimal?)9.1,
                    //AirQuality = new CurrentAirQuality { Co=, No2=, O3=, So2=, Pm25=, Pm10=, UsEpaIndex=, GbDefraIndex= }

                }
            };
            var cachingServiceMock = new Mock<ICachingService>();
            cachingServiceMock.Setup(mock => mock.GetCachedWeatherAsync(cityName)).ReturnsAsync(cachedData);

            var weatherAPIClientMock = new Mock<IWeatherAPIClient>();
            var weatherService = new WeatherAPIService(weatherAPIClientMock.Object, cachingServiceMock.Object);

            // Act
            var result = await weatherService.GetCityWeatherInfo(cityName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Same(cachedData, result.Data);
        }

        [Fact]
        public async Task GetCityWeatherInfo_CachedDataDoesNotExist_ReturnsApiResponse()
        {
            // Arrange
            var cityName = "Makkah";
            var cachingServiceMock = new Mock<ICachingService>();
            cachingServiceMock.Setup(mock => mock.GetCachedWeatherAsync(cityName)).ReturnsAsync((CityWeatherQueryResponse)null);

            var apiResponse = new GenericResponse<CityWeatherQueryResponse>(true, (int)HttpStatusCode.OK, new CityWeatherQueryResponse
            {
                location = new Location
                {
                    Name = "Makkah",
                    Region = "Makkah",
                    Country = "Saudi Arabia",
                    Lat = (decimal?)21.43,
                    Lon = (decimal?)39.83,
                    TzId = "Asia/Riyadh",
                    LocaltimeEpoch = 1703026236,
                    Localtime = "2023-12-20 1:50"
                },
                Current = new Current
                {
                    LastUpdatedEpoch = 1703025900,
                    LastUpdated = "2023-12-20 01:45",
                    TempC = (decimal?)23.4,
                    TempF = 74,
                    IsDay = 0,
                    Condition = new CurrentCondition { Text = "Clear", Icon = "//cdn.weatherapi.com/weather/64x64/night/113.png", Code = 1000 },
                    WindMph = (decimal?)3.8,
                    WindKph = (decimal?)6.1,
                    WindDegree = 343,
                    WindDir = "NNW",
                    PressureMb = 1014,
                    PressureIn = (decimal?)29.95,
                    PrecipMm = 0,
                    PrecipIn = 0,
                    Humidity = 60,
                    Cloud = 4,
                    FeelslikeC = (decimal?)25.1,
                    FeelslikeF = (decimal?)77.1,
                    VisKm = 10,
                    VisMiles = 6,
                    Uv = 1,
                    GustMph = (decimal?)5.7,
                    GustKph = (decimal?)9.1,
                    //AirQuality = new CurrentAirQuality { Co=, No2=, O3=, So2=, Pm25=, Pm10=, UsEpaIndex=, GbDefraIndex= }

                }
            });
            var weatherAPIClientMock = new Mock<IWeatherAPIClient>();
            weatherAPIClientMock.Setup(mock => mock.PerformRequest(It.IsAny<string>())).ReturnsAsync(apiResponse);

            var weatherService = new WeatherAPIService(weatherAPIClientMock.Object, cachingServiceMock.Object);

            // Act
            var result = await weatherService.GetCityWeatherInfo(cityName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsType<CityWeatherQueryResponse>(result.Data);
            //Assert.Equal(apiResponse.Data, result.Data, new CityWeatherQueryResponseComparer());
            Assert.Same(apiResponse.Data, result.Data);
        }

        [Fact]
        public async Task GetCityWeatherInfo_ApiRequestFails_ReturnsErrorResponse()
        {
            // Arrange
            var cityName = "Makkah";
            var cachingServiceMock = new Mock<ICachingService>();
            cachingServiceMock.Setup(mock => mock.GetCachedWeatherAsync(cityName)).ReturnsAsync((CityWeatherQueryResponse)null);

            var errorResponse = new GenericResponse<CityWeatherQueryResponse>(false, (int)HttpStatusCode.InternalServerError, "API request failed");
            var weatherAPIClientMock = new Mock<IWeatherAPIClient>();
            weatherAPIClientMock.Setup(mock => mock.PerformRequest(It.IsAny<string>())).ReturnsAsync(errorResponse);

            var weatherService = new WeatherAPIService(weatherAPIClientMock.Object, cachingServiceMock.Object);

            // Act
            var result = await weatherService.GetCityWeatherInfo(cityName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal(errorResponse.ErrorMessage, result.ErrorMessage);
        }


        [Fact]
        public async Task WeatherPollingService_UpdateWeatherData_Success_CachesData()
        {
            // Arrange
            var cityName = "Makkah";
            var weatherServiceMock = new Mock<IWeatherAPIServices>();
            var cachingServiceMock = new Mock<ICachingService>();
            var loggerMock = new Mock<ILogger<IWeatherPollingService>>();
            var cityServiceMock = new Mock<ICitySrvice>();

            var response = new GenericResponse<CityWeatherQueryResponse>(true, (int)HttpStatusCode.OK, new CityWeatherQueryResponse
            {
                location = new Location
                {
                    Name = "Makkah",
                    Region = "Makkah",
                    Country = "Saudi Arabia",
                    Lat = (decimal?)21.43,
                    Lon = (decimal?)39.83,
                    TzId = "Asia/Riyadh",
                    LocaltimeEpoch = 1703026236,
                    Localtime = "2023-12-20 1:50"
                },
                Current = new Current
                {
                    LastUpdatedEpoch = 1703025900,
                    LastUpdated = "2023-12-20 01:45",
                    TempC = (decimal?)23.4,
                    TempF = 74,
                    IsDay = 0,
                    Condition = new CurrentCondition { Text = "Clear", Icon = "//cdn.weatherapi.com/weather/64x64/night/113.png", Code = 1000 },
                    WindMph = (decimal?)3.8,
                    WindKph = (decimal?)6.1,
                    WindDegree = 343,
                    WindDir = "NNW",
                    PressureMb = 1014,
                    PressureIn = (decimal?)29.95,
                    PrecipMm = 0,
                    PrecipIn = 0,
                    Humidity = 60,
                    Cloud = 4,
                    FeelslikeC = (decimal?)25.1,
                    FeelslikeF = (decimal?)77.1,
                    VisKm = 10,
                    VisMiles = 6,
                    Uv = 1,
                    GustMph = (decimal?)5.7,
                    GustKph = (decimal?)9.1,
                    //AirQuality = new CurrentAirQuality { Co=, No2=, O3=, So2=, Pm25=, Pm10=, UsEpaIndex=, GbDefraIndex= }

                }
            });
        
            weatherServiceMock.Setup(mock => mock.GetCityWeatherInfo(cityName)).ReturnsAsync(response);

            var weatherPollingService = new WeatherPollingService(weatherServiceMock.Object, loggerMock.Object, cachingServiceMock.Object, cityServiceMock.Object);

            // Act
            await weatherPollingService.UpdateCityWeatherData(cityName,response.Data!);

            // Assert
            cachingServiceMock.Verify(mock => mock.CacheWeatherAsync(cityName.ToLower(), response.Data!), Times.Once);
        }



        [Fact]
        public async Task GetWeathereDataOfCities_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var weatherAPIServicesMock = new Mock<IWeatherAPIServices>();
            var controller = new WeatherController(weatherAPIServicesMock.Object);

            var bulkRequest = new BulkRequest
            {
                Locations = new List<BulkRequestLocation>
                {
                    new BulkRequestLocation
                    {
                        q = "Madinah",
                        custom_id = "cityId1"
                    },
                    new BulkRequestLocation
                    {
                        q = "Dammam",
                        custom_id = "CityId2"
                    }
                }
            }; 

            var expectedResponse = new GenericResponse<BulkCommandResponse>(true, (int)HttpStatusCode.OK, new BulkCommandResponse());

            weatherAPIServicesMock
                .Setup(x => x.GetCitiesWeatherInfo(bulkRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetWeathereDataOfCities(bulkRequest);

            // Assert
           

            Assert.True(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
        }


        [Fact]
        public async Task GetWeathereDataOfCities_ApiError_ReturnsInternalServerError()
        {
            // Arrange
            var weatherAPIServicesMock = new Mock<IWeatherAPIServices>();
            var controller = new WeatherController(weatherAPIServicesMock.Object);

            var bulkRequest = new BulkRequest
            {
                Locations = new List<BulkRequestLocation>
                {
                    new BulkRequestLocation
                    {
                        q = "Madinah",
                        custom_id = "cityId1"
                    },
                    new BulkRequestLocation
                    {
                        q = "Dammam",
                        custom_id = "CityId2"
                    }
                }
            };

            var expectedErrorMessage = "Error from API";

            var expectedResponse = new GenericResponse<BulkCommandResponse>(false, (int)HttpStatusCode.BadRequest, expectedErrorMessage);

            weatherAPIServicesMock
                .Setup(x => x.GetCitiesWeatherInfo(bulkRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetWeathereDataOfCities(bulkRequest);

            // Assert
          

            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedErrorMessage, result.ErrorMessage);
        }

       



    }


    public class CityWeatherQueryResponseComparer : IEqualityComparer<CityWeatherQueryResponse>
    {
        public bool Equals(CityWeatherQueryResponse x, CityWeatherQueryResponse y)
        {
            
            return x?.location.Name == y?.location.Name
                && x?.location.Country == y?.location.Country
                && x?.location.Lon == y?.location.Lon
                && x?.location.Lat == y?.location.Lat
                && x?.location.TzId == y?.location.TzId;
        }

        public int GetHashCode(CityWeatherQueryResponse obj)
        {
            
            return obj?.GetHashCode() ?? 0;
        }





    }
}
