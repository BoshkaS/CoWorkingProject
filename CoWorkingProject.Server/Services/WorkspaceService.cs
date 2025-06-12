using CoWorkingProject.Server.Data;
using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Entities.Enums;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using System.Runtime.Serialization;

namespace CoWorkingProject.Server.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly CoWorkingContext context;
	private readonly IHttpContextAccessor httpContextAccessor;

    public WorkspaceService(CoWorkingContext context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
		this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<WorkspaceDto>> GetAllAsync()
    {
		var workspaces = await this.context.Workspaces
	        .Include(w => w.Amenities)
		        .ThenInclude(w => w.Amenity)
	        .Include(w => w.Rooms)
		        .ThenInclude(w => w.Bookings)
	        .Include(w => w.Images)
	        .ToListAsync();

		var displayList = workspaces.Select(w => new WorkspaceDto
		{
			WorkspaceId = w.Id,
			WorkspaceType = EnumService.GetEnumMemberValue(w.Type),
			Description = w.Description,
			Amenities = w.Amenities
				.OrderBy(a => a.Amenity?.Name)
				.Select(a => a.Amenity?.Name ?? string.Empty)
				.ToList(),
			Rooms = w.Rooms
				.OrderBy(r => r.CapacityPerPerson)
				.Select(r => new RoomDto
				{
					CapacityPerPerson = r.CapacityPerPerson,
					RoomCount = r.RoomCount,
				})
				.ToList(),
			ImagePaths = w.Images
				.Select(img => $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/{img.ImagePath}")
				.ToList()
		}).ToList();

		return displayList;
    }

	public async Task<IEnumerable<RoomDto>> GetRoomsByWorkspaceType(string workspaceType)
	{
		var parsedType = EnumService.GetEnumFromEnumMemberValue<WorkspaceType>(workspaceType);
		if (parsedType == null)
			throw new ArgumentException("Invalid workspace type.");

		var rooms = await this.context.Rooms
            .Include(r => r.Workspace)
			.Where(r => r.Workspace.Type == parsedType)
            .OrderBy(r => r.CapacityPerPerson)
			.ToListAsync();

		return rooms.Select(r => new RoomDto
		{
            RoomCount = r.RoomCount,
			CapacityPerPerson = r.CapacityPerPerson,
		});
	}
}
