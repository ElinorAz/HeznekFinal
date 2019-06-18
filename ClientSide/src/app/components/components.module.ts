import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatProgressSpinnerModule, MatTableModule, MatInputModule, MatFormFieldModule, MatSnackBarModule, MatButtonModule, MatIconModule, MatCheckboxModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CSVComponent } from './csv/csv.component';
import { ParentsSalaryComponent } from './parents-salary/parents-salary.component';

@NgModule({
  declarations: [
    CSVComponent,
    ParentsSalaryComponent
  ],
  imports: [
    BrowserModule,
    MatProgressSpinnerModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  exports: [
    CSVComponent,
    ParentsSalaryComponent
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class ComponentsModule { }
