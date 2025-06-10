import { Component, effect, inject } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { GatewayDataService } from '../../../services/env-gateway/gateway-data.service';
import { GatewayConfig } from '../../../models/gateway-config/gateway-config.model';
import { RoutesComponent } from './routes/routes.component';
import { ClustersComponent } from './clusters/clusters.component';
import { CONFIG_EDITOR_CONTROL_NAMES } from './shared/config-editor-control-names';

@Component({
  selector: 'app-config-editor',
  imports: [
    ReactiveFormsModule,
    RoutesComponent,
    ClustersComponent,
  ],
  templateUrl: './config-editor.component.html',
  styleUrl: './config-editor.component.css'
})
export class ConfigEditorComponent {

  private gatewayDataService = inject(GatewayDataService);
  private formBuilder = inject(FormBuilder);
  readonly routesControlName = CONFIG_EDITOR_CONTROL_NAMES.ROUTES;
  readonly clustersControlName = CONFIG_EDITOR_CONTROL_NAMES.CLUSTERS;
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
      [CONFIG_EDITOR_CONTROL_NAMES.CONFIG_NAME]: this.formBuilder.control({ value: this.currentConfigData.name || 'build error', disabled: true }),
      [CONFIG_EDITOR_CONTROL_NAMES.ROUTES]: this.formBuilder.array([
        this.formBuilder.group({
          [CONFIG_EDITOR_CONTROL_NAMES.ROUTE_NAME]: this.formBuilder.control({ value: this.currentConfigData.routes[0].routeName || 'build error', disabled: true }),
          [CONFIG_EDITOR_CONTROL_NAMES.CLUSTER_NAME]: this.formBuilder.control({ value: this.currentConfigData.routes[0].clusterName || 'build error', disabled: true }),
          [CONFIG_EDITOR_CONTROL_NAMES.MATCH]: this.formBuilder.group({
            [CONFIG_EDITOR_CONTROL_NAMES.MATCH_PATH]: this.formBuilder.control({ value: this.currentConfigData.routes[0].match.path || 'build error', disabled: true }),
          })
        })
      ]),
      [CONFIG_EDITOR_CONTROL_NAMES.CLUSTERS]: this.formBuilder.array([
        this.formBuilder.group({
          [CONFIG_EDITOR_CONTROL_NAMES.CLUSTER_NAME]: this.formBuilder.control({ value: this.currentConfigData.clusters[0].clusterName || 'build error', disabled: true }),
          [CONFIG_EDITOR_CONTROL_NAMES.DESTINATIONS]: this.formBuilder.array([
            this.formBuilder.group({

              [CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME]: this.formBuilder.control({ value: this.currentConfigData.clusters[0].destinations[0].destinationName || 'build error', disabled: true }),
              [CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS]: this.formBuilder.control(this.currentConfigData.clusters[0].destinations[0].address || 'build error'),
            })
          ])
        })
      ]),
    });
  }

}
