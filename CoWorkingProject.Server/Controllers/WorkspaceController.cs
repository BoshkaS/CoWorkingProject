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

	[HttpGet]
	public async Task<IActionResult> GetWorkspaces()
	{
		var result = await this.workspaceService.GetAllAsync();
		return Ok(result);
	}

	[HttpGet("by-type/{workspaceType}")]
	public async Task<IActionResult> GetRoomsByType(string workspaceType)
	{
		var rooms =  await workspaceService.GetRoomsByWorkspaceType(workspaceType);
		return Ok(rooms);
	}
}
