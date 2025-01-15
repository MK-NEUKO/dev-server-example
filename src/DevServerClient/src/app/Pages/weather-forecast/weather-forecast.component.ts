import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefaultWeather } from '../../../models/weatherforecast/defaultweather';
import { Location } from '../../../models/weatherforecast/location';
import { WeatherForecastService } from '../../../Services/WeatherForecast/weather-forecast.service';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-weather-forecast',
    imports: [
        CommonModule,
        FormsModule
    ],
    templateUrl: './weather-forecast.component.html',
    styleUrls: ['./weather-forecast.component.css']
})
export class WeatherForecastComponent {

  public city: string = '';
  public locations: Location[] = [];
  public forecasts: DefaultWeather[] = [];

  constructor(private weatherForecastService: WeatherForecastService) { }

  //ngOnInit() {
  //  this.weatherForecastService.getWeatherForecast().subscribe((data: DefaultWeather[]) => {
  //    this.forecasts = data;
  //    console.log(data);
  //  });
  //}

  onSubmit() {
    console.log(this.city);
    this.weatherForecastService.getLocations(this.city).subscribe((data: Location[]) => {
      this.locations = data;
      console.log(data);
    });
  }

  getForecast(location: Location) {
    console.log(location);
  }

}
