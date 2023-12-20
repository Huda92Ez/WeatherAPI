# WeatherAPI
Project Overview

The main purpose of this project to develop a backend system that communicate with Weatherapi.com ,implement caching and polling mechanisms, and
exposes endpoints for retrieving weather data.

API Endpoints

1. GET /weather/city
Description: Retrieves weather data for a specific city.
Parameters:
city (query parameter): The name of the city for which weather information is requested.
2. POST /weather/bulk
Description: Retrieves weather data for multiple cities in bulk.
Request Body:
BulkRequest: A JSON object containing a list of cities.
3. GET /weather/statistics
Description: Retrieves highest, lowest,and average temprature for a specific cities list.

Setting Up

To setup this project, you need to clone the git repo

git clone https://github.com/Huda92Ez/WeatherAPI.git

cd WeatherAPI

followed by

dotnet restore
