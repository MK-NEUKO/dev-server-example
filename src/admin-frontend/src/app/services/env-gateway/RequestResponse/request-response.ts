import { ErrorDetails } from "../error/error-details";

export interface RequestResponse {
    isError: boolean;
    message: string;
    details?: ErrorDetails;
}