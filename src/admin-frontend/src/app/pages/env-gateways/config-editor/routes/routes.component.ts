import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";

@Component({
  selector: 'app-routes',
  imports: [
    ReactiveFormsModule,
    EditableInputComponent
  ],
  templateUrl: './routes.component.html',
  styleUrls: [
    './routes.component.css',
    '../config-editor.component.css'
  ]
})
export class RoutesComponent implements OnInit {

  readonly formArrayName = input.required<string>();
  private rootFormGroup = inject(FormGroupDirective);
  formArray!: FormArray;
  parentForm!: FormGroup;
  public readonly labelClusterId = 'Cluster Id: ';

  get routeName() {
    const route = this.formArray.at(0) as FormGroup;
    return route.get(CONFIG_EDITOR_CONTROL_NAMES.ROUTE_NAME);
  }

  get clusterName() {
    const route = this.formArray.at(0) as FormGroup;
    return route.get(CONFIG_EDITOR_CONTROL_NAMES.CLUSTER_NAME);
  }

  get path() {
    const route = this.formArray.at(0) as FormGroup;
    return route.get(CONFIG_EDITOR_CONTROL_NAMES.MATCH_PATH);
  }

  ngOnInit(): void {
    this.parentForm = this.rootFormGroup.control;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;
  };
}
