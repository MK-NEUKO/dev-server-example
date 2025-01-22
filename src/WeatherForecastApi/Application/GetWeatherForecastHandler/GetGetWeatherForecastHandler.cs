using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.WeatherForecast;

namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public class GetGetWeatherForecastHandler(
    IWeatherForecastRepository weatherForecastRepository
    ) : IGetWeatherForecastHandler
{
    public async Task<WeatherForecastDto> HandleAsync(double lat, double lon, CancellationToken cancellationToken)
    {
        var result = await weatherForecastRepository.GetWeatherForecastAsync(lat, lon, cancellationToken);
        
        if (result == null)
        {
            throw new BadHttpRequestException("Invalid weather forecast query result.");
        }

        var dto = new WeatherForecastDto
        {
            MetadataDto = new MetadataDto
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
            UnitsDto = new UnitsDto
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
            ForecastDataPerHourDto = new ForecastDataPerHourDto
            {
                Time = result.ForecastDataPerHour.Time,
                SnowFraction = result.ForecastDataPerHour.SnowFraction,
                WindSpeed = result.ForecastDataPerHour.WindSpeed,
                PrecipitationProbability = result.ForecastDataPerHour.PrecipitationProbability,
                ConvectivePrecipitation = result.ForecastDataPerHour.ConvectivePrecipitation,
                RainSpot = result.ForecastDataPerHour.RainSpot,
                PicToCode = result.ForecastDataPerHour.PicToCode,
                FeltTemperature = result.ForecastDataPerHour.FeltTemperature,
                Precipitation = result.ForecastDataPerHour.Precipitation,
                IsDayLight = result.ForecastDataPerHour.IsDayLight,
                UvIndex = result.ForecastDataPerHour.UvIndex,
                RelativeHumidity = result.ForecastDataPerHour.RelativeHumidity,
                SeaLevelPressure = result.ForecastDataPerHour.SeaLevelPressure,
                WindDirection = result.ForecastDataPerHour.WindDirection
            },
            ForecastDataPerDayDto = new ForecastDataPerDayDto
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
                PicToCode = result.ForecastDataPerDay.PicToCode,
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