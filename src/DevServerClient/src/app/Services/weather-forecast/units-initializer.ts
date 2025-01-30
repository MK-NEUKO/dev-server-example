import { Units } from '../../models/weather-forecast/units';

export function initializeUnits(): Units {
    return {
        predictability: '',
        precipitation: '',
        windSpeed: '',
        precipitationProbability: '',
        relativeHumidity: '',
        temperature: '',
        time: '',
        pressure: '',
        windDirection: ''
    } as Units;
}