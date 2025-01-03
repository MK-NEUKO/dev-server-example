import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DefaultWeather } from '../models/weatherforecast/defaultweather';

@Injectable({
  providedIn: 'root'
})
export class WeatherForecastService {

  constructor(private http: HttpClient) { }

  getWeatherForecast(): Observable<DefaultWeather[]> {
    return this.http.get<DefaultWeather[]>('weather-api/WeatherForecast');
  }

}
