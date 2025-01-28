import { Component, OnInit, input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { BaseChartDirective } from 'ng2-charts';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';
import { elements, LineController, scales } from 'chart.js';
import { ForecastDataPerHour } from '../../../../models/weather-forecast/forecastDataPerHour';

@Component({
  selector: 'app-forecast-tab-content',
  imports: [
    BaseChartDirective
  ],
  templateUrl: './forecast-tab-content.component.html',
  styleUrl: './forecast-tab-content.component.css'
})
export class ForecastTabContentComponent implements OnInit, OnDestroy {

  private dataSubscription!: Subscription;
  public weatherForecast!: WeatherForecast;
  tabIndex = input<number>(0);


  forecastPerDay1Hour: ForecastDataPerHour[] = [];

  options: {} = {};
  data!: {
    labels: string[],
    datasets: { label: string, data: number[], borderWidth: number }[]
  };

  constructor(private weatherForecastDataService: WeatherForecastDataService) {

  }


  ngOnInit(): void {
    this.dataSubscription = this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
      }
    });



    this.data = {
      labels: this.weatherForecast.forecastDataPerHourDto.time,
      datasets: [{
        label: 'Temperature',
        data: this.weatherForecast.forecastDataPerHourDto.feltTemperature,
        borderWidth: 1
      }]
    };

    this.options = {
      elements: {
        line: {
          backgroundColor: 'rgba(160, 160, 160, 1)',
          cubicInterpolationMode: 'monotone',
          borderWidth: 10,
          borderColor: 'rgb(23, 31, 31)',
        }
      },
      scales: {
        y: {
          beginAtZero: true

        }
      }
    };
  }

  ngOnDestroy(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
    }
  }
}
