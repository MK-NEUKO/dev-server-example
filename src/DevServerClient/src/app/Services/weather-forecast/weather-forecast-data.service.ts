import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { initializeWeatherForecast } from './weather-forecast-initializer';

@Injectable({
  providedIn: 'root'
})
export class WeatherForecastDataService {
  private locationQueryResultSubject = new BehaviorSubject<LocationQueryResult | null>(null);
  private weatherForecastSubject = new BehaviorSubject<WeatherForecast>(initializeWeatherForecast());

  constructor() { }

  setLocationQueryResult(data: LocationQueryResult) {
    this.locationQueryResultSubject.next(data);
  }

  getLocationQueryResult(): Observable<LocationQueryResult | null> {
    return this.locationQueryResultSubject.asObservable();
  }

  setWeatherForecast(data: WeatherForecast) {
    this.weatherForecastSubject.next(data);
  }

  getWeatherForecast(): Observable<WeatherForecast | null> {
    return this.weatherForecastSubject.asObservable();
  }
}
