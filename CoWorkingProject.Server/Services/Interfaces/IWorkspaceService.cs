using CoWorkingProject.Server.DTOs;
using System.Collections.Generic;

namespace CoWorkingProject.Server.Services.Interfaces;

public interface IWorkspaceService
{
	Task<IEnumerable<WorkspaceDto>> GetAllAsync(Guid coworkingId);

	Task<IEnumerable<RoomDto>> GetRoomsByWorkspaceType(Guid id);
}
