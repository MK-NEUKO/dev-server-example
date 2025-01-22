export interface ForecastDataPerHour {
    time: string[];
    snowFraction: number[];
    windSpeed: number[];
    precipitationProbability: number[];
    convectivePrecipitation: number[];
    rainSpot: string[];
    picToCode: number[];
    feltTemperature: number[];
    precipitation: number[];
    isDayLight: number[];
    uvIndex: number[];
    relativeHumidity: number[];
    seaLevelPressure: number[];
    windDirection: number[];
}