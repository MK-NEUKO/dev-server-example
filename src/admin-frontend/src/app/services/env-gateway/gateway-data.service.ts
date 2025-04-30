import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, finalize } from 'rxjs';
import { GatewayApiService } from './gateway-api.service';
import { GatewayConfig } from '../../models/gateway-config/gateway-config.model';
import { defaultGatewayConfig } from './default-gateway-config';
import { GatewayError } from '../../models/gateway-config/gateway-error';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GatewayDataService {

  private currentConfig = new BehaviorSubject<GatewayConfig>(defaultGatewayConfig());
  private isLoading = new BehaviorSubject<boolean>(false);
  private error = new BehaviorSubject<GatewayError>({
    hasError: false,
    title: '',
    name: '',
    ok: true,
    status: undefined,
    statusText: '',
    type: undefined,
    url: ''
  });

  constructor(private gatewayApiService: GatewayApiService) { }

  queryCurrentConfig(): void {
    this.isLoading.next(true);
    this.error.next({
      hasError: false,
      title: '',
      name: '',
      ok: true,
      status: undefined,
      statusText: '',
      type: undefined,
      url: ''
    });

    this.gatewayApiService.getCurrentConfig()
      .pipe(catchError(error => {
        this.processUiError(error);
        return [defaultGatewayConfig()];
      }),
        finalize(() => this.isLoading.next(false))

      ).subscribe((config) => {
        this.currentConfig.next(config);
      });
  }

  getCurrentConfig(): BehaviorSubject<GatewayConfig> {
    return this.currentConfig;
  }

  getLoadingState(): BehaviorSubject<boolean> {
    return this.isLoading;
  }

  getErrorState(): BehaviorSubject<GatewayError> {
    return this.error;
  }

  private processUiError(error: HttpErrorResponse): void {
    let processedUiError: GatewayError = {
      hasError: false,
      title: '',
      name: '',
      ok: true,
      status: undefined,
      statusText: '',
      type: 'undefined',
      url: 'undefined'
    }

    switch (error.status) {
      case 0:
        processedUiError.hasError = true;
        processedUiError.title = 'Network Error';
        processedUiError.name = error.name;
        processedUiError.ok = error.ok;
        processedUiError.status = error.status;
        processedUiError.statusText = error.statusText;
        processedUiError.type = error.type?.toString();
        processedUiError.url = error.url?.toString();
        break;
      case 500:
        processedUiError.hasError = true;
        processedUiError.title = 'Server Error';
        processedUiError.name = error.name;
        processedUiError.ok = error.ok;
        processedUiError.status = error.status;
        processedUiError.statusText = error.statusText;
        processedUiError.type = error.type?.toString();
        processedUiError.url = error.url?.toString();
        break;
      default:
        processedUiError.hasError = true;
        processedUiError.title = 'Unknown Error';
        processedUiError.name = error.name;
        processedUiError.ok = error.ok;
        processedUiError.status = error.status;
        processedUiError.statusText = error.statusText;
        processedUiError.type = error.type?.toString();
        processedUiError.url = error.url?.toString();
    }

    this.error.next(processedUiError);
  }
}
