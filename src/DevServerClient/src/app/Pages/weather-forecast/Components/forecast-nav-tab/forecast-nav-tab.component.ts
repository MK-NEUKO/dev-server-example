import { Component, computed, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForecastNavItemComponent } from "../forecast-nav-item/forecast-nav-item.component";
import { ForecastTabContentComponent } from "../forecast-tab-content/forecast-tab-content.component";
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';
import { ForecastDataPerDay } from '../../../../models/weather-forecast/forecastDataPerDay';

@Component({
  selector: 'app-forecast-nav-tab',
  standalone: true,
  imports: [
    ForecastNavItemComponent,
    ForecastTabContentComponent,
    CommonModule
  ],
  templateUrl: './forecast-nav-tab.component.html',
  styleUrl: './forecast-nav-tab.component.css'
})
export class ForecastNavTabComponent {

  demoList: string[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];

}
