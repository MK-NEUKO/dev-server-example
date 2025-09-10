import { afterNextRender, Component, ElementRef, inject, ViewChild, viewChild } from '@angular/core';
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
export class EditingModalComponent {
  private editingModalService = inject(EditingModalService);
  @ViewChild('modalContainer') modalContainer!: ElementRef;

  public formControl!: FormControl;
  public label!: string;
  public onSubmit?: (value: any) => void;

  constructor() {
    afterNextRender(() => {
      this.setEditingModalSize();
    });
  }

  public onFormSubmit() {
    if (this.onSubmit) {
      this.onSubmit({ value: this.formControl.value });
    }
  }

  get modalPosition() {
    return this.editingModalService.modalPosition();
  }

  setEditingModalSize() {
    const editingModalRect = this.modalContainer.nativeElement.getBoundingClientRect();
    this.editingModalService.setModalSize({ width: editingModalRect.width, height: editingModalRect.height });
  }

  public onCancelClick() {
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
}