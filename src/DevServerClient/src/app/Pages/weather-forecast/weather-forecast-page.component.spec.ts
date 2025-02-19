import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherForecastPageComponent } from './weather-forecast-page.component';

describe('WeatherForecastComponent', () => {
  let component: WeatherForecastPageComponent;
  let fixture: ComponentFixture<WeatherForecastPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeatherForecastPageComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(WeatherForecastPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
