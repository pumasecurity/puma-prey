import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services/auth.service';
import { LoginUser } from './loginuser';

@Component({
  selector: 'app-login-form',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model = new LoginUser('', '');
  
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router) { }

  ngOnInit(): void {
  }
    
  onLogin(): void {    
    this.authenticationService.login(this.model.username, this.model.password).subscribe(() =>
      this.router.navigateByUrl(''));
  }

}
