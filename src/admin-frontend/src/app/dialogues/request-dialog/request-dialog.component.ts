import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
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
  public title: string = '';
  public requestResponse: RequestResponse | null = null;
  public showDetails = false;
  @ViewChild('modal') modalReference!: ElementRef;

  ngOnInit(): void {
    this.showError();
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
      modal.classList.add('modal--error');
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
