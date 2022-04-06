import { ProjectTask } from "../../projecttask/models/projecttask";

export interface Project{
    id:string,
    userId:string,
    title:string,
    projecttask:Array<ProjectTask>,
    date:Date
}
