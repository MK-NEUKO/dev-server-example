import { RouteMatch } from "./route-match.model";

export interface RouteConfig {
    routeName: string;
    clusterName: string;
    match: RouteMatch;
}