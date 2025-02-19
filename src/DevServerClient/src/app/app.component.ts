import { Component, Injectable, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterModule } from '@angular/router';
import { DarkModeService } from './services/dark-mode.service';

@Injectable()
@Component({
  selector: 'app-root',
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'Dev-Server-App';

  constructor(private darkModeService: DarkModeService) { }

  ngOnInit() {
    this.darkModeService.applyTheme();
  }
}