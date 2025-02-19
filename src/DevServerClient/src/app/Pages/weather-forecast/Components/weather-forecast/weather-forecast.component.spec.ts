import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForecastNavTabComponent } from './weather-forecast.component';

describe('ForecastNavTabComponent', () => {
  let component: ForecastNavTabComponent;
  let fixture: ComponentFixture<ForecastNavTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForecastNavTabComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ForecastNavTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
