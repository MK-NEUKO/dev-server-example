import { Component, output } from '@angular/core';

@Component({
  selector: 'app-editing-tools',
  imports: [],
  templateUrl: './editing-tools.component.html',
  styleUrl: './editing-tools.component.css'
})
export class EditingToolsComponent {

  public cancelEvent = output<void>();

  public onCancelClick() {
    this.cancelEvent.emit();
  }
}
