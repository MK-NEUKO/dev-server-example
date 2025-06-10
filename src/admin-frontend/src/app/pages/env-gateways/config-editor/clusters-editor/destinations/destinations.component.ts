import { Component, inject, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';

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

  @Input() formArrayName!: string;
  @Input() parentArrayName!: string;
  private rootFormGroup = inject(FormGroupDirective);
  formArray!: FormArray;
  parentForm!: FormGroup;

  ngOnInit(): void {
    const rootForm = this.rootFormGroup.control;
    const clustersArray = rootForm.get(this.parentArrayName) as FormArray;
    this.parentForm = clustersArray.at(0) as FormGroup;
    this.formArray = this.parentForm.get(this.formArrayName) as FormArray;
  }
}
