import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CreateComponent } from "./components/create/create.component";
import { CurrentComponent } from "./components/current/current.component";
import { ListComponent } from "./components/list/list.component";
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';

const routes: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full', canActivate: [AuthorizeGuard]},
  { path: 'list', component: ListComponent, canActivate: [AuthorizeGuard]},
  { path: 'add', component: CreateComponent, canActivate: [AuthorizeGuard]},
  { path: 'detail/:id', component: CurrentComponent, canActivate: [AuthorizeGuard]}
];
  
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProjectRoutingModule { }
