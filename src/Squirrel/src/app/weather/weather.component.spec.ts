import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { WeatherComponent } from './weather.component';
import { WeatherForecastService, WeatherForecast } from 'projects/coyote-swagger-client/src';
import { AuthenticationService } from '../_services/auth.service';

describe('WeatherComponent', () => {
  let component: WeatherComponent;
  let fixture: ComponentFixture<WeatherComponent>;
  let weatherServiceSpy: jasmine.SpyObj<WeatherForecastService>;

  beforeEach(async () => {
    weatherServiceSpy = jasmine.createSpyObj('WeatherForecastService', [
      'weatherForecastGet'
    ]);
    (weatherServiceSpy.weatherForecastGet as jasmine.Spy).and.returnValue(of([]));

    const authServiceSpy = jasmine.createSpyObj('AuthenticationService', [
      'getToken', 'isUserLoggedIn'
    ]);

    await TestBed.configureTestingModule({
      declarations: [WeatherComponent],
      providers: [
        { provide: WeatherForecastService, useValue: weatherServiceSpy },
        { provide: AuthenticationService, useValue: authServiceSpy }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WeatherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call weatherForecastGet on init', () => {
    expect(weatherServiceSpy.weatherForecastGet).toHaveBeenCalled();
  });

  it('should render Weather forecast heading', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Weather forecast');
  });

  it('should assign forecasts observable', () => {
    expect(component.forecasts).toBeDefined();
  });
});
