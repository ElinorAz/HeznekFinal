import { Component, OnInit, OnDestroy } from '@angular/core';
import { ScholarshipService } from '../../services/scholarships.service';
import { ScholarshipModel } from '../../models/scholarship/scholarship.model';
import { MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';
import { ScholarshipStatusEnum } from '../../enums/scholarship-status.enum';

@Component({
  selector: 'app-scholarships',
  templateUrl: './scholarships.component.html',
  styleUrls: ['./scholarships.component.scss']
})
export class ScholarshipsComponent implements OnInit, OnDestroy {
  dataSource = new MatTableDataSource();
  isLoading: boolean = false;
  displayedColumns = [ 'status', 'admission', 'budget', 'name', 'stundentsCount', 'actions' ];
  scholarshipsSubscription: Subscription;
  statuses: {id: number; value: string}[] = [];

  constructor(
    private scholarshipService: ScholarshipService,
    private router: Router,
    private navCtrl: NgxNavigationWithDataComponent
    ) {}

  ngOnInit() {
    this.initEnums();
    this.getScholarships();
  }

  initEnums() {
    for(let i in ScholarshipStatusEnum) {
      if (typeof ScholarshipStatusEnum[i] === 'number') {
        this.statuses.push({id: <any>ScholarshipStatusEnum[i], value: i});
      }
    }
  }

  getScholarships(): void {
    this.isLoading = true;
    this.scholarshipsSubscription = this.scholarshipService.getScholarships()
    .subscribe(data => {
      this.dataSource.data = data; 
      this.isLoading = false;
    }, err => this.isLoading = false);
  }

  create(): void {
    this.navCtrl.navigate('scholarships/new', {id: null});
  }

  editScholarship(element: ScholarshipModel): void {
    this.navCtrl.navigate('scholarships/edit', {id: element.id});
  }

  deleteScholarship(id: number): void {
    this.scholarshipService.delete(id).subscribe(res => {
      this.getScholarships();
    });
  }

  ngOnDestroy() {
    this.scholarshipsSubscription.unsubscribe();
  }
}