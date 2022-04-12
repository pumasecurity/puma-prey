import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProjectTask } from '../../models/projecttask';
import { ProjectTaskApiService } from '../../services/projecttask-api.service';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {
  @Output() closeUpdate : EventEmitter<boolean> = new EventEmitter<boolean>();

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

  updateForm: FormGroup;

  
  get name() { return this.updateForm.get('name'); }
  get description() { return this.updateForm.get('description'); }
  get date() { return this.updateForm.get('date')}
  get priority() { return this.updateForm.get('priority'); }
  get isDone() { return this.updateForm.get('isDone'); }
  
  constructor(private projecttaskService: ProjectTaskApiService,
             private router: Router) {
    this.updateForm = new FormGroup({
      name: new FormControl(null,  [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      date: new FormControl(this.projecttask.date.toISOString().split("T")[0], [Validators.required]),
      priority: new FormControl(null, [Validators.required]),
      isDone: new FormControl(null, [Validators.required]),
    })
  }
  ngOnInit(): void {
    this.name?.setValue(this.projecttask.name);
    this.description?.setValue(this.projecttask.description);
    this.date?.setValue(this.projecttask.date);
    this.priority?.setValue(this.projecttask.priority);
    this.isDone?.setValue(this.projecttask.isDone);
  }

  async OnSubmit(): Promise<void> {
    
    let tagArray: Array<string> = []; //TODO

    this.updateForm.value['id'] = this.projecttask.id;
    
    this.updateForm.value['projectID'] = this.projecttask.projectID;
    
    //this.updateForm.value['isDone'] = false;
    
    this.updateForm.value['tagIDs'] =tagArray;

    await this.projecttaskService.UpdateProjectTask(this.updateForm.value['id'],this.updateForm.value);
    
    window.location.reload();
    this.OnClick(true);
    
  }

  async OnClick(bool : boolean = false) {
    if(bool) this.closeUpdate.emit(true);
    else this.closeUpdate.emit(false);
  }


}
