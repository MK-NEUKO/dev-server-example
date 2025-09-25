import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";

@Component({
  selector: 'config-editor-routes',
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

  public readonly CONTROL_NAMES = CONFIG_EDITOR_CONTROL_NAMES;
  public readonly parent = input.required<FormGroup<any> | null>();
  readonly routesArrayName = input.required<string>();
  public parentFormGroup!: FormGroup;
  public routes!: FormArray;

  public readonly labelClusterId = 'Cluster Id: ';
  public readonly labelRouteName = 'Route Id: ';
  public readonly labelMatchPath = 'Path: ';

  get path() {
    const route = this.routes.at(0) as FormGroup;
    return route.get(CONFIG_EDITOR_CONTROL_NAMES.MATCH_PATH);
  }

  ngOnInit(): void {
    this.parentFormGroup = this.parent() as FormGroup;
    this.routes = this.parentFormGroup.get(this.routesArrayName()) as FormArray;
  };
}
