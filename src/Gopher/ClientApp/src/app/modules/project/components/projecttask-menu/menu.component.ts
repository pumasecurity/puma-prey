import { Component, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectTask } from 'src/app/modules/projecttask/models/projecttask';
import { ProjectTaskApiService } from 'src/app/modules/projecttask/services/projecttask-api.service';
import { faPenToSquare, faTrash, faEye } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-projecttask-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class ProjectTaskMenuComponent implements OnInit {

  faPenToSquare = faPenToSquare;
  faTrash = faTrash;
  faEye = faEye;

  @Output() update: boolean = false;
  @Input() projecttask : ProjectTask = {
    id:'',
    description:'',
    isDone:false,
    name:'',
    date:new Date(),
    priority:0,
    projectID:'',
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
  async OnEditClicked() {
    this.update=true;
  }
  async OnViewClicked() {
    console.log(this.projecttask)
    this.router.navigate([`/projecttask/detail/${this.projecttask.id}/`]);
  }
  async OnUpdate(bool : boolean){
    this.update=false;
  }
}
