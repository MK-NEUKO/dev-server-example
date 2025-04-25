import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnvGatewaysComponent } from './env-gateways.component';

describe('EnvGatewaysComponent', () => {
  let component: EnvGatewaysComponent;
  let fixture: ComponentFixture<EnvGatewaysComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnvGatewaysComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnvGatewaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
