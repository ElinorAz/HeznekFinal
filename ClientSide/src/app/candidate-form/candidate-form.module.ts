import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatIconModule, MatButtonModule, MatProgressSpinnerModule, MatInputModule, MatFormFieldModule, MatSnackBarModule, MatRadioModule, MatAutocompleteModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ComponentsModule } from '../components/components.module';

import { FormComponent } from './form/form.component';
import { ParentsSalaryPageComponent } from './parents-salary-page/parents-salary-page.component';
import { StatusComponent } from './status/status.component';
import { TelephonyComponent } from '../candidate-form/telephony/telephony.component';
import { StatisticComponent } from '../candidate-form/statistic/statistic.component';
import { ChartsModule } from 'ng2-charts/ng2-charts';

@NgModule({
  declarations: [FormComponent, StatusComponent, ParentsSalaryPageComponent, TelephonyComponent, StatisticComponent],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatRadioModule,
    MatIconModule,
    MatAutocompleteModule,
    MatProgressSpinnerModule,
    ComponentsModule,
    ChartsModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CandidateFormModule { }
