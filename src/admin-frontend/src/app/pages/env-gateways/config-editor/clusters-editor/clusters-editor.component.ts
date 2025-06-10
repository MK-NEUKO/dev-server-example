import { Component, inject, OnInit, Input } from '@angular/core';
import { FormArray, FormGroup, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { DestinationsComponent } from "./destinations/destinations.component";


@Component({
  selector: 'app-clusters-editor',
  imports: [
    ReactiveFormsModule,
    DestinationsComponent
  ],
  templateUrl: './clusters-editor.component.html',
  styleUrls: [
    './clusters-editor.component.css',
    '../config-editor.component.css'
  ]
})
export class ClustersEditorComponent implements OnInit {

  @Input() formArrayName!: string;
  private rootFormGroup = inject(FormGroupDirective);
  formArray!: FormArray;
  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.rootFormGroup.control;
    this.formArray = this.form.get(this.formArrayName) as FormArray;
  };
}
