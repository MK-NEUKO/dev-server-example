import { Component, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";
import { CONFIG_EDITOR_CONTROL_LABELS } from '../shared/config-editor-control-labels';

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
  public readonly CONTROL_LABELS = CONFIG_EDITOR_CONTROL_LABELS;
  public readonly parent = input.required<FormGroup<any> | null>();
  readonly routesArrayName = input.required<string>();
  public parentFormGroup!: FormGroup;
  public routes!: FormArray;

  ngOnInit(): void {
    this.parentFormGroup = this.parent() as FormGroup;
    this.routes = this.parentFormGroup.get(this.routesArrayName()) as FormArray;
  };
}
