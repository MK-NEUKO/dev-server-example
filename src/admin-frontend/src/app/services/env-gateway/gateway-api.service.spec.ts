import { TestBed } from '@angular/core/testing';

import { GatewayApiService } from './gateway-api.service';

describe('GatewayConfigService', () => {
  let service: GatewayApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GatewayApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
