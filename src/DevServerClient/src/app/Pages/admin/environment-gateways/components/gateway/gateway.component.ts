import { Component, ViewChild, ElementRef, OnInit, OnDestroy } from '@angular/core';
import { EnvironmentGatewayService } from '../../../../../services/environment-gateway/environment-gateway.service';
import { EnvironmentGatewayDataService } from '../../../../../services/environment-gateway/environment-gateway-data.service';
import { Observable, Subscription } from 'rxjs';
import { GatewayConfiguration } from '../../../../../models/environment-gateway/gatewayConfiguration';
import { RouteEditorComponent } from "../route-editor/route-editor.component";
import { GatewayInfo } from '../../../../../models/environment-gateway/gatewayInfo';


@Component({
  selector: 'app-gateway',
  imports: [RouteEditorComponent],
  templateUrl: './gateway.component.html',
  styleUrl: './gateway.component.css'
})
export class GatewayComponent implements OnInit, OnDestroy {
  private subscription: Subscription = new Subscription();
  private gatewayConfiguration?: GatewayConfiguration;
  private gatewayInfo?: GatewayInfo;

  public gatewayName: string = '';

  constructor(
    private environmentService: EnvironmentGatewayService,
    private environmentDataService: EnvironmentGatewayDataService,
  ) { }

  ngOnInit() {
    this.subscription.add(this.environmentDataService.getGatewayConfiguration().subscribe((data) => {
      this.gatewayConfiguration = data ?? this.environmentDataService.getDefaultGatewayConfiguration();
    }));
    this.subscription.add(this.environmentService.getGatewayInfo().subscribe((data) => {
      this.environmentDataService.setGatewayInfo(data);
      this.gatewayInfo = data;
      this.gatewayName = this.gatewayInfo?.gatewayName ?? '';
    }));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getConfig() {
    this.subscription.add(this.environmentService.getGatewayConfigurations().subscribe((data) => {
      this.environmentDataService.setGatewayConfiguration(data);
    }));
  }

  getSlotOne() {
    console.log("got to slot one");
    window.open("https://localhost:7118", "_blank");
  }
}
