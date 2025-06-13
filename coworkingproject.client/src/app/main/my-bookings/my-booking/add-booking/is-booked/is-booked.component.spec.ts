import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IsBookedComponent } from './is-booked.component';

describe('IsBookedComponent', () => {
  let component: IsBookedComponent;
  let fixture: ComponentFixture<IsBookedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IsBookedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IsBookedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
