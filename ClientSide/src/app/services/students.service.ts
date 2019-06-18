import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DebugRenderer2 } from '@angular/core/src/view/services';

import { Observable } from 'rxjs';

import { debug } from 'util';

import { environment } from 'src/environments/environment.prod';

import { StudentScholarshipModel } from '../models/student/student-scholarship.model';
import { CandidateProfileModel } from '../models/user/candidate-profile.model';
import { UserPermissionsModel } from '../models/user/user-permissions.model';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  private apiPath = environment.URL;
  
  constructor(private http: HttpClient) {
  }

  public getStudents(): Observable<CandidateProfileModel[]> {
    return this.http.get<CandidateProfileModel[]>(`${this.apiPath}/Accounts/Students`);
  }

  public getStudentsStudentScholarshipByID(userId: string): Observable<StudentScholarshipModel[]> {
    return this.http.get<StudentScholarshipModel[]>(`${this.apiPath}/StudentScholarship/${userId}`);
  }

  public save(model:CandidateProfileModel): Observable<CandidateProfileModel> {
    const headers = new HttpHeaders().set("Content-Type", "multipart/form-data");
    let data = this.convertModelToFormData(model);
    return this.http.post<CandidateProfileModel>(`${this.apiPath}/Profile`, data);
  }

  public updateStudentStatus(model: UserPermissionsModel): Observable<UserPermissionsModel> {
    return this.http.put<UserPermissionsModel>(`${this.apiPath}/Accounts/Permission`, model);
  }

  public createStudentScholarship(model: StudentScholarshipModel, id: number) {
    return id !== 0
        ? this.http.put<StudentScholarshipModel>(`${this.apiPath}/StudentScholarship`, model)
        : this.http.post<StudentScholarshipModel>(`${this.apiPath}/StudentScholarship`, model);
  }

  public deleteStudentScholarship(id: number) {
    return this.http.delete(`${environment.URL}/StudentScholarship/${id}`);
  }

  public getTelephony(userFullName: string) {
    return this.http.get(`${this.apiPath}/Telephony/${userFullName}`);
  }  

  public getTelephonyEvents() {
    return this.http.get(`${this.apiPath}/Telephony/events`);
  }  

  private convertModelToFormData(model: any, form: FormData = null, namespace = ''): FormData {
    let formData = form || new FormData();
    let formKey;

    for (let propertyName in model) {
        if (!model.hasOwnProperty(propertyName)) continue;
        let formKey = namespace ? `${namespace}.${propertyName}` : propertyName;
        if (model[propertyName] instanceof Date)
        {
            formData.append(formKey, model[propertyName].toISOString());
            console.log(formKey + ": "+ model[propertyName].toISOString());
        }
        else if (model[propertyName] instanceof Array) {
            //continue;
            model[propertyName].forEach((element, index) => {
                const tempFormKey = `${formKey}[${index}]`;
                this.convertModelToFormData(element, formData, tempFormKey);
            });
        }
        else if (typeof model[propertyName] === 'object' && !(model[propertyName] instanceof File))
        {
            //continue;
            this.convertModelToFormData(model[propertyName], formData, formKey);
        }
        else
        {
            if(model[propertyName] instanceof File)
                formData.append(formKey, model[propertyName]);
            else
                formData.append(formKey, model[propertyName].toString());
            console.log(formKey + ": "+ model[propertyName].toString());
        }
    }
    return formData;
  }
}
