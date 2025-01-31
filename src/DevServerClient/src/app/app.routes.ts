import { Routes } from '@angular/router';
import { WeatherForecastComponent } from './Pages/weather-forecast/weather-forecast.component';
import { BaseLayoutComponent } from './Layout/base-layout/base-layout.component';
import { DashboardComponent } from './Pages/admin/dashboard/dashboard.component';

export const routes: Routes = [
    {
        path: '',
        component: BaseLayoutComponent,
    },
    {
        path: 'weather-forecast',
        component: BaseLayoutComponent,
        children: [
            { path: '', component: WeatherForecastComponent }
        ]
    },
    {
        path: 'admin',
        component: BaseLayoutComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent }
        ]
    }

];
