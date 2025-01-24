import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { initializeLocationQueryResult } from './locationQueryResult-initializer';
import { initializeWeatherForecast } from './weather-forecast-initializer';
import { initializeForecastDataPerDay } from './forecastPerDay-initializer';
import { ForecastDataPerDay } from '../../models/weather-forecast/forecastDataPerDay';

@Injectable({
  providedIn: 'root'
})
export class WeatherForecastDataService {
  private locationQueryResultSubject = new BehaviorSubject<LocationQueryResult>(initializeLocationQueryResult());
  private weatherForecastSubject = new BehaviorSubject<WeatherForecast>(initializeWeatherForecast());
  private forecastPerDaySubject = new BehaviorSubject<ForecastDataPerDay>(initializeForecastDataPerDay());

  constructor() {

  }

  setLocationQueryResult(data: LocationQueryResult) {
    this.locationQueryResultSubject.next(data);
  }

  getLocationQueryResult(): Observable<LocationQueryResult | null> {
    return this.locationQueryResultSubject.asObservable();
  }


  setWeatherForecast(data: WeatherForecast) {
    this.weatherForecastSubject.next(data);
    this.forecastPerDaySubject.next(data.forecastDataPerDayDto);
  }

  getWeatherForecast(): Observable<WeatherForecast | null> {
    return this.weatherForecastSubject.asObservable();
  }

  getWeatherForecastPerDay(): Observable<ForecastDataPerDay> {
    return this.forecastPerDaySubject.asObservable();
  }
}
