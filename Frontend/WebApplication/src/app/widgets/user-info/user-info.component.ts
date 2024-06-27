import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import {UserMenuComponent} from '../user-menu/user-menu.component'

@Component({
  selector: 'app-user-info',
  standalone: true,
  imports: [MatIcon, MatButtonModule, MatProgressSpinnerModule, RouterModule, UserMenuComponent],
  templateUrl: './user-info.component.html',
  styleUrl: './user-info.component.scss'
})
export class UserInfoComponent {
  private readonly oidcSecurityService = inject(OidcSecurityService);
  isLoading = false;
  isLoggedIn = false;

  userData: any;

  ngOnInit() {
    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated, userData }) => {
      this.isLoggedIn = isAuthenticated;
      this.userData = userData;
    });
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff().subscribe((result) => console.log(result));
  }

  isExpanded = false;
}
