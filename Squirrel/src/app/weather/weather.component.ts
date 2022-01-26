import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherForecast, WeatherForecastService } from 'projects/coyote-swagger-client/src';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit {

  public forecasts?: Observable<Array<WeatherForecast>>;

  constructor(private service: WeatherForecastService){ }

  
  ngOnInit(): void {
    this.forecasts = this.service.weatherForecastGet();
  }

}