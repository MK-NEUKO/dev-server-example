import { Component, ElementRef, inject, Input, input, OnInit, ViewChild, signal, Output, EventEmitter } from '@angular/core';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { EditingModalService } from '../../../../../services/config-editor/editing-modal.service';
import { NgStyle } from '@angular/common';

@Component({
  selector: 'config-editor-editable-input',
  imports: [
    ReactiveFormsModule,
    NgStyle
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
  public readonly controlName = input.required<string>();
  public readonly showLabel = input<boolean>(true);
  @ViewChild('editButton') editButton!: ElementRef<HTMLButtonElement>;
  @Input() label!: string;
  @Output() editComplete = new EventEmitter<{ newValue: string, oldValue: string }>();
  public editingModalService = inject(EditingModalService);
  private editableInputReference = inject(ElementRef);

  public formControl!: FormControl;
  public parentFormGroup!: FormGroup;
  public isInputEditable = false;
  public editingModalSize = signal<{ width: number, height: number } | null>(null);


  ngOnInit(): void {
    this.formControl = this.parent()?.get(this.controlName()) as FormControl;
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
    const currentControlValue = this.formControl.value;
    const editableInputPosition = this.getEditableInputPosition();
    const modalInstance = this.editingModalService.open(editableInputPosition, this.formControl, this.label);
    if (modalInstance) {
      modalInstance.onSubmit = (data: any) => {
        this.editingModalSize.set(data.width && data.height ? { width: data.width, height: data.height } : null);
        if (data.value === 'cancel') {
          this.isInputEditable = false;
          this.editingModalService.close();
          return;
        }
        if (data && data.value !== undefined) {
          this.isInputEditable = false;
          this.formControl.setValue(data.value);
          this.editingModalService.close();
          this.editComplete.emit({ newValue: data.value, oldValue: currentControlValue });
        }
      };
    }
  }

  private getEditableInputPosition() {
    const editableInputRect = this.editableInputReference.nativeElement.getBoundingClientRect();
    const editableInputPosition = {
      top: editableInputRect.top,
      left: editableInputRect.left,
      width: editableInputRect.width,
      height: editableInputRect.height
    };
    return editableInputPosition;
  }
}
