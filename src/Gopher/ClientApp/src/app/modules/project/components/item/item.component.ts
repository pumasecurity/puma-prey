import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from '../../models/project';
import { faPenToSquare, faTrash, faListCheck } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss']
})
export class ItemComponent {
  faPenToSquare = faPenToSquare;
  faTrash = faTrash;
  faListCheck = faListCheck;

  @Input()
  project: Project = {
    id: '',
    title: '',
    description: '',
    userID: '',
    date: new Date(),
    ProjectTasks: []
  };

  @Output()
  onDelete: EventEmitter<void> = new EventEmitter();

  @Output()
  onUpdate: EventEmitter<void> = new EventEmitter();
  @Output()
  update: boolean = false;

  contextmenu = false;
  contextmenuX = 0;
  contextmenuY = 0;

  constructor(private router: Router) {}


  OnClick(): void {
    this.router.navigate([`/project/detail/${this.project.id}`]);
  }

  onRightClick(event: any, project: Project) {
    this.contextmenuX = event.clientX
    this.contextmenuY = event.clientY
    this.project = project;
    this.contextmenu = true;
    event.preventDefault()
  }

  clickedOutsideMenu() {
    this.disableContextMenu();
  }

  disableContextMenu() {
    this.contextmenu = false;
  }

  public deleteProject() {
    this.onDelete.emit();
  }

  public updateProject() {
    this.update = true;
    //this.onUpdate.emit();
  }

  public onUpdateProject(bool: boolean) {
    console.log("OnCreation:" + bool)
    this.update = false;
    this.onUpdate.emit();
  }

}
