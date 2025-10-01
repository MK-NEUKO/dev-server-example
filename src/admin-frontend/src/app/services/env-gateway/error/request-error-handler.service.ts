import { Injectable } from '@angular/core';
import { RequestResponse } from '../RequestResponse/request-response';
import { ErrorDetails } from './error-details';


@Injectable({
  providedIn: 'root'
})
export class RequestErrorHandler {

  constructor() { }

  public handle(error: any): RequestResponse {
    if (error) {
      let errorMessage = '';
      if (error.errors) {
        errorMessage = error.errors[0].errorMessage;
      } else {
        errorMessage = error.message || 'An error occurred';
      }

      const errorTitle = error.title;
      const errorType = error.type;
      const errorStatus = error.status;
      const errorDetail = error.detail;
      const errorErrors = this.processErrorDetails(error.errors);

      return {
        isError: true,
        message: errorMessage,
        details: new ErrorDetails(
          errorTitle,
          errorType,
          errorStatus,
          errorDetail,
          errorErrors
        )
      };
    }

    return {
      isError: true,
      message: 'An unexpected error occurred.'
    };
  }
  processErrorDetails(errors: any[]): Record<string, string>[] {
    if (!Array.isArray(errors)) return [];
    return errors.map(e => ({
      propertyName: e.propertyName || 'unknown',
      errorMessage: e.errorMessage || 'unknown'
    }));
  }
}
