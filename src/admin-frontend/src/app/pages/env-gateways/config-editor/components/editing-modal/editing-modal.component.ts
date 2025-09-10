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

  constructor() {
    afterNextRender(() => {
      this.setEditingModalSize();
    });
  }

  get modalPosition() {
    return this.editingModalService.modalPosition();
  }


  setEditingModalSize() {
    const editingModalRect = this.modalContainer.nativeElement.getBoundingClientRect();
    this.editingModalService.setModalSize({ width: editingModalRect.width, height: editingModalRect.height });
  }
}