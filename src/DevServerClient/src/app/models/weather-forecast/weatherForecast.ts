import { MetaData } from "./metaData";
import { Units } from "./units";
import { ForecastDataPerHour } from "./forecastDataPerHour";
import { ForecastDataPerDay } from "./forecastDataPerDay";

export interface WeatherForecast {
    metadataDto: MetaData;
    unitsDto: Units;
    forecastDataPerHourDto: ForecastDataPerHour[];
    forecastDataPerDayDto: ForecastDataPerDay[];
}