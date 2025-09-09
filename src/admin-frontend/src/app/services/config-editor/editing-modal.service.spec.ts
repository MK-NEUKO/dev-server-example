import { TestBed } from '@angular/core/testing';

import { EditingModalService } from './editing-modal.service';

describe('EditingModalService', () => {
  let service: EditingModalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditingModalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
