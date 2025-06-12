import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Destination } from '../../../models/gateway-config/destination.model';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private httpClient = inject(HttpClient);

  constructor() { }

  public SaveChanges(request: any): void {
    this.httpClient.put(`envGateway/update-destination`, request).subscribe({
      next: (response) => {
        console.log('Destination updated successfully:', response);
      }
    });
  }
}
