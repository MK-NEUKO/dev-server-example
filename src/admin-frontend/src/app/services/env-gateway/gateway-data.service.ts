import { Injectable } from '@angular/core';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { httpResource } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GatewayDataService {

  private url = 'envGateway/current-config';

  public getCurrentConfig() {
    return httpResource<GatewayConfig>(
      () => this.url
    );
  }
}
