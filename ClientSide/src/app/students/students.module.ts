import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatSelectModule, MatNativeDateModule, MatRadioModule, MatTableModule, MatSnackBarModule, MatSpinner, MatProgressSpinnerModule, MatButtonModule, MatCheckboxModule, MatIconModule, MatSlideToggleModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';
import { ComponentsModule } from '../components/components.module';
import { StudentsComponent } from '../students/students/students.component';
import { EditStudentComponent } from '../students/edit-student/edit-student.component';

@NgModule({
  declarations: [
    StudentsComponent,
    EditStudentComponent
  ],
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
    MatSlideToggleModule,
    ComponentsModule
  ],
  providers: [
    NgxNavigationWithDataComponent
  ]
})
export class StudentsModule { }
