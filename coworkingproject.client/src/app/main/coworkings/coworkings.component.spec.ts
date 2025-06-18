import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoworkingsComponent } from './coworkings.component';

describe('CoworkingsComponent', () => {
  let component: CoworkingsComponent;
  let fixture: ComponentFixture<CoworkingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CoworkingsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoworkingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
