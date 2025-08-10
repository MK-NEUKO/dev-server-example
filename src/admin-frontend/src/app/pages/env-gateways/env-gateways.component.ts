import { Component, effect, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { GatewayDataService } from '../../services/env-gateway/gateway-data.service';
import { Highlight } from 'ngx-highlightjs';
import { HighlightLineNumbers } from 'ngx-highlightjs/line-numbers';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { HttpClient } from '@angular/common/http';
import Keycloak from 'keycloak-js';


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
export class EnvGatewaysComponent implements OnInit {

  private gatewayDataService = inject(GatewayDataService);
  private router = inject(Router);
  private keycloak = inject(Keycloak);
  public currentConfig = this.gatewayDataService.getCurrentConfig();
  public configData!: GatewayConfig;
  public isMaximized = false;

  private destinationUrl = 'https://localhost:9100/service2/test';
  private http = inject(HttpClient);

  constructor() {
    effect(() => {
      const value = this.currentConfig.value();
      if (value !== undefined) {
        this.configData = value;
      }
    });
  }
  ngOnInit(): void {

  }

  destinationTest() {
    this.http.get(this.destinationUrl, {
      headers: {
        'Authorization': `Bearer ${this.keycloak.token}`,
        'Content-Type': 'application/json'
      },
      withCredentials: true,
      responseType: 'json'
    }).subscribe({
      next: (response) => {
        console.log('Destination test successful:', response);
      },
      error: (error) => {
        console.error('Error testing destination:', error);
      }
    });
  }

  public editConfig() {
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