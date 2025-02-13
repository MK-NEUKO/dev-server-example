import { Component, OnInit, input, OnDestroy, computed, afterRender } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { min, Subscription } from 'rxjs';
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
  styleUrls: [
    './day-details.component.css',
    '../../weather-forecast-page.component.css',
  ]
})
export class ForecastTabContentComponent implements OnInit, OnDestroy {

  private dataSubscription!: Subscription;
  public weatherForecast!: WeatherForecast;
  public pictogramBgList: string[] = [];
  public dayIndex = input<number>(0);
  public forecastPerHour!: ForecastDataPerHour;
  public pictogramPathList: string[] = [];
  public temperatureList: string[] = [];
  public tempChartOptions: {} = {};
  public tempChartData!: {
    labels: string[],
    datasets: {
      label: string,
      data: number[],
      borderWidth: number,
      type: string,
      backgroundColor: string,
      borderColor: string
    }[]
  };
  public precipitationChartOptions: {} = {};
  public precipitationChartData!: {
    labels: string[],
    datasets: {
      label: string,
      data: number[],
      borderWidth: number,
      type: string,
      backgroundColor: string,
      borderColor: string,
      yAxisID: string
    }[]
  };

  constructor(private weatherForecastDataService: WeatherForecastDataService) {

  }


  ngOnInit(): void {
    this.dataSubscription = this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      if (data) {
        this.weatherForecast = data;
        this.forecastPerHour = this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()];
        this.processDayDetailForecast();
      }
    });

    this.processDayDetailForecast();
  }

  processDayDetailForecast(): void {
    this.pictogramPathList = this.processPictogramPaths(
      this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].pictogramCode,
      this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].isDayLight);
    this.pictogramBgList = this.processPictogramBgs(this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].isDayLight);
    this.temperatureList = this.processTemperatures(
      this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].temperature,
      this.weatherForecast.units.temperature);
    this.createTemperatureChart();
    this.createPrecipitationChart();
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

  processPictogramBgs(isDayLight: number[]): string[] {
    let pictogramBgList: string[] = [];
    isDayLight.forEach(element => {
      const pictogramBg = element === 1 ? 'bg-day' : 'bg-night';
      pictogramBgList.push(pictogramBg);
    });
    return pictogramBgList;
  }

  processTemperatures(temperatures: number[], unit: string): string[] {
    let temperatureList: string[] = [];
    temperatures.forEach(element => {
      const temperature = this.roundTemperature(element);
      temperatureList.push(`${temperature} °${unit}`);
    });
    return temperatureList;
  }
  roundTemperature(temperature: number): number {
    let roundedTemperature = Math.round(temperature);
    return roundedTemperature === -0 ? 0 : roundedTemperature;
  }

  createTemperatureChart(): void {
    this.tempChartData = {
      labels: this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].time,
      datasets: [
        {
          label: 'Temp \uE010',
          data: this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].temperature,
          borderWidth: 2,
          backgroundColor: 'rgba(47, 131, 156, 0.2)',
          borderColor: 'rgb(47, 131, 156)',
          type: 'line'
        },
        {
          label: 'Temp \uE011',
          data: this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].feltTemperature,
          borderWidth: 2,
          type: 'line',
          backgroundColor: 'rgb(12, 82, 64)',
          borderColor: 'rgb(28, 179, 141)',
        }
      ]
    };

    this.tempChartOptions = {
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
      font: {
        family: 'MeteobluePictofont',
        size: 12,
        weight: 'bold'
      },
      plugins: {
        legend: {
          display: true,
          labels: {
            font: {
              family: 'MeteobluePictofont',
              size: 12,
            },
            color: 'rgb(47, 131, 156)'
          },
        },
        tooltip: {
          callbacks: {
            label: this.processTemperatureTooltip.bind(this),
          },
          titleFont: {
            family: 'MeteobluePictofont',
            size: 14,
            weight: 'bold'
          },
          bodyFont: {
            family: 'MeteobluePictofont',
            size: 12,
            weight: 'bold'
          },
          footerFont: {
            family: 'MeteobluePictofont',
            size: 10,
            weight: 'bold'
          }
        }
      },
      scales: {
        x: {
          display: true,
          title: {
            display: true,
            text: 'Time',
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)'
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
          display: true,
          title: {
            display: true,
            text: `°${this.weatherForecast.units.temperature}`,
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)'
          },
          ticks: {
            autoSkip: true,
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)'
          }
        }
      }
    };
  }

  createPrecipitationChart(): void {
    this.precipitationChartData = {
      labels: this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].time,
      datasets: [
        {
          label: 'Precipitation \uE016',
          data: [] = this.processRainPrecipitations(this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()]),
          borderWidth: 2,
          backgroundColor: 'rgb(20, 175, 223)',
          borderColor: 'rgb(47, 131, 156)',
          type: 'bar',
          yAxisID: 'y-axis-rain',
        },
        {
          label: 'Precipitation \uE025',
          data: this.processSnowPrecipitations(this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()]),
          borderWidth: 2,
          backgroundColor: 'rgb(199, 242, 255)',
          borderColor: 'rgb(83, 218, 255)',
          type: 'bar',
          yAxisID: 'y-axis-snow',
        }
      ]
    };

    this.precipitationChartOptions = {
      font: {
        family: 'MeteobluePictofont',
        size: 12,
        weight: 'bold'
      },
      plugins: {
        legend: {
          display: true,
          labels: {
            font: {
              family: 'MeteobluePictofont',
              size: 12,
            },
            color: 'rgb(47, 131, 156)'
          },
        },
        tooltip: {
          callbacks: {
            label: this.processPrecipitationTooltip.bind(this),
          },
          titleFont: {
            family: 'MeteobluePictofont',
            size: 14,
            weight: 'bold'
          },
          bodyFont: {
            family: 'MeteobluePictofont',
            size: 12,
            weight: 'bold'
          },
          footerFont: {
            family: 'MeteobluePictofont',
            size: 10,
            weight: 'bold'
          }
        }
      },
      scales: {
        x: {
          display: true,
          title: {
            display: true,
            text: 'Time',
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)'
          },
          ticks: {
            autoSkip: true,
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)'
          },
        },
        'y-axis-rain': {
          display: true,
          title: {
            display: true,
            text: 'Rain in mm',
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)'
          },
          ticks: {
            autoSkip: true,
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(47, 131, 156)',
            stepSize: 2,
          },
          min: 0,
        },
        'y-axis-snow': {
          display: true,
          title: {
            display: true,
            text: 'Snow in cm',
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(83, 218, 255)'
          },
          ticks: {
            autoSkip: true,
            font: {
              size: 12,
              weight: 'bold'
            },
            color: 'rgb(83, 218, 255)',
            stepSize: 2,
          },
          min: 0,
        },
      }
    };
  }

  processRainPrecipitations(forecastPerHour: ForecastDataPerHour): number[] {
    let precipitationList: number[] = [];
    forecastPerHour.precipitation.forEach((element, index) => {
      let precipitation = element;
      let precipitationType = '';
      if (forecastPerHour.snowFraction[index] === 1) {
        precipitation = 0;
      }
      precipitationList.push(precipitation);
    });
    return precipitationList;
  }

  processSnowPrecipitations(forecastPerHour: ForecastDataPerHour): number[] {
    let precipitationList: number[] = [];
    forecastPerHour.precipitation.forEach((element, index) => {
      let precipitation = element;
      let precipitationType = forecastPerHour.snowFraction[index] === 1 ? 'snow' : 'rain';
      if (precipitationType === 'snow') {
        precipitation = precipitation * 7 / 10;
        precipitation = parseFloat(precipitation.toFixed(1));
        precipitationList.push(precipitation);
      } else {
        precipitationList.push(0);
      }
    });
    return precipitationList;
  }

  processPrecipitationTooltip(context: any): string {
    const index = context.dataIndex;
    const precipitationType = this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].snowFraction[index];
    const value = context.raw;
    let label = 'Rain \uE016';
    let unit = 'mm';
    if (precipitationType === 1) {
      label = 'Snow \uE025';
      unit = 'cm';
    }
    return `${label} | ${value} ${unit}`;
  }

  processTemperatureTooltip(context: any): string {
    const index = context.dataIndex;
    const datasetIndex = context.datasetIndex;
    const temperature = this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].temperature[index];
    const feltTemperature = this.weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].feltTemperature[index];
    const iconTemperature = '\uE010';
    const iconFeltTemperature = '\uE011';
    if (datasetIndex === 0) {
      return `${iconTemperature} | ${temperature} °${this.weatherForecast.units.temperature}`;
    } else {
      return `${iconFeltTemperature} | ${feltTemperature} °${this.weatherForecast.units.temperature}`;
    }
  }

}
