import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefaultWeather } from '../../models/weatherforecast/defaultweather';
import { WeatherForecastService } from '../weather-forecast.service';

@Component({
  selector: 'weather-forecast',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './weather-forecast.component.html',
  styleUrl: './weather-forecast.component.css'
})
export class WeatherForecastComponent {

  public forecasts: DefaultWeather[] = [];

  constructor(private weatherForecastService: WeatherForecastService) { }

  ngOnInit() {
    this.weatherForecastService.getWeatherForecast().subscribe((data: DefaultWeather[]) => {
      this.forecasts = data;
      console.log(data);
    });
  }

}
