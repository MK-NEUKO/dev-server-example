import { ApplicationRef, ComponentRef, createComponent, inject, Injectable, Injector, signal } from '@angular/core';
import { EditingModalComponent } from '../../pages/env-gateways/config-editor/components/editing-modal/editing-modal.component';
import { FormControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class EditingModalService {
  private editingModalReference!: ComponentRef<EditingModalComponent>;

  public modalPosition = signal<{ top: number, left: number, width: number, height: number } | null>(null);
  public modalSize = signal<{ width: number, height: number } | null>(null);

  private applicationReference = inject(ApplicationRef);
  private injector = inject(Injector);

  open(
    position: { top: number, left: number, width: number, height: number },
    formControl: FormControl,
    label: string
  ): EditingModalComponent | undefined {
    if (this.editingModalReference) {
      return undefined;
    }

    this.modalPosition.set(position);
    this.editingModalReference = createComponent(EditingModalComponent, { environmentInjector: this.applicationReference.injector });
    this.applicationReference.attachView(this.editingModalReference.hostView);
    document.body.appendChild(this.editingModalReference.location.nativeElement);
    this.editingModalReference.instance.formControl = formControl;
    this.editingModalReference.instance.label = label;

    return this.editingModalReference.instance;
  }

  setModalSize(size: { width: number, height: number }) {
    this.modalSize.set(size);
  }

  close() {
    if (this.editingModalReference) {
      this.applicationReference.detachView(this.editingModalReference.hostView);
      this.editingModalReference.destroy();
      this.editingModalReference = undefined!;
      this.modalPosition.set(null);
      this.modalSize.set(null);
    }
  }
}
