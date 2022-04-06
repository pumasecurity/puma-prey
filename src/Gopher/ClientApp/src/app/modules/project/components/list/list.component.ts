import { Component, OnInit, Output, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectApiService } from '../../services/project-api.service';
import { AuthorizeService } from '../../../../../api-authorization/authorize.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  
  @Output() projects : Array<Project> = [];
  @Output() create = false;
  public userId: string = "";

  constructor(private projectService : ProjectApiService, private authorizeService: AuthorizeService) { }

  async ngOnInit() {

    console.log('onsubmit')
    this.authorizeService.getUser()
      .subscribe(async user => {
        console.log(user);
        this.userId = user!.name!;
        this.projects = await this.projectService.getByUserId(this.userId);
      }, error => {
        console.warn(error.responseText)
        console.log({ error })
      })
  }
  
  async OnCreateClicked() {
    this.create = true;
  } 

  async OnCreation(bool : boolean) {
    this.create = false;
    if (bool) {
      console.log(this.userId);
      this.projects = await this.projectService.getByUserId(this.userId) as Array<Project>;
    }
  }

}
