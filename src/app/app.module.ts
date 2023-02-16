import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminComponent } from './theme/layout/admin/admin.component';
import { NavigationComponent } from './theme/layout/admin/navigation/navigation.component';
import { NavContentComponent } from './theme/layout/admin/navigation/nav-content/nav-content.component';
import { NavCollapseComponent } from './theme/layout/admin/navigation/nav-content/nav-collapse/nav-collapse.component';
import { NavGroupComponent } from './theme/layout/admin/navigation/nav-content/nav-group/nav-group.component';
import { NavItemComponent } from './theme/layout/admin/navigation/nav-content/nav-item/nav-item.component';
import { NavBarComponent } from './theme/layout/admin/nav-bar/nav-bar.component';
import { NavLeftComponent } from './theme/layout/admin/nav-bar/nav-left/nav-left.component';
import { NavRightComponent } from './theme/layout/admin/nav-bar/nav-right/nav-right.component';
import { NavSearchComponent } from './theme/layout/admin/nav-bar/nav-left/nav-search/nav-search.component';
import { ConfigurationComponent } from './theme/layout/admin/configuration/configuration.component';
import { SharedModule } from './theme/shared/shared.module';
import { NgbButtonsModule, NgbDropdownModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { NavigationItem } from './theme/layout/admin/navigation/navigation';
import { AuthComponent } from './theme/layout/auth/auth.component';
import { ToggleFullScreenDirective } from './theme/shared/full-screen/toggle-full-screen';
import { ActiveReportsModule } from '@grapecity/activereports-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';


@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    NavigationComponent,
    NavContentComponent,
    NavCollapseComponent,
    NavGroupComponent,
    NavItemComponent,
    NavBarComponent,
    NavLeftComponent,
    NavRightComponent,
    NavSearchComponent,
    ConfigurationComponent,
    AuthComponent,
    ToggleFullScreenDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    HttpClientModule,
    NgbDropdownModule,
    NgbTooltipModule,
    NgbButtonsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    AngularMultiSelectModule
  ],
  providers: [NavigationItem],
  bootstrap: [AppComponent]
})
export class AppModule { }
