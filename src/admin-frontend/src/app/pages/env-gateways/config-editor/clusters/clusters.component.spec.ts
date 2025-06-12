import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClustersComponent } from './clusters.component';

describe('ClustersEditorComponent', () => {
  let component: ClustersComponent;
  let fixture: ComponentFixture<ClustersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClustersComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ClustersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
