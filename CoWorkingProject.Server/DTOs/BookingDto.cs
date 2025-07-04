﻿using CoWorkingProject.Server.Entities.Enums;

namespace CoWorkingProject.Server.DTOs;

public class BookingDto
{
	public Guid Id { get; set; }

	public string WorkspaceType { get; set; }

	public string CoworkingName { get; set; }

	public Guid WorkspaceId { get; set; }

	public UserDto User { get; set; }

	public RoomDto Room { get; set; }

	public DateTime From { get; set; }

	public DateTime To { get; set; }

	public string? ImagePath { get; set; }
}
