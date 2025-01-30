using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.WeatherForecast;

namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public class GetWeatherForecastHandler(
    IWeatherForecastRepository weatherForecastRepository,
    IWeatherForecastProcessor forecastProcessor
    ) : IGetWeatherForecastHandler
{
    public async Task<WeatherForecastDto> HandleAsync(double lat, double lon, CancellationToken cancellationToken)
    {
        var result = await weatherForecastRepository.GetWeatherForecastAsync(lat, lon, cancellationToken);

        if (result == null)
        {
            throw new BadHttpRequestException("Invalid weather forecast query result.");
        }

        var processedPerDayPerHour = forecastProcessor.ProcessPerDayPerHour(result.ForecastDataPerHour);

        if (processedPerDayPerHour == null)
        {
            throw new BadHttpRequestException("Invalid processedPerDayPerHour result.");
        }

        var dto = new WeatherForecastDto
        {
            Metadata = new MetadataDto
            {
                ModelRunUpdateTimeUtc = result.Metadata.ModelRunUpdateTimeUtc,
                Name = result.Metadata.Name,
                Height = result.Metadata.Height,
                TimezoneAbbreviation = result.Metadata.TimezoneAbbrevation,
                Latitude = result.Metadata.Latidude,
                ModelRunUtc = result.Metadata.ModelRunUtc,
                Longitude = result.Metadata.Longitude,
                UtcTimeOffset = result.Metadata.UtcTimeoffset,
                GenerationTimeMs = result.Metadata.GenerationTimeMs
            },
            Units = new UnitsDto
            {
                Predictability = result.Units.Predictebility,
                Precipitation = result.Units.Precipitation,
                WindSpeed = result.Units.Windspeed,
                PrecipitationProbability = result.Units.PrecipitationProbability,
                RelativeHumidity = result.Units.Relativehumidity,
                Temperature = result.Units.Temperature,
                Time = result.Units.Time,
                Pressure = result.Units.Pressure,
                WindDirection = result.Units.Winddirection
            },
            ForecastDataPerDayPerHour = processedPerDayPerHour.Select(x => new ForecastDataPerHourDto
            {
                Time = x.Time,
                SnowFraction = x.SnowFraction,
                WindSpeed = x.WindSpeed,
                Temperature = x.Temperature,
                PrecipitationProbability = x.PrecipitationProbability,
                ConvectivePrecipitation = x.ConvectivePrecipitation,
                RainSpot = x.RainSpot,
                PictogramCode = x.PictogramCode,
                FeltTemperature = x.FeltTemperature,
                Precipitation = x.Precipitation,
                IsDayLight = x.IsDayLight,
                UvIndex = x.UvIndex,
                RelativeHumidity = x.RelativeHumidity,
                SeaLevelPressure = x.SeaLevelPressure,
                WindDirection = x.WindDirection
            }).ToList(),
            ForecastDataPerDay = new ForecastDataPerDayDto
            {
                Time = result.ForecastDataPerDay.Time,
                TemperatureInstant = result.ForecastDataPerDay.TemperatureInstant,
                Precipitation = result.ForecastDataPerDay.Precipitation,
                Predictability = result.ForecastDataPerDay.Predictability,
                TemperatureMax = result.ForecastDataPerDay.TemperatureMax,
                SeaLevelPressureMean = result.ForecastDataPerDay.SeaLevelPressureMean,
                WindSpeedMean = result.ForecastDataPerDay.WindSpeedMean,
                PrecipitationHours = result.ForecastDataPerDay.PrecipitationHours,
                SeaLevelPressureMin = result.ForecastDataPerDay.SeaLevelPressureMin,
                PictogramCode = result.ForecastDataPerDay.PictogramCode,
                SnowFraction = result.ForecastDataPerDay.SnowFraction,
                HumidityGreater90Hours = result.ForecastDataPerDay.HumidityGreater90Hours,
                ConvectivePrecipitation = result.ForecastDataPerDay.ConvectivePrecipitation,
                RelativeHumidityMax = result.ForecastDataPerDay.RelativeHumidityMax,
                TemperatureMin = result.ForecastDataPerDay.TemperatureMin,
                WindDirection = result.ForecastDataPerDay.WindDirection,
                FeltTemperatureMax = result.ForecastDataPerDay.FeltTemperatureMax,
                IndexTo1HValuesEnd = result.ForecastDataPerDay.IndexTo1hValuesEnd,
                RelativeHumidityMin = result.ForecastDataPerDay.RelativeHumidityMin,
                FeltTemperatureMean = result.ForecastDataPerDay.FeltTemperatureMean,
                WindSpeedMin = result.ForecastDataPerDay.WindSpeedMin,
                FeltTemperatureMin = result.ForecastDataPerDay.FeltTemperatureMin,
                PrecipitationProbability = result.ForecastDataPerDay.PrecipitationProbability,
                UvIndex = result.ForecastDataPerDay.UvIndex,
                IndexTo1HValuesStart = result.ForecastDataPerDay.IndexTo1hValuesStart,
                RainSpot = result.ForecastDataPerDay.RainSpot,
                TemperatureMean = result.ForecastDataPerDay.TemperatureMean,
                SeaLevelPressureMax = result.ForecastDataPerDay.SeaLevelPressureMax,
                RelativeHumidityMean = result.ForecastDataPerDay.RelativeHumidityMean,
                PredictabilityClass = result.ForecastDataPerDay.PredictabilityClass,
                WindSpeedMax = result.ForecastDataPerDay.WindSpeedMax
            }
        };

        return dto;
    }
}
