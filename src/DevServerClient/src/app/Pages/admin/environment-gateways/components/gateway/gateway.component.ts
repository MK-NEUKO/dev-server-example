import { Component, OnInit, OnDestroy, Input } from '@angular/core';
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
  @Input() gatewayId: number = 0;
  private subscription: Subscription = new Subscription();
  private gatewayConfiguration?: GatewayConfiguration;
  private gatewayInfo?: GatewayInfo;



  public gatewayName: string = '';

  constructor(
    private environmentDataService: EnvironmentGatewayDataService,
  ) { }

  ngOnInit() {
    this.subscription.add(this.environmentDataService.getGatewayConfiguration().subscribe((data) => {
      this.gatewayConfiguration = data ?? this.environmentDataService.getDefaultGatewayConfiguration();
    }));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
