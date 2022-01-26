import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  constructor(
    private authService: AuthenticationService,
    private router: Router) { }

  logout(): void {
    this.authService.logout();
    this.router.navigateByUrl("/login");
  }

  title = 'Squirrel';
}
