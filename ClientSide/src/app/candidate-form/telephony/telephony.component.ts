import { Component, OnInit, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar, MatTableDataSource } from '@angular/material';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { FormService } from '../../services/form.service';
import { StudentsService } from '../../services/students.service';
import { environment } from '../../../environments/environment';
import { Observable, of } from 'rxjs';
import { startWith } from 'rxjs/internal/operators/startWith';
import { debounceTime, switchMap, map, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-telephony',
  templateUrl: './telephony.component.html',
  styleUrls: ['./telephony.component.scss']
})
export class TelephonyComponent implements OnInit {
  latestEvent;
  attendedEvents = [];
  displayedColumns = ['name', 'subject', 'date', 'time', 'location', 'expected', 'actions' ];
  isLoading:boolean = false;
  isSearched: boolean = false;
  candidates = [];
  candidatesAutoComplete$: Observable<any> = null;
  autoCompleteControl = new FormControl();

  constructor(
    private studentsService: StudentsService,
    private snackBar: MatSnackBar,
    private navCtrl: NgxNavigationWithDataComponent,
    ) {
  }

  ngOnInit() {
    this.trackAutocomplete();
    this.getEventsData();
  }

  private getEventsData(): void {
    this.studentsService.getTelephonyEvents().subscribe((data: any) => {
      this.latestEvent = data.latestEvent;
      this.attendedEvents = data.newEvents;
    },
    err => {});
  }

  private trackAutocomplete(): void {
    this.candidatesAutoComplete$ = this.autoCompleteControl.valueChanges.pipe(
      startWith(''),
      // delay emits
      debounceTime(300),
      // use switch map so as to cancel previous subscribed events, before creating new once
      switchMap(value => {
        if (value !== '') {
          return this.lookup(value);
        } else {
          // if no value is pressent, return null
          return of(null);
        }
      })
    );
  }

  private lookup(value: string): Observable<any> {
    return this.studentsService.getTelephony(value.toLowerCase())
    .pipe(
      map(results => results),
      catchError(_ => {
        return of(null);
      })
    );
  }

  editCandidate(user): void {
    this.navCtrl.navigate('candidates/edit', {id: user.userId, isTelephony: true, modelWithTelephony: user});
  }
}
