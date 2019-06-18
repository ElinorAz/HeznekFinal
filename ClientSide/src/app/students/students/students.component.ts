import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

import { Subscription } from 'rxjs';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { CitiesService } from '../../data/cities.service';
import { GenderEnum } from '../../enums/gender.enum';
import { UserRoleEnum } from '../../enums/user-role.enum';
import { UserStatusEnum } from '../../enums/user-status.enum';
import { AuthService } from '../../services/auth.service';
import { CandidatesService } from '../../services/candidates.service';
import { ProfileService } from '../../services/pofile.service';
import { StudentsService } from '../../services/students.service';
import { CandidateProfileModel } from '../../models/user/candidate-profile.model';
import { UserPermissionsModel } from '../../models/user/user-permissions.model';
import { StudentStatusEnum } from '../../enums/student-status.enum';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.scss']
})
export class StudentsComponent implements OnInit, OnDestroy {
  dataSource = new MatTableDataSource();
  isLoading: boolean = false;
  filtersForm: FormGroup;
  displayedColumns = [ 'id', 'firstName', 'lastName', 'status', 'email', 'phone', 'university', 'domain', 'faculty', 'actions'];
  studentsSubscription: Subscription;
  isAdmin: boolean = false;
  userStatuses: {id: number; value: string}[] = [];
  cities: { id: string; city: string }[] = [];
  genders: { id: number; value: string }[] = [];

  constructor(
    private studentsService: StudentsService,
    private auth: AuthService,
    private navCtrl: NgxNavigationWithDataComponent,
    private profileService: ProfileService,
    private formBuilder: FormBuilder,
    private citiesService: CitiesService,
    ) {
      this.buildFiltersForm();
      this.cities = citiesService.Cities;
    }

  ngOnInit() {
    this.initEnums();
    this.getRole();
    this.getStudents();
  }

  private buildFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      search: [''],
      status: [''],
      city: [''],
      gender: [''],
      graduationYear: [''],
    });
  }

  private initEnums() {
    for (const i in StudentStatusEnum) {
      if (typeof StudentStatusEnum[i] === 'number') {
        this.userStatuses.push({id: <any>StudentStatusEnum[i], value: i});
      }
    }

    for (const i in GenderEnum) {
      if (typeof GenderEnum[i] === 'number') {
        this.genders.push({ id: <any>GenderEnum[i], value: i });
      }
    }
  }

  public doFilter = (value: string, dropdown: boolean) => {
    if (dropdown) {
      this.dataSource.filter = value.toString();
    }
    else {
      this.dataSource.filter = value.trim().toLocaleLowerCase();
    }
  }

  private clearForm(form: FormGroup): void {
    for(let name in form.controls) {
      form.controls[name].patchValue('');
    }
  }

  private getfilterPredicate(): (data, filter: string) => boolean {
    let filterPredicate = (data, filter): boolean => {
      let searchTerms = JSON.parse(filter);
      return data.email.toLowerCase().indexOf(searchTerms.email) !== -1 
      && data.status === searchTerms.status;
    }
    return filterPredicate;
  }

  private mapStudents(students) {
    return students.map((student) => {
      student.status = student.status  || student.status === 0? StudentStatusEnum[student.status] : null;
      student.gender = student.gender || student.gender === 0? GenderEnum[student.gender] : null;
      return student;
    })
  }

  clearFilters(): void {
    this.dataSource.filter = null;
    this.clearForm(this.filtersForm);
  }

  getStudents(): void {
    this.isLoading = true;
    this.studentsSubscription = this.studentsService.getStudents()
    .subscribe(data => {
      this.dataSource.data = this.mapStudents(data);
      this.isLoading = false;
    }, err => this.isLoading = false);
  }

  getRole() {
    this.auth.currentUser.subscribe((user) => {
      const role: any = !!user ? user.userRole : null;
      this.isAdmin = !!user && role === 'Admin' ? true : false;
    });
  }

  isSemiAdmin(role: number) {
    return role !== 0 ? true : false;
  }

  create(): void {
    this.navCtrl.navigate('students/new', {id: null});
  }

  editStudent(element): void {
    this.navCtrl.navigate('students/edit', {id: element.id});
  }

  toggleStatus(isSemiadmin: Event, model): void {
    const userModel = {
      role: isSemiadmin ? 1 : 0,
      firstName: model.firstName,
      lastName: model.lastName,
      email: model.email,
      id: model.id
    } as UserPermissionsModel;
    this.studentsService.updateStudentStatus(userModel).subscribe((data) => {},
    err => {});
  }


  deleteStudent(userId): void {
    this.profileService.delete(userId).subscribe(() => {
      this.getStudents();
    },
    err => {})
  }

  ngOnDestroy() {
    this.studentsSubscription.unsubscribe();
  }
}