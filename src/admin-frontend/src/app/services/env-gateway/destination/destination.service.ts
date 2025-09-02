import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RequestErrorHandler } from '../error/request-error-handler.service';
import { RequestResponse } from '../RequestResponse/request-response';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private httpClient = inject(HttpClient);
  private requestErrorHandler = inject(RequestErrorHandler);

  constructor() { }

  public SaveDestinationNameChanges(request: any): Promise<string> {
    return new Promise(resolve => {
      setTimeout(() => resolve('Test: Destination name change simulated'), 2000);
    })
    /*
    return new Promise((resolve, reject) => {
      this.httpClient.put(`https://localhost:9100/update-destination`, request, {

      }).subscribe({
        next: (response) => {
          console.log('Destination updated successfully:', response);
          resolve(`Destination updated successfully: ${JSON.stringify(response)}`);
        },
        error: (error) => {
          console.error('Error updating destination:', error);
          reject(`Error updating destination: ${JSON.stringify(error.message)}`);
        }
      });
    });
    */
  }

  public SaveDestinationAddressChanges(request: any): Promise<RequestResponse> {
    request.address = 'Httppp';

    return new Promise((resolve, reject) => {
      this.httpClient.put(`https://localhost:9100/update-destination`, request, {

      }).subscribe({
        next: (response) => {
          resolve({
            isError: false,
            message: 'Destination address was successfully changed.'
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
}
