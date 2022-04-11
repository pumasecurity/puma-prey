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
import { ContextmenuComponent } from './components/contextmenu/contextmenu.component';
import { UpdateComponent } from '../projecttask/components/update/update.component';
import { ProjectUpdateComponent } from '../project/components/update/update.component';
import { ProjectContextmenuComponent } from './components/project-contextmenu/project-contextmenu.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


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
    ContextmenuComponent,
    UpdateComponent,
    ProjectUpdateComponent,
    ProjectContextmenuComponent
    
  ],
  imports: [
    CommonModule,
    ProjectRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule
  ]
})
export class ProjectModule { }
