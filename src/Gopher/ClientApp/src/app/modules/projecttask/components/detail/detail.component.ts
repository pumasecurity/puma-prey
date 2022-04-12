import { Component, OnInit, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectTask } from '../../models/projecttask';
import { ProjectTaskApiService } from '../../services/projecttask-api.service';
import { faPenToSquare, faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-projecttask-item-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class ProjectTaskDetailItemComponent implements OnInit {

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

    try {
      console.log('detail task load')
      this.projecttask = await this.projecttaskService.getByProjectTaskId(this.projecttaskId) as ProjectTask;
      
    }
    catch{
      this.router.navigate(["/error"]);
      console.log('detail task load error')
    }
  }

}

