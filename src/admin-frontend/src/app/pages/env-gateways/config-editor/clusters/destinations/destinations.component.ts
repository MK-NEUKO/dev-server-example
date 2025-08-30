import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { DestinationService } from '../../../../../services/env-gateway/destination/destination.service';
import { ClustersComponent } from '../clusters.component';
import { ModalDialogComponent } from "../../../../../dialogues/modal-dialog/modal-dialog.component";

@Component({
  selector: 'app-destinations',
  imports: [
    ReactiveFormsModule,
    ModalDialogComponent
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
  public modalTitle: string = '';
  public modalMessage: string = '';
  public formArray!: FormArray;
  public parentForm!: FormGroup;
  public isControlOptionsDisplayed: boolean = false;

  ngOnInit(): void {
    const rootForm = this.rootFormGroup.control;
    const parentArray = rootForm.get(this.parentArrayName()) as FormArray;
    this.parentForm = parentArray.at(0) as FormGroup;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;

  }

  public onDestinationNameFocus(index: number): void {
    this.isControlOptionsDisplayed = true;
    console.log('Destination Name focused:', index, this.isControlOptionsDisplayed);
  }

  public onDestinationNameBlur(index: number): void {


    console.log('Destination Name blurred:', index, this.isControlOptionsDisplayed);
  }

  public canUpdateIsDisabled(index: number): boolean | undefined {
    const address = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME);
    if (address?.pristine) {
      return true;
    }
    if (address?.invalid) {
      return true;
    }

    return false;
  }

  public reset(index: number): void {
    const address = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME);
    address?.reset();
    address?.markAsTouched();
    address?.markAsDirty();
  }

  public async makeChangeRequest(index: number): Promise<void> {
    console.log('Make Change Request at index:', index);

    /*
    const clusterId = this.parentForm.get('clusterId')?.value;
    const destinationId = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ID)?.value;
    const address = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS)?.value;
    const destinationName = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME)?.value;
    const request = {
      clusterId: clusterId,
      destinationId: destinationId,
      address: address,
      destinationName: destinationName
    };

    var message = '';
    try {
      message = await this.destinationService.SaveChanges(request);

    } catch (error: any) {
      console.error('Error updating destination:', error.message);
      message = error.message || 'An error occurred while updating the destination.';
    }
    */

    const message = "This is the test request";
    this.openDialog(
      'Destination update response',
      `HttpClient - response: ${message}`
    );

    this.isControlOptionsDisplayed = false;
  }


  openDialog(title: string, message: string): void {
    this.modalTitle = title;
    this.modalMessage = message;
    this.showDialog = true;
  }
}
