import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) { }

  logout(): void {
    this.authService.logout();
    this.router.navigateByUrl("/login");
  }

}
