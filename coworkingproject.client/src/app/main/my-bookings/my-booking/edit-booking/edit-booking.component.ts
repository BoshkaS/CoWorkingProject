import { Component, OnInit } from '@angular/core';
import { BookingDto } from '../../../../Dto/BookingDto.model';
import { MyBookingsService } from '../../my-bookings.service';
import { ActivatedRoute } from '@angular/router';
import { RoomDto } from '../../../../Dto/RoomDto.model';
import { BookingPostDto } from '../../../../Dto/BookingPostDto.model';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit-booking',
  templateUrl: './edit-booking.component.html',
  styleUrl: './edit-booking.component.css',
})
export class EditBookingComponent implements OnInit {
  booking!: BookingDto;
  bookingId!: string;

  name: string = '';
  email: string = '';
  workspaceType: string = '';
  selectedDeskCount!: number;
  selectedRoomCapacity!: number;
  rooms: RoomDto[] = [];

  selectedDateTime: {
    startDate: string;
    endDate: string;
    startTime: string;
    endTime: string;
  } | null = null;

  constructor(
    private bookingService: MyBookingsService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.bookingId = id;
        this.bookingService.getBooking(this.bookingId).subscribe((booking) => {
          this.booking = booking;

          this.name = booking.user.name;
          this.email = booking.user.email;
          this.workspaceType = booking.workspaceType;

          this.bookingService
            .getRoomsByType(this.workspaceType)
            .subscribe((rooms) => {
              this.rooms = rooms;

              if (this.workspaceType.toLowerCase() === 'open space') {
                this.selectedDeskCount = booking.room.capacityPerPerson;
              } else {
                this.selectedRoomCapacity = this.booking.room.capacityPerPerson;
              }
            });
        });
      }
    });
  }

  goBack() {
    this.location.back();
  }

  getDeskOptions(): number[] {
    const maxCapacity =
      this.rooms.length > 0 ? this.rooms[0].capacityPerPerson : 10;
    return Array.from({ length: maxCapacity }, (_, i) => i + 1);
  }

  getDateTimeFromPicker(date: string, time: string): string {
    const dateObj = new Date(`${date}T${this.convertTo24HourFormat(time)}`);
    return dateObj.toISOString();
  }

  convertTo24HourFormat(time: string): string {
    const [timePart, modifier] = time.split(' ');
    let [hours, minutes] = timePart.split(':').map(Number);

    if (modifier === 'PM' && hours < 12) {
      hours += 12;
    } else if (modifier === 'AM' && hours === 12) {
      hours = 0;
    }

    return `${hours.toString().padStart(2, '0')}:${minutes
      .toString()
      .padStart(2, '0')}:00`;
  }

  onDateTimeChanged(data: {
    startDate: string;
    endDate: string;
    startTime: string;
    endTime: string;
  }) {
    this.selectedDateTime = data;
  }

  onUpdateBooking(formData: any) {
    console.log('...');
    if (!this.selectedDateTime) return;

    const start = this.getDateTimeFromPicker(
      this.selectedDateTime.startDate,
      this.selectedDateTime.startTime
    );
    const end = this.getDateTimeFromPicker(
      this.selectedDateTime.endDate,
      this.selectedDateTime.endTime
    );

    const updatedBooking: BookingPostDto = {
      name: formData.name,
      email: formData.email,
      workspaceType: this.workspaceType,
      deskCount:
        this.workspaceType.toLowerCase() === 'open space'
          ? +this.selectedDeskCount
          : undefined,
      roomCapacity:
        this.workspaceType.toLowerCase() !== 'open space'
          ? this.selectedRoomCapacity
          : undefined,
      startDateTime: start,
      endDateTime: end,
    };

    console.log('Patch booking DTO:', updatedBooking);

    this.bookingService.patchBooking(this.bookingId, updatedBooking).subscribe({
      next: (res) => {
        console.log('Booking updated successfully:', res);
        alert('Booking updated successfully!');
        this.goBack();
      },
      error: (err) => {
        console.error('Update failed:', err.message);
        alert('Update failed: ' + err.message);
      },
    });
  }
}
