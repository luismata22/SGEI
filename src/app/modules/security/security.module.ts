import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersListComponent } from './users/users-list/users-list.component';
import { SecurityRoutingModule } from './security-routing-module';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { ActiveReportsModule } from '@grapecity/activereports-angular';
import { UserNewComponent } from './users/user-new/user-new.component';
import { RoleListComponent } from './roles/role-list/role-list.component';
import { RoleNewComponent } from './roles/role-new/role-new.component';



@NgModule({
  declarations: [
    UsersListComponent,
    UserNewComponent,
    RoleListComponent,
    RoleNewComponent
  ],
  imports: [
    CommonModule,
    SecurityRoutingModule,
    SharedModule,
  ],
})
export class SecurityModule { }
