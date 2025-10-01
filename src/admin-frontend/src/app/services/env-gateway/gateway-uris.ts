export const GATEWAY_URIS = {
    PRODUCTION_BASE_URL: 'https://localhost:9100',
    CHANGE_CLUSTER_NAME: '/change-cluster-name',
    CHANGE_DESTINATION_NAME: '/change-destination-name',
    CHANGE_DESTINATION_ADDRESS: '/change-destination-address'
} as const;

export type GatewayUris = typeof GATEWAY_URIS[keyof typeof GATEWAY_URIS];