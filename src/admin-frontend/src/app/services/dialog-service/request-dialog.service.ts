import { Injectable, ApplicationRef, ComponentRef, Injector, createComponent } from '@angular/core';
import { RequestDialogComponent } from '../../dialogues/request-dialog/request-dialog.component';
import { RequestResponse } from '../env-gateway/RequestResponse/request-response';

@Injectable({ providedIn: 'root' })
export class RequestDialogService {
  private requestDialogReference?: ComponentRef<RequestDialogComponent>;

  constructor(private appRef: ApplicationRef, private injector: Injector) { }

  open(title: string = ''): RequestDialogComponent | undefined {
    if (this.requestDialogReference) return;

    this.requestDialogReference = createComponent(RequestDialogComponent, { environmentInjector: this.appRef.injector });
    this.requestDialogReference.instance.title.set(title);
    this.appRef.attachView(this.requestDialogReference.hostView);
    document.body.appendChild(this.requestDialogReference.location.nativeElement);

    return this.requestDialogReference.instance;
  }

  setRequestResponse(requestResponse: RequestResponse) {
    if (this.requestDialogReference) {
      this.requestDialogReference.instance.requestResponse.set(requestResponse);
    }
  }

  setTitle(title: string) {
    if (this.requestDialogReference) {
      this.requestDialogReference.instance.title.set(title);
    }
  }

  close() {
    if (this.requestDialogReference) {
      this.appRef.detachView(this.requestDialogReference.hostView);
      this.requestDialogReference.destroy();
      this.requestDialogReference = undefined;
    }
  }
}
