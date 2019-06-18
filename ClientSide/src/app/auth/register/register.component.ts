import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material';
import { RegisterModel } from 'src/app/models/auth/register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  isError: boolean = false;
  isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private auth: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.buildForm();
  }

  buildForm() {
    this.registerForm = this.formBuilder.group({
      id:  ['', Validators.required, Validators.minLength(9),Validators.maxLength(9)],
      firstName: ['', Validators.required],
      lastName:  ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [''],
      passwordConfirmation:  ['']
    });
  }

  login()
  {
    this.router.navigate(["login"]);
  }

  register()
  {
    this.isLoading = true;
    this.auth.register(this.registerForm.value as RegisterModel).subscribe(()=>{
      this.isLoading = false;
      this.snackBar.open("הודעת האימייל נשלחה", "",{duration : 2000});      
    },(err)=>{
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "",{duration : 2000});
    });  
  }

  ngOnInit() {
  }

}
