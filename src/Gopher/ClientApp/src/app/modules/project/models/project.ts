import { ProjectTask } from "../../projecttask/models/projecttask";

export interface Project{
    id:string,
    userID:string,
    title: string,
    description: string,
    ProjectTasks: Array<ProjectTask>,
    //projecttasks: Array<ProjectTask>,
    date:Date
}
