import { effect, inject, Injectable, signal } from '@angular/core';
import { HighlightLoader } from 'ngx-highlightjs';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private hljsLoader: HighlightLoader = inject(HighlightLoader);

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
    this.hljsLoader.setTheme(theme === 'dark' ? 'styles/highlightjs/github-dark.min.css' : 'styles/highlightjs/github.min.css');
  }
}
