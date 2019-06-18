import { TestBed } from '@angular/core/testing';

import { DegreeTypesService } from './degree-types.service';

describe('DegreeTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DegreeTypesService = TestBed.get(DegreeTypesService);
    expect(service).toBeTruthy();
  });
});
