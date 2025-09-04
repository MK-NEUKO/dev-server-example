import { Component, effect, ElementRef, inject, OnInit, signal, ViewChild } from '@angular/core';
import { RequestResponse } from '../../services/env-gateway/RequestResponse/request-response';
import { DialogService } from '../../services/dialog-service/dialog.service';

@Component({
  selector: 'app-request-dialog',
  imports: [],
  templateUrl: './request-dialog.component.html',
  styleUrl: './request-dialog.component.css'
})
export class RequestDialogComponent implements OnInit {

  private dialogService = inject(DialogService);
  public requestResponse = signal<RequestResponse | null>(null);
  public title = signal<string | ''>('');
  public showDetails = false;
  @ViewChild('modal') modalReference!: ElementRef;

  constructor() {
    effect(() => {
      console.log(this.requestResponse());
      if (this.requestResponse()) {
        if (this.requestResponse()?.isError) {
          this.showError();
        } else {
          this.showSuccess();
        }
      }

    });
  }

  ngOnInit(): void {
    //this.showError();
  }


  onClose() {
    this.dialogService.close();
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
