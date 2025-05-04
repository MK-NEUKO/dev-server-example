import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GatewayConfig } from '../../models/environment-gateway/gatewayConfig';
import { InitializeGatewayConfig } from './gatewayConfig-initializer';


@Injectable({
  providedIn: 'root'
})
export class EnvironmentGatewayDataService {
  private gatewayConfig = new BehaviorSubject<GatewayConfig>(InitializeGatewayConfig());

  constructor() { }

  setGatewayConfig(data: GatewayConfig) {
    this.gatewayConfig.next(data);
  }

  getGatewayConfig(): Observable<GatewayConfig | null> {
    return this.gatewayConfig.asObservable();
  }

  getDefaultGatewayConfig(): GatewayConfig {
    return InitializeGatewayConfig();
  }
}
