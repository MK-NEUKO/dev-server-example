import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { ForecastNavItemComponent as ForecastDayOverview } from "../day-overview/day-overview.component";
import { ForecastTabContentComponent as ForecastDayDetails } from "../day-details/day-details.component";
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';

@Component({
  selector: 'app-weather-forecast',
  imports: [
    ForecastDayOverview,
    ForecastDayDetails,
    CommonModule
  ],
  templateUrl: './weather-forecast.component.html',
  styleUrl: './weather-forecast.component.css'
})
export class WeatherForecastComponent implements OnInit, OnDestroy {

  private subscripton: Subscription = new Subscription();
  private weatherForecast?: WeatherForecast;

  public forecastDays: string[] = [];
  public index: number = 0;

  constructor(private weatherForecastDataService: WeatherForecastDataService) { }


  ngOnInit(): void {
    this.subscripton.add(this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      this.weatherForecast = data ?? this.weatherForecastDataService.getDefaultWeatherForecast();
    }));

    this.forecastDays = this.weatherForecast!.forecastDataPerDay.time;
  }

  ngOnDestroy(): void {
    this.subscripton.unsubscribe();
  }
}
