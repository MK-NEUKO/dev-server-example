using WeatherForecastApi.Location;
using WeatherForecastApi.Services.Abstractions;
using WeatherForecastApi.Services.LocationService;
using WeatherForecastApi.Services.WeatherForecastService;
using WeatherForecastApi.WeatherForecast;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy => policy
        .SetIsOriginAllowed(origin =>
        {
            // Check if the origin is from localhost
            if (Uri.TryCreate(origin, UriKind.Absolute, out var uri))
            {
                return uri.IsLoopback;
            }
            return false;
        })
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();



builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
