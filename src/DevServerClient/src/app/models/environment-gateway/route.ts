import { RouteMatch } from './routeMatch';

export interface Route {
    routeName: string;
    clusterName: string;
    match: RouteMatch;
}