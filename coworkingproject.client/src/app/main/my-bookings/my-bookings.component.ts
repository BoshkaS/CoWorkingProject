import { Component } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
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
  userQuestion: string = '';
  assistantResponse: string = '';

  constructor(
    private bookingService: MyBookingsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.bookings$ = this.bookingService.getBookings();
    console.log(this.bookings$);
  }

  onQuestionClick(question: string) {
    this.userQuestion = question;
    this.onSubmit();
  }

  onSubmit() {
    const trimmed = this.userQuestion.trim();
    if (!trimmed) return;

    this.bookingService
      .askAssistant(trimmed)
      .pipe(
        catchError((err) => {
          console.error('Assistant error:', err);
          this.assistantResponse = 'Sorry, something went wrong.';
          return of(null);
        })
      )
      .subscribe((response) => {
        if (response) {
          this.assistantResponse = response.answer || 'No answer returned.';
        }
      });
  }

  navigateToWorkspaces() {
    this.router.navigate(['/coworkings']);
  }
}
