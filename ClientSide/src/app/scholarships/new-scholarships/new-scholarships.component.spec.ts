import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NewScholarshipsComponent } from '../../scholarships/new-scholarships/new-scholarships.component';

describe('ProfileCandidateComponent', () => {
  let component: NewScholarshipsComponent;
  let fixture: ComponentFixture<NewScholarshipsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewScholarshipsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewScholarshipsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
