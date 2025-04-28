import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { RouteConfig } from '../../models/gateway-config/route-config.model';
import { ClusterConfig } from '../../models/gateway-config/cluster-config.model';
import { RouteMatch } from '../../models/gateway-config/route-match.model';
import { Destination } from '../../models/gateway-config/destination.model';


export function defaultGatewayConfig(): GatewayConfig {
    return {
        id: 'default',
        name: 'Default Configuration',
        isCurrentConfig: false,
        routes: [
            {
                routeName: 'default-route',
                clusterName: 'default-cluster',
                match: {
                    path: '/default',
                } as RouteMatch
            } as RouteConfig
        ],
        clusters: [
            {
                clusterName: 'default-cluster',
                destinations: [
                    {
                        destinationName: 'default-destination',
                        address: 'default-address',
                    } as Destination
                ]
            } as ClusterConfig
        ]
    }
}