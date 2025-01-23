import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { Location } from '../../models/weather-forecast/location';
import { WeatherForecastService } from '../../services/weather-forecast/weather-forecast.service';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { MetaData } from '../../models/weather-forecast/metaData';
import { Units } from '../../models/weather-forecast/units';
import { ForecastDataPerHour } from '../../models/weather-forecast/forecastDataPerHour';
import { ForecastDataPerDay } from '../../models/weather-forecast/forecastDataPerDay';
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

  public weatherForecast: WeatherForecast = {
    metadataDto: {
      modelRunUpdateTimeUtc: '',
      name: '',
      height: 0,
      timezoneAbbreviation: '',
      latitude: 0,
      modelRunUtc: '',
      longitude: 0,
      utcTimeOffset: 0,
      generationTimeMs: 0
    } as MetaData,
    unitsDto: {
      predictability: '',
      precipitation: '',
      windSpeed: '',
      precipitationProbability: '',
      relativeHumidity: '',
      temperature: '',
      time: '',
      pressure: '',
      windDirection: ''
    } as Units,
    forecastDataPerHourDto: [] as ForecastDataPerHour[],
    forecastDataPerDayDto: [] as ForecastDataPerDay[]
  };



  constructor(private weatherForecastService: WeatherForecastService) { }

  onSubmit() {
    this.weatherForecastService.getLocations(this.query = "Copenhagen").subscribe((data: LocationQueryResult) => {
      this.locationQueryResult = data;
      console.log(data)
    });
  }

  onGetForecast(location: Location) {
    const lat = location.lat;
    const lon = location.lon;
    this.weatherForecastService.getForecast(lat, lon).subscribe((data: WeatherForecast) => {
      this.weatherForecast = data;
      console.log(this.weatherForecast);
    });

    console.log(lat, lon);
  }

}
