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
  public configName: string = '';
  public config$!: Observable<GatewayConfig>;
  public isLoading$!: Observable<boolean>;

  constructor(private gatewayDataService: GatewayDataService) { }

  ngOnInit(): void {
    this.getCurrentConfig();
  }


  public getCurrentConfig() {
    this.gatewayDataService.queryCurrentConfig();
    this.isLoading$ = this.gatewayDataService.getLoadingState();
    this.config$ = this.gatewayDataService.getCurrentConfig();
    console.log('Config:', this.config$);
  }
}


