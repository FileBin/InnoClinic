import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { UserInfoComponent } from '../user-info/user-info.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterModule, MatButtonModule, MatIconModule, MatToolbarModule, UserInfoComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Input() appName = 'App'

  @Output() toggleSidenav = new EventEmitter<void>();
}
