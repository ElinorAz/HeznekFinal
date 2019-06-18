import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule, MatInputModule, MatTableModule, MatSnackBarModule, MatSpinner, MatProgressSpinnerModule, MatButtonModule, MatIconModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AdminSystemDetailsComponent } from './admin-system-details/admin-system-details.component';

@NgModule({
  declarations: [AdminSystemDetailsComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatTableModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class AdminModule { }
