import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class CSVService {
    constructor(private http: HttpClient) { }

    public downloadCSV(ids: number[]) {
        return this.http.post(`${environment.URL}/Accounts/export`, ids, {
            responseType: "blob",
            headers: new HttpHeaders().append("Content-Type", "application/json")
        });
    }
}
