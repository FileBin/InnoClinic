import { Component, Input, inject } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-user-menu',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatDividerModule],
  templateUrl: './user-menu.component.html',
  styleUrl: './user-menu.component.scss'
})
export class UserMenuComponent {
  private readonly oidcSecurityService = inject(OidcSecurityService);

  @Input({required: true}) userInfo: any;

  logout() {
    this.oidcSecurityService.logoff().subscribe((result) => console.log(result));
  }
}
