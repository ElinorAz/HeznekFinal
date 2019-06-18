import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProfileService } from '../../services/pofile.service';
import { SystemDetailsModel } from '../../models/user/system-details.model';

@Component({
  selector: 'app-admin-system-details',
  templateUrl: './admin-system-details.component.html',
  styleUrls: ['./admin-system-details.component.scss']
})
export class AdminSystemDetailsComponent implements OnInit {
  adminDetailsForm: FormGroup;
  isLoading: boolean = false;
  admin: SystemDetailsModel;

  constructor(
    private formBuilder: FormBuilder,
    private profileService: ProfileService,
    private snackBar: MatSnackBar,
    ) {
    this.buildForm();
  }

  private buildForm() {
    this.adminDetailsForm = this.formBuilder.group({
        id: [{value: '', disabled: true}],
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        password: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.profileService.getSystemDetails().subscribe((data) => {
      this.setFormValue(data);
      this.admin = data;
    })
  }

  private setFormValue(value) {
    Object.keys(value).forEach(name => {
      if (this.adminDetailsForm.controls[name]) {
        this.adminDetailsForm.controls[name].setValue(value[name]);
      }
    });
  }

  save() {
    this.isLoading = true;
    let model: SystemDetailsModel = this.adminDetailsForm.value;
    this.profileService.updateSystemDetails(model).subscribe(() => {
      this.isLoading = false;
      this.snackBar.open("הנתונים נשמרו", "",{duration : 2000});
    },(err)=>{
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "",{duration : 2000});
    });
  }

}
