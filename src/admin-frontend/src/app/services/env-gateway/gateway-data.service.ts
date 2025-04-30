import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, finalize } from 'rxjs';
import { GatewayApiService } from './gateway-api.service';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { defaultGatewayConfig } from './default-gateway-config';

@Injectable({
  providedIn: 'root'
})
export class GatewayDataService {

  private currentConfig = new BehaviorSubject<GatewayConfig>(defaultGatewayConfig());
  private isLoading = new BehaviorSubject<boolean>(false);

  constructor(private gatewayApiService: GatewayApiService) { }

  queryCurrentConfig(): void {
    this.isLoading.next(true);
    this.gatewayApiService.getCurrentConfig().pipe(
      catchError((error) => {
        console.error('Error fetching current config:', error);
        return [defaultGatewayConfig()];
      }),
      finalize(() => this.isLoading.next(false))

    ).subscribe((config) => {
      this.currentConfig.next(config);
    });
  }

  getCurrentConfig(): BehaviorSubject<GatewayConfig> {
    return this.currentConfig;
  }

  getLoadingState(): BehaviorSubject<boolean> {
    return this.isLoading;
  }
}
