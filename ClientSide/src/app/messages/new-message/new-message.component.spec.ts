import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileCandidateComponent } from './profile-candidate.component';

describe('ProfileCandidateComponent', () => {
  let component: ProfileCandidateComponent;
  let fixture: ComponentFixture<ProfileCandidateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileCandidateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileCandidateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
