import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators, FormControl } from '@angular/forms';
import { MatSnackBar, MatTableDataSource } from '@angular/material';

import { first } from 'rxjs/operators';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { ScholarshipStatusEnum } from '../../enums/scholarship-status.enum';
import { UserRoleEnum } from '../../enums/user-role.enum';
import { UserStatusEnum } from '../../enums/user-status.enum';
import { ScholarshipService } from '../../services/scholarships.service';
import { ScholarshipModel } from '../../models/scholarship/scholarship.model';

@Component({
  selector: 'app-new-scholarships',
  templateUrl: './new-scholarships.component.html',
  styleUrls: ['./new-scholarships.component.scss']
})
export class NewScholarshipsComponent implements OnInit {
  scholarshipForm: FormGroup;
  scholarship: ScholarshipModel = null;
  scholarshipId: number = null;
  isLoading:boolean = false;
  dataSource = new MatTableDataSource();
  displayedColumns = ['id', 'firstName', 'lastName', 'faculty', 'domain', 'university', 'phone', 'email', 'role', 'status'];
  statuses: {id: number; value: string}[] = [];
  userStatuses: {id: number; value: string}[] = [];
  userRoles: {id: number; value: string}[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private scholarshipService: ScholarshipService,
    private snackBar: MatSnackBar,
    private navCtrl: NgxNavigationWithDataComponent,
    ) {

    this.buildForm();
    this.initEnums();
  }

  ngOnInit() {
    this.scholarshipId = this.navCtrl.get('id');
    if (this.scholarshipId) {
      this.scholarshipService.getScholarship(this.scholarshipId)
      .pipe(first())
      .subscribe(data => {
        this.scholarship = data as ScholarshipModel; 
        this.dataSource.data = data.students;
        this.setFormValue(this.scholarship);
      }, err => console.dir(err));
    }
  }

  initEnums() {
    for(let i in ScholarshipStatusEnum) {
      if (typeof ScholarshipStatusEnum[i] === 'number') {
        this.statuses.push({id: <any>ScholarshipStatusEnum[i], value: i});
      }
    }
    for (const i in UserStatusEnum) {
      if (typeof UserStatusEnum[i] === 'number') {
        this.userStatuses.push({id: <any>UserStatusEnum[i], value: i});
      }
    }
    for (const i in UserRoleEnum) {
      if (typeof UserRoleEnum[i] === 'number') {
        this.userRoles.push({id: <any>UserRoleEnum[i], value: i});
      }
    }
  }

  private buildForm() {
    this.scholarshipForm = this.formBuilder.group({
        id: [0],
        name: ['', [Validators.required]],
        admission: ['', [Validators.required]],
        status: ['', [Validators.required]],
        budget: ['', [Validators.required]]
    });
  }

  private setFormValue(value: ScholarshipModel) {
    Object.keys(value).forEach(name => {
      if (this.scholarshipForm.controls[name]) {
        this.scholarshipForm.controls[name].setValue(value[name]);
      }
    });
  }

  private redirectToScholarships() {
    this.navCtrl.navigate(['scholarships']);
  }

  save(): void {
    this.isLoading = true;
    let model: ScholarshipModel = this.scholarshipForm.value;
    model.id = this.scholarshipId || 0;
    this.scholarshipService.create(model, this.scholarshipId).subscribe(()=>{
      this.isLoading = false;
      this.snackBar.open("הנתונים נשמרו", "",{duration : 2000});
      this.redirectToScholarships();
    },(err)=>{
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "",{duration : 2000});
    });
  }

}
