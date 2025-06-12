import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { DestinationService } from '../../../../../services/env-gateway/destination/destination.service';

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
  formArray!: FormArray;
  parentForm!: FormGroup;
  destination!: FormGroup;

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
    this.destination = this.formArray.at(0) as FormGroup;
  }

  public canUpdate(): boolean {
    const address = this.destination.get(CONFIG_EDITOR_CONTROL_NAMES.DESTINATION_ADDRESS);
    const result = !(address?.valid && (address?.dirty || address?.touched));
    return result;

  }

  public updateDestination(): void {
    const destinationId = this.destination.get('destinationId')?.value;
    const address = this.destination.get('address')?.value;
    const request = {
      id: destinationId,
      address: address
    };
    this.destinationService.SaveChanges(request);
  }

  public resetDestination(): void {
    this.address?.reset();
    this.address?.markAsTouched();
    this.address?.markAsDirty();
  }

}
