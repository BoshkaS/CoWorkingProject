using System.ComponentModel.DataAnnotations;

namespace CoWorkingProject.Server.DTOs;

public class BookingPostDto
{
	[Required]
	public string Name { get; set; } = null!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Required]
	public string WorkspaceType { get; set; } = null!;

	public int? DeskCount { get; set; }

	public int RoomCapacity { get; set; }

	[Required]
	public DateTime StartDateTime { get; set; }

	[Required]
	public DateTime EndDateTime { get; set; }

	public string? ImagePath { get; set; }
}
