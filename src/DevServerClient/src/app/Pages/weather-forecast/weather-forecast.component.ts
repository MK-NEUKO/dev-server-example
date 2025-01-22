import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { Location } from '../../models/weather-forecast/location';
import { WeatherForecastService } from '../../services/weather-forecast/weather-forecast.service';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { ForecastNavTabComponent } from "./Components/forecast-nav-tab/forecast-nav-tab.component";


@Component({
  selector: 'app-weather-forecast',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ForecastNavTabComponent
  ],
  templateUrl: './weather-forecast.component.html',
  styleUrls: ['./weather-forecast.component.css']
})
export class WeatherForecastComponent {



  public query: string = 'Copenhagen';
  public locationQueryResult: LocationQueryResult = {
    query: '',
    iso2: '',
    currentPage: 0,
    itemsPerPage: 0,
    pages: 0,
    count: 0,
    orderBy: '',
    lat: 0,
    lon: 0,
    radius: 0,
    type: '',
    results: []
  };

  public weatherForecast!: WeatherForecast;

  constructor(private weatherForecastService: WeatherForecastService) { }

  onSubmit() {
    this.weatherForecastService.getLocations(this.query = "Copenhagen").subscribe((data: LocationQueryResult) => {
      this.locationQueryResult = data;
    });
  }

  onGetForecast(location: Location) {
    const lat = location.lat;
    const lon = location.lon;
    this.weatherForecastService.getForecast(lat, lon).subscribe((data) => {
      this.weatherForecast = data;
      console.log(this.weatherForecast);
    });

    console.log(lat, lon);
  }

}
