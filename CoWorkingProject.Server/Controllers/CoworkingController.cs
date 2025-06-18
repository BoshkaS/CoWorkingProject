using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingProject.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoworkingController : ControllerBase
	{
		private readonly ICoworkingService coworkingService;

		public CoworkingController(ICoworkingService coworkingService)
		{
			this.coworkingService = coworkingService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCoworkings()
		{
			var result = await this.coworkingService.GetAllAsync();
			return Ok(result);
		}
	}
}
