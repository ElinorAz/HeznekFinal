import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material';

import { first } from 'rxjs/operators';

import { FormService } from '../../services/form.service';
import { ParentsSalaryModel } from '../../models/candidate/parents-salary.model';
import { Output, EventEmitter } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-parents-salary',
  templateUrl: './parents-salary.component.html',
  styleUrls: ['./parents-salary.component.scss']
})
export class ParentsSalaryComponent implements OnInit {
  @Input() showBtnApprove: boolean = true;
  @Input() isDisabled: boolean = false;
  @Input() userId: string = null;
  mediaPath = environment.MEDIA;
  parentsSalaryForm: FormGroup;
  parentsSalary: ParentsSalaryModel
  isLoading:boolean = false;
  salarySlipsFileName: string;
  disabilityFileName:string;
  disability2FileName:string;
  salarySlips:File = null;
  disability:File = null;
  disability2:File = null;

  constructor(
    private formBuilder: FormBuilder,
    private formService: FormService,
    private snackBar: MatSnackBar,
    ) {

  }

  private buildForm() {
    this.parentsSalaryForm = this.formBuilder.group({
        id: [''],
        motherName: [{value: '', disabled: this.isDisabled}],
        fatherName: [{value: '', disabled: this.isDisabled}],
        salarySlips: [{value: '', disabled: this.isDisabled}],
        disability: [{value: '', disabled: this.isDisabled}],
        disability2: [{value: '', disabled: this.isDisabled}],
        fatherDisability: [{value: '', disabled: this.isDisabled}],
        motherDisability: [{value: '', disabled: this.isDisabled}]
    });
  }

  uploadSalarySlips(event)
  {
    if(event.target.files && event.target.files.length && !this.isDisabled) {
      this.salarySlipsFileName = event.target.files[0].name;
      this.salarySlips = event.target.files[0];
    }
  }

  uploadDisability(event)
  {
    if(event.target.files && event.target.files.length && !this.isDisabled) {
      this.disabilityFileName = event.target.files[0].name;
      this.disability = event.target.files[0];
    }
  }

  uploadDisabilitySecond(event)
  {
    if(event.target.files && event.target.files.length && !this.isDisabled) {
      this.disability2FileName = event.target.files[0].name;
      this.disability2 = event.target.files[0];
    }
  }

  ngOnInit() {
    this.buildForm();
    if (this.userId) {
      this.formService.getFormById(this.userId)
      .pipe(first())
      .subscribe(data => {
        this.parentsSalary = data.parentsSalary;      
        this.salarySlipsFileName = data.parentsSalary.salarySlipsFileName;
        this.disabilityFileName = data.parentsSalary.disabilityFileName;
        this.disability2FileName = data.parentsSalary.disability2FileName;
        this.setFormValue(this.parentsSalaryForm, data.parentsSalary);
      }, err => console.dir(err));
    }
    else {
      this.formService.getForm()
      .pipe(first())
      .subscribe(data => {
        this.parentsSalary = data.parentsSalary;      
        this.salarySlipsFileName = data.parentsSalary.salarySlipsFileName;
        this.disabilityFileName = data.parentsSalary.disabilityFileName;
        this.disability2FileName = data.parentsSalary.disability2FileName;
        this.setFormValue(this.parentsSalaryForm, data.parentsSalary);
      }, err => console.dir(err));
    }
  }

  generateDownloadLink(downloadLink: string): string {
    return `${this.mediaPath}/${downloadLink}`;
  }

  private setFormValue(form: FormGroup, value) {
    Object.keys(value).forEach(name => {
      if (form.controls[name] && form.controls[name] instanceof FormControl) {
        form.controls[name].setValue(value[name]);
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
    let model = this.parentsSalaryForm.value;
    model.salarySlips = this.salarySlips;
    model.disability = this.disability;
    model.disability2 = this.disability2;
    this.formService.updateParentsSalary(model).subscribe(()=>{
      this.isLoading = false;
      this.snackBar.open("הנתונים נשמרו", "",{duration : 2000});
    },(err)=>{
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "",{duration : 2000});
    });
  }

}
