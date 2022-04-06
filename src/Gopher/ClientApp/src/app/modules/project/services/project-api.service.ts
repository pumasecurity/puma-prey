import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Project } from '../models/project';
import { environment } from "../../../../environments/environment";
import { Component, Inject } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class ProjectApiService {
  
  readonly baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + environment.projectApiUrl;
  }

  getById(id: string) {
    return this.httpClient.get<Project>(`${this.baseUrl}/${id}`).toPromise();
  }

  getByUserId(userId : string) {
    return this.httpClient.get<Array<Project>>(`${this.baseUrl}?userId=${userId}`).toPromise() ;
  }
  CreateProject(project:Project){
    let options = { responseType: 'blob' }
    return this.httpClient.post<Project>(`${this.baseUrl}/add`,project, {headers: options}).toPromise() ;
  }
  DeleteProject(projectId:string){
    let options = { responseType: 'blob' }
    return this.httpClient.delete(`${this.baseUrl}/`+projectId, {headers: options}).toPromise() ;
  }
}
