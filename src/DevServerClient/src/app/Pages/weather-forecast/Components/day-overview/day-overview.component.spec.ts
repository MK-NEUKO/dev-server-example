import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForecastNavItemComponent } from './day-overview.component';

describe('ForecastNavItemComponent', () => {
  let component: ForecastNavItemComponent;
  let fixture: ComponentFixture<ForecastNavItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForecastNavItemComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ForecastNavItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
