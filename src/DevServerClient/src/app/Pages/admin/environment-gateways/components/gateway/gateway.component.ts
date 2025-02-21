import { Component, OnInit, OnDestroy } from '@angular/core';
import { EnvironmentService } from '../../../../../services/environment-gateway/environment-gateway.service';
import { EnvironmentGatewayDataService } from '../../../../../services/environment-gateway/environment-gateway-data.service';
import { GatewayConfiguration } from '../../../../../models/environment-gateway/gatewayConfiguration';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-gateway',
  imports: [],
  templateUrl: './gateway.component.html',
  styleUrl: './gateway.component.css'
})
export class GatewayComponent implements OnInit, OnDestroy {
  private subscription: Subscription = new Subscription();
  private gatewayConfiguration?: GatewayConfiguration;


  constructor(
    private environmentService: EnvironmentService,
    private environmentDataService: EnvironmentGatewayDataService
  ) { }

  ngOnInit() {
    this.subscription.add(this.environmentService.getConfigurations().subscribe((data) => {
      this.environmentDataService.setGatewayConfiguration(data);
    }));

    this.subscription.add(this.environmentDataService.getGatewayConfiguration().subscribe((data) => {
      this.gatewayConfiguration = data ?? this.environmentDataService.getDefaultGatewayConfiguration();
    }));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
