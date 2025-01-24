import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { Location } from '../../models/weather-forecast/location';
import { WeatherForecastService } from '../../services/weather-forecast/weather-forecast.service';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { Metadata } from '../../models/weather-forecast/metaData';
import { Units } from '../../models/weather-forecast/units';
import { ForecastDataPerHour } from '../../models/weather-forecast/forecastDataPerHour';
import { ForecastNavTabComponent } from "./Components/forecast-nav-tab/forecast-nav-tab.component";
import { WeatherForecastDataService } from '../../services/weather-forecast/weather-forecast-data.service';


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
export class WeatherForecastComponent implements OnInit {



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
    } as Metadata,
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
    forecastDataPerHourDto: {
      time: [],
      snowFraction: [],
      windSpeed: [],
      precipitationProbability: [],
      convectivePrecipitation: [],
      rainSpot: [],
      picToCode: [],
      feltTemperature: [],
      precipitation: [],
      isDayLight: [],
      uvIndex: [],
      relativeHumidity: [],
      seaLevelPressure: [],
      windDirection: [],
    } as ForecastDataPerHour,
    forecastDataPerDayDto: {
      time: [],
      temperatureInstant: [],
      precipitation: [],
      predictability: [],
      temperatureMax: [],
      seaLevelPressureMean: [],
      windSpeedMean: [],
      precipitationHours: [],
      seaLevelPressureMin: [],
      picToCode: [],
      snowFraction: [],
      humidityGreater90Hours: [],
      convectivePrecipitation: [],
      relativeHumidityMax: [],
      temperatureMin: [],
      windDirection: [],
      feltTemperatureMax: [],
      indexTo1HValuesEnd: [],
      relativeHumidityMin: [],
      feltTemperatureMean: [],
      windSpeedMin: [],
      feltTemperatureMin: [],
      precipitationProbability: [],
      uvIndex: [],
      indexTo1HValuesStart: [],
      rainSpot: [],
      temperatureMean: [],
      seaLevelPressureMax: [],
      relativeHumidityMean: [],
      predictabilityClass: [],
      windSpeedMax: [],
    }
  };



  constructor(
    private weatherForecastService: WeatherForecastService,
    private weatherForecastDataService: WeatherForecastDataService
  ) { }

  ngOnInit() {
    this.weatherForecastDataService.getLocationQueryResult().subscribe(data => {
      if (data) {
        this.locationQueryResult = data;
      }
    });

    this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
      }
    });
  }

  onSubmit() {
    this.weatherForecastService.getLocations(this.query).subscribe((data: LocationQueryResult) => {
      this.locationQueryResult = data;
      this.weatherForecastDataService.setLocationQueryResult(data);
      console.log(data);
    });
  }

  onGetForecast(location: Location) {
    const lat = location.lat;
    const lon = location.lon;
    this.weatherForecastService.getForecast(lat, lon).subscribe((data: WeatherForecast) => {
      this.weatherForecast = data;
      this.weatherForecastDataService.setWeatherForecast(data);
      console.log(this.weatherForecast);
    });

    console.log(lat, lon);
  }

}
