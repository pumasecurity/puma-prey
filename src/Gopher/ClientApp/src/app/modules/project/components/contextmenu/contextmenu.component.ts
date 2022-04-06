import { Component, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectTask } from 'src/app/modules/projecttask/models/projecttask';
import { ProjectTaskApiService } from 'src/app/modules/projecttask/services/projecttask-api.service';

@Component({
  selector: 'app-contextmenu',
  templateUrl: './contextmenu.component.html',
  styleUrls: ['./contextmenu.component.scss']
})
export class ContextmenuComponent implements OnInit {

  @Output() update : boolean = false;
  @Input() x=0;
  @Input() y=0;
  @Input() projecttask : ProjectTask = {
    id:'',
    description:'',
    isDone:false,
    name:'',
    date:new Date(),
    priority:0,
    projectId:'',
    tagIds:Array<string>()
  }; 

  constructor(public projecttaskService:ProjectTaskApiService, private router : Router) { }

  ngOnInit(): void {
    console.log(this.projecttask)
  }

  async onDeleteClick() {
    console.log(this.projecttask);
    await this.projecttaskService.DeleteProjectTask(this.projecttask.id);
    window.location.reload();
  }
  async OnEditClicked(){
    this.update=true;
  }
  async OnUpdate(bool : boolean){
    this.update=false;
  }
}
