import { Routes } from '@angular/router';
import { WeatherForecastComponent } from './Pages/weather-forecast/weather-forecast.component';

export const routes: Routes = [
    {path: '', component: WeatherForecastComponent},
    {path: 'weather-forecast', redirectTo: '', pathMatch: 'full'}
];
