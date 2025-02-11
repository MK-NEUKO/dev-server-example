import { Component, OnInit, input, OnDestroy, computed } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { Subscription } from 'rxjs';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';
import { ForecastDataPerHour } from '../../../../models/weather-forecast/forecastDataPerHour';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-day-details',
  imports: [
    CommonModule,
    NgFor,
    BaseChartDirective,
  ],
  templateUrl: './day-details.component.html',
  styleUrl: './day-details.component.css'
})
export class ForecastTabContentComponent implements OnInit, OnDestroy {

  private dataSubscription!: Subscription;
  public weatherForecast!: WeatherForecast;
  dayIndex = input<number>(0);

  forecastPerHour!: ForecastDataPerHour;
  pictogramPathList: string[] = [];
  options: {} = {};
  data!: {
    labels: string[],
    datasets: {
      label: string,
      data: number[],
      borderWidth: number,
      type: string,
      backgroundColor:
      string, borderColor: string
    }[]
  };

  constructor(private weatherForecastDataService: WeatherForecastDataService) {

  }


  ngOnInit(): void {
    this.dataSubscription = this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
        this.forecastPerHour = this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()];
        this.generateForecastData();
        this.processTemperatureChart();
      }
    });

    this.generateForecastData();
    this.processTemperatureChart();
  }

  generateForecastData(): void {
    this.pictogramPathList = this.processPictogramPaths(this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].pictogramCode, this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].isDayLight);
  }

  ngOnDestroy(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
    }
  }

  processPictogramPaths(pictogramCode: number[], isDaylight: number[]): string[] {
    const basePath = '/images/weather-forecast/hourly/';
    let pictogramPathList: string[] = [];
    pictogramCode.forEach((element, index) => {
      const pictogramCode = element;
      const daytime = isDaylight[index] === 1 ? 'day' : 'night';
      const pictogramPath = `${basePath}${pictogramCode}_${daytime}.svg`;
      pictogramPathList.push(pictogramPath);
    });
    return pictogramPathList;
  }

  processTemperatureChart(): void {


    this.data = {
      labels: this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].time,
      datasets: [
        {
          label: 'Temp.',
          data: this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].temperature,
          borderWidth: 2,
          backgroundColor: 'rgba(47, 131, 156, 0.2)',
          borderColor: 'rgb(47, 131, 156)',

          type: 'line'
        },
        {
          label: 'Temp. feels like',
          data: this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].feltTemperature,
          borderWidth: 2,
          type: 'line',
          backgroundColor: '#914c14',
          borderColor: '#995e1b',
        }
      ]
    };

    this.options = {
      elements: {
        line: {
          cubicInterpolationMode: 'monotone',
        },
        point: {
          radius: 5,
          hitRadius: 10,
          hoverRadius: 10,
          hoverBorderWidth: 2,
        }
      },
      scales: {
        x: {
          Title: {
            display: true,
            text: 'Time'
          },
          ticks: {
            autoSkip: true,
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)'
          }
        },
        y: {


        }
      }
    };
  }
}
