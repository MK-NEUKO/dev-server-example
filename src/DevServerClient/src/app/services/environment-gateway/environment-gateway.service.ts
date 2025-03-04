import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, forkJoin } from 'rxjs';
import { GatewayConfiguration } from '../../models/environment-gateway/gatewayConfiguration';
import { GatewayInfo } from '../../models/environment-gateway/gatewayInfo';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentGatewayService {
  private numberOfGateways: number = 2;
  private gatewayAddressList: string[] = ['productionGateway', 'stagingGateway'];

  constructor(private http: HttpClient) { }

  getGatewayConfigurations(): Observable<GatewayConfiguration> {
    return this.http.get<GatewayConfiguration>('productionGateway/configuration/GetConfiguration');
  }

  requestGateways(): Observable<GatewayInfo[]> {
    let requests: Observable<GatewayInfo>[] = [];
    for (let i = 0; i < this.numberOfGateways; i++) {
      const request = this.http.get<GatewayInfo>(`${this.gatewayAddressList[i]}/EnvironmentGateway/GetContext`);
      console.log(request);
      requests.push(request);

    }



    return forkJoin(requests);
  }
}

