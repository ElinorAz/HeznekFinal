import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule, MatInputModule, MatDatepickerModule, MatSelectModule, MatNativeDateModule, MatTableModule, MatSnackBarModule, MatSpinner, 
  MatProgressSpinnerModule, MatButtonModule, MatCheckboxModule, MatIconModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NewScholarshipsComponent } from '../scholarships/new-scholarships/new-scholarships.component';
import { ScholarshipsComponent } from '../scholarships/scholarships/scholarships.component';
import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

@NgModule({
  declarations: [
    NewScholarshipsComponent,
    ScholarshipsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatSelectModule,
    MatNativeDateModule,
    MatTableModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule
  ],
  providers: [
    NgxNavigationWithDataComponent
  ]
})
export class ScholarshipsModule { }
