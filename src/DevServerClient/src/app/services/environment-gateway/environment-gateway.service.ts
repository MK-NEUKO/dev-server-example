import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, forkJoin } from 'rxjs';
import { GatewayConfig } from '../../models/environment-gateway/gatewayConfig';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentGatewayService {

  constructor(private http: HttpClient) { }

  getGatewayConfig(): Observable<GatewayConfig> {
    return this.http.get<GatewayConfig>('productionGateway/current-config');
  }

  /*
  requestGateways(): Observable<GatewayInfo[]> {
    let requests: Observable<GatewayInfo>[] = [];
    for (let i = 0; i < this.numberOfGateways; i++) {
      const request = this.http.get<GatewayInfo>(`${this.gatewayAddressList[i]}/EnvironmentGateway/GetContext`)
        .pipe(
          catchError(this.handleError<GatewayInfo>(`requestGateways: ${this.gatewayAddressList[i]}`, {} as GatewayInfo))
        );

      requests.push(request);
    }



    return forkJoin(requests);
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
      //this.log(`${operation} failed: ${error.message}`);

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

