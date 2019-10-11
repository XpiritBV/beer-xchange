import { TestBed } from '@angular/core/testing';

import { FridgeService } from './fridge.service';

describe('FridgeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FridgeService = TestBed.get(FridgeService);
    expect(service).toBeTruthy();
  });
});
