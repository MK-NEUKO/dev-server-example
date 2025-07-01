import { Destination } from "./destination.model";

export interface ClusterConfig {
    clusterName: string;
    id: string;
    destinations: Record<string, Destination>[];
}