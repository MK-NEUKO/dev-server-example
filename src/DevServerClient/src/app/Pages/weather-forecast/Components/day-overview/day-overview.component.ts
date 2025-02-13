import { Component, OnInit, input, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { ForecastDataPerDay } from '../../../../models/weather-forecast/forecastDataPerDay';
import { Units } from '../../../../models/weather-forecast/units';

@Component({
  selector: 'app-day-overview',
  imports: [
    CommonModule
  ],
  templateUrl: './day-overview.component.html',
  styleUrls: [
    './day-overview.component.css',
    '../../weather-forecast-page.component.css'
  ]
})
export class ForecastNavItemComponent implements OnInit, OnDestroy {

  private dataSubscription!: Subscription;
  public forecastPerDay!: ForecastDataPerDay;
  public units!: Units;

  dayIndex = input<number>(0);
  weekdayList: string[] = [];
  dateList: string[] = [];
  pictogramPathDayList: string[] = [];
  temperatureMaxList: string[] = [];
  temperatureMinList: string[] = [];
  windDirectionList: string[] = [];
  windSpeedList: string[] = [];
  precipitationList: string[] = [];

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

    this.processForecastData();
  }

  ngOnDestroy(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
    }
  }

  checkAndGenerateForecastData(): void {
    if (this.forecastPerDay && this.units) {
      this.processForecastData();
    }
  }

  processForecastData(): void {
    this.weekdayList = this.processWeekdays(this.forecastPerDay.time);
    this.dateList = this.processDates(this.forecastPerDay.time);
    this.pictogramPathDayList = this.processPictogramPaths(this.forecastPerDay.pictogramCode);
    this.temperatureMaxList = this.processTemratures(this.forecastPerDay.temperatureMax, this.units.temperature);
    this.temperatureMinList = this.processTemratures(this.forecastPerDay.temperatureMin, this.units.temperature);
    this.windDirectionList = this.processWindDirections(this.forecastPerDay.windDirection);
    this.windSpeedList = this.processWindSpeeds(
      this.forecastPerDay.windSpeedMin,
      this.forecastPerDay.windSpeedMax,
      this.units.windSpeed);
    this.precipitationList = this.processPrecipitations(
      this.forecastPerDay.precipitation,
      this.forecastPerDay.snowFraction,
      this.units.precipitation);
  }

  processWeekdays(date: string[]): string[] {
    let weekdays: string[] = [];
    date.forEach(element => {
      const weekday = new Date(element).toLocaleDateString('en-US', { weekday: 'short' });
      weekdays.push(weekday);
    });
    return weekdays;
  }

  processDates(date: string[]): string[] {
    let dates: string[] = [];
    date.forEach((element, index) => {
      const day = new Date(element).toLocaleDateString('en-US', { day: 'numeric' });
      const month = new Date(element).toLocaleDateString('en-US', { month: 'short' });
      const date = `${day}. ${month}`;
      if (index === 0) {
        dates.push('Today');
      }
      else if (index === 1) {
        dates.push('Tmr');
      }
      else {
        dates.push(date);
      }
    });
    return dates;
  }

  processPictogramPaths(pictogramCode: number[]): string[] {
    const basePath = '/images/weather-forecast/daily/';
    let pictogramPathDayList: string[] = [];
    pictogramCode.forEach(element => {
      const pictogramCode = element;
      const pictogramPath = `${basePath}${pictogramCode}_iday.svg`;
      pictogramPathDayList.push(pictogramPath);
    });
    return pictogramPathDayList;
  }

  processTemratures(temperature: number[], unit: string): string[] {
    let temperatureList: string[] = [];
    temperature.forEach(element => {
      const temperature = `${this.roundTemperature(element)} °${unit}`;
      temperatureList.push(temperature);
    });
    return temperatureList;
  }
  roundTemperature(temperature: number): number {
    let roundedTemperature = Math.round(temperature);
    return roundedTemperature === -0 ? 0 : roundedTemperature;
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
      case (windDirection >= 0 && windDirection < 45):
        return '&#xE000;';
      case (windDirection >= 45 && windDirection < 90):
        return '&#xE002;';
      case (windDirection >= 90 && windDirection < 135):
        return '&#xE004;';
      case (windDirection >= 135 && windDirection < 180):
        return '&#xE006;';
      case (windDirection >= 180 && windDirection < 225):
        return '&#xE008;';
      case (windDirection >= 225 && windDirection < 270):
        return '&#xE00A;';
      case (windDirection >= 270 && windDirection < 315):
        return '&#xE00C;';
      case (windDirection >= 315 && windDirection <= 360):
        return '&#xE00E;';
      default:
        return '-';
    }
  }

  processWindSpeeds(windSpeedsMin: number[], windSpeedsMax: number[], unit: string): string[] {
    let windSpeedList: string[] = [];
    windSpeedsMin.forEach((element, index) => {
      const processedWindSpeedUnit = this.processWindSpeedUnit(unit);
      const windSpeed = `${element.toFixed(0)} - ${windSpeedsMax[index].toFixed(0)} ${processedWindSpeedUnit}`;
      windSpeedList.push(windSpeed);
    });
    return windSpeedList;
  }
  processWindSpeedUnit(unit: string): string {
    switch (unit) {
      case 'kmh': return 'km/h';
      default: return this.units.windSpeed;
    };
  }

  processPrecipitations(precipitations: number[], snowFraction: number[], unit: string): string[] {
    let precipitationList: string[] = [];
    precipitations.forEach((element, index) => {
      let precipitation = element;
      let precipitationType = snowFraction[index] === 0 ? 'rain' : 'snow';
      let precipitationText = '';
      if (precipitationType === 'rain') {
        const precipitationIcon = '&#xE016';
        precipitationText = `${precipitationIcon} ${precipitation.toFixed(0)} ${unit}`;
      } else if (snowFraction[index] === 1) {
        const precipitationIcon = '&#xE025';
        precipitation = precipitation * 7 / 10;
        const snowUnit = 'cm';
        precipitationText = `${precipitationIcon} ${precipitation.toFixed(1)} ${snowUnit}`
      }
      precipitationList.push(precipitationText);
    });
    return precipitationList;
  }
}
