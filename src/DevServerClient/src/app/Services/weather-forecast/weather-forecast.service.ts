import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';

@Injectable({
  providedIn: 'root'
})

export class WeatherForecastService {

  constructor(private http: HttpClient) { }

  private log(message: string): void {

    // TODO: Implement a message service for this
    console.log(message);
  }

  getLocations(query: string): Observable<LocationQueryResult> {
    return this.http.get<LocationQueryResult>(`weather-api/Locations/GetLocations/${query}`)
      .pipe(
        catchError(this.handleError<LocationQueryResult>('getLocations', {} as LocationQueryResult))
      );
  }

  getForecast(lat: number, lon: number): Observable<WeatherForecast> {
    return this.http.get<WeatherForecast>(`weather-api/WeatherForecast/GetWeatherForecast/${lat}/${lon}`)
      .pipe(
        catchError(this.handleError<WeatherForecast>('getForecast', {} as WeatherForecast))
      );
  }


  /**
 * Handle Http operation that failed.
 * Let the app continue.
 *
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}

function of<T>(value: T): Observable<T> {
  return new Observable<T>((observer) => {
    observer.next(value);
    observer.complete();
  });
}

