import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgFor } from '@angular/common';
import { Subscription } from 'rxjs';
import { GatewayConfiguration } from '../../../../../models/environment-gateway/gatewayConfig';
import { EnvironmentGatewayDataService } from '../../../../../services/environment-gateway/environment-gateway-data.service';

@Component({
  selector: 'app-route-editor',
  imports: [NgFor],
  templateUrl: './route-editor.component.html',
  styleUrl: './route-editor.component.css'
})
export class RouteEditorComponent implements OnInit, OnDestroy {
  private subscription: Subscription = new Subscription();
  private gatewayConfiguration?: GatewayConfiguration;

  public routeKeys: string[] = [];

  constructor(
    private environmentDataService: EnvironmentGatewayDataService,
  ) { }

  ngOnInit() {
    this.subscription.add(this.environmentDataService.getGatewayConfig().subscribe(data => {
      this.gatewayConfiguration = data ?? this.environmentDataService.getDefaultGatewayConfig();
      this.processRouteEditor();
    }));

    this.processRouteEditor();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  processRouteEditor() {
    this.routeKeys = this.processRouteKeys(this.gatewayConfiguration!);
  }

  processRouteKeys(obj: GatewayConfiguration): string[] {
    let objKeys: string[] = [];
    const keys = Object.keys(obj);
    const values = Object.values(obj);


    console.log(keys);
    console.log(values);



    return objKeys;
  }
}
