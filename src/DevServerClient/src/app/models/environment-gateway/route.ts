import { Match } from './match';

export interface Route {
    routeId: string;
    match: Match;
    order: string;
    clusterIs: string;
    authorizationPolicy: string;
    rateLimiterPolicy: string;
    outputCachePolicy: string;
    timeoutPolicy: string;
    timeout: string;
    corsPolicy: string;
    maxRequestBodySize: string;
    metadata: string;
    transforms: string;
}