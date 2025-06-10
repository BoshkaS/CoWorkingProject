using CoWorkingProject.Server.Data;
using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingProject.Server.Services;

public class BookingService : IBookingService
{
	private readonly CoWorkingContext workingContext;

	public BookingService(CoWorkingContext workingContext)
	{
		this.workingContext = workingContext;
	}

	public Task<BookingDto> AddAsync(BookingDto model)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteAsync(Guid id)
	{
		var booking = await workingContext.BookingRooms.FirstOrDefaultAsync(b => b.Id == id);

		if (booking == null)
		{
			return false;
		}

		_ = this.workingContext.BookingRooms.Remove(booking);
		_ = await this.workingContext.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<BookingDto>> GetAllAsync()
	{
		Guid userId = new Guid("5cae0753-4c13-4d65-8091-1d328f2176db"); // ToDo: if adding authentification.

		return await this.workingContext.BookingRooms
			.Include(br => br.Room)
				.ThenInclude(r => r.Workspace)
			.Include(br => br.User)
			.Where(br => br.User!.Id == userId && br.To > DateTime.UtcNow)
			.Select(br => new BookingDto
			{
				Id = br.Id,
				WorkspaceType = EnumService.GetEnumMemberValue(br.Room!.Workspace!.Type),
				User = new UserDto
				{ 
					Email = br.User!.Email,
					Name = br.User!.Name,
				},
				Room = new RoomDto
				{
					RoomCount = br.Room.RoomCount,
					CapacityPerPerson = br.Room.CapacityPerPerson,
				},
				From = br.From,
				To = br.To
			}).ToListAsync();
	}

	public Task<BookingDto?> UpdateAsync(Guid id, BookingDto model)
	{
		throw new NotImplementedException();
	}
}
