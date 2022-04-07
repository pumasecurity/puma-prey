import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectTask } from 'src/app/modules/projecttask/models/projecttask';
import { ProjectTaskApiService } from 'src/app/modules/projecttask/services/projecttask-api.service';
import { Project } from '../../models/project';
import { ProjectApiService } from '../../services/project-api.service';

@Component({
  selector: 'app-current',
  templateUrl: './current.component.html',
  styleUrls: ['./current.component.scss']
})
export class CurrentComponent  {
  @Output() showCreate : EventEmitter<void> = new EventEmitter<void>();

  @Output() create=false;
  @Output() projecttasks : ProjectTask[] = [];
  
  @Output() projecttask!: ProjectTask;
  @Output() projectId : string = "";
  @Output() project : Project = {
    id: '',
    title: '',
    description: '',
    userId: '',
    date: new Date(),
    projecttask: []
  };



  constructor(private projectService : ProjectApiService, 
              private routeService: ActivatedRoute,
              private projecttaskService : ProjectTaskApiService,
              private router : Router) { }

  async ngOnInit() {
   this.projectId = this.routeService.snapshot.paramMap.get('id') as string;
   this.project = await this.projectService.getById(this.projectId) as Project;
   this.projecttasks = await this.projecttaskService.getByProjectId(this.projectId) as Array<ProjectTask>
   console.log(this.projecttasks)
  }  
  async OnCreateClicked() {
    this.create = true;
  } 

  async OnCreation(bool : boolean) {
    this.create = false;
    if(bool) { 
      this.projecttasks = await this.projecttaskService.getByProjectId(this.projectId) as Array<ProjectTask>;
      console.log(this.projecttask)
    }
  }

  async OnBackClicked(){
    this.router.navigate(['/project/list']);
  }


}
