import { RouteConfig } from './route-config.model';
import { ClusterConfig } from './cluster-config.model';

export interface GatewayConfig {
    id: string;
    name: string;
    isCurrentConfig: boolean;
    routes: RouteConfig[];
    clusters: ClusterConfig[];
}