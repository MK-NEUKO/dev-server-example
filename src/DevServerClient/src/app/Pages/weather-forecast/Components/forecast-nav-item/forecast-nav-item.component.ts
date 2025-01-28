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
  picToCodePath: string[] = [];
  wind: string[] = [];
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
    this.picToCodePath = [];
    this.wind = [];
    this.precipitation = [];

    this.convertDateToWeekday(this.forecastPerDay.time);
    this.convertDateToDates(this.forecastPerDay.time);
    this.convertPicToCodePath(this.forecastPerDay.picToCode);
    this.gererateWindProperty();
    this.generatePrecipitation();
    console.log(this.forecastPerDay);
  }

  convertDateToWeekday(date: string[]): void {
    date.forEach(element => {
      const weekday = new Date(element).toLocaleDateString('en-US', { weekday: 'short' });
      this.weekdays.push(weekday);
    });
  }

  convertDateToDates(date: string[]): void {
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

  convertPicToCodePath(date: number[]): void {
    const basePath = '/images/weather-forecast/daily/';
    date.forEach(element => {
      const picToCode = element;
      const pathToPic = `${basePath}${picToCode}_iday.svg`;
      this.picToCodePath.push(pathToPic);
    });
  }

  gererateWindProperty(): void {
    this.forecastPerDay.windDirection.forEach((element, index) => {
      const windSpeed = (this.forecastPerDay.windSpeedMax[index] + this.forecastPerDay.windSpeedMin[index]) / 2;
      const windDirection = this.windDegToUnicode(this.forecastPerDay.windDirection[index]);
      const wind = `${windDirection} ${windSpeed.toFixed(1)} ${this.units.windSpeed}`;
      this.wind.push(wind);
    });
  }
  windDegToUnicode(windDirection: number): string {
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

  generatePrecipitation(): void {
    this.forecastPerDay.precipitation.forEach((element, index) => {
      let precipitation = element;
      let precipitationType = '';
      if (this.forecastPerDay.snowFraction[index] === 0) {
        precipitationType = '&#xE016';
      } else if (this.forecastPerDay.snowFraction[index] === 1) {
        precipitationType = '&#xE025';
        precipitation *= 7;
      }
      if (precipitation === 0) {
        precipitationType = '';
      }
      const precipitationText = `${precipitationType} ${precipitation.toFixed(1)} ${this.units.precipitation}`;
      this.precipitation.push(precipitationText);
    });
  }

}
