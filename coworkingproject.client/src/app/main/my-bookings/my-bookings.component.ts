import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { BookingDto } from '../../Dto/BookingDto.model';
import { MyBookingsService } from './my-bookings.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.css',
})
export class MyBookingsComponent {
  bookings$!: Observable<BookingDto[]>;

  constructor(
    private bookingService: MyBookingsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.bookings$ = this.bookingService.getBookings();
    console.log(this.bookings$);
  }

  navigateToWorkspaces() {
    this.router.navigate(['/workspaces']);
  }
}
