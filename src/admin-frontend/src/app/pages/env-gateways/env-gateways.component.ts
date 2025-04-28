import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GatewayDataService } from '../../services/env-gateway/gateway-data.service';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-env-gateways',
  imports: [CommonModule],
  templateUrl: './env-gateways.component.html',
  styleUrl: './env-gateways.component.css'
})
export class EnvGatewaysComponent implements OnInit {
  public configName: string = 'Configuration Name';
  public config$!: Observable<GatewayConfig>;

  constructor(private gatewayDataService: GatewayDataService) { }

  ngOnInit(): void {
    this.gatewayDataService.requestCurrentConfig();
    this.getCurrentConfig();
  }


  public getCurrentConfig() {
    this.config$ = this.gatewayDataService.getCurrentConfig();
    console.log('Config:', this.config$);
  }
}


