import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Project } from '../../models/project';
import { ProjectApiService } from '../../services/project-api.service';
import { AuthorizeService } from '../../../../../api-authorization/authorize.service';
import { BehaviorSubject, concat, from, Observable } from 'rxjs';
import { filter, map, mergeMap, take, tap } from 'rxjs/operators';


@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent {
  @Output() closeCreate : EventEmitter<boolean> = new EventEmitter<boolean>();

  
  createForm: FormGroup;
  get title() { return this.createForm.get('title'); }
  get description() { return this.createForm.get('description'); }
  
  constructor(private projectService: ProjectApiService,
    private router: Router, private authorizeService: AuthorizeService) {
    this.createForm = new FormGroup({
      title: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required])
    });
  }

  async onSubmit(): Promise<void> {

    console.log('onsubmit');
    this.onClick(true);
    this.authorizeService.getUser()
      .subscribe(async user => {
          console.log(user);
          
          this.createForm.value['userId'] = user!.name!;
          await this.projectService.CreateProject(this.createForm.value);
        },
        error => {
          console.warn(error.responseText);
          console.log({ error });
        });
  }

  async onClick(bool : boolean = false) {
    if (bool)
      this.closeCreate.emit(true);
    else
      this.closeCreate.emit(false);
  }
}
