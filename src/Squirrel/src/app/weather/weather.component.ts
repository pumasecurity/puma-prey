import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration, WeatherForecast, WeatherForecastService } from 'projects/coyote-swagger-client/src';
import { AuthenticationService } from '../_services/auth.service';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit {

  public forecasts?: Observable<Array<WeatherForecast>>;
  public configuration = new Configuration();

  constructor(private service: WeatherForecastService, private authService: AuthenticationService) { }

  
  ngOnInit(): void {
    //this.service.configuration.accessToken : this.authService.getToken();
    //this.service.configuration.apiKeys?["Authorization"] = this.authService.getToken();
    this.forecasts = this.service.weatherForecastGet();
  }

}
