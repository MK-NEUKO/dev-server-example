import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { GatewayDataService } from '../../services/env-gateway/gateway-data.service';
import { Highlight } from 'ngx-highlightjs';
import { HighlightLineNumbers } from 'ngx-highlightjs/line-numbers';


@Component({
  selector: 'app-env-gateways',
  imports: [
    CommonModule,
    Highlight,
    HighlightLineNumbers,
  ],
  templateUrl: './env-gateways.component.html',
  styleUrls: [
    './env-gateways.component.css'
  ]
})
export class EnvGatewaysComponent {

  private gatewayDataService = inject(GatewayDataService);
  private router = inject(Router);
  public currentConfig = this.gatewayDataService.getCurrentConfig();
  public isMaximized = false;

  private destinationUrl = 'https://localhost:5201/';


  constructor() { }

  destinationTest() {
    window.open(this.destinationUrl, '_blank');
  }

  public editConfig() {
    console.log('Edit config clicked');
    this.router.navigate(['/config-editor']);
  }

  changeCardBodyHeight() {
    if (this.isMaximized) {
      this.isMaximized = false;
    } else {
      this.isMaximized = true;
    }
  }

  processStatusText(): string {
    if (this.currentConfig.isLoading()) {
      return 'LOADING';
    } else if (this.currentConfig.hasValue()) {
      return 'SUCCESS';
    } else if (this.currentConfig.error()) {
      return 'ERROR';
    }
    return 'WAITING';
  }

  processStatusClass(): string {
    if (this.currentConfig.isLoading()) {
      return 'status--loading';
    } else if (this.currentConfig.hasValue()) {
      return 'status--success';
    } else if (this.currentConfig.error()) {
      return 'status--error';
    }
    return 'status--waiting';
  }
}