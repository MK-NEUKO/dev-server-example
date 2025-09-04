import { Injectable, ApplicationRef, ComponentRef, Injector, createComponent } from '@angular/core';
import { RequestDialogComponent } from '../../dialogues/request-dialog/request-dialog.component';
import { RequestResponse } from '../env-gateway/RequestResponse/request-response';

@Injectable({ providedIn: 'root' })
export class DialogService {
  private dialogRef?: ComponentRef<RequestDialogComponent>;

  constructor(private appRef: ApplicationRef, private injector: Injector) { }

  open(title: string = ''): RequestDialogComponent | undefined {
    if (this.dialogRef) return;

    this.dialogRef = createComponent(RequestDialogComponent, { environmentInjector: this.appRef.injector });
    this.dialogRef.instance.title.set(title);
    this.appRef.attachView(this.dialogRef.hostView);
    document.body.appendChild(this.dialogRef.location.nativeElement);

    return this.dialogRef.instance;
  }

  setRequestResponse(requestResponse: RequestResponse) {
    if (this.dialogRef) {
      this.dialogRef.instance.requestResponse.set(requestResponse);
    }
  }

  setRequestDialogTitle(title: string) {
    if (this.dialogRef) {
      this.dialogRef.instance.title.set(title);
    }
  }

  close() {
    if (this.dialogRef) {
      this.appRef.detachView(this.dialogRef.hostView);
      this.dialogRef.destroy();
      this.dialogRef = undefined;
    }
  }
}
