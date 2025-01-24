import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { ForecastDataPerDay } from '../../../../models/weather-forecast/forecastDataPerDay';

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

  public forecastPerDay!: ForecastDataPerDay;

  constructor(private weatherForecastDataService: WeatherForecastDataService) {
  }

  ngOnInit(): void {
    this.weatherForecastDataService.getWeatherForecastPerDay().subscribe(data => {
      if (data) {
        this.forecastPerDay = data;
      }
    });
  }


}
