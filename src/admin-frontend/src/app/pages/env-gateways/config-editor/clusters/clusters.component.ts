import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { DestinationsComponent } from "./destinations/destinations.component";
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";

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
  public readonly parent = input.required<FormGroup<any> | null>();
  readonly clustersArrayName = input.required<string>();
  public parentFormGroup!: FormGroup;
  public clusters!: FormArray;

  public readonly labelClusterName = 'Cluster Id: ';

  ngOnInit(): void {
    this.parentFormGroup = this.parent() as FormGroup;
    this.clusters = this.parentFormGroup.get(this.clustersArrayName()) as FormArray;
  };
}
