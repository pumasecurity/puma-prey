import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Router } from '@angular/router';

import { ProjectTask } from 'src/app/modules/projecttask/models/projecttask';

@Component({
  selector: 'app-projecttask-item',
  templateUrl: './projecttask-item.component.html',
  styleUrls: ['./projecttask-item.component.scss']
})
export class ProjectTaskItemComponent implements OnInit{


  @Input() projecttitle: string = '';
  @Output() showCreate : EventEmitter<void> = new EventEmitter<void>();

  @Output() create=false;
  @Output() projecttaskOut!: ProjectTask;
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
  
  constructor(private router : Router) { }

  ngOnInit(): void {
 
  }

  OnClick():void{
    console.log(this.projecttask.id)
    this.router.navigate([`/projecttask/${this.projecttask.id}`]);
  }

  async OnCreation(bool : boolean) {
    this.create = false;
  }

}
