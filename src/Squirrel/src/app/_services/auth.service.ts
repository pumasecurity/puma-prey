import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { TokenService, AuthenticateRequest, Configuration } from 'projects/coyote-swagger-client/src';
import { map } from 'rxjs/operators';
import { isNull } from '@angular/compiler/src/output/output_ast';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(
    private tokenService: TokenService,
    private configuration: Configuration) { }

  login(username: string, password: string): Observable<any> {
    var authRequest: AuthenticateRequest = { password : password, userName : username };
    // Mock a successful call to an API server.
    //this.tokenService.apiTokenAuthenticatePost(authRequest).subscribe(request => {
    //  request.
    //});
    //this.tokenService.apiTokenAuthenticatePost(() => { username = username, password = password })
    return this.tokenService.apiTokenAuthenticatePost(authRequest)
      .pipe(
        map(res => {
          localStorage.setItem('access_token', res.jwtToken);
          if (this.tokenService.configuration.apiKeys) {
            this.tokenService.configuration.apiKeys["Authorization"] = 'Bearer '+ res.jwtToken;
          }
          
          return res;
        })
    );
    //this.tokenService.apiTokenAuthenticatePost(authRequest).subscribe((res: any) => {
    //  localStorage.setItem('access_token', res.jwtToken);
    //  return res;
    //});

    //return this.tokenService.apiTokenAuthenticatePost(authRequest);

    //this.tokenService.apiTokenAuthenticatePost(authRequest).subscribe((res: any) => {
    //  localStorage.setItem('access_token', res.jwttoken)
    //  //this.getUserProfile(res._id).subscribe((res) => {
    //  //  this.currentUser = res;
    //  //  this.router.navigate(['user-profile/' + res.msg._id]);
    //  })
      
  }

  logout(): void {
    localStorage.removeItem("access_token");
  }

  isUserLoggedIn(): boolean {
    if (localStorage.getItem("access_token") != null) {
      return true;
    }
    return false;
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

}
