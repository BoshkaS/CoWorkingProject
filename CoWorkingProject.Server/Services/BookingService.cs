using CoWorkingProject.Server.Data;
using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Entities;
using CoWorkingProject.Server.Entities.Enums;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingProject.Server.Services;

public class BookingService : IBookingService
{
    private readonly CoWorkingContext workingContext;
	private readonly IHttpContextAccessor httpContextAccessor;

    public BookingService(CoWorkingContext workingContext, IHttpContextAccessor httpContextAccessor)
    {
        this.workingContext = workingContext;
		this.httpContextAccessor = httpContextAccessor;
    }

	public async Task<BookingDto> AddAsync(BookingPostDto model)
	{
		if (model.StartDateTime >= model.EndDateTime)
			throw new ArgumentException("End date must be after start date.");

		if (model.StartDateTime < DateTime.UtcNow)
			throw new ArgumentException("Booking cannot start in the past.");

		var duration = model.EndDateTime - model.StartDateTime;
		var workspaceType = model.WorkspaceType.ToLower();

		if ((workspaceType == "open space" || workspaceType == "private room") && duration.TotalDays > 30)
			throw new ArgumentException("Booking cannot exceed 30 days for Open Space or Private Room.");

		if (workspaceType == "meeting room" && duration.TotalDays > 1)
			throw new ArgumentException("Booking cannot exceed 1 day for Meeting Room.");

		var user = await workingContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
		if (user == null)
			throw new ArgumentException("User with the given email not found.");

		var parsedType = EnumService.GetEnumFromEnumMemberValue<WorkspaceType>(workspaceType);
		if (parsedType == null)
			throw new ArgumentException("Invalid workspace type.");

		var candidateRooms = await workingContext.Rooms
			.Include(r => r.Workspace)
			.Include(r => r.Bookings)
			.Where(r =>
				r.Workspace != null &&
				r.Workspace.Type == parsedType &&
				(
					parsedType == WorkspaceType.OpenSpace ||
					r.CapacityPerPerson == model.RoomCapacity
				))
			.ToListAsync();

		Room? availableRoom = null;

		if (parsedType == WorkspaceType.OpenSpace)
		{
			availableRoom = candidateRooms.FirstOrDefault(r =>
			{
				var overlappingBookings = r.Bookings
					.Where(b => b.To > DateTime.UtcNow &&
								b.From < model.EndDateTime && b.To > model.StartDateTime)
					.Count();

				var totalDesks = r.RoomCount ?? 0;
				return overlappingBookings + model.DeskCount <= totalDesks;
			});
		}
		else
		{
			availableRoom = candidateRooms.FirstOrDefault(r =>
			{
				var overlappingBookings = r.Bookings
					.Where(b => b.To > DateTime.UtcNow &&
								b.From < model.EndDateTime && b.To > model.StartDateTime)
					.Count();

				var totalRooms = r.RoomCount ?? 1;
				return overlappingBookings < totalRooms;
			});
		}

		if (availableRoom == null)
			throw new InvalidOperationException("No available rooms in the selected time range.");

		string imagePath = parsedType switch
		{
			WorkspaceType.MeetingRoom => "images/mr_1.png",
			WorkspaceType.OpenSpace => "images/os_1.png",
			WorkspaceType.PrivateRoom => "images/pr_1.jpg",
			_ => "images/default.png"
		};

		var booking = new BookingRoom();

		if (parsedType == WorkspaceType.OpenSpace && model.DeskCount.HasValue)
		{
			for (int i = 0; i < model.DeskCount.Value; i++)
			{
				booking = new BookingRoom
				{
					Id = Guid.NewGuid(),
					From = model.StartDateTime,
					To = model.EndDateTime,
					UserId = user.Id,
					RoomId = availableRoom.Id,
					User = user,
					Room = availableRoom,
					ImagePath = imagePath,
				};
				await workingContext.BookingRooms.AddAsync(booking);
			}
		}
		else
		{
			booking = new BookingRoom
			{
				Id = Guid.NewGuid(),
				From = model.StartDateTime,
				To = model.EndDateTime,
				UserId = user.Id,
				RoomId = availableRoom.Id,
				User = user,
				Room = availableRoom,
				ImagePath = imagePath,
			};
			await workingContext.BookingRooms.AddAsync(booking);
		}

		await workingContext.SaveChangesAsync();

		return new BookingDto
		{
			Id = booking.Id,
			From = booking.From,
			To = booking.To,
			WorkspaceType = model.WorkspaceType,
			User = new UserDto
			{
				Name = user.Name,
				Email = user.Email
			},
			Room = new RoomDto
			{
				RoomCount = availableRoom.RoomCount,
				CapacityPerPerson = availableRoom.CapacityPerPerson,
			},
			ImagePath = imagePath,
		};
	}


	[HttpDelete("{id}")]
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
		Guid userId = new Guid("9d2d16fc-6ce4-47b9-860a-7b5d11db13b0"); // ToDo: if adding authentification.

		return await this.workingContext.BookingRooms
			.Include(br => br.Room)
				.ThenInclude(r => r.Workspace)
			.Include(br => br.User)
			.Where(br => br.User!.Id == userId && br.To > DateTime.UtcNow)
			.OrderBy(br => br.From)
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
				To = br.To,
				ImagePath = $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/{br.ImagePath}"
			}).ToListAsync();
	}
	
	public async Task<BookingDto> GetById(Guid id)
	{
		Guid userId = new Guid("9d2d16fc-6ce4-47b9-860a-7b5d11db13b0");

		return await this.workingContext.BookingRooms
			.Include(br => br.Room)
				.ThenInclude(r => r.Workspace)
			.Include(br => br.User)
			.Where(br => br.User!.Id == userId && br.Id == id)
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
			}).FirstOrDefaultAsync();
	}

	public async Task<BookingDto?> UpdateAsync(Guid id, BookingPostDto model)
	{
		if (model.StartDateTime >= model.EndDateTime)
			throw new ArgumentException("End date must be after start date.");

		if (model.StartDateTime < DateTime.UtcNow)
			throw new ArgumentException("Booking cannot start in the past.");

		var duration = model.EndDateTime - model.StartDateTime;
		var workspaceType = model.WorkspaceType.ToLower();

		if ((workspaceType == "open space" || workspaceType == "private room") && duration.TotalDays > 30)
			throw new ArgumentException("Booking cannot exceed 30 days for Open Space or Private Room.");

		if (workspaceType == "meeting room" && duration.TotalDays > 1)
			throw new ArgumentException("Booking cannot exceed 1 day for Meeting Room.");

		var existingBooking = await workingContext.BookingRooms
			.Include(b => b.User)
			.Include(b => b.Room)
			.ThenInclude(r => r.Workspace)
			.FirstOrDefaultAsync(b => b.Id == id);

		if (existingBooking == null)
			throw new KeyNotFoundException("Booking not found.");

		bool overlapExists = await workingContext.BookingRooms
			.AnyAsync(b =>
				b.Id != id &&
				b.From < model.EndDateTime &&
				b.To > model.StartDateTime
			);

		if (overlapExists)
			throw new InvalidOperationException("A booking already exists in this time range.");

		var user = await workingContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
		if (user == null)
			throw new ArgumentException("User with the given email not found.");

		var parsedType = EnumService.GetEnumFromEnumMemberValue<WorkspaceType>(model.WorkspaceType.ToLower());
		if (parsedType == null)
			throw new ArgumentException("Invalid workspace type.");

		Room? room = await workingContext.Rooms
			.Include(r => r.Workspace)
			.FirstOrDefaultAsync(r =>
				r.Workspace != null &&
				r.Workspace.Type == parsedType &&
				r.CapacityPerPerson == model.RoomCapacity);

		existingBooking.From = model.StartDateTime;
		existingBooking.To = model.EndDateTime;
		existingBooking.UserId = user.Id;
		existingBooking.User.Name = model.Name;
		existingBooking.User.Email = model.Email;
		existingBooking.RoomId = room?.Id ?? Guid.Empty;
		existingBooking.Room.CapacityPerPerson = model.RoomCapacity;


		await workingContext.SaveChangesAsync();

		return new BookingDto
		{
			Id = existingBooking.Id,
			From = existingBooking.From,
			To = existingBooking.To,
			WorkspaceType = model.WorkspaceType,
			User = new UserDto
			{
				Name = user.Name,
				Email = user.Email
			},
			Room = room != null ? new RoomDto
			{
				RoomCount = room.RoomCount,
				CapacityPerPerson = room.CapacityPerPerson,
			} : null!
		};
	}
}
