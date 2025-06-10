import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { DestinationsComponent } from "./destinations/destinations.component";


@Component({
  selector: 'app-clusters',
  imports: [
    ReactiveFormsModule,
    DestinationsComponent
  ],
  templateUrl: './clusters.component.html',
  styleUrls: [
    './clusters.component.css',
    '../config-editor.component.css'
  ]
})
export class ClustersComponent implements OnInit {

  readonly formArrayName = input.required<string>();
  private rootFormGroup = inject(FormGroupDirective);
  formArray!: FormArray;
  parentForm!: FormGroup;

  ngOnInit(): void {
    this.parentForm = this.rootFormGroup.control;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;
  };
}
