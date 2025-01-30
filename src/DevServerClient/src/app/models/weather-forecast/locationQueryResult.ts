import { Location } from "./location";

export interface LocationQueryResult {
    query: string;
    iso2: string;
    currentPage: number;
    itemsPerPage: number;
    pages: number;
    count: number;
    orderBy: string;
    lat: number;
    lon: number;
    radius: number;
    type: string;
    results: Location[];
}