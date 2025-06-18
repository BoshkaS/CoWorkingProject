import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CoworkingDto } from '../../Dto/CoworkingDto.model';

@Injectable()
export class CoworkingsService {
  constructor(private http: HttpClient) {}

  getCoworkings() {
    return this.http.get<CoworkingDto[]>(
      'https://localhost:7065/api/coworking'
    );
  }
}
