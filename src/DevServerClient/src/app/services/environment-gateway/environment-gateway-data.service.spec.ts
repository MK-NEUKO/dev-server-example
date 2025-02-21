import { TestBed } from '@angular/core/testing';

import { EnvironmentGatewayDataService } from './environment-gateway-data.service';

describe('EnvironmentGatewayDataService', () => {
  let service: EnvironmentGatewayDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnvironmentGatewayDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
