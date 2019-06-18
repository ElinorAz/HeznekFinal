import { TestBed } from '@angular/core/testing';

import { MilitaryServicesService } from './military-services.service';

describe('MilitaryServicesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MilitaryServicesService = TestBed.get(MilitaryServicesService);
    expect(service).toBeTruthy();
  });
});
