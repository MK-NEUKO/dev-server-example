import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError } from 'rxjs';
import { GatewayApiService } from './gateway-api.service';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { defaultGatewayConfig } from './default-gateway-config';

@Injectable({
  providedIn: 'root'
})
export class GatewayDataService {

  private currentConfig = new BehaviorSubject<GatewayConfig>(defaultGatewayConfig());

  constructor(private gatewayApiService: GatewayApiService) { }

  requestCurrentConfig(): void {
    this.gatewayApiService.getCurrentConfig().pipe(
      catchError((error) => {
        console.error('Error fetching current config:', error);
        return [defaultGatewayConfig()]; // Return null or a default value in case of error
      })
    ).subscribe((config) => {
      this.currentConfig.next(config || defaultGatewayConfig());
    }
    );
  }

  getCurrentConfig(): BehaviorSubject<GatewayConfig> {
    return this.currentConfig;
  }
}
