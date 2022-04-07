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
    userId: '',
    date: new Date(),
    projecttask: []
  };

  @Output()
  onDelete: EventEmitter<void> = new EventEmitter();

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

  public deleteProject() {
    this.onDelete.emit();
  }
  clickedOutsideMenu() {
    this.disableContextMenu();
  }

  disableContextMenu() {
    this.contextmenu = false;
  }

  public deleteImage() {
    this.onDelete.emit();
  }
}
