import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment.prod';

import { ScholarshipModel } from '../models/scholarship/scholarship.model';

@Injectable({
  providedIn: 'root'
})
export class ScholarshipService {
  private apiPath = environment.URL;
  
  constructor(private http: HttpClient) {
  }

  public getScholarships(): Observable<ScholarshipModel[]> {
    return this.http.get<ScholarshipModel[]>(`${this.apiPath}/Scholarship`);
  }

  public getScholarship(id: number): Observable<ScholarshipModel> {
    return this.http.get<ScholarshipModel>(`${this.apiPath}/Scholarship/${id}`);
  }

  public create(model: ScholarshipModel, id: number): Observable<ScholarshipModel> {
    return id ?
      this.http.put<ScholarshipModel>(`${this.apiPath}/Scholarship`, model) 
      : this.http.post<ScholarshipModel>(`${this.apiPath}/Scholarship`, model);
  }

  public delete(id: number) {
    return this.http.delete(`${environment.URL}/Scholarship/${id}`);
  }
}
