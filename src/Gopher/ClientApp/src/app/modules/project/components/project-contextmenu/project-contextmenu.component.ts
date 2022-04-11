import { Component, Input, OnInit, Output } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectApiService } from '../../services/project-api.service';

@Component({
  selector: 'app-project-contextmenu',
  templateUrl: './project-contextmenu.component.html',
  styleUrls: ['./project-contextmenu.component.scss']
})
export class ProjectContextmenuComponent implements OnInit {

  @Output() update : boolean = false;
  @Input() x=0;
  @Input() y=0;
  @Input() project : Project = {
    id: '',
    title: '',
    description: '',
    userID: '',
    date: new Date(),
    ProjectTasks: []
  };
  constructor(private projectApiService:ProjectApiService) { }

  ngOnInit(): void {
  }
  
  async OnEditClicked(){
    this.update=true;
  }
  async OnUpdate(bool : boolean){
    this.update=false;
  }
}
