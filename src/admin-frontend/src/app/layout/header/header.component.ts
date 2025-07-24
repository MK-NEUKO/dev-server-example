import { Component, effect, inject, OnInit } from '@angular/core';
import { ThemeTogglerComponent } from "../components/theme-toggler/theme-toggler.component";
import Keycloak from 'keycloak-js';

@Component({
  selector: 'app-header',
  imports: [ThemeTogglerComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {

  private readonly keycloak = inject(Keycloak);
  public authenticated: boolean = false;
  public userInfo: { username: string, roll: string } | null = null;



  ngOnInit(): void {
    this.authenticated = this.keycloak.authenticated || false;
    this.userInfo = {
      username: this.keycloak.tokenParsed?.['preferred_username'] || '',
      roll: this.keycloak.tokenParsed?.['realm_access']?.['roles']?.[0] || ''
    }
  }

}
