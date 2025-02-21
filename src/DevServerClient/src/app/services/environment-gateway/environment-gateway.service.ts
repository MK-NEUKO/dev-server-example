import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GatewayConfiguration } from '../../models/environment-gateway/gatewayConfiguration';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService {

  constructor(private http: HttpClient) { }

  getConfigurations(): Observable<GatewayConfiguration> {
    return this.http.get<GatewayConfiguration>('production-gateway/configuration/getconfiguration');
  }
}
