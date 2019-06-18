import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators, FormControl } from '@angular/forms';
import { GenderEnum } from 'src/app/enums/gender.enum';
import { ProfileService } from 'src/app/services/pofile.service';
import { CandidateProfileModel } from 'src/app/models/user/candidate-profile.model';
import { first } from 'rxjs/operators';
import { ResidenceEnum } from 'src/app/enums/residence.enum';
import { MatSnackBar } from '@angular/material';
import { HeznekProgramModel } from 'src/app/models/user/heznek-program.model';
import { CitiesService } from 'src/app/data/cities.service';
import { MilitaryServicesService } from 'src/app/data/military-services.service';
import { DegreeTypesService } from 'src/app/data/degree-types.service';
import { ResidenceTypesService } from 'src/app/data/residence-types.service';

@Component({
  selector: 'app-profile-candidate',
  templateUrl: './profile-candidate.component.html',
  styleUrls: ['./profile-candidate.component.scss']
})
export class ProfileCandidateComponent implements OnInit {

  panelOpenState = false;
  generalInfoForm:FormGroup;
  militaryServiceForm:FormGroup;
  highSchoolForm:FormGroup;
  generalForm:FormGroup;
  additionalForm:FormGroup;
  academicStudiesForm:FormGroup;
  participationForm:FormGroup;
  filesForm: FormGroup;

  genders: {id: number; value: string}[] = [];
  typeOfServices: {id: number; value: string}[] = [];
  residenceTypes: {id: number; value: string}[] = [];
  degreeTypes: {id: number; value: string}[] = [];
  profile:CandidateProfileModel = null;
  isLoading:boolean = false;
  cities: {id:string; city:string}[] = [];
  aprovalFileName: string;
  gradesFileName:string;
  aproval:File = null;
  grades:File = null;
  userId: string = null;

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
    private residenceTypesService: ResidenceTypesService
    ) {
    this.cities = citiesService.Cities;
    this.typeOfServices = militaryServicesService.MilitaryServices;
    this.degreeTypes = degreeTypesService.DegreeTypes;
    this.residenceTypes = residenceTypesService.ResidenceTypes;
    this.initEnums();
    this.buildForm();
  }

  private initEnums()
  {
    for(var i in GenderEnum) {
      if (typeof GenderEnum[i] === 'number') {
        this.genders.push({id: <any>GenderEnum[i], value: i});
      }
    }
  }

  private buildForm() {
    this.generalInfoForm = this.formBuilder.group({
        id: ['', [Validators.required,Validators.maxLength(9),Validators.minLength(9)]],
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
        status: [{value: '',disabled: true}]        
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
  }

  uploadAproval(event)
  {
    if(event.target.files && event.target.files.length) {
      this.aprovalFileName = event.target.files[0].name;
      this.aproval = event.target.files[0];
    }
  }

  uploadGrades(event)
  {
    if(event.target.files && event.target.files.length) {
      this.gradesFileName = event.target.files[0].name;
      this.grades = event.target.files[0];
    }
  }

  

  ngOnInit() {
    this.profileService.getProfile()
    .pipe(first())
    .subscribe(data => {
      this.profile = data as CandidateProfileModel; 
      this.userId = this.profile.userId;
      this.setFormValue(this.generalInfoForm, this.profile);
      this.setFormValue(this.militaryServiceForm, this.profile.militaryService);
      this.setFormValue(this.highSchoolForm, this.profile.highSchool);
      this.setFormValue(this.generalForm, this.profile.general);
      this.setFormValue(this.additionalForm, this.profile.candidateAdditionalData);
      this.setFormValue(this.academicStudiesForm, this.profile.academicStudies);
      this.aprovalFileName = this.profile.academicStudies.aprovalFileName;
      this.gradesFileName = this.profile.academicStudies.gradesFileName;
    }, err => console.dir(err));
  }

  private setFormValue(form: FormGroup, value) {
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

  save()
  {
    this.isLoading = true;
    let model: CandidateProfileModel = this.generalInfoForm.value;
    model.militaryService = this.militaryServiceForm.value;
    model.highSchool = this.highSchoolForm.value;
    model.general = this.generalForm.value;
    model.candidateAdditionalData = this.additionalForm.value;
    model.academicStudies = this.academicStudiesForm.value;
    model.academicStudies.aproval = this.aproval;
    model.academicStudies.grades = this.grades;
    this.profileService.save(model).subscribe(()=>{
      this.isLoading = false;
      this.snackBar.open("הנתונים נשמרו", "",{duration : 2000});
    },(err)=>{
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "",{duration : 2000});
    });
  }

}
