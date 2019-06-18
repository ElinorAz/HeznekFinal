import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar, MatTableDataSource } from '@angular/material';
import { FormBuilder, FormGroup,FormArray, Validators, FormControl } from '@angular/forms';

import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { GenderEnum } from 'src/app/enums/gender.enum';
import { CitiesService } from 'src/app/data/cities.service';
import { ResidenceEnum } from 'src/app/enums/residence.enum';
import { ProfileService } from 'src/app/services/pofile.service';
import { MilitaryServicesService } from 'src/app/data/military-services.service';
import { HeznekProgramModel } from 'src/app/models/user/heznek-program.model';
import { CandidateProfileModel } from 'src/app/models/user/candidate-profile.model';

import { DegreeTypesService } from '../../data/degree-types.service';
import { SemesterEnum } from '../../enums/semester.enum';
import { UserStatusEnum } from '../../enums/user-status.enum';
import { VolunteerHoursActivityTypeEnum } from '../../enums/volunteer-hours-activity-type.enum';
import { StudentsService } from '../../services/students.service';
import { VolunteerHoursService } from '../../services/volunteer-hours.service';
import { AdminStudentProfileModel } from '../../models/admin/admin-student-profile.model';
import { StudentScholarshipModel } from '../../models/student/student-scholarship.model';
import { VolunteerHourModel } from '../../models/volunteer-hours/volunteer-hours.model';

@Component({
  selector: 'app-profile-student',
  templateUrl: './profile-student.component.html',
  styleUrls: ['./profile-student.component.scss']
})
export class ProfileStudentComponent implements OnInit, OnDestroy {
  displayedColumns = ['date', 'start', 'end', 'activityType'];
  studScholrDisplayedColumns = ['scholarship', 'total', 'currentYear', 'january', 'february','march','april','may','june','july','august','september','october','november','december', 'givenInPast'];
  studentScholarships = new MatTableDataSource();
  volunteerHoursSA = new MatTableDataSource();
  volunteerHoursSB = new MatTableDataSource();
  volunteerHoursSummer = new MatTableDataSource();
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
  volunteerHoursForm: FormGroup;
  volunteerDetails: FormGroup;
  genders: {id: number; value: string}[] = [];
  typeOfServices: {id: number; value: string}[] = [];
  residenceTypes: {id: number; value: string}[] = [];
  userStatuses: {id: number; value: string}[] = [];
  degreeTypes: {id: number; value: string}[] = [];
  volunteerHoursActivityType: {id: number; value: string}[] = [];
  semesters: {id: number; value: string}[] = [];
  profile: AdminStudentProfileModel = null;
  isLoading:boolean = false;
  isLoadingVH: boolean = false;
  cities: {id:string; city:string}[] = [];
  aprovalFileName: string;
  gradesFileName:string;
  aproval:File = null;
  grades:File = null;
  userId: string;
  isAddingNewVH: boolean = false;

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

    this.bankInfoForm = this.formBuilder.group({
      bankName: [''],
      branchNumber: [''],
      accountNumber: ['']
    });
    this.filesForm = this.formBuilder.group({
      grades: [''],
      aproval: ['']
    });
    this.volunteerHoursForm = this.formBuilder.group({
      date: [''],
      start: [''],
      end: [''],
      activityType: [''],
      semester: ['']
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
    this.getVolunteerHours();
    this.profileService.getProfile()
    .pipe(first())
    .subscribe(data => {
      this.profile = data as AdminStudentProfileModel;
      this.userId = this.profile.userId;
      this.setFormValue(this.generalInfoForm, this.profile);
      this.setFormValue(this.militaryServiceForm, this.profile.militaryService);
      this.setFormValue(this.highSchoolForm, this.profile.highSchool);
      this.setFormValue(this.generalForm, this.profile.general);
      this.setFormValue(this.additionalForm, this.profile.candidateAdditionalData);
      this.setFormValue(this.academicStudiesForm, this.profile.academicStudies);
      this.setFormValue(this.bankInfoForm, this.profile.bankInfo);
      this.setFormValue(this.volunteerDetails, this.profile.volunteerDetails);
      this.aprovalFileName = this.profile.academicStudies.aprovalFileName;
      this.gradesFileName = this.profile.academicStudies.gradesFileName;
      this.studentScholarships.data = this.profile.scholarships;
    }, err => console.dir(err));
  }

  private setFormValue(form: FormGroup, value) {
    if (value) {
      Object.keys(value).forEach(name => {
        if (form.controls[name] && form.controls[name] instanceof FormControl) {
          form.controls[name].setValue(value[name]);
          if (name === 'id') {
            form.controls[name].setValue(this.profile.userId);
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

  addVolunteerHours() {
    this.isLoadingVH = true;
    const model = this.volunteerHoursForm.value as VolunteerHourModel;
    this.volunteerHoursService.create(model).subscribe(() => {
      this.getVolunteerHours();
      this.toggleVolunteerHoursState();
      this.isLoadingVH = false;
    },
    err => {
      this.isLoadingVH = false;
    })
  }

  toggleVolunteerHoursState(): void {
    this.isAddingNewVH = !this.isAddingNewVH;
  }

  getVolunteerHours(): void {
    this.volunteerHoursService.getVolunteerHours()
    .subscribe((data: any) => {
      this.volunteerHoursSA.data = data.volunteerHoursA;
      this.volunteerHoursSB.data = data.volunteerHoursB;
      this.volunteerHoursSummer.data = data.volunteerHoursSummer;
      this.volunteerDetails.controls['hoursSpent'].setValue(data.hoursSpent);
    },
    err => {})
  }

  getCurrentYear(): number | string {
    var date = new Date();
    return date.getFullYear();
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
    model.bankInfo = this.bankInfoForm.value;
    model.volunteerDetails = this.volunteerDetails.value
    this.profileService.save(model).subscribe(() => {
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
