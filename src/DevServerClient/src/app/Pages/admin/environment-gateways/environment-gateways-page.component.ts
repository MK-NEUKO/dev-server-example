import { Component } from '@angular/core';
import { GatewayComponent } from "./components/gateway/gateway.component";

@Component({
  selector: 'app-environment-gateways',
  imports: [GatewayComponent],
  templateUrl: './environment-gateways-page.component.html',
  styleUrl: './environment-gateways-page.component.css'
})
export class EnvironmentGatewaysComponent {
}
