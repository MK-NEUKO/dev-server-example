import { Destination } from './destination';

export interface Cluster {
    clusterId: string;
    loadBalancingPolicy: string;
    sessionAffinity: string;
    healthCheck: string;
    httpClient: string;
    httpRequest: string;
    destinations: Destination[];
    metadata: string;
}