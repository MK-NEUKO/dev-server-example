import { TestBed } from '@angular/core/testing';

import { ConfigEditorEventService } from './config-editor-event.service';

describe('ConfigEditorEventService', () => {
  let service: ConfigEditorEventService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConfigEditorEventService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
