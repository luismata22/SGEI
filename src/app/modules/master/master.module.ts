import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentsListComponent } from './students/students-list/students-list.component';
import { MasterRoutingModule } from './master-routing-module';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { DataTablesModule } from 'angular-datatables';
import { StudentsNewComponent } from './students/students-new/students-new.component';



@NgModule({
  declarations: [
    StudentsListComponent,
    StudentsNewComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MasterRoutingModule,
    DataTablesModule
  ]
})
export class MasterModule { }
