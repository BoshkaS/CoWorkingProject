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
		var result = await this.workspaceService.GetWorkspacesAsync();
		return Ok(result);
	}
}
