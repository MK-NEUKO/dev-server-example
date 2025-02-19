import { Component, OnInit, input, OnDestroy, computed, afterRender } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { Subscription } from 'rxjs';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';
import { ForecastDataPerHour } from '../../../../models/weather-forecast/forecastDataPerHour';
import { BaseChartDirective } from 'ng2-charts';
import { TooltipItem } from 'chart.js';

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

  private subscription: Subscription = new Subscription();
  private weatherForecast?: WeatherForecast;

  public timeList: string[] = [];
  public pictogramBgList: string[] = [];
  public dayIndex = input<number>(0);
  public pictogramPathList: string[] = [];
  public temperatureList: string[] = [];
  public precipitationProbabilityList: string[] = [];
  public feltTemperatureList: string[] = [];
  public relativeHumidityList: string[] = [];
  public windDirectionList: string[] = [];
  public windSpeedList: string[] = [];
  public windSpeedUnit: string = '';


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
    this.subscription.add(this.weatherForecastDataService.getWeatherForecast().subscribe(data => {
      this.weatherForecast = data ?? this.weatherForecastDataService.getDefaultWeatherForecast();
      this.processDayDetailForecast();
    }));
    this.processDayDetailForecast();
  }

  processDayDetailForecast(): void {
    this.timeList = this.processTimes(this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].time);
    this.temperatureList = this.processTemperatures(
      this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].temperature,
      this.weatherForecast!.units.temperature);
    this.pictogramPathList = this.processPictogramPaths(
      this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].pictogramCode,
      this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].isDayLight);
    this.pictogramBgList = this.processPictogramBgs(this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].isDayLight);
    this.precipitationProbabilityList = this.processPrecipitationProbabilities(
      this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].precipitationProbability);
    this.feltTemperatureList = this.processFeltTemperatures(
      this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].feltTemperature,
      this.weatherForecast!.units.temperature);
    this.relativeHumidityList = this.processRelativeHumidities(this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].relativeHumidity);
    this.windDirectionList = this.processWindDirections(this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].windDirection);
    this.windSpeedList = this.processWindSpeeds(this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].windSpeed);
    this.windSpeedUnit = this.processWindSpeedUnit(this.weatherForecast!.units.windSpeed);
    this.createTemperatureChart(this.weatherForecast!);
    this.createPrecipitationChart(this.weatherForecast!);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  processTimes(times: string[]): string[] {
    let timeList: string[] = [];
    times.forEach(element => {
      const time = element;
      timeList.push(time);
    });
    return timeList;
  }

  processTemperatures(temperatures: number[], unit: string): string[] {
    let temperatureList: string[] = [];
    temperatures.forEach(element => {
      const temperature = this.roundTemperature(element);
      temperatureList.push(`${temperature} °${unit}`);
    });
    return temperatureList;
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

  processPrecipitationProbabilities(precipitationProbabilities: number[]): string[] {
    let precipitationProbabilityList: string[] = [];
    precipitationProbabilities.forEach(element => {
      const precipitationProbability = `${element.toFixed(0)}%`;
      precipitationProbabilityList.push(precipitationProbability);
    });
    return precipitationProbabilityList;
  }

  processFeltTemperatures(feltTemperatures: number[], unit: string): string[] {
    let feltTemperatureList: string[] = [];
    feltTemperatures.forEach(element => {
      const feltTemperature = this.roundTemperature(element);
      feltTemperatureList.push(`${feltTemperature} °${unit}`);
    });
    return feltTemperatureList;
  }
  roundTemperature(temperature: number): number {
    let roundedTemperature = Math.round(temperature);
    return roundedTemperature === -0 ? 0 : roundedTemperature;
  }

  processRelativeHumidities(relativeHumidities: number[]): string[] {
    let relativeHumidityList: string[] = [];
    relativeHumidities.forEach(element => {
      const relativeHumidity = `${element.toFixed(0)}%`;
      relativeHumidityList.push(relativeHumidity);
    });
    return relativeHumidityList;
  }

  processWindDirections(windDirection: number[]): string[] {
    let windDirectionUnicodeList: string[] = [];
    windDirection.forEach(element => {
      const windDirectionUnicode = this.convertWindDegToUnicode(element);
      windDirectionUnicodeList.push(windDirectionUnicode);
    });
    return windDirectionUnicodeList;
  }
  convertWindDegToUnicode(windDirection: number): string {
    switch (true) {
      case (windDirection >= 348.75 || windDirection < 11.25):
        return '&#xE000;';
      case (windDirection >= 11.25 && windDirection < 33.75):
        return '&#xE001;';
      case (windDirection >= 33.75 && windDirection < 56.25):
        return '&#xE002;';
      case (windDirection >= 56.25 && windDirection < 78.75):
        return '&#xE003;';
      case (windDirection >= 78.75 && windDirection < 101.25):
        return '&#xE004;';
      case (windDirection >= 101.25 && windDirection < 123.75):
        return '&#xE005;';
      case (windDirection >= 123.75 && windDirection < 146.25):
        return '&#xE006;';
      case (windDirection >= 146.25 && windDirection < 168.75):
        return '&#xE007;';
      case (windDirection >= 168.75 && windDirection < 191.25):
        return '&#xE008;';
      case (windDirection >= 191.25 && windDirection < 213.75):
        return '&#xE009;';
      case (windDirection >= 213.75 && windDirection < 236.25):
        return '&#xE00A;';
      case (windDirection >= 236.25 && windDirection < 258.75):
        return '&#xE00B;';
      case (windDirection >= 258.75 && windDirection < 281.25):
        return '&#xE00C;';
      case (windDirection >= 281.25 && windDirection < 303.75):
        return '&#xE00D;';
      case (windDirection >= 303.75 && windDirection < 326.25):
        return '&#xE00E;';
      case (windDirection >= 326.25 && windDirection < 348.75):
        return '&#xE00F;';
      default:
        return '-';
    }
  }

  processWindSpeeds(windSpeeds: number[]): string[] {
    let windSpeedList: string[] = [];
    windSpeeds.forEach((element) => {
      const windSpeed = `${element.toFixed(0)}`;
      windSpeedList.push(windSpeed);
    });
    return windSpeedList;
  }

  processWindSpeedUnit(unit: string): string {
    switch (unit) {
      case 'kmh': return 'km/h';
      default: return unit;
    };
  }

  createTemperatureChart(weatherForecast: WeatherForecast): void {
    this.tempChartData = {
      labels: weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].time,
      datasets: [
        {
          label: 'Temp \uE010',
          data: weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].temperature,
          borderWidth: 2,
          backgroundColor: 'rgba(47, 131, 156, 0.2)',
          borderColor: 'rgb(47, 131, 156)',
          type: 'line'
        },
        {
          label: 'Temp \uE011',
          data: weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].feltTemperature,
          borderWidth: 2,
          type: 'line',
          backgroundColor: 'rgb(12, 82, 64)',
          borderColor: 'rgb(28, 179, 141)',
        }
      ]
    };

    this.tempChartOptions = {
      responsive: true,
      maintainAspectRatio: false,
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
            text: `°${weatherForecast.units.temperature}`,
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

  createPrecipitationChart(weatherForecast: WeatherForecast): void {
    this.precipitationChartData = {
      labels: weatherForecast.forecastDataPerDayPerHour[this.dayIndex()].time,
      datasets: [
        {
          label: 'Precipitation \uE016',
          data: [] = this.processRainPrecipitations(weatherForecast.forecastDataPerDayPerHour[this.dayIndex()]),
          borderWidth: 2,
          backgroundColor: 'rgb(20, 175, 223)',
          borderColor: 'rgb(47, 131, 156)',
          type: 'bar',
          yAxisID: 'y-axis-rain',
        },
        {
          label: 'Precipitation \uE025',
          data: [] = this.processSnowPrecipitations(weatherForecast.forecastDataPerDayPerHour[this.dayIndex()]),
          borderWidth: 2,
          backgroundColor: 'rgb(199, 242, 255)',
          borderColor: 'rgb(83, 218, 255)',
          type: 'bar',
          yAxisID: 'y-axis-snow',
        }
      ]
    };

    this.precipitationChartOptions = {
      responsive: true,
      maintainAspectRatio: false,
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

  processPrecipitationTooltip(context: TooltipItem<'bar'>): string {
    const index = context.dataIndex;
    const precipitationType = this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].snowFraction[index];
    const value = context.raw;
    let label = 'Rain \uE016';
    let unit = 'mm';
    if (precipitationType === 1) {
      label = 'Snow \uE025';
      unit = 'cm';
    }
    return `${label} | ${value} ${unit}`;
  }

  processTemperatureTooltip(context: TooltipItem<'line'>): string {
    const index = context.dataIndex;
    const datasetIndex = context.datasetIndex;
    const temperature = this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].temperature[index];
    const feltTemperature = this.weatherForecast!.forecastDataPerDayPerHour[this.dayIndex()].feltTemperature[index];
    const iconTemperature = '\uE010';
    const iconFeltTemperature = '\uE011';
    if (datasetIndex === 0) {
      return `${iconTemperature} | ${temperature} °${this.weatherForecast!.units.temperature}`;
    } else {
      return `${iconFeltTemperature} | ${feltTemperature} °${this.weatherForecast!.units.temperature}`;
    }
  }

}