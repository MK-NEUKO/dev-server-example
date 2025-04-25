import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { EnvGatewaysComponent } from "./pages/env-gateways/env-gateways.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, EnvGatewaysComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'admin-frontend';
}
