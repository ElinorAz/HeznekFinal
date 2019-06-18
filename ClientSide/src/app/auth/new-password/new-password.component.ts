import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material';
import { NewPasswordModel } from '../../models/auth/new-password.model';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.scss']
})
export class NewPasswordComponent implements OnInit {

  newPasswordForm: FormGroup;
  isError: boolean = false;
  code: string = "";
  isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private auth: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.buildForm();
  }

  buildForm() {
    this.newPasswordForm = this.formBuilder.group({
      password: [''],
      passwordConfirmation:  ['']
    });
  }

  login()
  {
    this.router.navigate(["login"]);
  }
  newPassword()
  {
    this.isLoading = true;
    let model = {code: this.code, password: this.newPasswordForm.controls['password'].value} as NewPasswordModel;
    this.auth.resetPassword(model).subscribe(()=>{
      this.isLoading = false;
      this.snackBar.open("סיסמה חדשה נשמרה");      
    },(err)=>{
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "",{duration : 2000});
    });
  }

  ngOnInit() {
    this.code = this.route.snapshot.paramMap.get('id');
  }

}
