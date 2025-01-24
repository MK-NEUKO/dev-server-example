import { TestBed } from '@angular/core/testing';

import { WeatherForecastDataService } from './weather-forecast-data.service';

describe('WeatherForecastDataService', () => {
  let service: WeatherForecastDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WeatherForecastDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
