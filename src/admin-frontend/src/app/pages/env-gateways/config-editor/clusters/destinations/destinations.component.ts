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

  ngOnInit(): void {
    const rootForm = this.rootFormGroup.control;
    const parentArray = rootForm.get(this.parentArrayName()) as FormArray;
    this.parentForm = parentArray.at(0) as FormGroup;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;
  }

  public canUpdate(index: number): boolean {
    const address = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS);
    const result = !(address?.valid && (address?.dirty || address?.touched));
    return result;
  }

  public async updateDestination(index: number): Promise<void> {
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

    this.openDialog(
      'Destination update response',
      `HttpClient - response: ${message}`
    );
  }

  public resetDestination(index: number): void {
    const address = this.formArray.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS);
    address?.reset();
    address?.markAsTouched();
    address?.markAsDirty();
  }

  openDialog(title: string, message: string): void {
    this.modalTitle = title;
    this.modalMessage = message;
    this.showDialog = true;
  }
}
