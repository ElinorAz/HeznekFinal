import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators, FormControl } from '@angular/forms';
import { MatSnackBar, MatTableDataSource } from '@angular/material';

import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { GenderEnum } from '../../enums/gender.enum';
import { CitiesService } from '../../data/cities.service';
import { ResidenceEnum } from '../../enums/residence.enum';
import { ProfileService } from '../../services/pofile.service';
import { MilitaryServicesService } from '../../data/military-services.service';
import { HeznekProgramModel } from '../../models/user/heznek-program.model';
import { CandidateProfileModel } from '../../models/user/candidate-profile.model';
import { DegreeTypesService } from '../../data/degree-types.service';
import { UserStatusEnum } from '../../enums/user-status.enum';
import { ScholarshipService } from '../../services/scholarships.service';
import { AdminStudentProfileModel } from '../../models/admin/admin-student-profile.model';
import { ScholarshipModel } from '../../models/scholarship/scholarship.model';
import { AuthService } from '../../services/auth.service';
import { UserRoleEnum } from '../../enums/user-role.enum';
import { VolunteerHoursService } from '../../services/volunteer-hours.service';
import { VolunteerHourModel } from '../../models/volunteer-hours/volunteer-hours.model';
import { SemesterEnum } from '../../enums/semester.enum';
import { VolunteerHoursActivityTypeEnum } from '../../enums/volunteer-hours-activity-type.enum';
import { StudentScholarshipModel } from '../../models/student/student-scholarship.model';
import { StudentsService } from '../../services/students.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.scss']
})
export class EditStudentComponent implements OnInit, OnDestroy {
  mediaPath = environment.MEDIA;
  displayedColumns = ['date', 'start', 'end', 'activityType'];
  studScholrDisplayedColumns = ['actions', 'scholarship', 'total', 'currentYear', 'givenInPast'];
  volunteerHoursSA = new MatTableDataSource();
  volunteerHoursSB = new MatTableDataSource();
  volunteerHoursSummer = new MatTableDataSource();
  studentScholarships = new MatTableDataSource();
  volunteerDetails: FormGroup;
  panelOpenState = false;
  generalInfoForm:FormGroup;
  militaryServiceForm:FormGroup;
  highSchoolForm:FormGroup;
  generalForm:FormGroup;
  additionalForm:FormGroup;
  academicStudiesForm:FormGroup;
  participationForm:FormGroup;
  filesForm: FormGroup;
  bankInfoForm: FormGroup;
  scholarShipForm: FormGroup;

  scholarshipsSubscription: Subscription;
  scholarships: ScholarshipModel[] = [];
  genders: {id: number; value: string}[] = [];
  typeOfServices: {id: number; value: string}[] = [];
  residenceTypes: {id: number; value: string}[] = [];
  userStatuses: {id: number; value: string}[] = [];
  degreeTypes: {id: number; value: string}[] = [];
  volunteerHoursActivityType: {id: number; value: string}[] = [];
  semesters: {id: number; value: string}[] = [];
  profile: AdminStudentProfileModel = null;
  isSetForms: boolean = false;
  isLoading:boolean = false;
  cities: {id:string; city:string}[] = [];
  aprovalFileName: string;
  gradesFileName:string;
  aproval:File = null;
  grades:File = null;
  userId: string;
  isLoadingSS: boolean = false;
  isAddingNewStudendScholar: boolean = false;
  studentScholarShipID: number = 0;
  isAdmin: boolean = false;

  get formData()
  {
    let arr = this.generalForm.get('participationInPrograms') as FormArray;
    return arr.controls; 
  }

  constructor(
    private formBuilder: FormBuilder,
    private profileService: ProfileService,
    private snackBar: MatSnackBar,
    private citiesService: CitiesService,
    private militaryServicesService: MilitaryServicesService,
    private degreeTypesService: DegreeTypesService,
    private navCtrl: NgxNavigationWithDataComponent,
    private scholarshipService: ScholarshipService,
    private auth: AuthService,
    private volunteerHoursService: VolunteerHoursService,
    private studentsService: StudentsService
    ) {
    this.cities = citiesService.Cities;
    this.typeOfServices = militaryServicesService.MilitaryServices;
    this.degreeTypes = degreeTypesService.DegreeTypes;
    this.initEnums();
    this.buildForm();
  }

  private initEnums()
  {
    for (const i in GenderEnum) {
      if (typeof GenderEnum[i] === 'number') {
        this.genders.push({id: <any>GenderEnum[i], value: i});
      }
    }

    for (const i in ResidenceEnum) {
      if (typeof ResidenceEnum[i] === 'number') {
        this.residenceTypes.push({id: <any>ResidenceEnum[i], value: i});
      }
    }

    for (const i in UserStatusEnum) {
      if (typeof UserStatusEnum[i] === 'number') {
        this.userStatuses.push({id: <any>UserStatusEnum[i], value: i});
      }
    }

    for (const i in SemesterEnum) {
      if (typeof SemesterEnum[i] === 'number') {
        this.semesters.push({id: <any>SemesterEnum[i], value: i});
      }
    }

    for (const i in VolunteerHoursActivityTypeEnum) {
      if (typeof VolunteerHoursActivityTypeEnum[i] === 'number') {
        this.volunteerHoursActivityType.push({id: <any>VolunteerHoursActivityTypeEnum[i], value: i});
      }
    }
  }

  private buildForm() {
    this.generalInfoForm = this.formBuilder.group({
        id: [''],
        userId: ['', [Validators.required, Validators.maxLength(9), Validators.minLength(9)]],
        phone: ['', [Validators.required]],
        email: ['', [Validators.required]],
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        birthDate: [''],
        gender: ['', [Validators.required]],
        siblings: [''],
        academicParents: [''],
        city: [''],
        address: [''],
        status: ['']
    });
    this.militaryServiceForm = this.formBuilder.group({
      role: [''],
      typeOfService: [''],
      details: [''],
      easeOfService: ['']
    });
    this.highSchoolForm = this.formBuilder.group({
      name: [''],
      year: ['']
    });
    this.generalForm = this.formBuilder.group({
      participationInPrograms: this.formBuilder.array([]),
      psychometricGrade: [''],
      points: [''],
      worthyOfAdvancment: [''],
      disabilities: ['']
    });
    this.additionalForm = this.formBuilder.group({
      participationDescription: [''],
      reason: [''],
      difficulties: [''],
      status: [''],
      lifeStory: [''],
      situationDetails: [''],
      healthProblems: [''],
      financialProblems: [''],
      familyDifficulties: [''],
      hasHealthProblems: [''],
      hasFinancialProblems: [''],
      hasFamilyDifficulties: ['']
    });
    this.academicStudiesForm = this.formBuilder.group({
      academicDegree: [''],
      fieldOfStudy: [''],
      academicInstitution: [''],
      residence: [''],
      graduationYear: [''],
      beginningDegree: [''],
      tuition: [''],
      otherFundings: [''],
      fundStudies: [''],
      studyYear: [''],
      typeOfDegree: ['']
    });
    this.scholarShipForm = this.formBuilder.group({
      scholarship: [''],
      january: [0],
      february: [0],
      march: [0],
      april: [0],
      may: [0],
      june: [0],
      july: [0],
      august: [0],
      september: [0],
      october: [0],
      november: [0],
      december: [0],
      givenInPast: ['']
    });
    this.bankInfoForm = this.formBuilder.group({
      bankName: [''],
      branchNumber: [''],
      accountNumber: ['']
    });
    this.filesForm = this.formBuilder.group({
      grades: [''],
      aproval: ['']
    });
    this.volunteerDetails = this.formBuilder.group({
      hoursSpent: [],
      hours: [''],
      contribution: [''],
    });
  }

  uploadAproval(event) {
    if (event.target.files && event.target.files.length) {
      this.aprovalFileName = event.target.files[0].name;
      this.aproval = event.target.files[0];
    }
  }

  uploadGrades(event) {
    if (event.target.files && event.target.files.length) {
      this.gradesFileName = event.target.files[0].name;
      this.grades = event.target.files[0];
    }
  }

  ngOnInit() {
    this.userId = this.navCtrl.get('id');
    this.getRole();
    this.getVolunteerHours();
    this.getScholarships();
    if (this.userId) {
      this.profileService.getProfileByID(this.userId)
      .pipe(first())
      .subscribe(data => {
        this.profile = data as AdminStudentProfileModel;
        this.setFormValue(this.generalInfoForm, this.profile);
        this.setFormValue(this.militaryServiceForm, this.profile.militaryService);
        this.setFormValue(this.highSchoolForm, this.profile.highSchool);
        this.setFormValue(this.generalForm, this.profile.general);
        this.setFormValue(this.additionalForm, this.profile.candidateAdditionalData);
        this.setFormValue(this.academicStudiesForm, this.profile.academicStudies);
        this.setFormValue(this.scholarShipForm, this.profile.scholarDetails);
        this.setFormValue(this.bankInfoForm, this.profile.bankInfo);
        this.setFormValue(this.volunteerDetails, this.profile.volunteerDetails);
        this.aprovalFileName = this.profile.academicStudies.aprovalFileName;
        this.gradesFileName = this.profile.academicStudies.gradesFileName;
        this.studentScholarships.data = this.profile.scholarships;
        this.isSetForms = true;
      }, err => console.dir(err));
    }
  }

  private setFormValue(form: FormGroup, value) {
    if (value) {
      Object.keys(value).forEach(name => {
        if (form.controls[name] && form.controls[name] instanceof FormControl) {
          form.controls[name].setValue(value[name]);
          if (name === 'id') {
            form.controls[name].setValue(this.userId);
          }
        }
        else if(form.controls[name] && form.controls[name] instanceof FormArray) {
          let array = form.controls[name] as FormArray;
          value[name].forEach((item)=>{
            array.push(this.formBuilder.group(item));
          });
        }
      });
    }
  }

  getScholarships(): void {
    this.scholarshipsSubscription = this.scholarshipService.getScholarships()
    .subscribe(data => {
      this.scholarships = data;
    }, err => console.dir(err));
  }

  getRole() {
    this.auth.currentUser.subscribe((user) => {
      const role: any = !!user ? user.userRole : null;
      if (!!user && role === 'Admin') {
        //this.generalInfoForm.controls['status'].disable();
        this.isAdmin = true;
      }
    });
  }

  getVolunteerHours(): void {
    this.volunteerHoursService.getVolunteerHoursByID(this.userId)
    .subscribe((data: any) => {
      this.volunteerHoursSA.data = data.volunteerHoursA;
      this.volunteerHoursSB.data = data.volunteerHoursB;
      this.volunteerHoursSummer.data = data.volunteerHoursSummer;
      this.volunteerDetails.controls['hoursSpent'].setValue(data.hoursSpent);
    },
    err => {})
  }

  generateDownloadLink(downloadLink: string): string {
    return `${this.mediaPath}/${downloadLink}`;
  }

  addStudentScholarship() {
    this.isLoadingSS = true;
    let model = this.scholarShipForm.value as StudentScholarshipModel;
    model.profileId = this.profile.id;
    model.id = this.studentScholarShipID;
    this.studentsService.createStudentScholarship(model, model.id)
    .subscribe((data) => {
      this.isLoadingSS = false;
      this.studentScholarShipID = 0;
      this.getStudentScholarShipsByID();
      this.toggleStudensScholarState();
    },
    err => {
      this.isLoadingSS = false;
      this.studentScholarShipID = 0;
    })
  }

  getCurrentYear(): number | string {
    var date = new Date();
    return date.getFullYear();
  }

  toggleStudensScholarState(): void {
    this.isAddingNewStudendScholar = !this.isAddingNewStudendScholar;
  }

  getStudentScholarShipsByID() {
    this.studentsService.getStudentsStudentScholarshipByID(this.userId)
    .subscribe((data) => {
      this.studentScholarships.data = data;
    },
    err => {})
  }

  editStudentSchholarship(studentSchholarship) {
    this.toggleStudensScholarState();
    this.studentScholarShipID = studentSchholarship.id;
    this.setFormValue(this.scholarShipForm, studentSchholarship);
  }

  deleteStudentScholarship(id: number) {
    this.studentsService.deleteStudentScholarship(id)
    .subscribe(() => {
      this.getStudentScholarShipsByID();
    },
    err => {});
  }

  save(): void {
    this.isLoading = true;
    const model: AdminStudentProfileModel = this.generalInfoForm.value;
    model.militaryService = this.militaryServiceForm.value;
    model.highSchool = this.highSchoolForm.value;
    model.general = this.generalForm.value;
    model.candidateAdditionalData = this.additionalForm.value;
    model.academicStudies = this.academicStudiesForm.value;
    model.academicStudies.aproval = this.aproval;
    model.academicStudies.grades = this.grades;
    model.scholarDetails = this.scholarShipForm.value;
    model.bankInfo = this.bankInfoForm.value;
    model.volunteerDetails = this.volunteerDetails.value;
    model.userId = this.userId ? this.userId : this.generalInfoForm.controls['userId'].value;
    model.id = !!this.profile ? this.profile.id : 0;
    this.profileService.updateStudent(model, model.id).subscribe(() => {
      this.isLoading = false;
      this.snackBar.open("הנתונים נשמרו", "", {duration : 2000});
    }, (err) => {
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "",{duration : 2000});
    });
  }

  ngOnDestroy() {
  }
}
