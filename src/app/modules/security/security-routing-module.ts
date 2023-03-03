import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guard/auth.guard';
import { ProfileComponent } from './profile/profile.component';
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
  },
  {
    path: 'profile',
    canActivate: [AuthGuard],
    component: ProfileComponent
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }
