import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProjectTaskRoutingModule } from './projecttask-routing.module';
import { CurrentProjectTaskComponent } from './components/current-projecttask/current-projecttask.component';
import { ProjectTaskCurrentItemComponent } from './components/projecttask-item/projecttask-item.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


@NgModule({
  declarations: [
    CurrentProjectTaskComponent,ProjectTaskCurrentItemComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ProjectTaskRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule
  ]
})
export class ProjectTaskModule { }
