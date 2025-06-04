using CoWorkingProject.Server.Data;
using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingProject.Server.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly CoWorkingContext context;

    public WorkspaceService(CoWorkingContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<WorkspaceDto>> GetWorkspacesAsync()
    {
        var workspaces = await this.context.Workspaces
            .Include(w => w.Amenities)
                .ThenInclude(w => w.Amenity)
            .Include(w => w.Rooms)
                .ThenInclude(w => w.Bookings)
            .ToListAsync();

        var displayList = workspaces.Select(w => new WorkspaceDto
        {
            WorkspaceId = w.Id,
            WorkspaceType = w.Type,
            Description = w.Description,
            Amenities = w.Amenities.Select(a => a.Amenity?.Name ?? string.Empty).ToList(),
            Rooms = w.Rooms.Select(r => new RoomDto
            {
                CapacityPerPerson = r.CapacityPerPerson,
                RoomCount = r.RoomCount,
            }).ToList(),
        }).ToList();

        return displayList;
    }
}
