import { Component, inject, OnInit, Input } from '@angular/core';
import { Form, FormArray, FormBuilder, FormControl, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-routes-editor',
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './routes-editor.component.html',
  styleUrls: [
    './routes-editor.component.css',
    '../config-editor.component.css'
  ]
})
export class RoutesComponent implements OnInit {

  @Input() formArrayName!: string;
  private rootFormGroup = inject(FormGroupDirective);
  formArray!: FormArray;
  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.rootFormGroup.control;
    this.formArray = this.form.get(this.formArrayName) as FormArray;
  };
}
