import { afterNextRender, Component, ElementRef, inject, OnInit, ViewChild, viewChild } from '@angular/core';
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
  public validationErrors: Record<string, any>[] | null = null;

  private initialValue!: string;
  public onSubmit?: (value: any) => void;

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
    this.editingModalService.setModalSize({ width: editingModalRect.width, height: editingModalRect.height });
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
  }

  public onResetClick() {
    this.formControl.setValue(this.initialValue);
    this.isSubmitDisabled = true;
  }

  public onCancelClick() {
    this.formControl.setValue(this.initialValue);
    this.isSubmitDisabled = true;
    this.editingModalService.close()
    if (this.onSubmit) {
      this.onSubmit({ value: 'cancel' });
    }
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
    if (this.formControl.errors) {
      this.validationErrors = Object.entries(this.formControl.errors).map(([key, value]) => ({ key, value }));
      console.log('Validation Errors:', this.validationErrors);

    } else {
      this.validationErrors = null;
    }
  }
}