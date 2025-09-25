export const CONFIG_EDITOR_CONTROL_LABELS = {
    CONFIG_NAME: 'Config Name',
    ROUTE_NAME: 'Route Id',
    CLUSTER_NAME: 'Cluster Id',
    MATCH_PATH: 'Path',
    DESTINATION_NAME: 'Destination Name',
    DESTINATION_ADDRESS: 'Address'
} as const;

export type ConfigEditorControlName = typeof CONFIG_EDITOR_CONTROL_LABELS[keyof typeof CONFIG_EDITOR_CONTROL_LABELS];