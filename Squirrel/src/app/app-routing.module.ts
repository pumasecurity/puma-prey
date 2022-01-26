import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationGuard } from './authentication-guard.guard';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path: 'login',
    //loadChildren: () => import('./login/login.module').then(m => m.LoginModule),
    component: LoginComponent
  },
  {
    path: '',
    //loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
    component: DashboardComponent,
    canActivate: [AuthenticationGuard]    
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
