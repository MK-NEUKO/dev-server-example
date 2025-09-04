import { Component, inject, OnInit, input, signal } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { DestinationService } from '../../../../../services/env-gateway/destination/destination.service';
import { RequestDialogService } from '../../../../../services/dialog-service/request-dialog.service';
import { RequestResponse } from '../../../../../services/env-gateway/RequestResponse/request-response';

@Component({
  selector: 'app-destinations',
  imports: [
    ReactiveFormsModule,
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
  private requestDialogService = inject(RequestDialogService);
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
      control.classList.remove('control__input-success', 'control__input-error');
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
          this.blurCausedByControlOptionButton = false;
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

  public reset(index: number, controlName: string): void {
    const control = this.formArray.at(index).get(controlName);
    control?.reset();
    control?.markAsTouched();
    control?.markAsDirty();
    this.blurCausedByControlOptionButton = false;
  }

  public async changeDestinationProperty(index: number, controlName: string): Promise<void> {
    const requestDialog = this.requestDialogService.open(`Destination ${controlName} will be changed`);

    const request = {
      clusterId: this.parentForm.get('clusterId')?.value,
      destinationId: this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ID)?.value,
      [controlName]: this.formArray.at(index).get(controlName)?.value
    };

    let responseTitle = '';
    let requestResponse: RequestResponse = { isError: false, message: '' };

    if (controlName === CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME) {
      requestResponse = await this.destinationService.SaveDestinationNameChanges(request);
      responseTitle = 'Destination name changed response';
    } else if (controlName === CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS) {
      requestResponse = await this.destinationService.SaveDestinationAddressChanges(request);
      responseTitle = 'Destination address changed response';
    }

    if (requestDialog && requestResponse) {
      this.requestDialogService.setRequestResponse(requestResponse);
      this.requestDialogService.setTitle(responseTitle);
    }

    this.resetControlProperties(index, controlName);
    this.setControlStatusColors(index, controlName, requestResponse.isError);
    this.resetCanControlOptionsDisplayed();
    this.blurCausedByControlOptionButton = false;
  }

  private resetControlProperties(index: number, controlName: string): void {
    const control = this.formArray.at(index).get(controlName);
    control?.markAsPristine();
    control?.markAsUntouched();

  }

  private setControlStatusColors(index: number, controlName: string, isError: boolean): void {
    const input = document.querySelector('input[formControlName="' + controlName + '"]') as HTMLInputElement;
    if (input) {
      input.classList.remove('control__input-error', 'control__input-success', 'control__input-reminder');
      if (isError) {
        input.classList.add('control__input-error');
      } else {
        input.classList.add('control__input-success');
      }
    }
  }

  private resetCanControlOptionsDisplayed(): void {
    Object.keys(this.canControlOptionsDisplayed).forEach(key => {
      this.canControlOptionsDisplayed[key] = false;
    });
  }
}
