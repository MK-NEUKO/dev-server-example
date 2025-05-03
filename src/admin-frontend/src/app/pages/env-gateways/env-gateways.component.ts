import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
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
  public currentConfig = this.gatewayDataService.getCurrentConfig();
  public isMaximized = false;

  private destinationUrl = 'https://localhost:5201/';


  constructor() { }

  destinationTest() {
    window.open(this.destinationUrl, '_blank');
  }

  changeCardBodyHeight() {
    if (this.isMaximized) {
      this.isMaximized = false;
    } else {
      this.isMaximized = true;
    }
  }
}