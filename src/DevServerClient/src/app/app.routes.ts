import { Routes } from '@angular/router';
import { WeatherForecastComponent } from './Pages/weather-forecast/weather-forecast.component';
import { BaseLayoutComponent } from './Layout/base-layout/base-layout.component';

export const routes: Routes = [
    {
        path: '',
        component: BaseLayoutComponent,
        children: [
            {path: '', component: WeatherForecastComponent}
        ]
    },
    
];
