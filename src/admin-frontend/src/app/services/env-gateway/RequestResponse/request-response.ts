import { ErrorDetails } from "../error/error-details";

export interface RequestResponse {
    isError: boolean;
    isSuccess: boolean;
    message: string;
    details?: ErrorDetails;
}