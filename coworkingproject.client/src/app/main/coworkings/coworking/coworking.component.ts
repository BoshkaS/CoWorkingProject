import { Component, Input } from '@angular/core';
import { CoworkingDto } from '../../../Dto/CoworkingDto.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-coworking',
  templateUrl: './coworking.component.html',
  styleUrl: './coworking.component.css',
})
export class CoworkingComponent {
  @Input() coworking!: CoworkingDto;

  constructor(private router: Router) {}

  navigateToWorkspaces() {
    this.router.navigate(['/workspaces/' + this.coworking.id]);
  }
}
