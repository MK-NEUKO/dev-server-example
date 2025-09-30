import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClusterService {

  constructor() { }

  public SaveClusterNameChanges(request: any): Promise<void> {
    return new Promise((resolve) => {
      // Simulate an HTTP request with a timeout
      setTimeout(() => {
        console.log('Cluster name updated successfully:', request);
        resolve();
      }, 1000);
    });
  }
}
