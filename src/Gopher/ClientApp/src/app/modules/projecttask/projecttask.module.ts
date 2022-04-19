import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProjectTaskRoutingModule } from './projecttask-routing.module';
import { CurrentProjectTaskComponent } from './components/current-projecttask/current-projecttask.component';
import { ProjectTaskDetailItemComponent } from './components/detail/detail.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from 'src/app/material/material.module';



@NgModule({
  declarations: [
    CurrentProjectTaskComponent, ProjectTaskDetailItemComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ProjectTaskRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    FlexLayoutModule,
    MaterialModule
  ]
})
export class ProjectTaskModule { }
