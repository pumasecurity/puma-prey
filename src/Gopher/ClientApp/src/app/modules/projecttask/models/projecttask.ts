
export interface ProjectTask{
    id:string,
    description:string,
    isDone:boolean,
    name:string,
    date:Date,
    priority:number,
    projectId:string,
    tagIds:Array<string>
}
