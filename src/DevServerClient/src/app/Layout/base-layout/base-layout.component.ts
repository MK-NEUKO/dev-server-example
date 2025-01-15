import { Component } from '@angular/core';
import { NavbarComponent } from "../Components/navbar/navbar.component";
import { SidebarComponent } from '../Components/sidebar/sidebar.component';
import { RouterOutlet } from '@angular/router';


@Component({
    selector: 'app-base-layout',
    imports: [
        NavbarComponent,
        SidebarComponent,
        RouterOutlet
    ],
    templateUrl: './base-layout.component.html',
    styleUrl: './base-layout.component.css'
})
export class BaseLayoutComponent {

}
