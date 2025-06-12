import { Component, Input, ViewEncapsulation } from '@angular/core';
import { BookingDto } from '../../../Dto/BookingDto.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-booking',
  templateUrl: './my-booking.component.html',
  styleUrl: './my-booking.component.css',
})
export class MyBookingComponent {
  @Input() booking!: BookingDto;
  isDeleteModalOpened: boolean = false;

  constructor(private router: Router) {}

  formatDateRange(from: string, to: string): string {
    const fromDate = new Date(from);
    const toDate = new Date(to);

    const options: Intl.DateTimeFormatOptions = {
      month: 'short',
      day: 'numeric',
      year: 'numeric',
    };
    const fromFormatted = fromDate.toLocaleDateString('en-US', options);
    const toFormatted = toDate.toLocaleDateString('en-US', options);

    const diffInDays = Math.ceil(
      (toDate.getTime() - fromDate.getTime()) / (1000 * 60 * 60 * 24)
    );

    return `${fromFormatted} - ${toFormatted} (${diffInDays} day${
      diffInDays > 1 ? 's' : ''
    })`;
  }

  formatTimeRange(from: string, to: string): string {
    const fromDate = new Date(from);
    const toDate = new Date(to);

    const timeOptions: Intl.DateTimeFormatOptions = {
      hour: 'numeric',
      minute: '2-digit',
      hour12: true,
    };

    const fromTime = fromDate.toLocaleTimeString('en-US', timeOptions);
    const toTime = toDate.toLocaleTimeString('en-US', timeOptions);

    const fromHour = fromDate.getHours() + fromDate.getMinutes() / 60;
    const toHour = toDate.getHours() + toDate.getMinutes() / 60;

    const duration = toHour - fromHour;
    const roundedDuration = Math.round(duration * 10) / 10;

    return `from ${fromTime} to ${toTime} (${roundedDuration} hour${
      roundedDuration !== 1 ? 's' : ''
    })`;
  }

  handleOpenDeleleteModel() {
    this.isDeleteModalOpened = true;
  }

  handleCloseDeleteModal() {
    this.isDeleteModalOpened = false;
  }

  navigateToEditBooking() {
    this.router.navigate(['/edit-booking/' + this.booking.id]);
  }
}
