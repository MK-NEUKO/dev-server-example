import { Component, ElementRef, inject, Input, input, OnInit, ViewChild } from '@angular/core';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { EditingModalService } from '../../../../../services/config-editor/editing-modal.service';
import { NgStyle } from '@angular/common';

@Component({
  selector: 'app-editable-input',
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
  @ViewChild('editButton') editButton!: ElementRef<HTMLButtonElement>;
  @Input() label!: string;
  public editingModalService = inject(EditingModalService);
  private editableInputReference = inject(ElementRef);

  public formControl!: FormControl;
  public parentFormGroup!: FormGroup;
  public isInputEditable = false;

  get editingModalSize() {
    return this.editingModalService.modalSize();
  }

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
    const editableInputRect = this.editableInputReference.nativeElement.getBoundingClientRect();
    const editableInputPosition = {
      top: editableInputRect.top,
      left: editableInputRect.left,
      width: editableInputRect.width,
      height: editableInputRect.height
    };
    this.editingModalService.open(editableInputPosition, this.formControl, this.label);
  }
}
