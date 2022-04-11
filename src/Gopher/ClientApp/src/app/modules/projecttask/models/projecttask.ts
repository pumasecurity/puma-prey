
export interface ProjectTask{
    id:string,
    description:string,
    isDone:boolean,
    name:string,
    date:Date,
    priority:number,
    projectID:string,
    tagIds:Array<string>
}
