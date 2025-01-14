export interface Location {
    name: string;
    localNames: Record<string, string>;
    lat: number;
    lon: number;
    country: string;
    state: string;
}