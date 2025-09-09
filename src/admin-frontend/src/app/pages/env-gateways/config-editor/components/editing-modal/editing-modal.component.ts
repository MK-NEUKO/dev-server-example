import { Component, ElementRef, HostListener, inject, input, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../../shared/config-editor-control-names';
import { EditingToolsComponent } from '../editing-tools/editing-tools.component';
import { NgClass } from '@angular/common';
import { ConfigEditorEventService } from '../../../../../services/config-editor/config-editor-event.service';

@Component({
  selector: 'app-name-input',
  imports: [
    ReactiveFormsModule,
    EditingToolsComponent,
    NgClass,
  ],
  templateUrl: './editing-modal.component.html',
  styleUrls: [
    './editing-modal.component.css',
    '../../config-editor.component.css'
  ]
})
export class EditingModalComponent implements OnInit {


  private elementRef = inject(ElementRef);
  @ViewChild('inputElement') inputElement!: ElementRef<HTMLElement>;

  private configEditorEventService = inject(ConfigEditorEventService);



  public isEditingToolsVisible = false;
  public isShaking = false;


  public isModalVisible = false;

  ngOnInit(): void {
    this.configEditorEventService.mouseDown$.subscribe(event => {
      this.handleMouseDownOutside(event);
    });

  }

  @HostListener('blur', ['$event']) onComponentBlur(event: FocusEvent) {

  }


  public onInputControlFocus() {
    this.isEditingToolsVisible = true;

  }

  public onInputControlBlur() {

  }

  private handleMouseDownOutside(event: MouseEvent) {
    console.log('Input Element:', this.inputElement.nativeElement);
    console.log('Active:', document.activeElement);


    const hostComponentHasFocus = this.elementRef.nativeElement.contains(document.activeElement);
    const mouseDownOutsideOfHostComponent = !this.elementRef.nativeElement.contains(event.target);
    if (mouseDownOutsideOfHostComponent && hostComponentHasFocus) {
      console.log('Component in progress, ignoring mouse down outside.');
      this.triggerShakeAnimation();
      setTimeout(() => {
        this.inputElement.nativeElement.focus();
      }, 0);
      console.log('Active element after refocus:', document.activeElement);

    }
  }

  private triggerShakeAnimation() {
    this.isShaking = true;
    setTimeout(() => {
      this.isShaking = false;
    }, 500);
  }

  public onEditingToolsCancel() {
    this.isEditingToolsVisible = false;
  }
}