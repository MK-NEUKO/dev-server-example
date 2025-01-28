import { Component } from '@angular/core';
import { ThemeTogglerComponent } from "../theme-toggler/theme-toggler.component";

@Component({
    selector: 'app-navbar',
    imports: [ThemeTogglerComponent],
    templateUrl: './navbar.component.html',
    styleUrl: './navbar.component.css'
})
export class NavbarComponent {

}
