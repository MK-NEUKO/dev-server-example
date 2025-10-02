import { inject, Injectable } from '@angular/core';
import { GATEWAY_URIS } from '../gateway-uris';
import { HttpClient } from '@angular/common/http';
import { RequestErrorHandler } from '../error/request-error-handler.service';
import { RequestResponse } from '../RequestResponse/request-response';

@Injectable({
  providedIn: 'root'
})
export class ClusterService {

  private readonly CHANGE_CLUSTER_NAME = GATEWAY_URIS.PRODUCTION_BASE_URL + GATEWAY_URIS.CHANGE_CLUSTER_NAME;
  private httpClient = inject(HttpClient);
  private requestErrorHandler = inject(RequestErrorHandler);

  constructor() { }

  public SaveClusterNameChanges(request: any): Promise<RequestResponse> {
    return new Promise((resolve) => {
      this.httpClient.put(this.CHANGE_CLUSTER_NAME, request, {})
        .subscribe({
          next: (response) => {
            resolve({
              isError: false,
              isSuccess: true,
              message: 'Cluster name was successfully changed.'
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
