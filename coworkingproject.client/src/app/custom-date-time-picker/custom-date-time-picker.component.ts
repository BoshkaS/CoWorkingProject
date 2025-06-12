import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
} from '@angular/core';

@Component({
  selector: 'app-custom-date-time-picker',
  templateUrl: './custom-date-time-picker.component.html',
  styleUrl: './custom-date-time-picker.component.css',
})
export class CustomDateTimePickerComponent implements OnChanges {
  @Input() fromDateTime?: string;
  @Input() toDateTime?: string;

  @Output() dateTimeChanged = new EventEmitter<{
    startDate: string;
    endDate: string;
    startTime: string;
    endTime: string;
  }>();

  days = Array.from({ length: 31 }, (_, i) => i + 1);
  months = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December',
  ];
  years = Array.from({ length: 10 }, (_, i) => 2025 + i);

  startDay = '';
  startMonth = '';
  startYear = '';
  endDay = '';
  endMonth = '';
  endYear = '';

  timeSlots = [
    '0:00 PM',
    '1:00 PM',
    '1:30 PM',
    '2:00 PM',
    '2:30 PM',
    '3:00 PM',
    '3:30 PM',
    '4:00 PM',
    '4:30 PM',
    '5:00 PM',
  ];

  selectedStartTime = '';
  selectedEndTime = '';

  ngOnChanges(): void {
    if (this.fromDateTime) {
      const from = new Date(this.fromDateTime);
      this.startDay = from.getDate().toString();
      this.startMonth = this.months[from.getMonth()];
      this.startYear = from.getFullYear().toString();
      this.selectedStartTime = this.formatTime(from);
    }

    if (this.toDateTime) {
      const to = new Date(this.toDateTime);
      this.endDay = to.getDate().toString();
      this.endMonth = this.months[to.getMonth()];
      this.endYear = to.getFullYear().toString();
      this.selectedEndTime = this.formatTime(to);
    }
  }

  private formatTime(date: Date): string {
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const suffix = hours >= 12 ? 'PM' : 'AM';
    const hour12 = hours % 12 === 0 ? 12 : hours % 12;
    const formattedMinutes = minutes.toString().padStart(2, '0');
    return `${hour12}:${formattedMinutes} ${suffix}`;
  }

  emitDateTimeChange() {
    this.dateTimeChanged.emit({
      startDate: `${this.startYear}-${this.padMonth(
        this.startMonth
      )}-${this.pad(this.startDay)}`,
      endDate: `${this.endYear}-${this.padMonth(this.endMonth)}-${this.pad(
        this.endDay
      )}`,
      startTime: this.selectedStartTime,
      endTime: this.selectedEndTime,
    });
  }

  pad(value: string): string {
    return value.padStart(2, '0');
  }

  padMonth(monthName: string): string {
    const index = this.months.indexOf(monthName) + 1;
    return index.toString().padStart(2, '0');
  }
}
