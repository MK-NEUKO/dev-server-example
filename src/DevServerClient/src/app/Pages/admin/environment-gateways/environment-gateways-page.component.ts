import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { EnvironmentGatewayService } from '../../../services/environment-gateway/environment-gateway.service';
import { EnvironmentGatewayDataService } from '../../../services/environment-gateway/environment-gateway-data.service';
import { GatewayConfig } from '../../../models/environment-gateway/gatewayConfig';

@Component({
  selector: 'app-environment-gateways',
  imports: [
  ],
  templateUrl: './environment-gateways-page.component.html',
  styleUrl: './environment-gateways-page.component.css'
})
export class EnvironmentGatewaysComponent implements OnInit, OnDestroy {
  private subscription: Subscription = new Subscription();
  public gatewayConfig: GatewayConfig = {} as GatewayConfig;

  constructor(
    private environmentGatewayService: EnvironmentGatewayService,
    private environmentGatewayDataService: EnvironmentGatewayDataService,
  ) { }

  ngOnInit() {
    this.subscription.add(this.environmentGatewayService.getGatewayConfig().subscribe((data: GatewayConfig) => {
      this.environmentGatewayDataService.setGatewayConfig(data);
      console.log(data);

    }));

    this.subscription.add(this.environmentGatewayDataService.getGatewayConfig().subscribe((data: GatewayConfig | null) => {
      if (data) {
        this.gatewayConfig = data;
      }
    }));


  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}