using CoWorkingProject.Server.DTOs;

namespace CoWorkingProject.Server.Services.Interfaces
{
	public interface ICoworkingService
	{
		Task<IEnumerable<CoworkingDto>> GetAllAsync();
	}
}
