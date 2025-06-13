import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class DeleteBookingService {
  constructor(private http: HttpClient) {}

  deleteBook(bookingId: string) {
    return this.http.delete('https://localhost:7065/api/booking/' + bookingId);
  }
}
