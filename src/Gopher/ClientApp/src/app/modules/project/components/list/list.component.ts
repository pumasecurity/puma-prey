import { Component, OnInit, Output } from '@angular/core';
import { AuthorizeService } from '../../../../../api-authorization/authorize.service';
import { Project } from '../../models/project';
import { ProjectApiService } from '../../services/project-api.service';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  @Output()
  projects: Array<Project> = [];
  @Output()
  create = false;
  @Output()
  public userId: string;

  constructor(private projectService: ProjectApiService, private authorizeService: AuthorizeService) {
    this.userId = '';
  }

  async ngOnInit() {

    console.log('onsubmit')
    this.authorizeService.getUser()
      .subscribe(async user => {
          console.log(user);
          this.userId = user!.name!;
        this.projects = await this.projectService.getByUserId(this.userId);
        console.log(this.projects.length)
        },
        error => {
          console.warn(error.responseText)
          console.log({ error })
        })
  }
  
  async onUpdateProject(bool: boolean) {
    console.log("onUpdateProject:" + bool)
    this.create = false;
    if (bool) {
      console.log(this.userId);
      console.log("fetching new projects");
      this.projects = await this.projectService.getByUserId(this.userId) as Array<Project>;
      console.log(this.projects.length)
    }
  }
  async OnCreateClicked() {
    this.create = true;
  }

  async OnCreation(bool: boolean) {
    console.log("OnCreation:" + bool)
    this.create = false;
    if (bool) {
      console.log(this.userId);
      console.log("fetching new projects");
      this.projects = await this.projectService.getByUserId(this.userId) as Array<Project>;
      console.log(this.projects.length)
    }
  }

  async onDeleteProject(index: string) {
    await this.projectService.DeleteProject(index);
    this.projects = await this.projectService.getByUserId(this.userId) as Array<Project>;
  }
}
