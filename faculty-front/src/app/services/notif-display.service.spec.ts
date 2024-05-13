import { TestBed } from '@angular/core/testing';

import { NotifDisplayService } from './notif-display.service';

describe('NotifDisplayService', () => {
  let service: NotifDisplayService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NotifDisplayService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
