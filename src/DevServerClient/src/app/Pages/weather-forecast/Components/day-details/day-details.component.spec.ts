import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForecastTabContentComponent } from './day-details.component';

describe('ForecastTabContentComponent', () => {
  let component: ForecastTabContentComponent;
  let fixture: ComponentFixture<ForecastTabContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForecastTabContentComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ForecastTabContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
