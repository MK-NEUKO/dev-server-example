import { Component, EventEmitter, input, Output } from '@angular/core';

@Component({
  selector: 'app-request-progress',
  imports: [],
  templateUrl: './request-progress.component.html',
  styleUrl: './request-progress.component.css'
})
export class RequestProgressComponent {
  readonly title = input<string>('');
  readonly visible = input<boolean>(false);
  @Output() close = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }
}
