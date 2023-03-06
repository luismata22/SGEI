import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentsListComponent } from './students/students-list/students-list.component';
import { MasterRoutingModule } from './master-routing-module';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { DataTablesModule } from 'angular-datatables';
import { StudentsNewComponent } from './students/students-new/students-new.component';
import { AngularFileUploaderModule } from 'angular-file-uploader';
import { FileUploadModule } from 'ng2-file-upload';
import { StudentProfileComponent } from './students/student-profile/student-profile.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    StudentsListComponent,
    StudentsNewComponent,
    StudentProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MasterRoutingModule,
    DataTablesModule,
    AngularFileUploaderModule,
    FileUploadModule,
    NgbModule
  ]
})
export class MasterModule { }
