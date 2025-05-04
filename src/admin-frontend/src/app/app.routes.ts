import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { EnvGatewaysComponent } from './pages/env-gateways/env-gateways.component';

export const routes: Routes = [
    {
        path: '',
        children: [
            { path: '', component: DashboardComponent },
        ]
    },
    {
        path: 'gateways',
        children: [
            { path: '', component: EnvGatewaysComponent },
        ]
    }
];
