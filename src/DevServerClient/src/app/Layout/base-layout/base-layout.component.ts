import { Component, inject } from '@angular/core';
import { NavbarComponent } from "../Components/navbar/navbar.component";
import { SidebarComponent } from '../Components/sidebar/sidebar.component';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from "../Components/footer/footer.component";


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
export class BaseLayoutComponent {

}
