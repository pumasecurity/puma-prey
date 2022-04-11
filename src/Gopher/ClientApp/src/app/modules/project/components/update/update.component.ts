import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Project } from '../../models/project';
import { ProjectApiService } from '../../services/project-api.service';

@Component({
  selector: 'app-project-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class ProjectUpdateComponent implements OnInit {
  @Output() closeUpdate : EventEmitter<boolean> = new EventEmitter<boolean>();

  @Input() project : Project = {
    id: '',
    userID: '',
    title: '',
    ProjectTasks: [],
    description:'',
    date:new Date(),
  };

  updateForm: FormGroup;

  
  get title() { return this.updateForm.get('title'); }
  get description() { return this.updateForm.get('description'); }
  get date() { return this.updateForm.get('date') }
  
  
  constructor(private projectApiService: ProjectApiService,
             private router: Router) {
    this.updateForm = new FormGroup({
      description: new FormControl(null, [Validators.required]),
      title: new FormControl(null, [Validators.required]),
      date: new FormControl(null, [Validators.required]),
    })
  }
  ngOnInit(): void {
    this.title?.setValue(this.project.title);
    this.description?.setValue(this.project.description);
    this.date?.setValue(this.project.date);    
  }

  async OnSubmit(): Promise<void> {
    console.log(this.project)
    this.updateForm.value['id'] = this.project.id;
    this.updateForm.value['userID'] = this.project.userID;
    console.log('edit');
    console.log(this.project.ProjectTasks);
    console.log([]);
    this.updateForm.value['ProjectTasks'] = this.project.ProjectTasks; //TODO: tasks not carried over??
    this.updateForm.value['ProjectTasks'] = [];
    console.log(this.updateForm.value);
    await this.projectApiService.UpdateProject(this.updateForm.value['id'],this.updateForm.value);
    
    window.location.reload();
    this.OnClick(true);
    
  }

  async OnClick(bool : boolean = false) {
    if(bool) this.closeUpdate.emit(true);
    else this.closeUpdate.emit(false);
  }


}
