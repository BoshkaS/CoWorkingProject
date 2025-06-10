using CoWorkingProject.Server.DTOs;

namespace CoWorkingProject.Server.Services.Interfaces
{
	public interface IBookingService
	{
		Task<IEnumerable<BookingDto>> GetAllAsync();

		Task<BookingDto> AddAsync(BookingDto model);

		Task<BookingDto?> UpdateAsync(Guid id, BookingDto model);

		Task<bool> DeleteAsync(Guid id);
	}
}
