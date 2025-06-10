import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BookingDto } from '../../Dto/BookingDto.model';

@Injectable()
export class MyBookingsService {
  constructor(private http: HttpClient) {}

  getBookings() {
    return this.http.get<BookingDto[]>('https://localhost:7065/api/booking');
  }
}
