import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AuthenticationService } from './auth.service';
import { TokenService, Configuration } from 'projects/coyote-swagger-client/src';

describe('AuthenticationService', () => {
  let service: AuthenticationService;
  let tokenServiceSpy: jasmine.SpyObj<TokenService>;
  let configuration: Configuration;

  beforeEach(() => {
    tokenServiceSpy = jasmine.createSpyObj('TokenService', [
      'apiTokenAuthenticatePost'
    ]);
    tokenServiceSpy.configuration = new Configuration({ apiKeys: {} });

    configuration = new Configuration();

    TestBed.configureTestingModule({
      providers: [
        AuthenticationService,
        { provide: TokenService, useValue: tokenServiceSpy },
        { provide: Configuration, useValue: configuration }
      ]
    });
    service = TestBed.inject(AuthenticationService);
    localStorage.clear();
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('login should store JWT in localStorage', (done) => {
    const fakeResponse = { jwtToken: 'test-jwt-token' };
    tokenServiceSpy.apiTokenAuthenticatePost.and.returnValue(of(fakeResponse as any));

    service.login('user@test.com', 'password123').subscribe(() => {
      expect(localStorage.getItem('access_token')).toBe('test-jwt-token');
      done();
    });
  });

  it('login should call tokenService with credentials', () => {
    tokenServiceSpy.apiTokenAuthenticatePost.and.returnValue(of({ jwtToken: 'tok' } as any));

    service.login('user@test.com', 'pass');

    expect(tokenServiceSpy.apiTokenAuthenticatePost).toHaveBeenCalledWith(
      jasmine.objectContaining({ userName: 'user@test.com', password: 'pass' })
    );
  });

  it('logout should remove access_token from localStorage', () => {
    localStorage.setItem('access_token', 'some-token');

    service.logout();

    expect(localStorage.getItem('access_token')).toBeNull();
  });

  it('isUserLoggedIn should return true when token exists', () => {
    localStorage.setItem('access_token', 'some-token');

    expect(service.isUserLoggedIn()).toBeTrue();
  });

  it('isUserLoggedIn should return false when no token', () => {
    expect(service.isUserLoggedIn()).toBeFalse();
  });

  it('getToken should return the stored token', () => {
    localStorage.setItem('access_token', 'my-token');

    expect(service.getToken()).toBe('my-token');
  });

  it('getToken should return null when no token stored', () => {
    expect(service.getToken()).toBeNull();
  });
});
