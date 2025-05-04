import { TestBed } from '@angular/core/testing';

import { GatewayDataService } from './gateway-data.service';

describe('GatewayDataService', () => {
  let service: GatewayDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GatewayDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
