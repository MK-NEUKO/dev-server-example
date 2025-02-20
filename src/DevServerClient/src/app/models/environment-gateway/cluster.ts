import { Destinations } from './destinations';

export interface Cluster {
    clusterId: string;
    loadBalancingPolicy: string;
    sessionAffinity: string;
    healthCheck: string;
    httpClient: string;
    httpRequest: string;
    destinations: Destinations[];
    metadata: string;
}