import { Component, effect, ElementRef, inject, signal, ViewChild } from '@angular/core';
import { RequestResponse } from '../../services/env-gateway/RequestResponse/request-response';
import { RequestDialogService } from '../../services/dialog-service/request-dialog.service';

@Component({
  selector: 'app-request-dialog',
  imports: [],
  templateUrl: './request-dialog.component.html',
  styleUrl: './request-dialog.component.css'
})
export class RequestDialogComponent {

  private requestDialogService = inject(RequestDialogService);
  public requestResponse = signal<RequestResponse | null>(null);
  public title = signal<string | ''>('');
  public showDetails = false;
  @ViewChild('modal') modalReference!: ElementRef;

  constructor() {
    effect(() => {
      if (this.requestResponse()) {
        if (this.requestResponse()?.isError) {
          this.showError();
        } else {
          this.showSuccess();
        }
      }

    });
  }

  onClose() {
    this.requestDialogService.close();
    this.showDetails = false;
  }

  public toggleDetails(): void {
    this.showDetails = !this.showDetails;
  }

  private showError() {
    const modal = document.getElementById('modal');

    if (modal) {
      modal.classList.remove('modal--success');
      modal.classList.add('modal--error');
    }
  }

  private showSuccess() {
    const modal = document.getElementById('modal');

    if (modal) {
      modal.classList.remove('modal--error');
      modal.classList.add('modal--success');
    }
  }

  public onBackdropClick(): void {
    const modalElement = this.modalReference.nativeElement;
    if (modalElement) {
      modalElement.classList.add('shake');
      modalElement.addEventListener('animationend', () => {
        modalElement.classList.remove('shake');
      }, { once: true });
    }
  }
}
