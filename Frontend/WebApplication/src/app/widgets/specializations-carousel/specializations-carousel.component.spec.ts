import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecializationsCarouselComponent } from './specializations-carousel.component';

describe('SpecializationsCarouselComponent', () => {
  let component: SpecializationsCarouselComponent;
  let fixture: ComponentFixture<SpecializationsCarouselComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpecializationsCarouselComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SpecializationsCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
