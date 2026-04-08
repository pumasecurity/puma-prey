import { TestBed } from '@angular/core/testing';
import { HttpRequest, HttpHandler, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenticationInterceptor } from './authentication.interceptor';
import { AuthenticationService } from './_services/auth.service';

describe('AuthenticationInterceptor', () => {
  let interceptor: AuthenticationInterceptor;
  let authServiceSpy: jasmine.SpyObj<AuthenticationService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    authServiceSpy = jasmine.createSpyObj('AuthenticationService', ['getToken', 'logout']);
    routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    TestBed.configureTestingModule({
      providers: [
        AuthenticationInterceptor,
        { provide: AuthenticationService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    });
    interceptor = TestBed.inject(AuthenticationInterceptor);
  });

  it('should be created', () => {
    expect(interceptor).toBeTruthy();
  });

  it('should add Authorization header with Bearer token', () => {
    authServiceSpy.getToken.and.returnValue('my-jwt');
    const request = new HttpRequest('GET', '/api/test');
    const handler: jasmine.SpyObj<HttpHandler> = jasmine.createSpyObj('HttpHandler', ['handle']);
    handler.handle.and.returnValue(of(new HttpResponse({ status: 200 })));

    interceptor.intercept(request, handler);

    const clonedRequest = handler.handle.calls.first().args[0] as HttpRequest<unknown>;
    expect(clonedRequest.headers.get('Authorization')).toBe('Bearer my-jwt');
  });

  it('should logout and redirect on 401', (done) => {
    authServiceSpy.getToken.and.returnValue('token');
    const request = new HttpRequest('GET', '/api/test');
    const handler: jasmine.SpyObj<HttpHandler> = jasmine.createSpyObj('HttpHandler', ['handle']);
    handler.handle.and.returnValue(
      throwError(() => new HttpErrorResponse({ status: 401 }))
    );

    interceptor.intercept(request, handler).subscribe({
      error: () => {
        expect(authServiceSpy.logout).toHaveBeenCalled();
        expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/login');
        done();
      }
    });
  });

  it('should logout and redirect on 403', (done) => {
    authServiceSpy.getToken.and.returnValue('token');
    const request = new HttpRequest('GET', '/api/test');
    const handler: jasmine.SpyObj<HttpHandler> = jasmine.createSpyObj('HttpHandler', ['handle']);
    handler.handle.and.returnValue(
      throwError(() => new HttpErrorResponse({ status: 403 }))
    );

    interceptor.intercept(request, handler).subscribe({
      error: () => {
        expect(authServiceSpy.logout).toHaveBeenCalled();
        expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/login');
        done();
      }
    });
  });

  it('should not logout on other HTTP errors', (done) => {
    authServiceSpy.getToken.and.returnValue('token');
    const request = new HttpRequest('GET', '/api/test');
    const handler: jasmine.SpyObj<HttpHandler> = jasmine.createSpyObj('HttpHandler', ['handle']);
    handler.handle.and.returnValue(
      throwError(() => new HttpErrorResponse({ status: 500 }))
    );

    interceptor.intercept(request, handler).subscribe({
      error: () => {
        expect(authServiceSpy.logout).not.toHaveBeenCalled();
        expect(routerSpy.navigateByUrl).not.toHaveBeenCalled();
        done();
      }
    });
  });
});
