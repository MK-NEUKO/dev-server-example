import { Routes } from '@angular/router';
import { WeatherForecastPageComponent } from './Pages/weather-forecast/weather-forecast-page.component';
import { BaseLayoutComponent } from './Layout/base-layout/base-layout.component';
import { DashboardComponent } from './Pages/admin/dashboard/dashboard.component';
import { EnvironmentGatewaysComponent } from './Pages/admin/environment-gateways/environment-gateways-page.component';

export const routes: Routes = [
    {
        path: '',
        component: BaseLayoutComponent,
    },
    {
        path: 'weather-forecast',
        component: BaseLayoutComponent,
        children: [
            { path: '', component: WeatherForecastPageComponent }
        ]
    },
    {
        path: 'admin',
        component: BaseLayoutComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent }
        ]
    },
    {
        path: 'admin',
        component: BaseLayoutComponent,
        children: [
            { path: 'environments', component: EnvironmentGatewaysComponent }
        ]
    },

];
