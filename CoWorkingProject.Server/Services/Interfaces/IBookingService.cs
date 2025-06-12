using CoWorkingProject.Server.DTOs;

namespace CoWorkingProject.Server.Services.Interfaces
{
	public interface IBookingService
	{
		Task<IEnumerable<BookingDto>> GetAllAsync();

		Task<BookingDto> AddAsync(BookingPostDto model);

		Task<BookingDto?> UpdateAsync(Guid id, BookingPostDto model);

		Task<bool> DeleteAsync(Guid id);

		Task<BookingDto> GetById(Guid id);
	}
}
