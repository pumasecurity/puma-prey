import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProjectApiService } from 'src/app/modules/project/services/project-api.service';
import { ProjectTaskApiService } from '../../services/projecttask-api.service';

@Component({
  selector: 'app-create-projecttask',
  templateUrl: './create-projecttask.component.html',
  styleUrls: ['./create-projecttask.component.scss']
})
export class CreateProjectTaskComponent {
  @Output() closeCreate : EventEmitter<boolean> = new EventEmitter<boolean>();

  @Input() projectId : string = "";
  createForm: FormGroup;
  get name() { return this.createForm.get('name'); }
  get description() { return this.createForm.get('description'); }
  get date() { return this.createForm.get('date')}
  get priority() { return this.createForm.get('priority'); }

  //tagArray: Array<string> = ['6FB8EFBB-CA6D-44C3-BDF5-6FB7D95FC384'];
  tagArray: Array<string> = [];

  constructor(private projecttaskService: ProjectTaskApiService,
             private router: Router) {
    this.createForm = new FormGroup({
      name: new FormControl(null,  [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      date: new FormControl(null, [Validators.required]),
      priority: new FormControl(null, [Validators.required]),
      tagIds:new FormControl('', [Validators.required, Validators.pattern(/^\S*$/)])
    })
  }

  async OnSubmit(): Promise<void> {
    
    
    this.createForm.value['isDone'] = false;
    this.createForm.value['projectID'] = this.projectId;
    this.createForm.value['tagIDs'] = this.tagArray;
    this.createForm.value['tagIDs'].forEach((element: string) => element.replace(/\s/g, ""));
    //this.createForm.value['tagIDs'][0].replace(/\s/g, "");

    await this.projecttaskService.CreateProjectTask(this.createForm.value);
    this.OnClick(true);
  }

  async OnClick(bool : boolean = false) {
    if(bool) this.closeCreate.emit(true);
    else this.closeCreate.emit(false);
  }
}
