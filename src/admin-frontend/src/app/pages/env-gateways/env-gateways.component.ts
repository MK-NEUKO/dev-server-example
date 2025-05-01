import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GatewayDataService } from '../../services/env-gateway/gateway-data.service';

@Component({
  selector: 'app-env-gateways',
  imports: [CommonModule],
  templateUrl: './env-gateways.component.html',
  styleUrl: './env-gateways.component.css'
})
export class EnvGatewaysComponent {

  private gatewayDataService = inject(GatewayDataService);
  public currentConfig = this.gatewayDataService.getCurrentConfig();
}


