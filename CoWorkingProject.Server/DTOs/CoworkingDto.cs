namespace CoWorkingProject.Server.DTOs;

public class CoworkingDto
{
	public Guid Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string Address {  get; set; } = string.Empty;

	public int? DeskCount { get; set; }

	public int? PrivateRoomCount { get; set; }

	public int? MeetingRoomCount { get; set; }

	public string? ImagePath { get; set; }
}
