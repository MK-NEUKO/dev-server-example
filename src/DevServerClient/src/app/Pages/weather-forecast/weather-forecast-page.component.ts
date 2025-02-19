import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { Location } from '../../models/weather-forecast/location';
import { WeatherForecast } from '../../models/weather-forecast/weatherForecast';
import { WeatherForecastService } from '../../services/weather-forecast/weather-forecast.service';
import { WeatherForecastComponent } from "./Components/weather-forecast/weather-forecast.component";
import { WeatherForecastDataService } from '../../services/weather-forecast/weather-forecast-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-weather-forecast-page',
  imports: [
    CommonModule,
    FormsModule,
    WeatherForecastComponent,

  ],
  templateUrl: './weather-forecast-page.component.html',
  styleUrls: ['./weather-forecast-page.component.css']
})
export class WeatherForecastPageComponent implements OnInit, OnDestroy {

  private subscription: Subscription = new Subscription();
  //private locationQueryResult?: LocationQueryResult;

  public query: string = 'copenhagen';
  public locationList: Location[] = new Array<Location>();

  constructor(
    private weatherForecastService: WeatherForecastService,
    private weatherForecastDataService: WeatherForecastDataService,
    private modalService: NgbModal) { }

  ngOnInit() {
    this.subscription.add(this.weatherForecastService.getLocations(this.query).subscribe((data: LocationQueryResult) => {
      this.weatherForecastService.getForecast(data.results[0].lat, data.results[0].lon).subscribe((data: WeatherForecast) => {
        this.weatherForecastDataService.setWeatherForecast(data);
      });
      this.weatherForecastDataService.setLocationQueryResult(data);
    }));

    this.subscription.add(this.weatherForecastDataService.getLocationQueryResult().subscribe(data => {
      //this.locationQueryResult = data ?? this.weatherForecastDataService.getDefaultLocationQueryResult();
      this.locationList = data?.results ?? [];
    }));
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  onGetLocations(context: any) {
    this.modalService.open(context, { centered: true });
  }

  onGetForecast(location: Location) {
    const lat = location.lat;
    const lon = location.lon;
    this.subscription.add(this.weatherForecastService.getForecast(lat, lon).subscribe((data: WeatherForecast) => {
      this.weatherForecastDataService.setWeatherForecast(data);
    }));
  }
}
