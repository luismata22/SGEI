import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthSigninComponent } from './modules/authentication/auth-signin/auth-signin.component';
import { AdminComponent } from './theme/layout/admin/admin.component';
import { AuthComponent } from './theme/layout/auth/auth.component';
//import {AuthComponent} from './theme/layout/auth/auth.component';

const routes: Routes = [
  {
    path: '',
    component: AuthSigninComponent,
  },
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
      },
      {
        path: '',
        loadChildren: () => import('./modules/dashboard/dashboard.module').then(module => module.DashboardModule)
      },
       {
        path: '',
        loadChildren: () => import('./modules/security/security.module').then(module => module.SecurityModule)
      },
      {
        path: '',
        loadChildren: () => import('./modules/master/master.module').then(module => module.MasterModule)
      },
      /*{
        path: '',
        //loadChildren: () => import('./demo/pages/form-elements/form-elements.module').then(module => module.FormElementsModule)
      },
      {
        path: '',
        //loadChildren: () => import('./demo/pages/tables/tbl-bootstrap/tbl-bootstrap.module').then(module => module.TblBootstrapModule)
      },
      {
        path: '',
        //loadChildren: () => import('./demo/pages/core-chart/core-chart.module').then(module => module.CoreChartModule)
      },
      {
        path: '',
        //loadChildren: () => import('./demo/pages/core-maps/core-maps.module').then(module => module.CoreMapsModule)
      },
      {
        path: '',
        //loadChildren: () => import('./demo/pages/sample-page/sample-page.module').then(module => module.SamplePageModule)
      } */
    ]
  },
  {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./modules/authentication/authentication.module').then(module => module.AuthenticationModule)
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
