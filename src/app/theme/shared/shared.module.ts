import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { PerfectScrollbarConfigInterface, PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BreadcrumbModule } from './components/breadcrumb/breadcrumb.module';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { CardModule } from './components/card/card.module';
import { CardComponent } from './components/card/card.component';
import { ActiveReportsModule } from '@grapecity/activereports-angular';
import { ModalModule } from './components/modal/modal.module';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    SpinnerComponent,
  ],
  imports: [
    CommonModule,
    PerfectScrollbarModule,
    FormsModule,
    ReactiveFormsModule,
    BreadcrumbModule,
    CardModule,
    ActiveReportsModule,
    ModalModule
  ],
  exports: [
    CommonModule,
    PerfectScrollbarModule,
    FormsModule,
    ReactiveFormsModule,
    SpinnerComponent,
    BreadcrumbModule,
    CardModule,
    ActiveReportsModule,
    ModalModule
  ],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class SharedModule { }
