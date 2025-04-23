import { GatewayConfig } from '../../models/environment-gateway/gatewayConfig';
import { Route } from '../../models/environment-gateway/route';
import { Cluster } from '../../models/environment-gateway/cluster';
import { RouteMatch } from '../../models/environment-gateway/routeMatch';
import { Destination } from '../../models/environment-gateway/destination';

export function InitializeGatewayConfig(): GatewayConfig {
    return {
        name: '',
        isCurrentConfig: false,
        routes: [
            {
                routeName: '',
                clusterName: '',
                match: {
                    path: '',
                } as RouteMatch,
            } as Route,

        ],
        clusters: [
            {
                clusterName: '',
                destinations: [
                    {
                        address: '',
                    } as Destination,
                ],
            } as Cluster,
        ],
    }
}