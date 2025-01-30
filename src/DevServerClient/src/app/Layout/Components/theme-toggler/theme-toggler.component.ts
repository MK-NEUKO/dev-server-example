import { Component, inject } from '@angular/core';
import { DarkModeService } from '../../../services/dark-mode.service';

@Component({
    selector: 'app-theme-toggler',
    imports: [],
    templateUrl: './theme-toggler.component.html',
    styleUrl: './theme-toggler.component.css'
})
export class ThemeTogglerComponent {

  darkModeService: DarkModeService = inject(DarkModeService);

  toggleDarkMode() {
    this.darkModeService.updateDarkMode();
  }
}
