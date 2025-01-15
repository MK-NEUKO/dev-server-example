import { Component } from '@angular/core';
import { ThemeTogglerComponent } from "../theme-toggler/theme-toggler.component";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [ThemeTogglerComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

}
