import { Component, ViewChild, ElementRef } from '@angular/core';
import { EnvironmentService } from '../../../services/environment-gateway/environment-gateway.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  @ViewChild('canvas', { static: true }) canvas!: ElementRef<HTMLCanvasElement>;

  constructor(private environmentService: EnvironmentService) { }


  getConfig() {
    this.environmentService.getConfigurations().subscribe(data => {
      this.renderJsonOnCanvas(data);
    });
  }

  renderJsonOnCanvas(json: any) {
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
