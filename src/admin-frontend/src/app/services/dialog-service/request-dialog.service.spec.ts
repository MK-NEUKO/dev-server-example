import { TestBed } from '@angular/core/testing';

import { RequestDialogService as RequestDialogService } from './request-dialog.service';

describe('RequestDialogService', () => {
  let service: RequestDialogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RequestDialogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
