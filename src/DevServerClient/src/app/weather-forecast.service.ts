import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DefaultWeather } from '../models/weatherforecast/defaultweather';
import { Location } from '../models/weatherforecast/location';

@Injectable({
  providedIn: 'root'
})
export class WeatherForecastService {

  constructor(private http: HttpClient) { }

  getWeatherForecast(): Observable<DefaultWeather[]> {
    return this.http.get<DefaultWeather[]>('weather-api/WeatherForecast');
  }

  getLocations(city: string): Observable<Location[]> {
    return this.http.get<Location[]>(`weather-api/Location/GetLocations/${city}`);
  }

}
