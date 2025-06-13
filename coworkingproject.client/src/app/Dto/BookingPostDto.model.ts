export interface BookingPostDto {
  name: string;
  email: string;
  workspaceType: string;
  deskCount?: number;
  roomCapacity?: number;
  startDateTime: string;
  endDateTime: string;
}
