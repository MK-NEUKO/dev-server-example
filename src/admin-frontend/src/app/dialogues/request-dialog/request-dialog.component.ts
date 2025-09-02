import { Component, EventEmitter, Output, input } from '@angular/core';
import { RequestResponse } from '../../services/env-gateway/RequestResponse/request-response';
import { ErrorDetails } from '../../services/env-gateway/error/error-details';

@Component({
  selector: 'app-request-dialog',
  imports: [],
  templateUrl: './request-dialog.component.html',
  styleUrl: './request-dialog.component.css'
})
export class RequestDialogComponent {
  readonly title = input<string>('');
  readonly requestResponse = input<RequestResponse | null>(null);
  readonly visible = input<boolean>(false);
  @Output() close = new EventEmitter<void>();

  public showDetails = false;

  onClose() {
    this.close.emit();
  }

  public toggleDetails(): void {
    this.showDetails = !this.showDetails;
  }
}
