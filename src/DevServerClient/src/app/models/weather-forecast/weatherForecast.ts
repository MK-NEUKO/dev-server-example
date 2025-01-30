import { Metadata } from "./metaData";
import { Units } from "./units";
import { ForecastDataPerHour } from "./forecastDataPerHour";
import { ForecastDataPerDay } from "./forecastDataPerDay";

export interface WeatherForecast {
    metadata: Metadata;
    units: Units;
    forecastDataPerDayPerHour: ForecastDataPerHour[];
    forecastDataPerDay: ForecastDataPerDay;
}