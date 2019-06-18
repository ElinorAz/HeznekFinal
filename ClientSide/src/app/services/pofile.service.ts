import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DebugRenderer2 } from '@angular/core/src/view/services';

import { Observable } from 'rxjs';

import { debug } from 'util';

import { environment } from 'src/environments/environment.prod';

import { AdminCandidateProfileModel } from '../models/admin/admin-candidate-profile.model';
import { AdminStudentProfileModel } from '../models/admin/admin-student-profile.model';
import { CandidateProfileModel } from '../models/user/candidate-profile.model';
import { SystemDetailsModel } from '../models/user/system-details.model';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    private apiPath = environment.URL;

    constructor(private http: HttpClient) {
    }

    public getProfile(): Observable<CandidateProfileModel> {
        return this.http.get<CandidateProfileModel>(`${this.apiPath}/Profile`);
    }

    public getSystemDetails(): Observable<SystemDetailsModel> {
        return this.http.get<SystemDetailsModel>(`${this.apiPath}/Profile/SystemDetails`);
    }

    public getProfileByID(userId: string): Observable<CandidateProfileModel> {
        return this.http.get<CandidateProfileModel>(`${this.apiPath}/Profile/${userId}`);
    }

    public save(model: CandidateProfileModel): Observable<CandidateProfileModel> {
        const headers = new HttpHeaders().set("Content-Type", "multipart/form-data");
        let data = this.convertModelToFormData(model);
        return this.http.put<CandidateProfileModel>(`${this.apiPath}/Profile`, data);
    }

    public updateSystemDetails(model: SystemDetailsModel): Observable<SystemDetailsModel> {
        return this.http.put<SystemDetailsModel>(`${this.apiPath}/Profile/SystemDetails`, model) 
      }

    public updateCandidate(model: AdminCandidateProfileModel, id: number): Observable<AdminCandidateProfileModel> {
        const headers = new HttpHeaders().set("Content-Type", "multipart/form-data");
        let data = this.convertModelToFormData(model);
        return id
            ? this.http.put<AdminCandidateProfileModel>(`${this.apiPath}/Profile/admin`, data)
            : this.http.post<AdminCandidateProfileModel>(`${this.apiPath}/Profile/admin`, data)
    }

    public updateStudent(model: AdminStudentProfileModel, id): Observable<AdminStudentProfileModel> {
        const headers = new HttpHeaders().set("Content-Type", "multipart/form-data");
        let data = this.convertModelToFormData(model);
        return id
        ? this.http.put<AdminStudentProfileModel>(`${this.apiPath}/Profile/admin`, data)
        : this.http.post<AdminStudentProfileModel>(`${this.apiPath}/Profile/admin`, data)
    }

    private convertModelToFormData(model: any, form: FormData = null, namespace = ''): FormData {
        let formData = form || new FormData();
        let formKey;

        for (let propertyName in model) {
            if (!model.hasOwnProperty(propertyName)) continue;
            let formKey = namespace ? `${namespace}.${propertyName}` : propertyName;
            if (model[propertyName] instanceof Date) {
                formData.append(formKey, model[propertyName].toISOString());
                console.log(formKey + ": " + model[propertyName].toISOString());
            }
            else if (model[propertyName] instanceof Array) {
                //continue;
                model[propertyName].forEach((element, index) => {
                    const tempFormKey = `${formKey}[${index}]`;
                    this.convertModelToFormData(element, formData, tempFormKey);
                });
            }
            else if (typeof model[propertyName] === 'object' && !(model[propertyName] instanceof File)) {
                //continue;
                this.convertModelToFormData(model[propertyName], formData, formKey);
            }
            else {
                if (model[propertyName] instanceof File)
                    formData.append(formKey, model[propertyName]);
                else
                    formData.append(formKey, model[propertyName].toString());
                    console.log(formKey + ": " + model[propertyName].toString());
            }
        }
        return formData;
    }


    delete(userId: string) {
        return this.http.delete<CandidateProfileModel>(`${this.apiPath}/Profile/${userId}`);
    } 
}
