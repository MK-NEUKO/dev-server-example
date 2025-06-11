import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';

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
  private httpClient = inject(HttpClient);
  formArray!: FormArray;
  parentForm!: FormGroup;

  get address() {
    const destination = this.formArray.at(0) as FormGroup;
    return destination.get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS);
  }

  get destinationName() {
    const destination = this.formArray.at(0) as FormGroup;
    return destination.get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_NAME);
  }

  ngOnInit(): void {
    const rootForm = this.rootFormGroup.control;
    const parentArray = rootForm.get(this.parentArrayName()) as FormArray;
    this.parentForm = parentArray.at(0) as FormGroup;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;

    const destination = this.formArray.at(0) as FormGroup;
    const address = destination.get('address');
    console.log('Address:', address);


  }

  public updateDestination(): void {
    const destination = this.formArray.at(0) as FormGroup;
    const destinationId = destination.get('destinationId')?.value;
    const address = destination.get('address')?.value;
    const updatedDestination = {
      id: destinationId,
      address: address
    };
    this.httpClient.put(`envGateway/update-destination`, updatedDestination).subscribe({
      next: (response) => {
        console.log('Destination updated successfully:', response);
      }
    });
  }

  public resetDestination(): void {
    console.log('Resetting destination form');
  }

}
