import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile/profile.component';
import { ProfileCandidateComponent } from './profile-candidate/profile-candidate.component';
import { MatExpansionModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatSelectModule, MatNativeDateModule, MatRadioModule, MatTableModule, MatSnackBarModule, MatSpinner, MatProgressSpinnerModule, MatButtonModule, MatCheckboxModule, MatIconModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileStudentComponent } from '../profile/profile-student/profile-student.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

@NgModule({
  declarations: [ProfileComponent, ProfileCandidateComponent, ProfileStudentComponent],
  imports: [
    CommonModule,    
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatSelectModule,
    MatNativeDateModule,
    MatRadioModule,
    MatTableModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    NgxMaterialTimepickerModule.forRoot()
  ]
})
export class ProfileModule { }
