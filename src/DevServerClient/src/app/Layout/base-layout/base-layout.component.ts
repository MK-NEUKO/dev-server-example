import { AfterViewInit, Component, inject } from '@angular/core';
import { NavbarComponent } from "../Components/navbar/navbar.component";
import { SidebarComponent } from '../Components/sidebar/sidebar.component';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from "../Components/footer/footer.component";
import { DarkModeService } from '../../services/dark-mode.service';


@Component({
  selector: 'app-base-layout',
  standalone: true,
  imports: [
    NavbarComponent,
    SidebarComponent,
    RouterOutlet,
    FooterComponent
  ],
  templateUrl: './base-layout.component.html',
  styleUrl: './base-layout.component.css'
})
export class BaseLayoutComponent implements AfterViewInit {

  darkModeService: DarkModeService = inject(DarkModeService);

  ngAfterViewInit(): void {
    this.setContentMinHeight();
  }

  setContentMinHeight() {
    const navbarHeight = document.getElementById('navbar')?.offsetHeight;
    const footerHeight = document.getElementById('footer')?.offsetHeight;
    const content = document.getElementById('content');

    if (navbarHeight && footerHeight && content) {
      const contentMinHeight = window.innerHeight - navbarHeight - footerHeight;
      content.style.minHeight = `${contentMinHeight}px`;
    }
  }
}
