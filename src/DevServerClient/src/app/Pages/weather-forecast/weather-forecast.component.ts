import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { Location } from '../../models/weather-forecast/location';
import { WeatherForecastService } from '../../services/weather-forecast/weather-forecast.service';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { ForecastNavTabComponent } from "./Components/forecast-nav-tab/forecast-nav-tab.component";
import { WeatherForecastDataService } from '../../services/weather-forecast/weather-forecast-data.service';


@Component({
  selector: 'app-weather-forecast',
  imports: [
    CommonModule,
    FormsModule,
    ForecastNavTabComponent
  ],
  templateUrl: './weather-forecast.component.html',
  styleUrls: ['./weather-forecast.component.css']
})
export class WeatherForecastComponent implements OnInit, OnDestroy {

  private dataSubscription!: Subscription;
  public query: string = 'copenhagen';
  public locationQueryResult!: LocationQueryResult;
  public weatherForecast!: WeatherForecast;

  locations: Location[] = [];

  constructor(
    private weatherForecastService: WeatherForecastService,
    private weatherForecastDataService: WeatherForecastDataService
  ) { }

  ngOnInit() {
    this.dataSubscription = this.weatherForecastDataService.getLocationQueryResult().subscribe(data => {
      if (data) {
        this.locationQueryResult = data;
      }
    });

    this.dataSubscription = this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
      }
    });
  }

  ngAfterViewInit() {
    this.onGetLocations();

  }

  ngOnDestroy(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
    }
  }

  generateLocations(): void {
    this.locationQueryResult.results.forEach((location: Location, index: number) => {
      if (index < 2) {
        this.locations.push(location);
      }
    });
  }

  onGetLocations() {
    this.weatherForecastService.getLocations(this.query).subscribe((data: LocationQueryResult) => {
      this.weatherForecastDataService.setLocationQueryResult(data);
      this.generateLocations();
    });
  }

  onGetForecast(location: Location) {
    const lat = location.lat;
    const lon = location.lon;
    this.weatherForecastService.getForecast(lat, lon).subscribe((data: WeatherForecast) => {
      this.weatherForecastDataService.setWeatherForecast(data);
    });
  }
}
