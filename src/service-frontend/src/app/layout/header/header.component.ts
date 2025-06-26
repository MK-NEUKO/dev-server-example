import { Component, inject, OnInit } from '@angular/core';
import { ThemeTogglerComponent } from "../components/theme-toggler/theme-toggler.component";
import { HttpClient } from '@angular/common/http';
import Keycloak from 'keycloak-js';

@Component({
  selector: 'app-header',
  imports: [ThemeTogglerComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  openAdminFrontend() {
    if (this.keycloak.authenticated && this.keycloak.token) {
      // Admin-Frontend öffnen
      const adminWindow = window.open('http://localhost:4300', '_blank');
      
      // Event Listener für Nachrichten vom Admin-Frontend
      const messageHandler = (event: MessageEvent) => {
        // Sicherheitscheck: Nur Nachrichten vom Admin-Frontend akzeptieren
        if (event.origin !== 'http://localhost:4300') return;
        
        if (event.data.type === 'REQUEST_TOKEN') {
          // Token an Admin-Frontend senden
          adminWindow?.postMessage({
            type: 'TOKEN_RESPONSE',
            token: this.keycloak.token,
            refreshToken: this.keycloak.refreshToken
          }, 'http://localhost:4300');
          
          // Event Listener nach dem Senden entfernen
          window.removeEventListener('message', messageHandler);
        }
      };
      
      window.addEventListener('message', messageHandler);
    } else {
      // Benutzer ist nicht authentifiziert
      console.warn('User is not authenticated. Cannot open admin frontend.');
      // Optional: Login-Dialog anzeigen
      this.manageAccess();
    }
  }



  private httpClient = inject(HttpClient);
  private readonly keycloak = inject(Keycloak);
  public logSwitch: string = '';
  public authenticated: boolean = false;
  public userInfo: { username: string, roll: string } | null = null;

  ngOnInit(): void {
    this.logSwitch = this.keycloak.authenticated ? 'Logout' : 'Login';
    this.authenticated = this.keycloak.authenticated || false;
    this.userInfo = {
      username: this.keycloak.tokenParsed?.['preferred_username'] || 'loadError',
      roll: this.keycloak.tokenParsed?.['realm_access']?.['roles']?.[0] || "loadError"
    }
  }


  public manageAccess(): void {
    if (this.keycloak.authenticated) {
      this.keycloak.logout().then(() => {
        this.logSwitch = 'Login';
      });
    } else {
      this.keycloak.login().then(() => {
        this.logSwitch = 'Logout';
      });
    }
  }

  public printTokenAndUserInfo(): void {
    if (this.keycloak.authenticated) {
      // Access Token
      console.log('Access Token:', this.keycloak.token);
      // User Info (decoded Token)
      const tokenParsed = this.keycloak.tokenParsed;
      console.log('User Info:', tokenParsed);
    } else {
      console.log('User is not logged in.');
    }
  }

}
