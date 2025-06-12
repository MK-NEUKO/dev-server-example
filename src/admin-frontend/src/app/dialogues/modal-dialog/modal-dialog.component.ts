import { Component, EventEmitter, Output, input } from '@angular/core';

@Component({
  selector: 'app-modal-dialog',
  imports: [],
  templateUrl: './modal-dialog.component.html',
  styleUrl: './modal-dialog.component.css'
})
export class ModalDialogComponent {
  readonly title = input<string>('');
  readonly visible = input<boolean>(false);
  @Output() close = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }
}
