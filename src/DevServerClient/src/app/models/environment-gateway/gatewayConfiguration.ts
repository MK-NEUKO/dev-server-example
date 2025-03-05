import { Route } from './route';
import { Cluster } from './cluster';

export interface GatewayConfiguration {
    routes: Route[];
    clusters: Cluster[];
}