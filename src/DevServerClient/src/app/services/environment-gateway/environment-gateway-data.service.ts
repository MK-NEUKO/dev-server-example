import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GatewayConfiguration } from '../../models/environment-gateway/gatewayConfiguration';
import { initializeGatewayConfiguration } from './gatewayConfiguration-initializer';


@Injectable({
  providedIn: 'root'
})
export class EnvironmentGatewayDataService {
  private gatewayConfiguration = new BehaviorSubject<GatewayConfiguration>(initializeGatewayConfiguration());

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
}
