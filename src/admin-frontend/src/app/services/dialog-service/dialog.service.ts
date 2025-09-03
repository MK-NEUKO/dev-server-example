import { Injectable, ApplicationRef, ComponentRef, Injector, createComponent } from '@angular/core';
import { RequestDialogComponent } from '../../dialogues/request-dialog/request-dialog.component';
import { RequestResponse } from '../env-gateway/RequestResponse/request-response';

@Injectable({ providedIn: 'root' })
export class DialogService {
  private dialogRef?: ComponentRef<RequestDialogComponent>;

  constructor(private appRef: ApplicationRef, private injector: Injector) { }

  open(requestResponse: RequestResponse, title: string = '') {
    if (this.dialogRef) return; // Nur ein Dialog gleichzeitig

    this.dialogRef = createComponent(RequestDialogComponent, { environmentInjector: this.appRef.injector });
    this.dialogRef.instance.requestResponse = requestResponse;
    this.dialogRef.instance.title = title;

    this.appRef.attachView(this.dialogRef.hostView);
    document.body.appendChild(this.dialogRef.location.nativeElement);
  }

  close() {
    if (this.dialogRef) {
      this.appRef.detachView(this.dialogRef.hostView);
      this.dialogRef.destroy();
      this.dialogRef = undefined;
    }
  }
}
