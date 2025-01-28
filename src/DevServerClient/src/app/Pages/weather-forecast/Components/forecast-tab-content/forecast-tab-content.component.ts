import { Component, OnInit, input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';

@Component({
    selector: 'app-forecast-tab-content',
    imports: [],
    templateUrl: './forecast-tab-content.component.html',
    styleUrl: './forecast-tab-content.component.css'
})
export class ForecastTabContentComponent implements OnInit, OnDestroy {

  private dataSubscription!: Subscription;
  public weatherForecast!: WeatherForecast;
  tabIndex = input<number>(0);

  constructor(private weatherForecastDataService: WeatherForecastDataService) { }


  ngOnInit(): void {
    this.dataSubscription = this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
      }
    });
  }

  ngOnDestroy(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
    }
  }

}
