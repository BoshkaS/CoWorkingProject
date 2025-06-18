namespace CoWorkingProject.Server.Services;

using CoWorkingProject.Server.Data;
using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Entities;
using CoWorkingProject.Server.Entities.Enums;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class CoworkingService : ICoworkingService
{
	private readonly CoWorkingContext context;
	private readonly IHttpContextAccessor httpContextAccessor;

	public CoworkingService(CoWorkingContext context, IHttpContextAccessor httpContextAccessor)
	{
		this.context = context;
		this.httpContextAccessor = httpContextAccessor;
	}

	public async Task<IEnumerable<CoworkingDto>> GetAllAsync()
	{
		var coworkings = await this.context.Coworkings
			.Include(c => c.Workspaces)
				.ThenInclude(c => c.Rooms)
			.ToListAsync();

		var coworkingsDto = coworkings.Select(c => new CoworkingDto
		{
			Id = c.Id,
			Name = c.Name,
			Description = c.Description,
			Address = c.Address,
			DeskCount = this.GetRoomCount(c.Workspaces, WorkspaceType.OpenSpace),
			MeetingRoomCount = this.GetRoomCount(c.Workspaces, WorkspaceType.MeetingRoom),
			PrivateRoomCount = this.GetRoomCount(c.Workspaces, WorkspaceType.PrivateRoom),
			ImagePath = $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/{c.Photo}",
		});
		return coworkingsDto;
	}

	private int? GetRoomCount(List<Workspace> workspaces, WorkspaceType type)
	{
		return workspaces
			.Where(w => w.Type == type)
			.SelectMany(w => w.Rooms)
			.Sum(r => r.RoomCount);
	}
}
