import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guard/auth.guard';
import { StudentProfileComponent } from './students/student-profile/student-profile.component';
import { StudentsListComponent } from './students/students-list/students-list.component';
import { StudentsNewComponent } from './students/students-new/students-new.component';

const routes: Routes = [
  {
    path: 'students',
    canActivate: [AuthGuard],
    component: StudentsListComponent
  },
  {
    path: 'students-new/:id',
    canActivate: [AuthGuard],
    component: StudentsNewComponent
  },
  {
    path: 'student-profile/:id',
    canActivate: [AuthGuard],
    component: StudentProfileComponent
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
