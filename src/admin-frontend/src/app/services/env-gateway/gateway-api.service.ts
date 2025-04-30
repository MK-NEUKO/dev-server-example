import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';

@Injectable({
  providedIn: 'root'
})
export class GatewayApiService {

  constructor(private http: HttpClient) { }

  getCurrentConfig(): Observable<GatewayConfig> {
    return this.http.get<GatewayConfig>('envGateway/current-config');
  }
}
