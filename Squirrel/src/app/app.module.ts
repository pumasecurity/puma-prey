import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { ApiModule, BASE_PATH } from 'projects/coyote-swagger-client/src';
import { WeatherComponent } from './weather/weather.component';
import { environment } from 'src/environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AuthenticationInterceptor } from './authentication.interceptor';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginModule } from './login/login.module';


@NgModule({
  declarations: [
    AppComponent,
    //RegisterComponent,
    //ProfileComponent,
    WeatherComponent
  ],
  imports: [
    BrowserModule, 
    CommonModule,
    FormsModule,

    HttpClientModule,
    ApiModule, 
    AppRoutingModule,        
    LoginModule
  ],
  providers: [
    AuthenticationInterceptor, 
    { provide: BASE_PATH, useValue: environment.API_BASE_PATH}],
  bootstrap: [AppComponent]
})
export class AppModule { }

