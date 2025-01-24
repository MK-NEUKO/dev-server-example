import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';

@Component({
  selector: 'app-forecast-nav-item',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './forecast-nav-item.component.html',
  styleUrl: './forecast-nav-item.component.css'
})
export class ForecastNavItemComponent implements OnInit {

  public weatherForecast!: WeatherForecast;

  constructor(private weatherForecastDataService: WeatherForecastDataService) {
  }

  ngOnInit() {
    this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
      }
    });
  }


}
