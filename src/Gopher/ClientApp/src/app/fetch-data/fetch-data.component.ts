import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: UserErrorData[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<UserErrorData[]>(baseUrl + 'UserErrorData').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface UserErrorData {
  date: string;
  userID: number;
  errorID: number;
  message: string;
}

