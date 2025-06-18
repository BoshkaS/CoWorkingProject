export interface BookingPostDto {
  name: string;
  email: string;
  workspaceId: string;
  workspaceType: string;
  deskCount?: number;
  roomCapacity?: number;
  startDateTime: string;
  endDateTime: string;
}
