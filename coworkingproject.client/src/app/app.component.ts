import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IconService } from './icon-service/icon-service.model';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient, private iconService: IconService) {}

  title = 'coworkingproject.client';
}
