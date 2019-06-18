import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { TokenModel } from 'src/app/models/auth/token.model';
import {  MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  isError: boolean = false;
  isLoading: boolean = false;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private auth: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.buildForm();
    // reset login status
    this.auth.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  buildForm() {
    this.loginForm = this.formBuilder.group({
      id: [''],
      password: ['']
    });
  }

  register()
  {
    this.router.navigate(["register"]);
  }

  forgotPassword()
  {
    this.router.navigate(["forgotPassword"]);
  }

  redirectToProfile() {
    const subscription = this.auth.currentUser.subscribe((user) => {
      const role: any = !!user ? user.userRole : null;
      !!user && role === 'Admin' ? this.router.navigate(['form']) : this.router.navigate(['profile']);
      subscription ? subscription.unsubscribe() : null;
    });
  }

  login()
  {
    this.isLoading = true;
    this.auth.login(this.loginForm.value).subscribe((data)=>{
      this.redirectToProfile();
      this.isLoading = false;
          
    },(err)=>{
      this.isLoading = false;
      this.snackBar.open("האימות נכשל", "",{duration : 2000});
    });       
  }

  ngOnInit() {
  }

}
