import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guard/auth.guard';
import { UsersListComponent } from './users/users-list/users-list.component';

const routes: Routes = [
  {
    path: 'users',
    canActivate: [AuthGuard],
    component: UsersListComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }
