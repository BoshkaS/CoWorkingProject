import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IsNotBookedComponent } from './is-not-booked.component';

describe('IsNotBookedComponent', () => {
  let component: IsNotBookedComponent;
  let fixture: ComponentFixture<IsNotBookedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IsNotBookedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IsNotBookedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
