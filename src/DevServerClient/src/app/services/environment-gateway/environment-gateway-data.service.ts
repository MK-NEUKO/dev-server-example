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
  private gatewayInfo = new BehaviorSubject<GatewayInfo>(initializeGatewayInfo());

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

  setGatewayInfo(data: GatewayInfo) {
    this.gatewayInfo.next(data);
  }

  getGatewayInfo(): Observable<GatewayInfo | null> {
    return this.gatewayInfo.asObservable();
  }

  getDefaultGatewayInfo(): GatewayInfo {
    return initializeGatewayInfo();
  }
}
