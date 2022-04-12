import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { ErrorComponent } from "src/app/shared/components/error/error.component";
import { CurrentComponent } from "../project/components/current/current.component";
import { ProjectTaskDetailItemComponent } from './components/detail/detail.component';


const routes: Routes = [
  { path: 'detail/:id', component: ProjectTaskDetailItemComponent, canActivate: [AuthorizeGuard]},
  {path: '**', component: ErrorComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectTaskRoutingModule { }
