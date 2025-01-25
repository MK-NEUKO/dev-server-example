import { Component, OnInit, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherForecastDataService } from '../../../../services/weather-forecast/weather-forecast-data.service';
import { ForecastDataPerDay } from '../../../../models/weather-forecast/forecastDataPerDay';
import { Units } from '../../../../models/weather-forecast/units';

@Component({
  selector: 'app-forecast-nav-item',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './forecast-nav-item.component.html',
  styleUrl: './forecast-nav-item.component.css'
})
export class ForecastNavItemComponent implements OnInit {

  public forecastPerDay!: ForecastDataPerDay;
  public units!: Units;

  itemIndex = input<number>(0);
  weekdays: string[] = [];
  dates: string[] = [];
  picToCodePath: string[] = [];

  constructor(private weatherForecastDataService: WeatherForecastDataService) {
  }

  ngOnInit(): void {
    this.weatherForecastDataService.getWeatherForecastPerDay().subscribe(data => {
      if (data) {
        this.forecastPerDay = data;
      }
    });
    this.weatherForecastDataService.getUnits().subscribe(data => {
      if (data) {
        this.units = data;
        console.log(this.units.temperature);
      }
    });

    this.convertDateToWeekday(this.forecastPerDay.time);
    this.convertDateToDates(this.forecastPerDay.time);
    this.convertPicToCodePath(this.forecastPerDay.picToCode);
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
        this.dates.push('Tomorrow');
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
      const pathToPic = `${basePath}0${picToCode}_iday.svg`;
      this.picToCodePath.push(pathToPic);
    });
  }
}
