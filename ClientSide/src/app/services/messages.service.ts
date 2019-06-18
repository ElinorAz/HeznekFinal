import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { MessageModel } from '../models/message/message.model';

@Injectable({
    providedIn: 'root'
})
export class MessagesService {
    private apiPath = environment.URL;

    constructor(private http: HttpClient) {
    }

    public create(model: MessageModel): Observable<MessageModel> {
        const headers = new HttpHeaders().set("Content-Type", "multipart/form-data");
        let data = this.convertModelToFormData(model);
        return this.http.post<MessageModel>(`${this.apiPath}/message`, data);
    }

    private convertModelToFormData(model: any, form: FormData = null, namespace = ''): FormData {
        let formData = form || new FormData();
        let formKey;

        for (let propertyName in model) {
            if (!model.hasOwnProperty(propertyName)) continue;
            let formKey = namespace ? `${namespace}.${propertyName}` : propertyName;

            if (model[propertyName] instanceof Array) {
                model[propertyName].forEach(val => {
                    formData.append(`${formKey}[]`, val);
                });
            }
            else if (typeof model[propertyName] === 'object' && !(model[propertyName] instanceof File)) {
                this.convertModelToFormData(model[propertyName], formData, formKey);
            }
            else {
                if (model[propertyName] instanceof File)
                    formData.append(formKey, model[propertyName]);
                else
                    formData.append(formKey, model[propertyName].toString());
            }
        }
        return formData;
    }
}
