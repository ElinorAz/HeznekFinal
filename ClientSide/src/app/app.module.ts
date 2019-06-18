import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AdminModule } from './admin/admin.module';
import { AuthModule } from './auth/auth.module';
import { CandidateFormModule } from './candidate-form/candidate-form.module';
import { CandidatesModule } from './candidates/candidates.module';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { NavigationModule } from './navigation/navigation.module';
import { ProfileModule } from './profile/profile.module';
import { ScholarshipsModule } from './scholarships/scholarships.module';
import { AuthService } from './services/auth.service';
import { ProfileService } from './services/pofile.service';
import { StudentsModule } from './students/students.module';
import { MessagesModule } from './messages/messages.module';
import { EventsModule } from './events/events.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NavigationModule,
    AuthModule,
    ProfileModule,
    ScholarshipsModule,
    HttpClientModule,
    CandidateFormModule,
    CandidatesModule,
    StudentsModule,
    AdminModule,
    MessagesModule,
    EventsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    AuthService,
    ProfileService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
