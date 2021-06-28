import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersListComponent } from './users/users-list/users-list.component';
import { SecurityRoutingModule } from './security-routing-module';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { ActiveReportsModule } from '@grapecity/activereports-angular';



@NgModule({
  declarations: [
    UsersListComponent
  ],
  imports: [
    CommonModule,
    SecurityRoutingModule,
    SharedModule,
    
  ],
})
export class SecurityModule { }
