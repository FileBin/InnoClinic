import { AfterViewInit, Component, afterNextRender } from '@angular/core';
import { LoadingComponent } from '../loading/loading.component';
import { Specialization } from '../../services/services-api-proxy.service';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-specializations-carousel',
  standalone: true,
  imports: [LoadingComponent, NgbCarouselModule],
  templateUrl: './specializations-carousel.component.html',
  styleUrl: './specializations-carousel.component.scss'
})
export class SpecializationsCarouselComponent implements AfterViewInit {
  loaded = false;
  data: Specialization[] = [
    {
      id: "063d6e3b-a991-4ad8-b22e-0bc84b9d9df2",
      isActive: true,
      name: "Deontology",
    },
    {
      id: "195b51e4-1d82-471d-a050-c7c0bfab81f4",
      isActive: true,
      name: "Healthcare",
    },
    {
      id: "676356f8-fcb5-430b-932b-240f678feb4f",
      isActive: true,
      name: "Therapy",
    }
  ]

  ngAfterViewInit(): void {
    // DEMO loading
    window.setTimeout(() => {
      this.loaded = true;
    }, 2000);
  }
}
