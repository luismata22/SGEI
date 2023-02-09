import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guard/auth.guard';
import { RoleListComponent } from './roles/role-list/role-list.component';
import { UsersListComponent } from './users/users-list/users-list.component';

const routes: Routes = [
  {
    path: 'users',
    canActivate: [AuthGuard],
    component: UsersListComponent
  },
  {
    path: 'roles',
    canActivate: [AuthGuard],
    component: RoleListComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }
