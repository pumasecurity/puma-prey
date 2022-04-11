import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ProjectTask } from '../models/projecttask';
import { Component, Inject } from '@angular/core';
import { ENVIRONMENT } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectTaskApiService {
  
  readonly baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + ENVIRONMENT.projectTaskApiUrl;
  }

  getByProjectId(projectId : string) {
    let tmp=this.httpClient.get<Array<ProjectTask>>(`${this.baseUrl}?projectId=${projectId}`)
    console.log(tmp)
    return this.httpClient.get<Array<ProjectTask>>(`${this.baseUrl}?projectId=${projectId}`).toPromise();
   
  }

  getByProjectTaskId(projecttaskId:string){
    
    let options = { responseType: 'blob' }
    return this.httpClient.get<ProjectTask>(`${this.baseUrl}/`+projecttaskId, {headers: options}).toPromise() ;
  }
  
  CreateProjectTask(project:ProjectTask){
    let options = { responseType: 'blob' }
    return this.httpClient.post<ProjectTask>(`${this.baseUrl}`,project, {headers: options}).toPromise() ;
  }
  DeleteProjectTask(projecttaskId:string){
    let options = { responseType: 'blob' }
    console.log(projecttaskId)
    return this.httpClient.delete(`${this.baseUrl}/`+projecttaskId, {headers: options}).toPromise() ;
  }

  UpdateProjectTask(id:string, projecttask:ProjectTask){
    return this.httpClient.put<ProjectTask>(`${this.baseUrl}/${id}`,projecttask).toPromise();
  }
}
