
using WatherAPI.Services;
using WeatherAPI.Models.Repo;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
//The weather service (WeatherAPIService) doesn't need to maintain long-term state,
//and a new instance for each request (scoped) or usage (transient) can be created.
//This ensures that each request or operation gets its own instance, preventing any potential issues with shared state
builder.Services.AddScoped<IWeatherAPIServices, WeatherAPIService>();
builder.Services.AddScoped<IWeatherAPIClient, WeatherAPIClient>();
//The caching service (ICachingService) is responsible for storing and retrieving data from cache.
//Using a singleton ensures that the cache is shared across the entire application,
//providing a centralized store for all data
builder.Services.AddSingleton<ICachingService, CachingService>();
//builder.Services.AddScoped<ICachingService, CachingService>();

//builder.Services.AddHostedService<WeatherPollingService>();
//The background service (WeatherPollingService) is a long-lived service responsible
//for periodic updates. Using a singleton ensures that there is only one instance of
//the background service throughout the application's lifetime
builder.Services.AddHostedService(provider =>
{
    // Create a scope to resolve scoped services within the hosted service
    using (var scope = provider.CreateScope())
    {
        var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherAPIServices>();
        var cachingService = scope.ServiceProvider.GetRequiredService<ICachingService>();

        return new WeatherPollingService(weatherService, provider.GetRequiredService<ILogger<WeatherPollingService>>(), cachingService);
    }
});
builder.Services.AddMemoryCache();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
