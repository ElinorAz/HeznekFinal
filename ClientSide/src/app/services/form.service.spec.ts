import { TestBed } from '@angular/core/testing';

import { FormService } from './form.service';

describe('FormServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormService = TestBed.get(FormService);
    expect(service).toBeTruthy();
  });
});
