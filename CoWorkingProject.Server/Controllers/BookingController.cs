using CoWorkingProject.Server.Services;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingProject.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IBookingService bookingService;

		public BookingController(IBookingService bookingService) 
		{
			this.bookingService = bookingService;
		}

		[HttpGet]
		public async Task<IActionResult> GetBookings()
		{
			var result = await this.bookingService.GetAllAsync();
			return this.Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBooking(Guid id)
		{
			var deleted = await this.bookingService.DeleteAsync(id);
			return deleted ? this.NoContent() : this.NotFound();
		}
	}
}
