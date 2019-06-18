import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule, MatInputModule, MatTableModule, MatSnackBarModule, MatSpinner, MatProgressSpinnerModule, MatButtonModule, MatCheckboxModule, MatIconModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NewMessageComponent } from '../messages/new-message/new-message.component';

@NgModule({
  declarations: [NewMessageComponent],
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
    MatCheckboxModule,
    MatIconModule
  ]
})
export class MessagesModule { }
