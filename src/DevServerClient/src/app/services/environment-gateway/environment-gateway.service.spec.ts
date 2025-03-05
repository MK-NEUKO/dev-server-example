import { TestBed } from '@angular/core/testing';

import { EnvironmentGatewayService } from './environment-gateway.service';

describe('EnvironmentService', () => {
  let service: EnvironmentGatewayService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnvironmentGatewayService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
