import { TestBed } from '@angular/core/testing';

import { RequestErrorHandler } from './request-error-handler.service';

describe('RequestErrorHandler', () => {
  let service: RequestErrorHandler;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RequestErrorHandler);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
