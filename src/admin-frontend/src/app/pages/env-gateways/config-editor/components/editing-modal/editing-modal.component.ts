import { afterNextRender, Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { EditingModalService } from '../../../../../services/config-editor/editing-modal.service';
import { NgStyle } from '@angular/common';

@Component({
  selector: 'app-editing-modal',
  imports: [
    ReactiveFormsModule,
    NgStyle,
  ],
  templateUrl: './editing-modal.component.html',
  styleUrls: [
    './editing-modal.component.css',
    '../../config-editor.component.css'
  ]
})
export class EditingModalComponent implements OnInit {
  private editingModalService = inject(EditingModalService);
  @ViewChild('modalContainer') modalContainer!: ElementRef;

  public formControl!: FormControl;
  public label!: string;
  public isSubmitDisabled = true;
  public validationErrors: { key: string, value: { key: string, value: any }[] }[] | null = null;
  private initialValue!: string;
  public onSubmit?: (value: any) => void;
  public feedbackOutOfViewport = false;

  constructor() {
    afterNextRender(() => {
      this.setEditingModalSize();
    });
  }

  ngOnInit() {
    this.initialValue = this.formControl.value;
  }

  get modalPosition() {
    return this.editingModalService.modalPosition();
  }

  setEditingModalSize() {
    const editingModalRect = this.modalContainer.nativeElement.getBoundingClientRect();
    if (this.onSubmit) {
      this.onSubmit({ width: editingModalRect.width, height: editingModalRect.height });
    }
  }

  public onInputChange(event: Event) {
    if (this.formControl.invalid) {
      this.processValidationErrors();
    }
    this.setIsSubmitDisabled(event);
  }

  public onFormSubmit() {
    if (this.onSubmit) {
      this.onSubmit({ value: this.formControl.value });
    }
    this.isSubmitDisabled = true;
  }

  public onResetClick() {
    this.formControl.setValue(this.initialValue);
    this.isSubmitDisabled = true;
  }

  public onCancelClick() {
    this.formControl.setValue(this.initialValue);
    if (this.onSubmit) {
      this.onSubmit({ value: 'cancel' });
    }
    this.isSubmitDisabled = true;
  }

  public onBackdropClick(): void {
    const modalElement = this.modalContainer.nativeElement;
    if (modalElement) {
      modalElement.classList.add('shake');
      modalElement.addEventListener('animationend', () => {
        modalElement.classList.remove('shake');
      }, { once: true });
    }
  }

  private setIsSubmitDisabled(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    const isValueValid = this.formControl.valid;
    const isValueChanged = value !== this.initialValue;
    if (isValueChanged && isValueValid) {
      this.isSubmitDisabled = false;
      return;
    }
    this.isSubmitDisabled = true;
  }

  private processValidationErrors() {
    this.validationErrors = [];
    if (this.formControl.errors) {
      const errors = Object.entries(this.formControl.errors).map(([key, value]) => ({ key, value }));
      for (const error of errors) {
        if (typeof error['value'] === 'object') {
          const errorDetailsObject = Object.entries(error['value']).map(([key, value]) => ({ key, value }));
          const processedErrorObject = { key: error.key, value: errorDetailsObject };
          this.validationErrors.push(processedErrorObject);
        } else {
          const errorDetailPrimitive = [{ key: 'Found value', value: error['value'] }];
          const processedErrorPrimitive = { key: error.key, value: errorDetailPrimitive };
          this.validationErrors.push(processedErrorPrimitive);
        }
      }
      return;
    } else {
      this.validationErrors = [];
    }
  }
}