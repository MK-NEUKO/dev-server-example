import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForecastNavItemComponent } from "../forecast-nav-item/forecast-nav-item.component";
import { ForecastTabContentComponent } from "../forecast-tab-content/forecast-tab-content.component";
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';
import { ForecastDataPerDay } from '../../../../models/weather-forecast/forecastDataPerDay';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';

@Component({
    selector: 'app-forecast-nav-tab',
    imports: [
        ForecastNavItemComponent,
        ForecastTabContentComponent,
        CommonModule
    ],
    templateUrl: './forecast-nav-tab.component.html',
    styleUrl: './forecast-nav-tab.component.css'
})
export class ForecastNavTabComponent implements OnInit {

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
