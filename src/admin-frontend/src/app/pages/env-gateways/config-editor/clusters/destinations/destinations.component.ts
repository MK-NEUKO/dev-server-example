import { Component, inject, OnInit, input, signal } from '@angular/core';
import { AbstractControl, FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { DestinationService } from '../../../../../services/env-gateway/destination/destination.service';
import { RequestDialogService } from '../../../../../services/dialog-service/request-dialog.service';
import { RequestResponse } from '../../../../../services/env-gateway/RequestResponse/request-response';
import { EditableInputComponent } from '../../components/editable-input/editable-input.component';

@Component({
  selector: 'config-editor-destinations',
  imports: [
    ReactiveFormsModule,
    EditableInputComponent
  ],
  templateUrl: './destinations.component.html',
  styleUrls: [
    './destinations.component.css',
    '../../config-editor.component.css'
  ]
})
export class DestinationsComponent implements OnInit {

  public readonly CONTROL_NAMES = CONFIG_EDITOR_CONTROL_NAMES;
  public readonly parent = input.required<AbstractControl<any, any> | null>();
  public readonly destinationsArrayName = input.required<string>();
  public parentFormGroup!: FormGroup;
  public destinations!: FormArray;

  private destinationService = inject(DestinationService);
  private requestDialogService = inject(RequestDialogService);

  public readonly labelDestinationName: string = 'Destination Name: ';
  public readonly labelDestinationAddress: string = 'Address: ';

  ngOnInit(): void {
    this.parentFormGroup = this.parent() as FormGroup;
    this.destinations = this.parent()?.get(this.destinationsArrayName()) as FormArray;
  }


  public async changeDestinationProperty(index: number, controlName: string): Promise<void> {
    const requestDialog = this.requestDialogService.open(`Destination ${controlName} will be changed`);

    const request = {
      clusterId: this.parentFormGroup.get('clusterId')?.value,
      destinationId: this.destinations.at(index).get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ID)?.value,
      [controlName]: this.destinations.at(index).get(controlName)?.value
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
  }

}
