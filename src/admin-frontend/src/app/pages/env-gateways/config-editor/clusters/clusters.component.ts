import { Component, EventEmitter, OnInit, Output, inject, input } from '@angular/core';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DestinationsComponent } from "./destinations/destinations.component";
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";
import { CONFIG_EDITOR_CONTROL_LABELS } from '../shared/config-editor-control-labels';
import { ClusterService } from '../../../../services/env-gateway/clusters/cluster.service';
import { RequestDialogService } from '../../../../services/dialog-service/request-dialog.service';

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
  private readonly requestDialogService = inject(RequestDialogService);
  @Output() clusterNameChanged = new EventEmitter<{ clusterNameBeforeChange: string, newClusterName: string }>();
  public parentFormGroup!: FormGroup;
  public clusters!: FormArray;

  ngOnInit(): void {
    this.parentFormGroup = this.parent() as FormGroup;
    this.clusters = this.parentFormGroup.get(this.clustersArrayName()) as FormArray;
  };

  public async onSaveClusterName(values: { newClusterName: string, oldClusterName: string }, clusterIndex: number): Promise<void> {
    const requestDialog = this.requestDialogService.open('Cluster name will be changed');
    const clusterId = this.clusters.at(clusterIndex).get(this.CONTROL_NAMES.CLUSTER_ID)?.value;
    const request = { clusterName: values.newClusterName, clusterId: clusterId };

    const requestResponse = await this.clusterService.SaveClusterNameChanges(request);

    if (requestDialog && requestResponse) {
      this.requestDialogService.setRequestResponse(requestResponse);
      this.requestDialogService.setTitle('Change cluster name response');
    }

    if (requestDialog && requestResponse.isSuccess) {
      this.requestDialogService.onClose(() => {
        this.clusterNameChanged.emit({ clusterNameBeforeChange: values.oldClusterName, newClusterName: values.newClusterName });
      });
    }
  }
}
