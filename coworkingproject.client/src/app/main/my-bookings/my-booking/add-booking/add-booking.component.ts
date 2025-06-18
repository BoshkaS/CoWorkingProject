import { Component, OnInit, ViewChild } from '@angular/core';
import { RoomDto } from '../../../../Dto/RoomDto.model';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { CustomDateTimePickerComponent } from '../../../../custom-date-time-picker/custom-date-time-picker.component';
import { BookingPostDto } from '../../../../Dto/BookingPostDto.model';
import { NgForm } from '@angular/forms';
import { MyBookingsService } from '../../my-bookings.service';
import { BookingDto } from '../../../../Dto/BookingDto.model';

@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrl: './add-booking.component.css',
})
export class AddBookingComponent implements OnInit {
  workspaceType: string = '';
  rooms: RoomDto[] = [];
  workspaceId: string = '';

  selectedRoom: RoomDto | null = null;
  selectedDeskCount: number | null = null;

  bookingDto!: BookingDto;

  @ViewChild(CustomDateTimePickerComponent)
  dateTimePicker!: CustomDateTimePickerComponent;

  isModalBookedOpened = false;
  isModalNotBookedOpened = false;

  constructor(
    private router: Router,
    private location: Location,
    private bookingService: MyBookingsService
  ) {}

  ngOnInit(): void {
    const state = history.state as {
      workspaceType: string;
      rooms: RoomDto[];
      workspaceId: string;
    };

    if (state && state.workspaceType && state.rooms) {
      this.workspaceType = state.workspaceType;
      this.rooms = state.rooms;
      this.workspaceId = state.workspaceId;
    } else {
      console.log('No state received.');
    }
  }

  goBack() {
    this.location.back();
  }

  getDeskOptions(): number[] {
    const maxCapacity = this.rooms.length > 0 ? this.rooms[0].roomCount : 10;
    return Array.from({ length: maxCapacity }, (_, i) => i + 1);
  }

  isFormValid(form: NgForm): boolean {
    if (!form.valid || !this.dateTimePicker) return false;

    const type = this.workspaceType.toLowerCase();

    if (type === 'open space' && !this.selectedDeskCount) return false;
    if (type !== 'open space' && !this.selectedRoom) return false;

    const start = new Date(
      this.getDateTime(
        this.dateTimePicker.startYear,
        this.dateTimePicker.startMonth,
        this.dateTimePicker.startDay,
        this.dateTimePicker.selectedStartTime
      )
    );
    const end = new Date(
      this.getDateTime(
        this.dateTimePicker.endYear,
        this.dateTimePicker.endMonth,
        this.dateTimePicker.endDay,
        this.dateTimePicker.selectedEndTime
      )
    );

    const now = new Date();

    if (end <= start) return false;
    if (start < now) return false;

    const durationMs = end.getTime() - start.getTime();
    const durationDays = durationMs / (1000 * 60 * 60 * 24);

    if ((type === 'open space' || type === 'private room') && durationDays > 30)
      return false;
    if (type === 'meeting room' && durationDays > 1) return false;

    return true;
  }

  getDateTime(year: string, month: string, day: string, time: string): string {
    const monthIndex = this.dateTimePicker.months.indexOf(month) + 1;
    const [timeStr, meridian] = time.split(' ');
    let [hour, minute] = timeStr.split(':').map(Number);
    if (meridian === 'PM' && hour < 12) hour += 12;
    if (meridian === 'AM' && hour === 12) hour = 0;

    const iso = new Date(
      Number(year),
      monthIndex - 1,
      Number(day),
      hour,
      minute
    ).toISOString();

    return iso;
  }

  onAddBooking(formData: any) {
    if (!this.dateTimePicker) return;

    const start = this.getDateTime(
      this.dateTimePicker.startYear,
      this.dateTimePicker.startMonth,
      this.dateTimePicker.startDay,
      this.dateTimePicker.selectedStartTime
    );
    const end = this.getDateTime(
      this.dateTimePicker.endYear,
      this.dateTimePicker.endMonth,
      this.dateTimePicker.endDay,
      this.dateTimePicker.selectedEndTime
    );

    type NewType = BookingPostDto;

    const booking: NewType = {
      name: formData.name,
      email: formData.email,
      workspaceType: this.workspaceType,
      deskCount:
        this.workspaceType.toLowerCase() === 'open space'
          ? +formData.deskCount
          : undefined,
      roomCapacity:
        this.workspaceType.toLowerCase() !== 'open space'
          ? this.selectedRoom?.capacityPerPerson
          : undefined,
      startDateTime: start,
      endDateTime: end,
      workspaceId: this.workspaceId,
    };

    console.log('Booking DTO:', booking);
    this.bookingService.addBooking(booking).subscribe({
      next: (response) => {
        console.log('Booking successful:', response);
        this.bookingDto = response;
        this.handleOpenBookedModal();
      },
      error: (error) => {
        console.error('Booking failed:', error.message);
        this.handleOpenNotBookedModal();
      },
    });
  }

  handleOpenBookedModal() {
    this.isModalBookedOpened = true;
  }

  handleBookedClose() {
    this.isModalBookedOpened = false;
  }

  handleOpenNotBookedModal() {
    this.isModalNotBookedOpened = true;
  }

  handleNotBookedClose() {
    this.isModalNotBookedOpened = false;
  }
}
