namespace CoWorkingProject.Server.DTOs;

using CoWorkingProject.Server.Entities.Enums;

public class WorkspaceDto
{
	public Guid WorkspaceId { get; set; }

	public string WorkspaceType { get; set; }

	public string Description { get; set; }

	public List<string> Amenities { get; set; } = new List<string>();

	public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();

	public List<string> ImagePaths { get; set; } = new List<string>();

	public int? Capacity { get; set; }

	public DateTime? From { get; set; }

	public DateTime? To { get; set; }
}
