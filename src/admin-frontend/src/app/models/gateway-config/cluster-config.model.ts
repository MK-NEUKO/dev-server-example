import { Destination } from "./destination.model";

export interface ClusterConfig {
    clusterName: string;
    destinations: Destination[];
}