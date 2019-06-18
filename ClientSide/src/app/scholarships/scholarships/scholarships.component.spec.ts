import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ScholarshipsComponent } from '../../scholarships/scholarships/scholarships.component';

describe('ProfileCandidateComponent', () => {
  let component: ScholarshipsComponent;
  let fixture: ComponentFixture<ScholarshipsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScholarshipsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScholarshipsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
