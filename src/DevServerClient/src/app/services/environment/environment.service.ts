import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GatewayConfiguration } from '../../models/environment/gatewayConfiguration';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService {

  constructor(private http: HttpClient) { }

  getConfigurations(): Observable<GatewayConfiguration> {
    return this.http.get<GatewayConfiguration>('https://localhost:7118/configuration/getconfiguration');
  }
}
