import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClustersEditorComponent } from './clusters-editor.component';

describe('ClustersEditorComponent', () => {
  let component: ClustersEditorComponent;
  let fixture: ComponentFixture<ClustersEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClustersEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClustersEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
