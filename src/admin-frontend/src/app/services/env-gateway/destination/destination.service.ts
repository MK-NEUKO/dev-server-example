import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Destination } from '../../../models/gateway-config/destination.model';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private httpClient = inject(HttpClient);

  constructor() { }

  public SaveChanges(request: any): Promise<string> {
    return new Promise((resolve, reject) => {
      this.httpClient.put(`envGateway/update-destination`, request).subscribe({
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
  }
}
