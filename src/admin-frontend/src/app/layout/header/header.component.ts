import { Component } from '@angular/core';
import { ThemeTogglerComponent } from "../components/theme-toggler/theme-toggler.component";

@Component({
  selector: 'app-header',
  imports: [ThemeTogglerComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}
