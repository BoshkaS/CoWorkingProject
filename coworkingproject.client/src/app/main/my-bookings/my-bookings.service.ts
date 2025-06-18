import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BookingDto } from '../../Dto/BookingDto.model';
import { catchError, Observable, throwError } from 'rxjs';
import { BookingPostDto } from '../../Dto/BookingPostDto.model';
import { RoomDto } from '../../Dto/RoomDto.model';

@Injectable()
export class MyBookingsService {
  constructor(private http: HttpClient) {}

  getBookings() {
    return this.http.get<BookingDto[]>('https://localhost:7065/api/booking');
  }

  getBooking(id: string) {
    return this.http.get<BookingDto>(
      'https://localhost:7065/api/booking/' + id
    );
  }

  getRoomsByType(workspaceId: string): Observable<RoomDto[]> {
    return this.http.get<RoomDto[]>(
      `https://localhost:7065/api/workspace/get-rooms/${workspaceId}`
    );
  }

  addBooking(booking: BookingPostDto): Observable<any> {
    return this.http
      .post('https://localhost:7065/api/booking', booking)
      .pipe(catchError(this.handleError));
  }

  patchBooking(id: string, booking: BookingPostDto): Observable<any> {
    return this.http
      .patch(`https://localhost:7065/api/booking/${id}`, booking, {
        headers: { 'Content-Type': 'application/json' },
      })
      .pipe(catchError(this.handleError));
  }

  askAssistant(question: string): Observable<any> {
    return this.http.post(
      'https://localhost:7065/api/booking/ask',
      JSON.stringify(question),
      { headers: { 'Content-Type': 'application/json' } }
    );
  }

  private handleError(error: HttpErrorResponse) {
    const message =
      error.error?.message || 'Something went wrong with booking submission';
    return throwError(() => new Error(message));
  }
}
