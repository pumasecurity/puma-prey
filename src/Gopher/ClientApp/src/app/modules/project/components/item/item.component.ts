import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from '../../models/project';
import { Input } from '@angular/core';




@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss']
})
export class ItemComponent {

  @Input() project : Project = {
    id: '',
    title: '',
    userId: '',
    date: new Date(),
    projecttask: []
  };
  
  contextmenu = false;
  contextmenuX = 0;
  contextmenuY = 0;
  
  constructor(private router : Router) { }

 
  OnClick() : void {
    this.router.navigate([`/project/detail/${this.project.id}`]);
  }
  onRightClick(event:any,project:Project) {
    this.contextmenuX = event.clientX
    this.contextmenuY = event.clientY
    this.project=project;
    this.contextmenu = true;
    event.preventDefault()
  }
  clickedOutsideMenu() {
    this.disableContextMenu();
 }
  disableContextMenu() {
    this.contextmenu = false;
  }
}
