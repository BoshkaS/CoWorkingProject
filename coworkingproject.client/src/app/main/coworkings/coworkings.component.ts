import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CoworkingDto } from '../../Dto/CoworkingDto.model';
import { CoworkingsService } from './coworkings.service';

@Component({
  selector: 'app-coworkings',
  templateUrl: './coworkings.component.html',
  styleUrl: './coworkings.component.css',
})
export class CoworkingsComponent implements OnInit {
  coworkings$!: Observable<CoworkingDto[]>;

  constructor(private coworkingsService: CoworkingsService) {}

  ngOnInit(): void {
    this.coworkings$ = this.coworkingsService.getCoworkings();
    console.log(this.coworkings$);
  }

  reloadPage(): void {
    window.location.reload();
  }
}
