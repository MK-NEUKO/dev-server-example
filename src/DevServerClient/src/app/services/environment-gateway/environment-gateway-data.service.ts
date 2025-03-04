import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GatewayConfiguration } from '../../models/environment-gateway/gatewayConfiguration';
import { GatewayInfo } from '../../models/environment-gateway/gatewayInfo';
import { initializeGatewayConfiguration } from './gatewayConfiguration-initializer';
import { initializeGatewayInfo } from './gatewayInfo-initializer';


@Injectable({
  providedIn: 'root'
})
export class EnvironmentGatewayDataService {
  private gatewayConfiguration = new BehaviorSubject<GatewayConfiguration>(initializeGatewayConfiguration());
  private gateways = new BehaviorSubject<GatewayInfo[]>(Array(initializeGatewayInfo()));

  constructor() { }

  setGatewayConfiguration(data: GatewayConfiguration) {
    this.gatewayConfiguration.next(data);
  }

  getGatewayConfiguration(): Observable<GatewayConfiguration | null> {
    return this.gatewayConfiguration.asObservable();
  }

  getDefaultGatewayConfiguration(): GatewayConfiguration {
    return initializeGatewayConfiguration();
  }

  setGateways(data: GatewayInfo[]) {
    this.gateways.next(data);
  }

  getGateways(): Observable<GatewayInfo[]> {
    return this.gateways.asObservable();
  }

  getDefaultGateways(): GatewayInfo[] {
    return Array(initializeGatewayInfo());
  }
}
