import { inject, Injectable } from '@angular/core';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { httpResource } from '@angular/common/http';
import Keycloak from 'keycloak-js';

@Injectable({
  providedIn: 'root'
})
export class GatewayDataService {

  private url = 'https://localhost:9201/current-config';
  private readonly keycloak = inject(Keycloak);
  private readonly token = this.keycloak.token;

  public getCurrentConfig() {
    return httpResource<GatewayConfig>(
      () => ({
        url: this.url,
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${this.token}`,
          'Content-Type': 'application/json'
        },
        withCredentials: true,
        responseType: 'json'
      })
    );
  }
}
