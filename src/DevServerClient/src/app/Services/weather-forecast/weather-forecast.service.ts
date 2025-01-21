import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';

@Injectable({
  providedIn: 'root'
})
export class WeatherForecastService {

  constructor(private http: HttpClient) { }

  getLocations(query: string): Observable<LocationQueryResult> {
    return this.http.get<LocationQueryResult>(`weather-api/Locations/GetLocations/${query}`);
  }
}
