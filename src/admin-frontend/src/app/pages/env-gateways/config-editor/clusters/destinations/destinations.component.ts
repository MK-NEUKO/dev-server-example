import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { DestinationService } from '../../../../../services/env-gateway/destination/destination.service';
import { ClustersComponent } from '../clusters.component';
import { ModalDialogComponent } from "../../../../../dialogues/modal-dialog/modal-dialog.component";
import { RequestProgressComponent } from '../../../../../dialogues/request-progress/request-progress.component';

@Component({
  selector: 'app-destinations',
  imports: [
    ReactiveFormsModule,
    ModalDialogComponent,
    RequestProgressComponent
  ],
  templateUrl: './destinations.component.html',
  styleUrls: [
    './destinations.component.css',
    '../../config-editor.component.css'
  ]
})
export class DestinationsComponent implements OnInit {

  readonly formArrayName = input.required<string>();
  readonly parentArrayName = input.required<string>();
  private rootFormGroup = inject(FormGroupDirective);
  private destinationService = inject(DestinationService);
  public showDialog: boolean = false;
  public showRequestProgressDialog: boolean = false;
  public modalTitle: string = '';
  public modalMessage: string = '';
  public formArray!: FormArray;
  public parentForm!: FormGroup;
  public canControlOptionsDisplayed: Record<string, boolean> = {
    destinationName: false,
    address: false,
    test1: true,
    test2: true,
  };
  private blurCausedByControlOptionButton: boolean = false;

  ngOnInit(): void {
    const rootForm = this.rootFormGroup.control;
    const parentArray = rootForm.get(this.parentArrayName()) as FormArray;
    this.parentForm = parentArray.at(0) as FormGroup;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;

  }

  public onControlFocus(index: number, controlName: string): void {
    const control = document.querySelector('input[formControlName="' + controlName + '"]') as HTMLInputElement;

    if (control) {
      control.classList.remove('control__input-changed');
    }

    Object.keys(this.canControlOptionsDisplayed).forEach(key => {
      this.canControlOptionsDisplayed[key] = (key === controlName);
    });
  }

  public onControlBlur(index: number, controlName: string): void {
    const control = document.querySelector('input[formControlName="' + controlName + '"]') as HTMLInputElement;

    setTimeout(() => {
      if (this.blurCausedByControlOptionButton) {
        if (control) {
          control.classList.remove('control__input-reminder');
        }
        return;
      }

      if (this.isControlValueChanged(index, controlName)) {
        if (control) {
          control.focus();
          control.classList.add('control__input-reminder');
        }
        return;
      }
    });
  }

  public onControlOptionButtonMouseDown(): void {
    this.blurCausedByControlOptionButton = true;
  }

  public isControlValueChanged(index: number, controlName: string): boolean | undefined {
    const control = this.formArray.at(index).get(controlName);
    if (control?.pristine) {
      return false;
    }
    if (control?.invalid) {
      return false;
    }

    return true;
  }

  public reset(index: number): void {
    const address = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME);
    address?.reset();
    address?.markAsTouched();
    address?.markAsDirty();
  }

  public async changeDestinationProperty(index: number, controlName: string): Promise<void> {
    this.openRequestProgressDialog(`${controlName} will be changed`);

    const request = {
      clusterId: this.parentForm.get('clusterId')?.value,
      destinationId: this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ID)?.value,
      [controlName]: this.formArray.at(index).get(controlName)?.value
    };

    let responseMessage = '';
    let responseTitle = '';

    if (controlName === CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME) {
      responseMessage = await this.destinationService.SaveDestinationNameChanges(request);
      responseTitle = 'Destination Name Update Response';
    } else if (controlName === CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS) {
      responseMessage = await this.destinationService.SaveDestinationAddressChanges(request);
      responseTitle = 'Destination Address Update Response';
    }

    this.showRequestProgressDialog = false;
    this.openDialog(responseTitle, responseMessage);
    this.resetControlProperties(index, controlName);
    this.resetCanControlOptionsDisplayed();
  }


  private resetControlProperties(index: number, controlName: string): void {
    const control = this.formArray.at(index).get(controlName);
    control?.markAsPristine();
    control?.markAsUntouched();
    const input = document.querySelector('input[formControlName="' + controlName + '"]') as HTMLInputElement;
    if (input) {
      input.classList.remove('control__input-reminder', 'control__input-processed');
      input.classList.add('control__input-changed');
      this.blurCausedByControlOptionButton = false;
    }
  }

  private resetCanControlOptionsDisplayed(): void {
    Object.keys(this.canControlOptionsDisplayed).forEach(key => {
      this.canControlOptionsDisplayed[key] = false;
    });
  }


  private openDialog(title: string, message: string): void {
    this.modalTitle = title;
    this.modalMessage = message;
    this.showDialog = true;
  }

  private openRequestProgressDialog(title: string): void {
    this.modalTitle = title;
    this.showRequestProgressDialog = true;
  }
}
