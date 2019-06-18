import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './guards/auth.guard';
import { AdminSystemDetailsComponent } from './admin/admin-system-details/admin-system-details.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { LoginComponent } from './auth/login/login.component';
import { NewPasswordComponent } from './auth/new-password/new-password.component';
import { RegisterComponent } from './auth/register/register.component';
import { FormComponent } from './candidate-form/form/form.component';
import { ParentsSalaryPageComponent } from './candidate-form/parents-salary-page/parents-salary-page.component';
import { StatusComponent } from './candidate-form/status/status.component';
import { CandidatesComponent } from './candidates/candidates/candidates.component';
import { EditCandidateComponent } from './candidates/edit-candidate/edit-candidate.component';
import { NewMessageComponent } from './messages/new-message/new-message.component';
import { ProfileComponent } from './profile/profile/profile.component';
import { NewScholarshipsComponent } from './scholarships/new-scholarships/new-scholarships.component';
import { ScholarshipsComponent } from './scholarships/scholarships/scholarships.component';
import { EditStudentComponent } from './students/edit-student/edit-student.component';
import { StudentsComponent } from './students/students/students.component';
import { EventsComponent } from './events/events/events.component';
import { NewEventComponent } from './events/new-event/new-event.component';

const routes: Routes = [
  { path: '', redirectTo: 'profile', pathMatch: 'full' },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'form', component: FormComponent, canActivate: [AuthGuard] },
  { path: 'status', component: StatusComponent, canActivate: [AuthGuard] },
  { path: 'parentsSalary', component: ParentsSalaryPageComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgotPassword', component: ForgotPasswordComponent },
  { path: 'newPassword/:id', component: NewPasswordComponent },
  { path: 'scholarships', component: ScholarshipsComponent, canActivate: [AuthGuard] },
  { path: 'scholarships/:id', component: NewScholarshipsComponent, canActivate: [AuthGuard] },
  { path: 'candidates', component: CandidatesComponent, canActivate: [AuthGuard] },
  { path: 'candidates/:id', component: EditCandidateComponent, canActivate: [AuthGuard] },
  { path: 'students', component: StudentsComponent, canActivate: [AuthGuard] },
  { path: 'students/:id', component: EditStudentComponent, canActivate: [AuthGuard] },
  { path: 'adminSystemDetails', component: AdminSystemDetailsComponent, canActivate: [AuthGuard] },
  { path: 'messages', component: NewMessageComponent, canActivate: [AuthGuard] },
  { path: 'events', component: EventsComponent, canActivate: [AuthGuard] },
  { path: 'events/:id', component: NewEventComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
