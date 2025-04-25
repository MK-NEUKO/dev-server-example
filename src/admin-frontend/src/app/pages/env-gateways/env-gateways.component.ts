import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-env-gateways',
  imports: [],
  templateUrl: './env-gateways.component.html',
  styleUrl: './env-gateways.component.css'
})
export class EnvGatewaysComponent {
  public configName: string = 'Configuration Name';

  constructor(private http: HttpClient) { }

  getCurrentConfig() {
    this.http.get<unknown>('envGateway/current-config').subscribe(data => {
      console.log(data);
    });
  }
}

