import { LocationQueryResult } from '../../models/weather-forecast/locationQueryResult';
import { Location } from '../../models/weather-forecast/location';

export function initializeLocationQueryResult(): LocationQueryResult {
    return {
        query: '',
        iso2: '',
        currentPage: 0,
        itemsPerPage: 0,
        pages: 0,
        count: 0,
        orderBy: '',
        lat: 0,
        lon: 0,
        radius: 0,
        type: '',
        results: [] as Location[]
    };
}