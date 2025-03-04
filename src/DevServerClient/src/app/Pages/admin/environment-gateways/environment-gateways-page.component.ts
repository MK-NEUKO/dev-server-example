import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgFor } from '@angular/common';
import { Subscription } from 'rxjs';
import { GatewayComponent } from "./components/gateway/gateway.component";
import { GatewayConfiguration } from '../../../models/environment-gateway/gatewayConfiguration';
import { EnvironmentGatewayService } from '../../../services/environment-gateway/environment-gateway.service';
import { EnvironmentGatewayDataService } from '../../../services/environment-gateway/environment-gateway-data.service';
import { GatewayInfo } from '../../../models/environment-gateway/gatewayInfo';

@Component({
  selector: 'app-environment-gateways',
  imports: [
    GatewayComponent,
    NgFor,
  ],
  templateUrl: './environment-gateways-page.component.html',
  styleUrl: './environment-gateways-page.component.css'
})
export class EnvironmentGatewaysComponent implements OnInit, OnDestroy {
  private subscription: Subscription = new Subscription();
  public gateways: GatewayInfo[] = [];


  constructor(
    private environmentGatewaysService: EnvironmentGatewayService,
    private environmentGatewaysDataService: EnvironmentGatewayDataService,
  ) { }

  ngOnInit() {
    this.subscription.add(this.environmentGatewaysService.requestGateways().subscribe((data: GatewayInfo[]) => {
      this.environmentGatewaysDataService.setGateways(data);
    }));

    this.subscription.add(this.environmentGatewaysDataService.getGateways().subscribe((data: GatewayInfo[]) => {
      this.gateways = data;
    }));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}