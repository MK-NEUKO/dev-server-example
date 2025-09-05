import { Component, effect, inject } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormBuilder, Validators, FormArray } from '@angular/forms';
import { GatewayDataService } from '../../../services/env-gateway/gateway-data.service';
import { GatewayConfig } from '../../../models/gateway-config/gateway-config.model';
import { RoutesComponent } from './routes/routes.component';
import { ClustersComponent } from './clusters/clusters.component';
import { CONFIG_EDITOR_CONTROL_NAMES } from './shared/config-editor-control-names';
import { DestinationAddressValidator } from './shared/destination-address-validator';
import { NameValidator } from './shared/name-validator';
import { RouteConfig } from '../../../models/gateway-config/route-config.model';
import { Destination } from '../../../models/gateway-config/destination.model';
import { ClusterConfig } from '../../../models/gateway-config/cluster-config.model';

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

  get configName() {
    return this.gatewayConfigForm.get(CONFIG_EDITOR_CONTROL_NAMES.CONFIG_NAME);
  }

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
      [CONFIG_EDITOR_CONTROL_NAMES.ROUTES]: this.buildRoutesConfigForm(this.currentConfigData.routes),
      [CONFIG_EDITOR_CONTROL_NAMES.CLUSTERS]: this.buildClustersConfigForm(this.currentConfigData.clusters)
    });
  }

  private buildRoutesConfigForm(routes: RouteConfig[]): FormArray {
    return this.formBuilder.array(
      routes.map(route =>
        this.formBuilder.group({
          [CONFIG_EDITOR_CONTROL_NAMES.ROUTE_NAME]: this.formBuilder.control({
            value: route.routeName || 'build error',
            disabled: true
          }),
          [CONFIG_EDITOR_CONTROL_NAMES.CLUSTER_NAME]: this.formBuilder.control({
            value: route.clusterName || 'build error',
            disabled: true
          }),
          [CONFIG_EDITOR_CONTROL_NAMES.MATCH]: this.formBuilder.group({
            [CONFIG_EDITOR_CONTROL_NAMES.MATCH_PATH]: this.formBuilder.control({
              value: route.match.path || 'build error',
              disabled: true
            }),
          })
        })
      )
    );
  }

  private buildClustersConfigForm(clusters: ClusterConfig[]): FormArray {
    return this.formBuilder.array(
      clusters.map(cluster =>
        this.formBuilder.group({
          [CONFIG_EDITOR_CONTROL_NAMES.CLUSTER_ID]: this.formBuilder.control({
            value: cluster.id || 'build error',
            disabled: true
          }),
          [CONFIG_EDITOR_CONTROL_NAMES.CLUSTER_NAME]: this.formBuilder.control(
            cluster.clusterName || 'build error',
            [
              Validators.required,
              NameValidator.validate()
            ]
          ),
          [CONFIG_EDITOR_CONTROL_NAMES.DESTINATIONS]: this.buildDestinationsConfigForm(cluster.destinations)
        })
      )
    )
  }

  private buildDestinationsConfigForm(destinations: Record<string, Destination>): FormArray {
    const formGroups: FormGroup[] = [];

    Object.entries(destinations).forEach(destination => {
      const destinationName = destination[0];
      const destinationData = destination[1];

      const destinationFormGroup = this.formBuilder.group({
        [CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ID]: this.formBuilder.control({
          value: destinationData['id'] || 'build error',
          disabled: true
        }),
        [CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME]: this.formBuilder.control(
          destinationName || 'build error',
          [
            Validators.required,
            NameValidator.validate()
          ]
        ),
        [CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS]: this.formBuilder.control(
          destinationData['address'] || 'build error',
          [
            Validators.required,
            DestinationAddressValidator.validate()
          ]
        )
      });

      formGroups.push(destinationFormGroup);
    });

    return this.formBuilder.array(formGroups);
  }

}
