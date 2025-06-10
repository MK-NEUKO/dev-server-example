import { Component, inject, OnInit, input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-routes',
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './routes.component.html',
  styleUrls: [
    './routes.component.css',
    '../config-editor.component.css'
  ]
})
export class RoutesComponent implements OnInit {

  readonly formArrayName = input.required<string>();
  private rootFormGroup = inject(FormGroupDirective);
  formArray!: FormArray;
  parentForm!: FormGroup;

  ngOnInit(): void {
    this.parentForm = this.rootFormGroup.control;
    this.formArray = this.parentForm.get(this.formArrayName()) as FormArray;
  };
}
