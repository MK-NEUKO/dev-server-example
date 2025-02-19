import { Component } from '@angular/core';
import { EnvironmentService } from '../../../services/environment/environment.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  constructor(private environmentService: EnvironmentService) { }


  getConfig() {
    this.environmentService.getConfigurations().subscribe(data => {
      console.log(data);
    });
  }

  getSlotOne() {
    console.log("got to slot one");
    window.open("https://localhost:7118", "_blank");
  }
}
