import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private httpClient = inject(HttpClient);

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

  public SaveDestinationAddressChanges(request: any): Promise<string> {
    return new Promise(resolve => {
      setTimeout(() => resolve('Test: Destination address change simulated'), 2000);
    });
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
}
