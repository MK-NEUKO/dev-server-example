import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { ThemeService } from './services/theme/theme.service';
import { SidebarComponent } from "./layout/sidebar/sidebar.component";
import { FooterComponent } from "./layout/footer/footer.component";

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    HeaderComponent,
    SidebarComponent,
    FooterComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'admin-frontend';

  themeService = inject(ThemeService);

  ngOnInit(): void {
    this.themeService.applyTheme();
  }
}
