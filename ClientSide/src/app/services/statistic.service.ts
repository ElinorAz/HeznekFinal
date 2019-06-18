import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {
  private apiPath = environment.URL;
  
  constructor(private http: HttpClient) {
  }

  public getStatistic(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiPath}/Statistic`);
  }
}
