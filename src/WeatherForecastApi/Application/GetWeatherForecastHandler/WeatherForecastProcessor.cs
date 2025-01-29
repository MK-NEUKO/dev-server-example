using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using WeatherForecastApi.WeatherForecast;

namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public class WeatherForecastProcessor : IWeatherForecastProcessor
{
    public List<ForecastDataPerHour> ProcessPerDayPerHour(ForecastDataPerHour data)
    {
        var result = data.Time
            .Select((time, index) => new
            {
                Day = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture).Date,
                Time = data.Time[index].Remove(0, 11),
                SnowFraction = data.SnowFraction[index],
                WindSpeed = data.WindSpeed[index],
                PrecipitationProbability = data.PrecipitationProbability[index],
                ConvectivePrecipitation = data.ConvectivePrecipitation[index],
                RainSpot = data.RainSpot[index],
                PicToCode = data.PicToCode[index],
                FeltTemperature = data.FeltTemperature[index],
                Precipitation = data.Precipitation[index],
                IsDayLight = data.IsDayLight[index],
                UvIndex = data.UvIndex[index],
                RelativeHumidity = data.RelativeHumidity[index],
                SeaLevelPressure = data.SeaLevelPressure[index],
                WindDirection = data.WindDirection[index]
            })
            .GroupBy(x => x.Day)
            .Select(g => new ForecastDataPerHour(
                g.Select(x => x.Time).ToList(),
                g.Select(x => x.SnowFraction).ToList(),
                g.Select(x => x.WindSpeed).ToList(),
                g.Select(x => x.PrecipitationProbability).ToList(),
                g.Select(x => x.ConvectivePrecipitation).ToList(),
                g.Select(x => x.RainSpot).ToList(),
                g.Select(x => x.PicToCode).ToList(),
                g.Select(x => x.FeltTemperature).ToList(),
                g.Select(x => x.Precipitation).ToList(),
                g.Select(x => x.IsDayLight).ToList(),
                g.Select(x => x.UvIndex).ToList(),
                g.Select(x => x.RelativeHumidity).ToList(),
                g.Select(x => x.SeaLevelPressure).ToList(),
                g.Select(x => x.WindDirection).ToList()
            ))
            .ToList();
        
        return result;
    }
}
