import { Component, ViewChild } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule, MatSidenavModule, MatIcon, MatButton, MatFormFieldModule, MatDividerModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  @ViewChild('drawer', { static: true }) public drawer: MatDrawer | undefined;

  private swipeCoord?: [number, number];
  private swipeTime?: number;

  swipe(e: TouchEvent, when: string): void {
    const coord: [number, number] = [e.changedTouches[0].clientX, e.changedTouches[0].clientY];
    const time = new Date().getTime();

    switch (when) {
      case 'start':
        this.swipeCoord = coord;
        this.swipeTime = time;
        break;
      case 'end':
        if (this.swipeCoord && this.swipeTime) {
          const direction = [coord[0] - this.swipeCoord[0], coord[1] - this.swipeCoord[1]];
          const duration = time - this.swipeTime;

          const isFast = duration < 1000;
          const isLong = Math.abs(direction[0]) > 30;
          const isHorizontal = Math.abs(direction[0]) > Math.abs(direction[1] * 3);

          if (isFast && isHorizontal && isLong) {
            if (direction[0] < 0) {
              this.drawer?.close();
            } else {
              this.drawer?.open();
            }
          }
        }
        break;
    }
  }
}