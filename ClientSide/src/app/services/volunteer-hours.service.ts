import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { VolunteerHourModel } from '../models/volunteer-hours/volunteer-hours.model';

@Injectable({
  providedIn: 'root'
})
export class VolunteerHoursService {
  private apiPath = environment.URL;
  
  constructor(private http: HttpClient) {
  }

  public getVolunteerHours(): Observable<VolunteerHourModel[]> {
    return this.http.get<VolunteerHourModel[]>(`${this.apiPath}/VolunteerHours`);
  }

  public getVolunteerHoursByID(userId: string): Observable<VolunteerHourModel[]> {
    return this.http.get<VolunteerHourModel[]>(`${this.apiPath}/VolunteerHours/${userId}`);
  }

  public create(model: VolunteerHourModel): Observable<VolunteerHourModel> {
    return this.http.post<VolunteerHourModel>(`${this.apiPath}/VolunteerHours`, model);
  }
}
