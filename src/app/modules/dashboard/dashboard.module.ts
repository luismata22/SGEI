import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { DashDefaultComponent } from './dash-default/dash-default.component';



@NgModule({
  declarations: [
    DashDefaultComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule
  ]
})
export class DashboardModule { }
