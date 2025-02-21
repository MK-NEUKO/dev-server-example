import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { EnvironmentService } from '../../../services/environment-gateway/environment-gateway.service';
import { EnvironmentGatewayDataService } from '../../../services/environment-gateway/environment-gateway-data.service';
import { Subscription } from 'rxjs';
import { GatewayConfiguration } from '../../../models/environment-gateway/gatewayConfiguration';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  private subscription: Subscription = new Subscription();
  private gatewayConfiguration?: GatewayConfiguration;
  @ViewChild('canvas', { static: true }) canvas!: ElementRef<HTMLCanvasElement>;

  constructor(
    private environmentService: EnvironmentService,
    private environmentDataService: EnvironmentGatewayDataService
  ) { }

  ngOnInit() {
    this.subscription.add(this.environmentDataService.getGatewayConfiguration().subscribe((data) => {
      this.gatewayConfiguration = data ?? this.environmentDataService.getDefaultGatewayConfiguration();
      this.renderJsonOnCanvas(this.gatewayConfiguration);
    }));
  }

  getConfig() {
    this.subscription.add(this.environmentService.getConfigurations().subscribe((data) => {
      this.environmentDataService.setGatewayConfiguration(data);
    }));
  }

  renderJsonOnCanvas(json: GatewayConfiguration) {
    const canvas = this.canvas.nativeElement;
    const ctx = canvas.getContext('2d');

    if (ctx) {
      ctx.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas
      ctx.font = '16px Arial';
      ctx.fillStyle = 'black';

      const jsonString = JSON.stringify(json, null, 2); // Pretty print JSON
      const lines = jsonString.split('\n');

      lines.forEach((line, index) => {
        ctx.fillText(line, 10, 20 + index * 20); // Adjust the position as needed
      });
    }
  }

  getSlotOne() {
    console.log("got to slot one");
    window.open("https://localhost:7118", "_blank");
  }
}
