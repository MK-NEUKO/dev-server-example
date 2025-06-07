import { Component, inject, effect } from '@angular/core';
import { FormArray, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { GatewayDataService } from '../../../../services/env-gateway/gateway-data.service';
import { GatewayConfig } from '../../../../models/gateway-config/gateway-config.model';

@Component({
  selector: 'app-routes',
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './routes.component.html',
  styleUrl: './routes.component.css'
})
export class RoutesComponent {

  private formBuilder = inject(FormBuilder);
  private gatewayDataService = inject(GatewayDataService);
  public currentConfigResource = this.gatewayDataService.getCurrentConfig();
  private currentConfigSignal!: GatewayConfig;
  public routes = this.formBuilder.array([], { updateOn: 'change' }) as FormArray;
  public parentForm = this.formBuilder.group({
    routes: this.routes
  });

  constructor() {
    effect(() => {
      const value = this.currentConfigResource.value();
      if (value !== undefined) {
        this.currentConfigSignal = value;
        this.initializeRoutes();
      }
    });
  }

  public addRoute() {
    this.routes.push(this.formBuilder.group({
      routeName: this.formBuilder.control('test route'),
      clusterName: this.formBuilder.control('test cluster'),
      match: this.formBuilder.group({
        path: this.formBuilder.control('test path'),
      })
    }));
  }

  public initializeRoutes() {
    if (this.currentConfigSignal) {
      this.currentConfigSignal.routes.forEach(route => {
        this.routes.push(this.formBuilder.group({
          routeName: this.formBuilder.control(route.routeName || 'loade failed'),
          clusterName: this.formBuilder.control(route.clusterName || 'loade failed'),
          match: this.formBuilder.group({
            path: this.formBuilder.control(route.match?.path || 'loade failed'),
          })
        }));
      });
    }
  }
}
