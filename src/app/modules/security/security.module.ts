import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersListComponent } from './users/users-list/users-list.component';
import { SecurityRoutingModule } from './security-routing-module';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { ActiveReportsModule } from '@grapecity/activereports-angular';
import { RoleListComponent } from './roles/role-list/role-list.component';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';
import { DataTablesModule } from 'angular-datatables';
import { ProfileComponent } from './profile/profile.component';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    UsersListComponent,
    RoleListComponent,
    ProfileComponent,
  ],
  imports: [
    CommonModule,
    SecurityRoutingModule,
    SharedModule,
    AngularMultiSelectModule,
    DataTablesModule,
    NgbTooltipModule
  ],
})
export class SecurityModule { }
