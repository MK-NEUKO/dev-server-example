import { Route } from './route';
import { Cluster } from './cluster';

export interface GatewayConfig {
    name: string;
    isCurrentConfig: boolean;
    routes: Route[];
    clusters: Cluster[];
}