import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { EnvGatewaysComponent } from './pages/env-gateways/env-gateways.component';
import { ConfigEditorComponent } from './pages/env-gateways/config-editor/config-editor.component';

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
    },
    {
        path: 'config-editor',
        children: [
            { path: '', component: ConfigEditorComponent },
        ]
    }
];
