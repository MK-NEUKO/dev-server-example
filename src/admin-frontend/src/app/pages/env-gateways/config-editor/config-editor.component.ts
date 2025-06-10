import { Component, effect, inject } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { GatewayDataService } from '../../../services/env-gateway/gateway-data.service';
import { GatewayConfig } from '../../../models/gateway-config/gateway-config.model';
import { RoutesComponent } from './routes/routes.component';
import { ClustersEditorComponent } from './clusters-editor/clusters-editor.component';

@Component({
  selector: 'app-config-editor',
  imports: [
    ReactiveFormsModule,
    RoutesComponent,
    ClustersEditorComponent,
  ],
  templateUrl: './config-editor.component.html',
  styleUrl: './config-editor.component.css'
})
export class ConfigEditorComponent {

  private gatewayDataService = inject(GatewayDataService);
  private formBuilder = inject(FormBuilder);
  public currentConfigResource = this.gatewayDataService.getCurrentConfig();
  public currentConfigData!: GatewayConfig;
  public gatewayConfigForm!: FormGroup;

  constructor() {
    effect(() => {
      const value = this.currentConfigResource.value();
      if (value !== undefined) {
        this.currentConfigData = value;
        this.buildGatewayConfigForm();
      }
    });
  }

  buildGatewayConfigForm() {
    this.gatewayConfigForm = this.formBuilder.group({
      configName: this.formBuilder.control(this.currentConfigData.name || 'build error'),
      routes: this.formBuilder.array([
        this.formBuilder.group({
          routeName: this.formBuilder.control(this.currentConfigData.routes[0].routeName || 'build error'),
          clusterName: this.formBuilder.control(this.currentConfigData.routes[0].clusterName || 'build error'),
          match: this.formBuilder.group({
            path: this.formBuilder.control(this.currentConfigData.routes[0].match.path || 'build error'),
          })
        })
      ]),
      clusters: this.formBuilder.array([
        this.formBuilder.group({
          clusterName: this.formBuilder.control(this.currentConfigData.clusters[0].clusterName || 'build error'),
          destinations: this.formBuilder.array([
            this.formBuilder.group({
              destinationName: this.formBuilder.control(this.currentConfigData.clusters[0].destinations[0].destinationName || 'build error'),
              address: this.formBuilder.control(this.currentConfigData.clusters[0].destinations[0].address || 'build error'),
            })
          ])
        })
      ]),
    });
  }

}
