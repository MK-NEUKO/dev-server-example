import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { Metadata } from '../../models/weather-forecast/metaData';
import { Units } from '../../models/weather-forecast/units';
import { ForecastDataPerHour } from '../../models/weather-forecast/forecastDataPerHour';
import { ForecastDataPerDay } from '../../models/weather-forecast/forecastDataPerDay';

export function initializeWeatherForecast(): WeatherForecast {
    return {
        metadataDto: {
            modelRunUpdateTimeUtc: '',
            name: '',
            height: 0,
            timezoneAbbreviation: '',
            latitude: 0,
            modelRunUtc: '',
            longitude: 0,
            utcTimeOffset: 0,
            generationTimeMs: 0
        } as Metadata,
        unitsDto: {
            predictability: '',
            precipitation: '',
            windSpeed: '',
            precipitationProbability: '',
            relativeHumidity: '',
            temperature: '',
            time: '',
            pressure: '',
            windDirection: ''
        } as Units,
        forecastDataPerHourDto: {
            time: [],
            snowFraction: [],
            windSpeed: [],
            precipitationProbability: [],
            convectivePrecipitation: [],
            rainSpot: [],
            picToCode: [],
            feltTemperature: [],
            precipitation: [],
            isDayLight: [],
            uvIndex: [],
            relativeHumidity: [],
            seaLevelPressure: [],
            windDirection: [],
        } as ForecastDataPerHour,
        forecastDataPerDayDto: {
            time: [],
            temperatureInstant: [],
            precipitation: [],
            predictability: [],
            temperatureMax: [],
            seaLevelPressureMean: [],
            windSpeedMean: [],
            precipitationHours: [],
            seaLevelPressureMin: [],
            picToCode: [],
            snowFraction: [],
            humidityGreater90Hours: [],
            convectivePrecipitation: [],
            relativeHumidityMax: [],
            temperatureMin: [],
            windDirection: [],
            feltTemperatureMax: [],
            indexTo1HValuesEnd: [],
            relativeHumidityMin: [],
            feltTemperatureMean: [],
            windSpeedMin: [],
            feltTemperatureMin: [],
            precipitationProbability: [],
            uvIndex: [],
            indexTo1HValuesStart: [],
            rainSpot: [],
            temperatureMean: [],
            seaLevelPressureMax: [],
            relativeHumidityMean: [],
            predictabilityClass: [],
            windSpeedMax: [],
        } as ForecastDataPerDay
    };
}