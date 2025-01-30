import { Component, OnInit, input, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { ForecastDataPerDay } from '../../../../models/weather-forecast/forecastDataPerDay';
import { Units } from '../../../../models/weather-forecast/units';

@Component({
  selector: 'app-forecast-nav-item',
  imports: [
    CommonModule
  ],
  templateUrl: './forecast-nav-item.component.html',
  styleUrl: './forecast-nav-item.component.css'
})
export class ForecastNavItemComponent implements OnInit, OnDestroy {

  private dataSubscription!: Subscription;
  public forecastPerDay!: ForecastDataPerDay;
  public units!: Units;

  itemIndex = input<number>(0);
  weekdays: string[] = [];
  dates: string[] = [];
  pictogramPath: string[] = [];
  temperatureMax: string[] = [];
  temperatureMin: string[] = [];
  windDirection: string[] = [];
  windSpeed: string[] = [];
  precipitation: string[] = [];

  constructor(private weatherForecastDataService: WeatherForecastDataService) {
  }

  ngOnInit(): void {
    this.dataSubscription = this.weatherForecastDataService.getWeatherForecastPerDay().subscribe(data => {
      if (data) {
        this.forecastPerDay = data;
        this.checkAndGenerateForecastData();
      }
    });
    this.dataSubscription = this.weatherForecastDataService.getUnits().subscribe(data => {
      if (data) {
        this.units = data;
        this.checkAndGenerateForecastData();
      }
    });

    this.generateForecastData();
  }

  ngOnDestroy(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
    }
  }

  checkAndGenerateForecastData(): void {
    if (this.forecastPerDay && this.units) {
      this.generateForecastData();
    }
  }

  generateForecastData(): void {
    this.weekdays = [];
    this.dates = [];
    this.pictogramPath = [];
    this.temperatureMax = this.processTemrature(this.forecastPerDay.temperatureMax, this.units.temperature);
    this.temperatureMin = this.processTemrature(this.forecastPerDay.temperatureMin, this.units.temperature);
    this.windDirection = [];
    this.windSpeed = [];
    this.precipitation = [];

    this.processWeekdays(this.forecastPerDay.time);
    this.processDates(this.forecastPerDay.time);
    this.processPictogramPath(this.forecastPerDay.pictogramCode);


    this.processWindDirection(this.forecastPerDay.windDirection);
    this.processWindSpeed(this.forecastPerDay, this.units.windSpeed);
    this.processPrecipitation(this.forecastPerDay);
  }

  processWeekdays(date: string[]): void {
    date.forEach(element => {
      const weekday = new Date(element).toLocaleDateString('en-US', { weekday: 'short' });
      this.weekdays.push(weekday);
    });
  }

  processDates(date: string[]): void {
    date.forEach((element, index) => {
      const day = new Date(element).toLocaleDateString('en-US', { day: 'numeric' });
      const month = new Date(element).toLocaleDateString('en-US', { month: 'short' });
      const date = `${day}. ${month}`;
      if (index === 0) {
        this.dates.push('Today');
      }
      else if (index === 1) {
        this.dates.push('Tmr');
      }
      else {
        this.dates.push(date);
      }
    });
  }

  processPictogramPath(date: number[]): void {
    const basePath = '/images/weather-forecast/daily/';
    date.forEach(element => {
      const picToCode = element;
      const pathToPic = `${basePath}${picToCode}_iday.svg`;
      this.pictogramPath.push(pathToPic);
    });
  }

  processTemrature(temperature: number[], unit: string): string[] {
    let temperatureList: string[] = [];
    temperature.forEach(element => {
      const temperature = `${this.roundTemperature(element)} °${unit}`;
      temperatureList.push(temperature);
    });
    return temperatureList;
  }
  roundTemperature(temperature: number): number {
    return Math.round(temperature);
  }

  processWindDirection(windDirection: number[]): void {
    windDirection.forEach(element => {
      const windDirectionUnicode = this.convertWindDegToUnicode(element);
      this.windDirection.push(windDirectionUnicode);
    });
  }
  convertWindDegToUnicode(windDirection: number): string {
    switch (windDirection) {
      case 0: return '&#xE000;';
      case 45: return '&#xE002;';
      case 90: return '&#xE004;';
      case 135: return '&#xE006;';
      case 180: return '&#xE008;';
      case 225: return '&#xE00A;';
      case 270: return '&#xE00C;';
      case 315: return '&#xE00E;';
      default: return '-';
    }
  }

  processWindSpeed(forecastPerDay: ForecastDataPerDay, windSpeedUnit: string): void {
    forecastPerDay.windSpeedMax.forEach((element, index) => {
      const processedWindSpeedUnit = this.processWindSpeedUnit(windSpeedUnit);
      const windSpeed = `${element.toFixed(0)} - ${forecastPerDay.windSpeedMin[index].toFixed(0)} ${processedWindSpeedUnit}`;
      this.windSpeed.push(windSpeed);
    });
  }
  processWindSpeedUnit(windSpeedUnit: string): string {
    switch (this.units.windSpeed) {
      case 'kmh': return 'km/h';
      default: return this.units.windSpeed;
    };
  }

  processPrecipitation(forecastPerDay: ForecastDataPerDay): void {
    forecastPerDay.precipitation.forEach((element, index) => {
      let precipitation = element;
      let precipitationType = '';
      if (forecastPerDay.snowFraction[index] === 0) {
        precipitationType = '&#xE016';
      } else if (forecastPerDay.snowFraction[index] === 1) {
        precipitationType = '&#xE025';
        precipitation *= 7;
      }
      if (precipitation === 0) {
        precipitationType = '';
      }
      const precipitationText = `${precipitationType} ${precipitation.toFixed(0)} ${this.units.precipitation}`;
      this.precipitation.push(precipitationText);
    });
  }

}
