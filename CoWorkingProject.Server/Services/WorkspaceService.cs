namespace CoWorkingProject.Server.Services;

using CoWorkingProject.Server.Data;
using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Entities.Enums;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class WorkspaceService : IWorkspaceService
{
    private readonly CoWorkingContext context;
    private readonly IHttpContextAccessor httpContextAccessor;

    public WorkspaceService(CoWorkingContext context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        this.httpContextAccessor = httpContextAccessor;
    }

    
    public async Task<IEnumerable<WorkspaceDto>> GetAllAsync(Guid coworkingId)
    {
		Guid userId = new Guid("8bb9d372-e3f9-433a-bd76-74f8f4887756");
		var workspaces = await this.context.Workspaces
            .Include(w => w.Amenities)
                .ThenInclude(w => w.Amenity)
            .Include(w => w.Rooms)
                .ThenInclude(w => w.Bookings)
					.ThenInclude(b => b.User)
            .Include(w => w.Images)
            .Where(w => w.CoworkingId == coworkingId)
            .ToListAsync();

		var displayList = workspaces.Select(w =>
		{
			var userBooking = w.Rooms
				.SelectMany(r => r.Bookings
					.Where(b => b.User!.Id == userId && b.To > DateTime.UtcNow))
				.OrderBy(b => b.From)
				.FirstOrDefault();

			return new WorkspaceDto
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
						RoomCount = r.RoomCount,
						CapacityPerPerson = r.CapacityPerPerson,
					})
					.ToList(),
				ImagePaths = w.Images
					.Select(img => $"{this.httpContextAccessor.HttpContext!.Request.Scheme}://{this.httpContextAccessor.HttpContext.Request.Host}/{img.ImagePath}")
					.ToList(),
				Capacity = userBooking!.Room?.CapacityPerPerson ?? 1,
				From = userBooking.From,
				To = userBooking.To,
			};
		}).ToList();

		return displayList;
    }

    public async Task<IEnumerable<RoomDto>> GetRoomsByWorkspaceType(Guid id)
    {
        var rooms = await this.context.Rooms
            .Include(r => r.Workspace)
            .Where(r => r.Workspace!.Id == id)
            .OrderBy(r => r.CapacityPerPerson)
            .ToListAsync();

        return rooms.Select(r => new RoomDto
        {
            RoomCount = r.RoomCount,
            CapacityPerPerson = r.CapacityPerPerson,
        });
    }
}
