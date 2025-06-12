import { RoomDto } from './RoomDto.model';

export interface WorkspaceDto {
  workspaceId: string;
  workspaceType: string;
  description: string;
  amenities: string[];
  rooms: RoomDto[];
  imagePaths: string[];
}
