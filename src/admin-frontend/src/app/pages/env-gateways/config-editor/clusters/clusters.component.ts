import { Component, OnInit, inject, input } from '@angular/core';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DestinationsComponent } from "./destinations/destinations.component";
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";
import { CONFIG_EDITOR_CONTROL_LABELS } from '../shared/config-editor-control-labels';
import { ClusterService } from '../../../../services/env-gateway/clusters/cluster.service';

@Component({
  selector: 'config-editor-clusters',
  imports: [
    ReactiveFormsModule,
    DestinationsComponent,
    EditableInputComponent,
  ],
  templateUrl: './clusters.component.html',
  styleUrls: [
    './clusters.component.css',
    '../config-editor.component.css'
  ]
})
export class ClustersComponent implements OnInit {

  public readonly CONTROL_NAMES = CONFIG_EDITOR_CONTROL_NAMES;
  public readonly CONTROL_LABELS = CONFIG_EDITOR_CONTROL_LABELS;
  public readonly parent = input.required<FormGroup<any> | null>();
  readonly clustersArrayName = input.required<string>();
  private readonly clusterService = inject(ClusterService);
  public parentFormGroup!: FormGroup;
  public clusters!: FormArray;

  ngOnInit(): void {
    this.parentFormGroup = this.parent() as FormGroup;
    this.clusters = this.parentFormGroup.get(this.clustersArrayName()) as FormArray;
  };

  public onSaveClusterName(newClusterName: string, clusterIndex: number): void {
    console.log(`Cluster name at index ${clusterIndex} changed to: ${newClusterName}`);
    this.clusterService.SaveClusterNameChanges({ newName: newClusterName });
  }
}
