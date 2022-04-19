import { Component, EventEmitter, Input, Output, OnInit, Inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar'
import { Project } from '../../models/project';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent implements OnInit {

  //@Input() project: Project;


  //constructor(
  //  private _snackBar: MatSnackBar,
  //  public dialogRef: MatDialogRef<DynamicFormDialogComponent>,
  //  @Inject(MAT_DIALOG_DATA) public data: DialogData
  //) {
  //  this.project = this.data;
  //}

  constructor(
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    //this.project = this.data;
  }

  ngOnInit() {
  }

  closeDialog() {
    //this.dialogRef.close();
  }

}

export interface DialogData {
  //project: Project
}

//todo: https://material.angular.io/components/dialog/overview