import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guard/auth.guard';
import { StudentsListComponent } from './students/students-list/students-list.component';

const routes: Routes = [
  {
    path: 'students',
    canActivate: [AuthGuard],
    component: StudentsListComponent
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
