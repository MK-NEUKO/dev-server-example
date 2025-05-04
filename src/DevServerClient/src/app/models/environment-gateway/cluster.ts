import { Destination } from './destination';

export interface Cluster {
    clusterName: string;
    destinations: Destination[];
}