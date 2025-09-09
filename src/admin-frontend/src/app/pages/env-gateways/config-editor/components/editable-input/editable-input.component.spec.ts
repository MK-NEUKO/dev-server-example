import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditableInputComponent } from './editable-input.component';

describe('EditableInputComponent', () => {
  let component: EditableInputComponent;
  let fixture: ComponentFixture<EditableInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditableInputComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditableInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
