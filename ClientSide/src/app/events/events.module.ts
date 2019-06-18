import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatTableModule, MatSnackBarModule, MatSpinner, 
  MatProgressSpinnerModule, MatButtonModule, MatCheckboxModule, MatIconModule, MatSlideToggleModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxNavigationWithDataComponent } from 'ngx-navigation-with-data';
import { EventsComponent } from '../events/events/events.component';
import { NewEventComponent } from '../events/new-event/new-event.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

@NgModule({
  declarations: [
    NewEventComponent,
    EventsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatSlideToggleModule,
    NgxMaterialTimepickerModule.forRoot()
  ],
  providers: [
    NgxNavigationWithDataComponent
  ]
})
export class EventsModule { }
