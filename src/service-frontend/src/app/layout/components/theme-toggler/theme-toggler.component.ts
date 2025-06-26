import { Component, inject } from '@angular/core';
import { ThemeService } from '../../../services/theme/theme.service';

@Component({
  selector: 'app-theme-toggler',
  imports: [],
  templateUrl: './theme-toggler.component.html',
  styleUrl: './theme-toggler.component.css'
})
export class ThemeTogglerComponent {

  themeService = inject(ThemeService);

  toggleDarkMode() {
    this.themeService.updateDarkMode();
  }
}
