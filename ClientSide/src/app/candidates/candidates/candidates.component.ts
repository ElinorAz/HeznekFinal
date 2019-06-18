import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { EmailValidator, FormGroup, FormBuilder } from '@angular/forms';

import { Subscription } from 'rxjs';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { UserStatusEnum } from 'src/app/enums/user-status.enum';

import { CitiesService } from '../../data/cities.service';
import { CandidateStatusEnum } from '../../enums/candidate-status.enum';
import { GenderEnum } from '../../enums/gender.enum';
import { AuthService } from '../../services/auth.service';
import { CandidatesService } from '../../services/candidates.service';
import { ProfileService } from '../../services/pofile.service';
import { CandidateProfileModel } from '../../models/user/candidate-profile.model';
import { UserPermissionsModel } from '../../models/user/user-permissions.model';

@Component({
  selector: 'app-candidates',
  templateUrl: './candidates.component.html',
  styleUrls: ['./candidates.component.scss']
})
export class CandidatesComponent implements OnInit, OnDestroy {
  dataSource = new MatTableDataSource();
  displayedColumns = [ 'id', 'firstName', 'lastName', 'status', 'email', 'phone', 'university', 'domain', 'faculty', 'actions'];
  candidatesSubscription: Subscription;
  isLoading: boolean = false;
  isAdmin: boolean = false;
  userStatuses: {id: number; value: string}[] = [];
  cities: { id: string; city: string }[] = [];
  genders: { id: number; value: string }[] = [];
  filtersForm: FormGroup;

  constructor(
    private candidatesService: CandidatesService,
    private navCtrl: NgxNavigationWithDataComponent,
    private auth: AuthService,
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
    this.getCandidates();
  }

  private getRole() {
    this.auth.currentUser.subscribe((user) => {
      const role: any = !!user ? user.userRole : null;
      this.isAdmin = !!user && role === 'Admin' ? true : false;
    });
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
    for (const i in CandidateStatusEnum) {
      if (typeof CandidateStatusEnum[i] === 'number') {
        this.userStatuses.push({id: <any>CandidateStatusEnum[i], value: i});
      }
    }

    for (const i in GenderEnum) {
      if (typeof GenderEnum[i] === 'number') {
        this.genders.push({ id: <any>GenderEnum[i], value: i });
      }
    }
  }

  public doFilter = (value: string, dropdown: boolean) => {
    dropdown ? this.dataSource.filter = value.toString() : this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

  private clearForm(form: FormGroup): void {
    for(let name in form.controls) {
      form.controls[name].patchValue('');
    }
  }

  private mapCandidates(candidates) {
    return candidates.map((candidate) => {
      candidate.status = candidate.status || candidate.status === 0 ? CandidateStatusEnum[candidate.status] : null;
      candidate.gender = candidate.gender || candidate.gender === 0? GenderEnum[candidate.gender] : null;
      return candidate;
    })
  }

  clearFilters(): void {
    this.dataSource.filter = null;
    this.clearForm(this.filtersForm);
  }


  getCandidates(): void {
    this.isLoading = true;
    this.candidatesSubscription = this.candidatesService.getCandidates()
    .subscribe(data => {
      this.dataSource.data = this.mapCandidates(data);
      console.log(this.dataSource.data, 'data')
      this.isLoading = false;
    }, err => this.isLoading = false);
  }

  create(): void {
    this.navCtrl.navigate('candidates/new', {id: null});
  }

  editCandidate(element): void {
    this.navCtrl.navigate('candidates/edit', {id: element.id, status: element.status});
  }

  isSemiAdmin(role: number) {
    return role !== 0 ? true : false;
  }

  deleteCandidate(userId): void {
    this.profileService.delete(userId).subscribe(() => {
      this.getCandidates();
    },
    err => {})
  }

  ngOnDestroy() {
    this.candidatesSubscription.unsubscribe();
  }
}