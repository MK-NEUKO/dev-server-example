import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RequestErrorHandler } from '../error/request-error-handler.service';
import { RequestResponse } from '../RequestResponse/request-response';
import { timeout } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private httpClient = inject(HttpClient);
  private requestErrorHandler = inject(RequestErrorHandler);

  constructor() { }

  public SaveDestinationNameChanges(request: any): Promise<RequestResponse> {
    return new Promise((resolve) => {
      this.httpClient.put(`https://localhost:9100/change-destination-name`, request, {})
        .subscribe({
          next: (response) => {
            console.log('Destination updated successfully:', response);
            resolve({
              isError: false,
              isSuccess: true,
              message: 'Destination name was successfully changed.'
            });
          },
          error: (error) => {
            console.error('Error updating destination:', error);
            const processedResponse = this.requestErrorHandler.handle(error.error);
            resolve(processedResponse);
          }
        });
    });
  }

  public SaveDestinationAddressChanges(request: any): Promise<RequestResponse> {
    return new Promise((resolve) => {
      this.httpClient.put(`https://localhost:9100/change-destination-address`, request, {})
        .subscribe({
          next: (response) => {
            resolve({
              isError: false,
              isSuccess: true,
              message: 'Destination address was successfully changed.'
            });
          },
          error: (error) => {
            const processedResponse = this.requestErrorHandler.handle(error.error);
            resolve(processedResponse);
          }
        });
    });
  }
}
