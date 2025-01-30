export interface ForecastDataPerHour {
    time: string[];
    snowFraction: number[];
    windSpeed: number[];
    temperature: number[];
    precipitationProbability: number[];
    convectivePrecipitation: number[];
    rainSpot: string[];
    pictogramCode: number[];
    feltTemperature: number[];
    precipitation: number[];
    isDayLight: number[];
    uvIndex: number[];
    relativeHumidity: number[];
    seaLevelPressure: number[];
    windDirection: number[];
}