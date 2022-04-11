import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectTask } from 'src/app/modules/projecttask/models/projecttask';
import { ProjectTaskApiService } from 'src/app/modules/projecttask/services/projecttask-api.service';
import { Project } from '../../models/project';
import { ProjectApiService } from '../../services/project-api.service';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-current',
  templateUrl: './current.component.html',
  styleUrls: ['./current.component.scss']
})
export class CurrentComponent  {
  faAngleLeft = faAngleLeft;
  @Output() showCreate : EventEmitter<void> = new EventEmitter<void>();

  @Output() create=false;
  @Output() projecttasks : ProjectTask[] = [];
  
  @Output() projecttask!: ProjectTask;
  @Output() projectId: string = "";
  @Output() projecttitle: string = "";
  @Output() project : Project = {
    id: '',
    title: '',
    description: '',
    userID: '',
    date: new Date(),
    ProjectTasks: []
  };



  constructor(private projectService : ProjectApiService, 
              private routeService: ActivatedRoute,
              private projecttaskService : ProjectTaskApiService,
              private router : Router) { }

  async ngOnInit() {
    this.projectId = this.routeService.snapshot.paramMap.get('id') as string;
    this.project = await this.projectService.getById(this.projectId) as Project;
    this.projecttitle = this.project.title;
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
