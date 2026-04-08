import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { LoginComponent } from './login.component';
import { AuthenticationService } from '../_services/auth.service';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authServiceSpy: jasmine.SpyObj<AuthenticationService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    authServiceSpy = jasmine.createSpyObj('AuthenticationService', ['login']);
    routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [LoginComponent],
      providers: [
        { provide: AuthenticationService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with empty model', () => {
    expect(component.model.username).toBe('');
    expect(component.model.password).toBe('');
  });

  it('should render Sign In heading', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Sign In');
  });

  it('should render username and password fields', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('#username')).toBeTruthy();
    expect(compiled.querySelector('#password')).toBeTruthy();
  });

  it('onLogin should call authService.login with credentials', () => {
    authServiceSpy.login.and.returnValue(of({}));
    component.model.username = 'user@test.com';
    component.model.password = 'secret';

    component.onLogin();

    expect(authServiceSpy.login).toHaveBeenCalledWith('user@test.com', 'secret');
  });

  it('onLogin should navigate to root on success', () => {
    authServiceSpy.login.and.returnValue(of({}));
    component.model.username = 'user@test.com';
    component.model.password = 'secret';

    component.onLogin();

    expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('');
  });
});
