import { MetaData } from "./metaData";
import { Units } from "./units";
import { ForecastDataPerHour } from "./forecastDataPerHour";
import { ForecastDataPerDay } from "./forecastDataPerDay";

export interface WeatherForecast {
    metaData: MetaData;
    units: Units;
    forecastDataPerHour: ForecastDataPerHour[];
    forecastDataPerDay: ForecastDataPerDay[];
}