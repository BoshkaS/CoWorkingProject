import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DeleteBookingService } from './delete-booking.service';

@Component({
  selector: 'app-delete-booking',
  templateUrl: './delete-booking.component.html',
  styleUrl: './delete-booking.component.css',
})
export class DeleteBookingComponent {
  @Output() onSearchClose = new EventEmitter();
  @Input() id!: string;

  constructor(private deleteBookingService: DeleteBookingService) {}

  deleteBook() {
    if (this.id) {
      this.deleteBookingService.deleteBook(this.id).subscribe(() => {
        console.log('Book deleted successfully');
        this.closeHandler();
      });
    }
  }

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }
}
