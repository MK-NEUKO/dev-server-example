import { Injectable, ApplicationRef, ComponentRef, Injector, createComponent } from '@angular/core';
import { RequestDialogComponent } from '../../dialogues/request-dialog/request-dialog.component';
import { RequestResponse } from '../env-gateway/RequestResponse/request-response';

@Injectable({ providedIn: 'root' })
export class RequestDialogService {
  private requestDialogReference?: ComponentRef<RequestDialogComponent>;
  private readonly onCloseCallbacks = new Set<() => void>();

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
      this.triggerCloseCallbacks();
    }
  }

  onClose(callback: () => void) {
    this.onCloseCallbacks.add(callback);
    return () => this.onCloseCallbacks.delete(callback);
  }

  private triggerCloseCallbacks(): void {
    this.onCloseCallbacks.forEach(callback => {
      try {
        callback();
      } catch (error) {
        console.error('Error executing onClose callback:', error);
      }
    });
    this.onCloseCallbacks.clear();
  }

}