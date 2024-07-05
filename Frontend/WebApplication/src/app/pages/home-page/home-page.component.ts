import { Component } from '@angular/core';
import { SpecializationsCarouselComponent } from '../../widgets/specializations-carousel/specializations-carousel.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [SpecializationsCarouselComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

}
