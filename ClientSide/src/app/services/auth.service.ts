import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { JwtHelperService } from '@auth0/angular-jwt';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment.prod';

import { LoginModel } from '../models/auth/login.model';
import { NewPasswordModel } from '../models/auth/new-password.model';
import { RegisterModel } from '../models/auth/register.model';
import { TokenModel } from '../models/auth/token.model';
import { UserModel } from '../models/user/user.model';
const helper = new JwtHelperService();

@Injectable({ providedIn: 'root' })
export class AuthService {
    private currentUserSubject: BehaviorSubject<UserModel>;
    public currentUser: Observable<UserModel>;
    private apiPath = environment.URL;
    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<UserModel>(JSON.parse(localStorage.getItem('heznek-currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): UserModel {
        return this.currentUserSubject.value;
    }

    public login(model: LoginModel) {
        return this.http.post(`${this.apiPath}/Auth`, model)
            .pipe(map(data => {
                if (data && data['accessToken']) {
                    const decodedToken = this.decodeToken(data);
                    const user = {
                        id: decodedToken.sid, token: data['accessToken'],
                        userRole: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
                        userStatus: decodedToken['http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor']
                    } as UserModel;
                    localStorage.setItem('heznek-currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                }
                return;
            }));
    }

    private decodeToken(data) {
        try {
            return JSON.parse(atob(data['accessToken'].split('.')[1]));
          } catch (e) {
            return null;
        }
    }

    public logout() {
        localStorage.removeItem('heznek-currentUser');
        this.currentUserSubject.next(null);
    }

    public forgot(model: string) {
      return this.http.get(`${this.apiPath}/ResetPassword/Forgot/${model}`);
    }
    
    public register(model: RegisterModel) {
      return this.http.post(`${this.apiPath}/Accounts`, model);
    }
  
    public resetPassword(model: NewPasswordModel){
      return this.http.post(`${this.apiPath}/ResetPassword`, model);
    }
}
