import { effect, Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  themeSignal = signal<string>(
    JSON.parse(window.localStorage.getItem('themeSignal') ?? 'null')
  );

  updateDarkMode() {
    this.themeSignal.update((value) => (value === 'dark' ? 'light' : 'dark'));
    this.applyTheme();
  }

  constructor() {
    effect(() => {
      window.localStorage.setItem('themeSignal', JSON.stringify(this.themeSignal()))
    });
  }

  applyTheme() {
    const theme = this.themeSignal() === 'dark' ? 'dark' : 'light';
    document.documentElement.setAttribute('data-neuko-theme', theme);
  }
}
