import { Component, Input } from '@angular/core';
import { WorkspaceDto } from '../../../Dto/WorkspaceDto.model';
import { RoomDto } from '../../../Dto/RoomDto.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrl: './workspace.component.css',
})
export class WorkspaceComponent {
  @Input() workspace!: WorkspaceDto;
  selectedImageIndex = 0;

  constructor(private router: Router) {}

  iconMap: Record<string, string> = {
    'Air Conditioning': 'air-conditioning',
    'Device gamepad': 'gamepad',
    WiFi: 'wifi',
    'Coffee Bar': 'coffee-bar',
    Headphones: 'headphones',
    Microphones: 'microphones',
  };

  mapAmenityToIcon(name: string): string {
    const icon = this.iconMap[name] || 'default-icon';
    return icon;
  }

  getCapacityPerPersonList(rooms: RoomDto[]): number[] {
    if (!rooms?.length) return [];

    const capacities = rooms.map((room) => room.capacityPerPerson);

    return Array.from(new Set(capacities)).sort((a, b) => a - b);
  }

  get capacityPerPersonText(): string {
    const type = this.workspace.workspaceType;
    const capacities = this.getCapacityPerPersonList(this.workspace.rooms);

    if (type === 'Open space') {
      const capacity = capacities[0] || 0;
      return `${capacity} decks`;
    }

    if (capacities.length === 2) {
      return `${capacities[0]} or ${capacities[1]} people`;
    }

    return capacities.join(', ') + ' people';
  }

  navigateToAddbooking(): void {
    this.router.navigate(['/add-booking'], {
      state: {
        workspaceType: this.workspace.workspaceType,
        rooms: this.workspace.rooms,
      },
    });
  }

  selectImage(index: number): void {
    this.selectedImageIndex = index;
  }
}
