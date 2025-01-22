import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';

@Injectable({
  providedIn: 'root'
})
export class WeatherForecastService {

  constructor(private http: HttpClient) { }

  getLocations(query: string): Observable<LocationQueryResult> {
    return this.http.get<LocationQueryResult>(`weather-api/Locations/GetLocations/${query}`);
  }

  getForecast(lat: number, lon: number): Observable<WeatherForecast> {
    return this.http.get<WeatherForecast>(`weather-api/WeatherForecast/GetWeatherForecast/${lat}/${lon}`);
  }
}
