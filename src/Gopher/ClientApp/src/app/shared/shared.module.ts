import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorComponent } from './components/error/error.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    ErrorComponent,
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: []
})
export class SharedModule { }
