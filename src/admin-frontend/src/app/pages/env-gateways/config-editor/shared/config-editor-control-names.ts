export const CONFIG_EDITOR_CONTROL_NAMES = {
    CONFIG_NAME: 'configName',
    ROUTES: 'routes',
    CLUSTERS: 'clusters',
    DESTINATIONS: 'destinations',
    ROUTE_NAME: 'routeName',
    CLUSTER_NAME: 'clusterName',
    CLUSTER_ID: 'clusterId',
    MATCH: 'match',
    MATCH_PATH: 'path',
    DESTINATION_ID: 'destinationId',
    DESTINATION_NAME: 'destinationName',
    DESTINATION_ADDRESS: 'address'
} as const;

export type ConfigEditorControlName = typeof CONFIG_EDITOR_CONTROL_NAMES[keyof typeof CONFIG_EDITOR_CONTROL_NAMES];