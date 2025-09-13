import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { DestinationsComponent } from "./destinations/destinations.component";
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";

@Component({
  selector: 'app-clusters',
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
  readonly formArrayName = input.required<string>();
  readonly childArrayControlName = CONFIG_EDITOR_CONTROL_NAMES.DESTINATIONS;
  readonly parentArrayControlName = CONFIG_EDITOR_CONTROL_NAMES.CLUSTERS;
  private rootFormGroup = inject(FormGroupDirective);
  formArray!: FormArray;
  parentForm!: FormGroup;

  public readonly labelClusterName = 'Cluster Id: ';

  get clusterName() {
    const cluster = this.formArray.at(0) as FormGroup;
    return cluster.get(CONFIG_EDITOR_CONTROL_NAMES.CLUSTER_NAME);
  }

  ngOnInit(): void {
    this.parentForm = this.rootFormGroup.control;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;
  };
}
