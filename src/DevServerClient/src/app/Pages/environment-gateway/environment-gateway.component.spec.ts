import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnvironmentGatewayComponent } from './environment-gateway.component';

describe('EnvironmentGatewayComponent', () => {
  let component: EnvironmentGatewayComponent;
  let fixture: ComponentFixture<EnvironmentGatewayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnvironmentGatewayComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnvironmentGatewayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
