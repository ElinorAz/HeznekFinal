import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatSelectModule, MatNativeDateModule, MatRadioModule, MatTableModule, MatSnackBarModule, MatSpinner, MatProgressSpinnerModule, MatButtonModule, MatCheckboxModule, MatIconModule, MatSlideToggleModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';

import { ComponentsModule } from '../components/components.module';

import { CandidatesComponent } from './candidates/candidates.component';
import { EditCandidateComponent } from './edit-candidate/edit-candidate.component';

@NgModule({
  declarations: [
    EditCandidateComponent,
    CandidatesComponent
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
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class CandidatesModule { }
