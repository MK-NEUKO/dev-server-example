export interface Location {
    id: number;
    name: string;
    countryCodeIso2: string;
    country: string;
    state: string;
    lat: number;
    lon: number;
    aboveSeaLevel: number;
    timezone: string;
    population: number;
    distance: number;
    icaoCode: string;
    iataCode: string;
    postcodes: string[];
    featureClass: string;
    featureCode: string;
    meteoBlueUrl: string;
}