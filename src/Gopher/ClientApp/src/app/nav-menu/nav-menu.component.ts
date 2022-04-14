import { Component } from '@angular/core';
import { routes } from '../app.module';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  routes = routes;
  
}

