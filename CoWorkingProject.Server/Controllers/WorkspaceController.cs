using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingProject.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkspaceController : ControllerBase
{
	private readonly IWorkspaceService workspaceService;

	public WorkspaceController(IWorkspaceService workspaceService)
	{
		this.workspaceService = workspaceService;
	}

	[HttpGet("{coworkingId}")]
	public async Task<IActionResult> GetWorkspaces(Guid coworkingId)
	{
		var result = await this.workspaceService.GetAllAsync(coworkingId);
		return this.Ok(result);
	}

	[HttpGet("get-rooms/{id}")]
	public async Task<IActionResult> GetRoomsByType(Guid id)
	{
		var rooms = await this.workspaceService.GetRoomsByWorkspaceType(id);
		return this.Ok(rooms);
	}
}
