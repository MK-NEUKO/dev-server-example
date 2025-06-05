import { Component, effect, inject } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule, FormArray } from '@angular/forms';
import { GatewayDataService } from '../../../services/env-gateway/gateway-data.service';
import { GatewayConfig } from '../../../models/gateway-config/gateway-config.model';

@Component({
  selector: 'app-config-editor',
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './config-editor.component.html',
  styleUrl: './config-editor.component.css'
})
export class ConfigEditorComponent {

  private gatewayDataService = inject(GatewayDataService);
  public currentConfig = this.gatewayDataService.getCurrentConfig();
  public configData!: GatewayConfig;

  constructor() {
    effect(() => {
      const value = this.currentConfig.value();
      if (value !== undefined) {
        this.configData = value;
        this.updateConfigForm();
      }
    });
  }

  config = new FormGroup({
    configName: new FormControl('not init'),
    configId: new FormControl('not init'),
    routes: new FormGroup({}),
    clusters: new FormGroup({
      clusterName: new FormControl('not init'),
      destination: new FormGroup({
        destinationName: new FormControl('not init'),
        address: new FormControl(),
      })
    }),
  });



  updateConfigForm() {
    this.config.get('configName')?.setValue(this.configData?.name || 'error');
    this.config.get('configId')?.setValue(this.configData?.id || 'error');
    this.config.get('clusters.clusterName')?.setValue(this.configData?.clusters[0].clusterName || 'error');
    this.config.get('clusters.destination.destinationName')?.setValue(this.configData?.clusters[0].destinations[0].destinationName || 'error');
    this.config.get('clusters.destination.address')?.setValue(this.configData?.clusters[0].destinations[0].address || 'error');
  }

}
