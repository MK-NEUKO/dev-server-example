import { Component } from '@angular/core';
import { ForecastNavItemComponent } from "../forecast-nav-item/forecast-nav-item.component";
import { ForecastTabContentComponent } from "../forecast-tab-content/forecast-tab-content.component";
import { WeatherForecast } from '../../../../models/weather-forecast/weatherForecast';

@Component({
  selector: 'app-forecast-nav-tab',
  standalone: true,
  imports: [ForecastNavItemComponent, ForecastTabContentComponent],
  templateUrl: './forecast-nav-tab.component.html',
  styleUrl: './forecast-nav-tab.component.css'
})
export class ForecastNavTabComponent {


}
