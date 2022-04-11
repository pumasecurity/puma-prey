import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectTask } from '../../models/projecttask';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-current-projecttask',
  templateUrl: './current-projecttask.component.html',
  styleUrls: ['./current-projecttask.component.scss']
})
export class CurrentProjectTaskComponent {

  faAngleLeft = faAngleLeft;

   sliceOptions = {
    end: 30,
    start: 0,
    default: 100
  };

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

  @Input() projecttitle: string = '';

  expand:boolean=false;
  constructor(private router:Router) { }

  onExpandText(evt:any): void{ 
    if(this.expand==true){

      this.sliceOptions.end = this.sliceOptions.end?
      this.projecttask.description.length:this.sliceOptions.default;
      this.expand=false;
    }
    else{
      this.sliceOptions.end = this.sliceOptions.end?
      30:this.sliceOptions.default;
      this.expand=true;
    }
 
   }

  OnBackClicked() {
    console.log('onback');
    console.log(this.projecttask);
     this.router.navigate([`/project/detail/${this.projecttask.projectID}`]);
  }

}
