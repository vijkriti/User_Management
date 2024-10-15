import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from '../guards/auth.guard';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { EmailSentComponent } from './email-sent/email-sent.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { AddUserComponent } from './add-user/add-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
  {path:'dashboard',component:DashboardComponent , canActivate: [AuthGuard]},
  {path:'forgot',component:ForgotPasswordComponent},
  {path:'sent',component:EmailSentComponent},
  {path:'reset',component:ResetPasswordComponent},
  {path:'add',component:AddUserComponent, canActivate: [AuthGuard]},
  { path: 'edit-user/:id', component: EditUserComponent , canActivate: [AuthGuard]},
  { path: 'change', component:ChangePasswordComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
