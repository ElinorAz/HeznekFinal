import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment.prod';

import { ParentEnum } from '../enums/parent.enum';
import { FormModel } from '../models/candidate/fom.model';
import { ParentsSalaryModel } from '../models/candidate/parents-salary.model';
import { TaskModel } from '../models/candidate/task.model';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  private apiPath = environment.URL;
  
  constructor(private http: HttpClient) {
  }

  public getForm(): Observable<FormModel> {
      return this.http.get<FormModel>(`${this.apiPath}/CandidateForm`);
  }

  public getFormById(userId: string): Observable<FormModel> {
    return this.http.get<FormModel>(`${this.apiPath}/CandidateForm/${userId}`);
}

  public submitToReview() {
    return this.http.post<TaskModel>(`${this.apiPath}/CandidateForm`,{});
  }

  public updateTask(task: TaskModel): Observable<TaskModel>{
    const formData = new FormData();
    formData.append("Id",task.id.toString());
    formData.append("File",task.file);
    return this.http.put<TaskModel>(`${this.apiPath}/CandidateForm/task`,formData);
  }

  public updateParentsSalary(parentSalary: ParentsSalaryModel): Observable<ParentsSalaryModel>{
    const formData = new FormData();
    formData.append('Id', '0');
    formData.append('MotherName', parentSalary.motherName);
    formData.append('FatherName', parentSalary.fatherName);
    formData.append('SalarySlips', parentSalary.salarySlips);
    formData.append('Disability', parentSalary.disability);
    formData.append('Disability2', parentSalary.disability2);
    formData.append('FatherDisability', parentSalary.fatherDisability ? 'true' : 'false');
    formData.append('MotherDisability', parentSalary.motherDisability ? 'true' : 'false');
    return this.http.put<ParentsSalaryModel>(`${this.apiPath}/CandidateForm`, formData);
  }
}
