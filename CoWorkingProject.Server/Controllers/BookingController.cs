namespace CoWorkingProject.Server.Controllers;

using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Services;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
	private readonly IBookingService bookingService;
	private readonly GroqService groqService;

	public BookingController(IBookingService bookingService, GroqService groqService) 
	{
		this.bookingService = bookingService;
		this.groqService = groqService;
	}

	[HttpGet]
	public async Task<IActionResult> GetBookings()
	{
		var result = await this.bookingService.GetAllAsync();
		return this.Ok(result);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetBooking(Guid id)
	{
		var result = await this.bookingService.GetById(id);
		return this.Ok(result);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBooking(Guid id)
	{
		var deleted = await this.bookingService.DeleteAsync(id);
		return deleted ? this.NoContent() : this.NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> AddBooking([FromBody] BookingPostDto dto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var result = await this.bookingService.AddAsync(dto);
			return Ok(result); // 200 OK with result
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message }); // 400 Bad Request
		}
		catch (InvalidOperationException ex)
		{
			return Conflict(new { message = ex.Message }); // 409 Conflict
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = "Unexpected error", detail = ex.Message });
		}
	}

	[HttpPost("ask")]
	public async Task<IActionResult> Ask([FromBody] string question)
	{
		var answer = await this.groqService.AskAssistant(question);

		return this.Ok(new { answer });
	}

	[HttpPatch("{id}")]
	public async Task<IActionResult> PatchBooking(Guid id, [FromBody] BookingPostDto dto)
	{
		var updatedBooking = await this.bookingService.UpdateAsync(id, dto);
		return Ok(updatedBooking);
	}
}
