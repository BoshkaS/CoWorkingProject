import { RoomDto } from './RoomDto.model';
import { UserDto } from './UserDto.model';

export interface BookingDto {
  id: string;
  user: UserDto;
  room: RoomDto;
  from: string;
  to: string;
  workspaceType: string;
}
