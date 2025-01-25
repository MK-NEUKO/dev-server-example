import { Component, OnInit, input } from '@angular/core';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';

@Component({
  selector: 'app-forecast-tab-content',
  standalone: true,
  imports: [],
  templateUrl: './forecast-tab-content.component.html',
  styleUrl: './forecast-tab-content.component.css'
})
export class ForecastTabContentComponent implements OnInit {

  public weatherForecast!: WeatherForecast;
  tabIndex = input<number>(0);

  constructor(private weatherForecastDataService: WeatherForecastDataService) { }


  ngOnInit(): void {
    this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
      }
    });
  }

}
