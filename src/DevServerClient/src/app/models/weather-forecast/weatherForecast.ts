import { Metadata } from "./metaData";
import { Units } from "./units";
import { ForecastDataPerHour } from "./forecastDataPerHour";
import { ForecastDataPerDay } from "./forecastDataPerDay";

export interface WeatherForecast {
    metadataDto: Metadata;
    unitsDto: Units;
    forecastDataPerHourDto: ForecastDataPerHour;
    forecastDataPerDayDto: ForecastDataPerDay;
}