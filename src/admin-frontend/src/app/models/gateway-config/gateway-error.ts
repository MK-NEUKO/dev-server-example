export interface GatewayError {
    hasError: boolean;
    title: string;
    name: string;
    ok: boolean;
    status?: number;
    statusText?: string;
    type?: string;
    url?: string;
}