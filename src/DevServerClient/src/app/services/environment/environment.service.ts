import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService {

  constructor(private http: HttpClient) { }

  getConfigurations(): Observable<any> {
    return this.http.get<any>('https://localhost:7118/configuration');
  }
}
