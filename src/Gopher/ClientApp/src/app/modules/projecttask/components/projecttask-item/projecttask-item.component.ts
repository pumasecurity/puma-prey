import { Component, OnInit, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectTask } from '../../models/projecttask';
import { ProjectTaskApiService } from '../../services/projecttask-api.service';
import { faPenToSquare, faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-projecttask-item',
  templateUrl: './projecttask-item.component.html',
  styleUrls: ['./projecttask-item.component.scss']
})
export class ProjectTaskCurrentItemComponent implements OnInit {

  faPenToSquare = faPenToSquare;
  faTrash = faTrash;

  projecttask : ProjectTask = {
    id:'',
    description:'',
    isDone:false,
    name:'',
    date:new Date(),
    priority:0,
    projectID:'',
    tagIds:Array<string>()
  };

  projecttitle: string = '';
  projecttaskId:string='';
  constructor(private projecttaskService : ProjectTaskApiService, private routeService: ActivatedRoute, private router : Router) { }

  async ngOnInit() {
    this.projecttaskId=this.routeService.snapshot.paramMap.get('id') as string;

    try{
      this.projecttask = await this.projecttaskService.getByProjectTaskId(this.projecttaskId) as ProjectTask;
      
    }
    catch{
      this.router.navigate(["/error"]);
    }
  }

}

