import { Component, OnInit } from '@angular/core';
import { MatSnackBar, MatTableDataSource } from '@angular/material';
import { FormBuilder, FormGroup, FormArray, Validators, FormControl } from '@angular/forms';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { first } from 'rxjs/operators';

import { GenderEnum } from '../../enums/gender.enum';
import { ResidenceEnum } from '../../enums/residence.enum';
import { ProfileService } from '../../services/pofile.service';
import { DegreeTypesService } from '../../data/degree-types.service';
import { MilitaryServicesService } from '../../data/military-services.service';
import { HeznekProgramModel } from '../../models/user/heznek-program.model';
import { CandidateProfileModel } from '../../models/user/candidate-profile.model';

import { CitiesService } from '../../data/cities.service';
import { UserStatusEnum } from '../../enums/user-status.enum';
import { environment } from '../../../environments/environment';
import { AdminCandidateProfileModel } from '../../models/admin/admin-candidate-profile.model';
import { TaskModel } from '../../models/candidate/task.model';
import { UserRoleEnum } from '../../enums/user-role.enum';
import { AuthService } from '../../services/auth.service';
import { CandidatesService } from '../../services/candidates.service';

@Component({
  selector: 'app-edit-candidate',
  templateUrl: './edit-candidate.component.html',
  styleUrls: ['./edit-candidate.component.scss']
})
export class EditCandidateComponent implements OnInit {
  mediaPath = environment.MEDIA;
  panelOpenState = false;
  generalInfoForm: FormGroup;
  militaryServiceForm: FormGroup;
  highSchoolForm: FormGroup;
  generalForm: FormGroup;
  additionalForm: FormGroup;
  academicStudiesForm: FormGroup;
  participationForm: FormGroup;
  filesForm: FormGroup;
  telephonyForm: FormGroup;
  dataSource = new MatTableDataSource();
  displayedColumns = ['task', 'updated', 'file', 'actions'];

  genders: { id: number; value: string }[] = [];
  typeOfServices: { id: number; value: string }[] = [];
  residenceTypes: { id: number; value: string }[] = [];
  userStatuses: { id: number; value: string }[] = [];
  degreeTypes: { id: number; value: string }[] = [];
  profile: AdminCandidateProfileModel = null;
  isLoading: boolean = false;
  cities: { id: string; city: string }[] = [];
  aprovalFileName: string;
  isSetForms: boolean = false;
  gradesFileName: string;
  aproval: File = null;
  grades: File = null;
  userId: string;
  isTelephony: boolean = false;
  isAdmin: boolean = false;
  isInactiveCandidate: boolean = null;
  get formData() {
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
    private auth: AuthService,
    private candidatesService: CandidatesService
  ) {
    this.cities = citiesService.Cities;
    this.typeOfServices = militaryServicesService.MilitaryServices;
    this.degreeTypes = degreeTypesService.DegreeTypes;
    this.initEnums();
    this.buildForm();
  }

  private initEnums() {
    for (const i in GenderEnum) {
      if (typeof GenderEnum[i] === 'number') {
        this.genders.push({ id: <any>GenderEnum[i], value: i });
      }
    }

    for (const i in ResidenceEnum) {
      if (typeof ResidenceEnum[i] === 'number') {
        this.residenceTypes.push({ id: <any>ResidenceEnum[i], value: i });
      }
    }

    for (const i in UserStatusEnum) {
      if (typeof UserStatusEnum[i] === 'number') {
        this.userStatuses.push({ id: <any>UserStatusEnum[i], value: i });
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
    this.filesForm = this.formBuilder.group({
      grades: [''],
      aproval: ['']
    });
    this.telephonyForm = this.formBuilder.group({
      dateBackFirst: [''],
      dateBackSecond: [''],
      dateBackThird: [''],
      remarks: [''],
      thoughts: [''],
      fundingAvailability: ['']
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
    } 1
  }

  ngOnInit() {
    this.getRole();
    let status: any = UserStatusEnum[this.navCtrl.get('status')];
    this.userId = this.navCtrl.get('id');
    this.isTelephony = this.navCtrl.get('isTelephony');
    this.isInactiveCandidate = status === 1 || status === 2 ? true : false;
    console.log(this.isTelephony, '4243')
    
    if (this.isTelephony) {
      this.profile = Object.assign(this.navCtrl.get('modelWithTelephony'));
      this.callSetForms();
    }
    else {
      if (this.userId) {
        this.profileService.getProfileByID(this.userId)
          .pipe(first())
          .subscribe(data => {
            this.profile = data as AdminCandidateProfileModel;
            this.callSetForms();
            this.isSetForms = true;
          }, err => console.dir(err));
      }
    }
  }

  callSetForms(): void {
    this.setFormValue(this.generalInfoForm, this.profile);
    this.setFormValue(this.militaryServiceForm, this.profile.militaryService);
    this.setFormValue(this.highSchoolForm, this.profile.highSchool);
    this.setFormValue(this.additionalForm, this.profile.candidateAdditionalData);
    if (!this.isTelephony) {
      this.setFormValue(this.generalForm, this.profile.general);
      this.setFormValue(this.academicStudiesForm, this.profile.academicStudies);
      this.aprovalFileName = this.profile.academicStudies.aprovalFileName;
      this.gradesFileName = this.profile.academicStudies.gradesFileName;
      this.dataSource.data = this.filterAttachedTasks(this.profile.candidateForm.tasks);
    }

    if(this.isInactiveCandidate || this.isTelephony) {
      this.setFormValue(this.telephonyForm, this.profile.telephony);
    }
  }

  private setFormValue(form: FormGroup, value) {
    if (value) {
      Object.keys(value).forEach(name => {
        if (form.controls[name] && form.controls[name] instanceof FormControl) {
          form.controls[name].setValue(value[name]);
          if (name === 'userId') {
            form.controls[name].setValue(this.userId);
          }
        }
        else if (form.controls[name] && form.controls[name] instanceof FormArray) {
          const array = form.controls[name] as FormArray;
          value[name].forEach((item) => {
            array.push(this.formBuilder.group(item));
          });
        }
      });
    }
  }

  private filterAttachedTasks(tasks: TaskModel[]): TaskModel[] {
    return tasks.filter((task: TaskModel) => {
      return task.fileName;
    });
  }

  private getRole() {
    this.auth.currentUser.subscribe((user) => {
      const role: any = !!user ? user.userRole : null;
      if (!!user && role === 'Admin') {
        //this.generalInfoForm.controls['status'].disable();
        this.isAdmin = true;
      }
    });
  }

  generateDownloadLink(downloadLink: string): string {
    return `${this.mediaPath}/${downloadLink}`;
  }


  save() {
    this.isLoading = true;
    let model: AdminCandidateProfileModel = this.generalInfoForm.value;
    model.militaryService = this.militaryServiceForm.value;
    model.highSchool = this.highSchoolForm.value;
    model.candidateAdditionalData = this.additionalForm.value;
    
    if (this.isInactiveCandidate || this.isTelephony) {
      model.telephony = this.telephonyForm.value;
    }
    if (!this.isTelephony) {
      model.academicStudies = this.academicStudiesForm.value;
      model.academicStudies.aproval = this.aproval;
      model.academicStudies.grades = this.grades;
      model.general = this.generalForm.value;
    }

    model.userId = this.userId ? this.userId : this.generalInfoForm.controls['userId'].value;

    if (this.isTelephony) {
      this.candidatesService.updateTelephony(model).subscribe((data) => {
        this.isLoading = false;
      },
        err => {
          this.isLoading = false;
        })
    }
    else {
      this.profileService.updateCandidate(model, model.id).subscribe(() => {
        this.isLoading = false;
        this.snackBar.open("הנתונים נשמרו", "", { duration: 2000 });
      }, (err) => {
        this.isLoading = false;
        this.snackBar.open("הנתונים נשמרו", "", { duration: 2000 });
      });
    }
  }

}
