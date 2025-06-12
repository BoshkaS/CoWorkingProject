import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookingDto } from '../../../../../Dto/BookingDto.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-is-booked',
  templateUrl: './is-booked.component.html',
  styleUrl: './is-booked.component.css',
})
export class IsBookedComponent {
  @Output() onSearchClose = new EventEmitter();
  @Input() booking!: BookingDto;

  constructor(private router: Router) {}

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }

  navigateToBooking() {
    this.router.navigate(['/my-bookings']);
  }
}
