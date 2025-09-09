import { Component, input, OnInit } from '@angular/core';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-editable-input',
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './editable-input.component.html',
  styleUrls: [
    './editable-input.component.css',
  ]
})
export class EditableInputComponent implements OnInit {

  public readonly CONTROL_NAMES = CONFIG_EDITOR_CONTROL_NAMES;
  public readonly parent = input.required<AbstractControl<any, any> | null>();
  public readonly parentIndex = input.required<number>();

  public formControl!: FormControl;
  public parentFormGroup!: FormGroup;

  ngOnInit(): void {
    this.formControl = this.parent()?.get(this.CONTROL_NAMES.CLUSTER_NAME) as FormControl;
    this.parentFormGroup = this.parent() as FormGroup;
  }

}
