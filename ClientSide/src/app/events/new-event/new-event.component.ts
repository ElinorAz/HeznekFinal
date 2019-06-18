import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators, FormControl } from '@angular/forms';
import { MatSnackBar, MatTableDataSource } from '@angular/material';
import { identifierModuleUrl } from '@angular/compiler';

import { first } from 'rxjs/operators';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { UserStatusEnum } from '../../enums/user-status.enum';
import { EventsService } from '../../services/events.service';
import { ScholarshipService } from '../../services/scholarships.service';
import { EventModel } from '../../models/event/event.model';

@Component({
  selector: 'app-event-scholarships',
  templateUrl: './new-event.component.html',
  styleUrls: ['./new-event.component.scss']
})
export class NewEventComponent implements OnInit {
  notAttendingEvents = new MatTableDataSource();
  attendingEvents = new MatTableDataSource();
  displayedColumns = ['id','lastName', 'firstName'];
  eventForm: FormGroup;
  event: EventModel = null;
  eventId: number = 0;
  isLoading:boolean = false;
  userStatuses: {id: number; value: string}[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private eventService: EventsService,
    private snackBar: MatSnackBar,
    private navCtrl: NgxNavigationWithDataComponent,
    ) {
    this.initEnums();
    this.buildForm();
  }

  ngOnInit() {
    this.eventId = this.navCtrl.get('id');
    if (this.eventId) {
      const subscription = this.eventService.getEvent(this.eventId)
      .pipe(first())
      .subscribe(data => {
        this.event = data as EventModel;
        this.notAttendingEvents.data = this.event.notAttending;
        this.attendingEvents.data = this.event.attending;
        this.setFormValue(this.eventForm, this.event);
        this.setParticipantsForm(this.eventForm, this.event);
        subscription ? subscription.unsubscribe() : null;
      }, err => console.dir(err));
    }
  }

  initEnums() {
    for (const i in UserStatusEnum) {
      if (typeof UserStatusEnum[i] === 'number') {
        this.userStatuses.push({id: <any>UserStatusEnum[i], value: i});
      }
    }
  }

  get formData() { 
    return <FormArray>this.eventForm.get('participantTypes'); 
  }

  private buildForm() {
    const participantControls = this.userStatuses.map(control => new FormControl(false));
    this.eventForm = this.formBuilder.group({
        id: [0],
        name: ['', [Validators.required]],
        subject: [''],
        date: ['', [Validators.required]],
        time: ['', [Validators.required]],
        finishTime: ['', [Validators.required]],
        location: ['', [Validators.required]],
        expected: ['', [Validators.required]],
        participantTypes: new FormArray(participantControls)
    });
  }

  private setFormValue(form: FormGroup, value: EventModel) {
      Object.keys(value).forEach(name => {
        if (form.controls[name] && form.controls[name] instanceof FormControl) {
          form.controls[name].setValue(value[name]);
        }
    });
  }

  private setParticipantsForm(form: FormGroup, value: EventModel) {
      Object.keys(value).forEach(name => {
          this.userStatuses.forEach((item, i: number) => {
            const isSelectedParticipant = this.findSelectedParticipant(this.event.participantTypes, i).length ? true : false;
            this.eventForm.controls['participantTypes']['controls'][i].setValue(isSelectedParticipant);
          });
      });
  }

  private redirectToEvents() {
    this.navCtrl.navigate(['events']);
  }

  compareTimes(): boolean {
    if (this.eventForm.controls['time'].value && this.eventForm.controls['finishTime'].value ) {
      return new Date('3/2/2018 '  + this.eventForm.controls['finishTime'].value) > new Date ('3/2/2018 ' + this.eventForm.controls['time'].value) 
    }
    else {
      return true;
    }
  }

  getTodayDate() {
    return new Date()
  }

  findSelectedParticipant(list: number[], participantId: number) {
    return list.filter((item: number) => {
      return item === participantId;
    });
  }

  mapParticipants(selectecParticipants) {
    const participantsIDs: number[] = [];
    selectecParticipants.forEach((item, i: number) => {
      item ? participantsIDs.push(i) : null;
    });

    return participantsIDs;
  }

  save(): void {
    if (this.compareTimes()) {
      this.isLoading = true;
      let model: EventModel = this.eventForm.value;
      model.participantTypes = this.mapParticipants(this.eventForm.value.participantTypes);
      model.id = this.eventId || 0;
      this.eventService.create(model, this.eventId).subscribe(()=>{
        this.isLoading = false;
        this.snackBar.open("הנתונים נשמרו", "",{duration : 2000});
        this.redirectToEvents();
      },(err)=>{
        this.isLoading = false;
        this.snackBar.open("המערכת נתקלה בבעיה", "",{duration : 2000});
      });
    }

    else {
      alert ('Time not valid');
    }
  }

}
