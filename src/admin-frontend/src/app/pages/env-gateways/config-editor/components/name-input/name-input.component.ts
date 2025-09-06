import { Component, input, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { EditingToolsComponent } from '../editing-tools/editing-tools.component';

@Component({
  selector: 'app-name-input',
  imports: [
    ReactiveFormsModule,
    EditingToolsComponent,
  ],
  templateUrl: './name-input.component.html',
  styleUrls: [
    './name-input.component.css',
    '../../config-editor.component.css'
  ]
})
export class NameInputComponent implements OnInit {

  public readonly CONTROL_NAMES = CONFIG_EDITOR_CONTROL_NAMES;
  public readonly parent = input.required<AbstractControl<any, any> | null>();
  public readonly parentIndex = input.required<number>();

  public formControl!: FormControl;
  public parentFormGroup!: FormGroup;
  public isEditingToolsVisible = false;

  ngOnInit(): void {
    this.formControl = this.parent()?.get(this.CONTROL_NAMES.CLUSTER_NAME) as FormControl;
    this.parentFormGroup = this.parent() as FormGroup;
  }

  public onInputControlFocus() {
    this.isEditingToolsVisible = true;
  }

  public onEditingToolsCancel() {
    this.isEditingToolsVisible = false;
    console.log('Editing tools canceled');

  }
}