import { Component, ElementRef, inject, input, OnInit, ViewChild } from '@angular/core';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { EditingModalService } from '../../../../../services/config-editor/editing-modal.service';

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
  @ViewChild('editButton') editButton!: ElementRef<HTMLButtonElement>;
  private editingModalService = inject(EditingModalService);
  private elementRef = inject(ElementRef);

  public formControl!: FormControl;
  public parentFormGroup!: FormGroup;
  public isInputEditable = false;

  ngOnInit(): void {
    this.formControl = this.parent()?.get(this.CONTROL_NAMES.CLUSTER_NAME) as FormControl;
    this.parentFormGroup = this.parent() as FormGroup;
  }

  public onInputControlFocus() {
    this.isInputEditable = true;
  }

  public onInputControlBlur() {
    if (this.editButton.nativeElement.matches(':hover')) {
      return;
    }
    this.isInputEditable = false;
  }

  public openEditingModal() {
    const hostComponentRect = this.elementRef.nativeElement.getBoundingClientRect();
    const position = { top: hostComponentRect.top, left: hostComponentRect.left, width: hostComponentRect.width };
    this.editingModalService.open(position);
  }
}
