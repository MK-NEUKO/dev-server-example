import { effect, Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DarkModeService {

  darkModeSignal = signal<string>(
    JSON.parse(window.localStorage.getItem('darkModeSignal') ?? 'null')
  );

  updateDarkMode() {
    this.darkModeSignal.update((value) => (value === 'dark' ? 'light' : 'dark'));
    this.applyTheme();
  }

  constructor() {
    effect(() => {
      window.localStorage.setItem('darkModeSignal', JSON.stringify(this.darkModeSignal()))
    });
  }

  applyTheme() {
    const theme = this.darkModeSignal() === 'dark' ? 'dark' : 'light';
    document.documentElement.setAttribute('data-bs-theme', theme);
  }
}
