import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AuthenticationGuard } from './authentication-guard.guard';
import { AuthenticationService } from './_services/auth.service';

describe('AuthenticationGuard', () => {
  let guard: AuthenticationGuard;
  let authServiceSpy: jasmine.SpyObj<AuthenticationService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    authServiceSpy = jasmine.createSpyObj('AuthenticationService', ['isUserLoggedIn']);
    routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    TestBed.configureTestingModule({
      providers: [
        AuthenticationGuard,
        { provide: AuthenticationService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    });
    guard = TestBed.inject(AuthenticationGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('canActivate should return true when user is logged in', () => {
    authServiceSpy.isUserLoggedIn.and.returnValue(true);

    const result = guard.canActivate({} as any, {} as any);

    expect(result).toBeTrue();
  });

  it('canActivate should return false and redirect when not logged in', () => {
    authServiceSpy.isUserLoggedIn.and.returnValue(false);

    const result = guard.canActivate({} as any, {} as any);

    expect(result).toBeFalse();
    expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/login');
  });

  it('canActivateChild should return true when user is logged in', () => {
    authServiceSpy.isUserLoggedIn.and.returnValue(true);

    const result = guard.canActivateChild({} as any, {} as any);

    expect(result).toBeTrue();
  });

  it('canLoad should return false and redirect when not logged in', () => {
    authServiceSpy.isUserLoggedIn.and.returnValue(false);

    const result = guard.canLoad({} as any, []);

    expect(result).toBeFalse();
    expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/login');
  });
});
