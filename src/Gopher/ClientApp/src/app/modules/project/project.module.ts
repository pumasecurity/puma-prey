import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './components/list/list.component';
import { CreateComponent } from './components/create/create.component';
import { CurrentComponent } from './components/current/current.component';
import { ProjectRoutingModule } from './project-routing.module';
import { ItemComponent } from './components/item/item.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProjectTaskItemComponent } from './components/projecttask-item/projecttask-item.component';
import { DeleteComponent } from './components/delete/delete.component';
import { DatePipePipe } from './pipes/date-pipe.pipe';
import { CreateProjectTaskComponent } from '../projecttask/components/create-projecttask/create-projecttask.component';
import { UpdateComponent } from '../projecttask/components/update/update.component';
import { ProjectUpdateComponent } from '../project/components/update/update.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProjectTaskMenuComponent } from './components/projecttask-menu/menu.component';
import { MaterialModule } from 'src/app/material/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';;
import { CreateDialogComponent } from './components/create-dialog/create-dialog.component'



@NgModule({
  declarations: [
    ListComponent,
    CreateComponent,
    CurrentComponent,
    ItemComponent,
    ProjectTaskItemComponent,
    CreateProjectTaskComponent,
    DeleteComponent,
    DatePipePipe,
    UpdateComponent,
    ProjectUpdateComponent,
    ProjectTaskMenuComponent
,
    CreateDialogComponent  ],
  imports: [
    CommonModule,
    ProjectRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    MaterialModule,
    FlexLayoutModule
  ]
})
export class ProjectModule { }
