import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

import { Subscription } from 'rxjs';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { AuthService } from '../../services/auth.service';
import { EventsService } from '../../services/events.service';
import { EventAttendModel } from '../../models/event/event-attend.model';
import { EventModel } from '../../models/event/event.model';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit, OnDestroy {
  dataSource = new MatTableDataSource();
  attendedEvents = new MatTableDataSource();
  displayedColumns = ['name', 'subject', 'date', 'time', 'finishTime', 'location', 'expected', 'actions' ];
  displayedAttendedColumns = [ 'name', 'date', 'time',  'finishTime', 'location'  ];
  eventSubscription: Subscription;
  statuses: {id: number; value: string}[] = [];
  isStudent: boolean = null;
  isAdmin: boolean = null;

  constructor(
    private navCtrl: NgxNavigationWithDataComponent,
    private eventService: EventsService,
    private auth: AuthService
    ) {}

  ngOnInit() {
    const subscription = this.auth.currentUser.subscribe((user) => {
      const role: any = !!user ? user.userRole : null;
      this.isAdmin = !!user && role === 'Admin' ? true : false;
      const status = !!user ? user.userStatus : null;
      if (status == 3 || status == 4 || status == 5 || status == 6) {
        this.isStudent = true;
      }

      this.getEvents();
      subscription ? subscription.unsubscribe() : null;
    });
  }

  private filterAttedEvents(events) {
    return events.filter((event) => {
      return event.attend;
    })
  }

  isCompletedEvent(date) { 
    return new Date(new Date().getFullYear(),new Date().getMonth() , new Date().getDate()) > new Date(date);
  }

  getEvents(): void {
    if (this.isAdmin) {
      this.eventSubscription = this.eventService.getEvents()
      .subscribe(data => {
        this.dataSource.data = data; 
        this.attendedEvents.data = this.filterAttedEvents(data);
        this.eventSubscription ? this.eventSubscription.unsubscribe() : null;
      }, err => {});
    }
    else if (this.isStudent) {
      this.eventSubscription = this.eventService.getMyEvents()
      .subscribe(data => {
        this.dataSource.data = data; 
        this.attendedEvents.data = this.filterAttedEvents(data);
        this.eventSubscription ? this.eventSubscription.unsubscribe() : null;
      }, err => {});
    }
  }

  toggleAttend(isAttend: boolean, id: number): void {
    const eventAttendModel = {
      eventId: id,
      attend: isAttend
    } as EventAttendModel;

    const attendSubscription = this.eventService.attend(eventAttendModel).subscribe((data) => {
      this.getEvents();
      attendSubscription ? attendSubscription.unsubscribe() : null;
    },
    err => {});
  }

  create(): void {
    this.navCtrl.navigate('events/new', {id: null});
  }

  editEvent(element: EventModel): void {
    this.navCtrl.navigate('events/edit', {id: element.id});
  }

  deleteEvent(id: number): void {
    this.eventService.delete(id).subscribe(res => {
      this.getEvents();
    });
  }

  ngOnDestroy() {
    this.eventSubscription ? this.eventSubscription.unsubscribe() : null;
  }
}