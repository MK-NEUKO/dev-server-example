using AutoMapper;
using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Application.GetLocations;
using WeatherForecastApi.Domain.Abstractions;
using WeatherForecastApi.Infrastructure;
using WeatherForecastApi.Infrastructure.MeteoBlueApi;

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

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.
builder.Services.Configure<MeteoBlueOptions>(builder.Configuration.GetSection("MeteoBlue"));
builder.Services.AddHttpClient<ILocationsService, MeteoBlueLocationsService>();
builder.Services.AddScoped<IGetLocation, GetLocations>();








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
