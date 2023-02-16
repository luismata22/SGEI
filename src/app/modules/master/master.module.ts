import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentsListComponent } from './students/students-list/students-list.component';
import { MasterRoutingModule } from './master-routing-module';
import { SharedModule } from 'src/app/theme/shared/shared.module';



@NgModule({
  declarations: [
    StudentsListComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MasterRoutingModule
  ]
})
export class MasterModule { }
