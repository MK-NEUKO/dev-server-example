import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForecastNavItemComponent } from "../day-overview/day-overview.component";
import { ForecastTabContentComponent } from "../day-details/day-details.component";
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';
import { ForecastDataPerDay } from '../../../../models/weather-forecast/forecastDataPerDay';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';

@Component({
  selector: 'app-weather-forecast',
  imports: [
    ForecastNavItemComponent,
    ForecastTabContentComponent,
    CommonModule
  ],
  templateUrl: './weather-forecast.component.html',
  styleUrl: './weather-forecast.component.css'
})
export class WeatherForecastComponent implements OnInit {

  public weatherForecast!: WeatherForecast;
  public forecastPerDay!: ForecastDataPerDay;
  public index: number = 0;

  constructor(private weatherForecastDataService: WeatherForecastDataService) { }


  ngOnInit(): void {
    this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
      }
    });

    this.weatherForecastDataService.getWeatherForecastPerDay().subscribe(data => {
      if (data) {
        this.forecastPerDay = data;
      }
    });
  }

}
