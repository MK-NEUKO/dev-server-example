import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnvironmentGatewaysComponent } from './environment-gateways-page.component';

describe('EnvironmentGatewaysComponent', () => {
  let component: EnvironmentGatewaysComponent;
  let fixture: ComponentFixture<EnvironmentGatewaysComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnvironmentGatewaysComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(EnvironmentGatewaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
