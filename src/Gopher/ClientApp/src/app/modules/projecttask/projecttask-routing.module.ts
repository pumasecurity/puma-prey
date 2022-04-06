import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { ErrorComponent } from "src/app/shared/components/error/error.component";
import { CurrentComponent } from "../project/components/current/current.component";
import { ProjectTaskCurrentItemComponent } from "./components/projecttask-item/projecttask-item.component";


const routes: Routes = [
  { path: ':id', component: ProjectTaskCurrentItemComponent, canActivate: [AuthorizeGuard]},
  {path: '**', component: ErrorComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectTaskRoutingModule { }
