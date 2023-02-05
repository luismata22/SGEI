import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthSigninComponent } from './auth-signin/auth-signin.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
  {
    path: 'login',
    component: AuthSigninComponent
  },
  {
    path: 'reset-password',
    component: ResetPasswordComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }