export interface CoworkingDto {
  id: string;
  name: string;
  description: string;
  address: string;
  deskCount?: number;
  privateRoomCount?: number;
  meetingRoomCount?: number;
  imagePath?: string;
}
