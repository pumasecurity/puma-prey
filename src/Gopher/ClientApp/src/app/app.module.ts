import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ErrorComponent } from './shared/components/error/error.component';
import { SharedModule } from './shared/shared.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';


export const routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', label: 'Home', icon: 'home', hidden: false },
  { path: 'counter', component: CounterComponent, label: 'Counter' },
  { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard], label: "Fetch Data"},
  { path: 'project', loadChildren: () => import('./modules/project/project.module').then(m => m.ProjectModule), canActivate: [AuthorizeGuard], label: 'Projects' },
  { path: 'projecttask', loadChildren: () => import('./modules/projecttask/projecttask.module').then(m => m.ProjectTaskModule), canActivate: [AuthorizeGuard], label: 'Project Tasks', hidden: true },
  { path: '**', component: ErrorComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    SharedModule,
    RouterModule.forRoot(routes),
    FontAwesomeModule,
    BrowserAnimationsModule,    
    MaterialModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

