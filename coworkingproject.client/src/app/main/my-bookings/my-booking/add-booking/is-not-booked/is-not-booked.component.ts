import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-is-not-booked',
  templateUrl: './is-not-booked.component.html',
  styleUrl: './is-not-booked.component.css',
})
export class IsNotBookedComponent {
  @Output() onSearchClose = new EventEmitter();

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }
}
