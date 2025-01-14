import { Component, inject, Injectable } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterModule } from '@angular/router';
import { DarkModeService } from './Services/dark-mode.service';

@Injectable()
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Dev-Server-App';

  darkModeService: DarkModeService = inject(DarkModeService);
  constructor() {}
}