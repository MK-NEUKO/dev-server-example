import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GatewayConfiguration } from '../../models/environment-gateway/gatewayConfiguration';
import { GatewayInfo } from '../../models/environment-gateway/gatewayInfo';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentGatewayService {

  constructor(private http: HttpClient) { }

  getGatewayConfigurations(): Observable<GatewayConfiguration> {
    return this.http.get<GatewayConfiguration>('productionGateway/configuration/GetConfiguration');
  }

  getGatewayInfo(): Observable<GatewayInfo> {
    return this.http.get<GatewayInfo>('productionGateway/EnvironmentGateway/GetContext');
    console.log('getEnvironmentName');
  }
}
