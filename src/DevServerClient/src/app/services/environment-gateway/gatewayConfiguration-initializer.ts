import { GatewayConfiguration } from './../../models/environment-gateway/gatewayConfiguration';
import { Route } from './../../models/environment-gateway/route';
import { Cluster } from './../../models/environment-gateway/cluster';
import { Match } from './../../models/environment-gateway/match';
import { Destination } from '../../models/environment-gateway/destination';

export function initializeGatewayConfiguration(): GatewayConfiguration {
    return {
        routes: [
            {
                routeId: '',
                match: {
                    methods: '',
                    hosts: '',
                    path: '',
                    queryParameters: '',
                    headers: '',
                } as Match,
                order: '',
                clusterIs: '',
                authorizationPolicy: '',
                rateLimiterPolicy: '',
                outputCachePolicy: '',
                timeoutPolicy: '',
                timeout: '',
                corsPolicy: '',
                maxRequestBodySize: '',
                metadata: '',
                transforms: '',
            } as Route,

        ],
        clusters: [
            {
                clusterId: '',
                loadBalancingPolicy: '',
                sessionAffinity: '',
                healthCheck: '',
                httpClient: '',
                httpRequest: '',
                destinations: [
                    {
                        address: '',
                        health: '',
                        metadata: '',
                        host: '',
                    } as Destination,
                ],
                metadata: '',
            } as Cluster,
        ],
    }
}