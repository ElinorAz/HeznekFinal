import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { EventModel } from '../models/event/event.model';
import { EventAttendModel } from '../models/event/event-attend.model';

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private apiPath = environment.URL;
  
  constructor(private http: HttpClient) {
  }

  public getEvents(): Observable<EventModel[]> {
    return this.http.get<EventModel[]>(`${this.apiPath}/events`);
  }

  public getMyEvents(): Observable<EventModel[]> {
    return this.http.get<EventModel[]>(`${this.apiPath}/events/my`);
  }

  public getEvent(id: number): Observable<EventModel> {
    return this.http.get<EventModel>(`${this.apiPath}/events/${id}`);
  }

  public create(model: EventModel, id: number): Observable<EventModel> {
    return id ?
      this.http.put<EventModel>(`${this.apiPath}/events`, model) 
      : this.http.post<EventModel>(`${this.apiPath}/events`, model);
  }

  public attend(model: EventAttendModel): Observable<EventAttendModel> {
      return this.http.put<EventAttendModel>(`${this.apiPath}/events/attend`, model) 
  }

  public delete(id: number) {
    return this.http.delete(`${environment.URL}/events/${id}`);
  }
}

