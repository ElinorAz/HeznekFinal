import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DebugRenderer2 } from '@angular/core/src/view/services';

import { Observable } from 'rxjs';

import { debug } from 'util';

import { environment } from 'src/environments/environment.prod';

import { CandidateProfileModel } from '../models/user/candidate-profile.model';
import { UserPermissionsModel } from '../models/user/user-permissions.model';
import { AdminCandidateProfileModel } from '../models/admin/admin-candidate-profile.model';

@Injectable({
  providedIn: 'root'
})
export class CandidatesService {
  private apiPath = environment.URL;
  
  constructor(private http: HttpClient) {
  }

  public getCandidates(): Observable<CandidateProfileModel[]> {
    return this.http.get<CandidateProfileModel[]>(`${this.apiPath}/Accounts/candidates`);
  }

  public updateCandidateStatus(model: UserPermissionsModel): Observable<UserPermissionsModel> {
    return this.http.put<UserPermissionsModel>(`${this.apiPath}/Accounts/Permission`, model);
  }

  public save(model:CandidateProfileModel): Observable<CandidateProfileModel> {
    const headers = new HttpHeaders().set("Content-Type", "multipart/form-data");
    let data = this.convertModelToFormData(model);
    return this.http.post<CandidateProfileModel>(`${this.apiPath}/Profile`, data);
  }

  public updateTelephony(model: AdminCandidateProfileModel): Observable<any> {
    return this.http.put(`${this.apiPath}/Telephony`, model);
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
