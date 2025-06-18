// <copyright file="ModelBuilderExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Data;

using CoWorkingProject.Server.Entities.Enums;
using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
	public static void Seed(this ModelBuilder modelBuilder)
	{
		// Seed Amenity
		var amenityAirConditioning = new Amenity { Id = Guid.NewGuid(), Name = "Air Conditioning" };
		var amenityGamepad = new Amenity { Id = Guid.NewGuid(), Name = "Device gamepad" };
		var amenityWifi = new Amenity { Id = Guid.NewGuid(), Name = "WiFi" };
		var amenityCoffee = new Amenity { Id = Guid.NewGuid(), Name = "Coffee Bar" };
		var amenityHeadphones = new Amenity { Id = Guid.NewGuid(), Name = "Headphones" };
		var amenityMicrophone = new Amenity { Id = Guid.NewGuid(), Name = "Microphones" };

		modelBuilder.Entity<Amenity>().HasData(amenityAirConditioning, amenityGamepad, amenityWifi, amenityCoffee, amenityHeadphones, amenityMicrophone);

		// Seed Coworking
		var coworkingPechersk = new Coworking
		{
			Id = Guid.NewGuid(),
			Name = "WorkClub Pechersk",
			Description = "Modern coworking in the heart of Pechersk with quiet rooms and coffee on tap.",
			Address = "123 Yaroslaviv Val St, Kyiv",
			Photo = "images/Pechersk.png",
		};

		var coworkingPodil = new Coworking
		{
			Id = Guid.NewGuid(),
			Name = "UrbanSpace Podil",
			Description = "A creative riverside hub ideal for freelancers and small startups.",
			Address = "78 Naberezhno-Khreshchatytska St, Kyiv",
			Photo = "images/Podil.png",
		};

		var coworkingLvivska = new Coworking
		{
			Id = Guid.NewGuid(),
			Name = "Creative Hub Lvivska",
			Description = "A compact, design-focused space with open desks and strong community vibes.",
			Address = "12 Lvivska Square, Kyiv",
			Photo = "images/Lvivska.png",
		};

		var coworkingOlimpiiska = new Coworking
		{
			Id = Guid.NewGuid(),
			Name = "TechNest Olimpiiska",
			Description = "A high-tech space near Olimpiiska metro, perfect for team sprints and solo focus.",
			Address = "45 Velyka Vasylkivska St, Kyiv",
			Photo = "images/Olimpiiska.png",
		};

		modelBuilder.Entity<Coworking>().HasData(coworkingPechersk, coworkingPodil, coworkingLvivska, coworkingOlimpiiska);

		// Updated Workspaces with CoworkingId

		var workspaceOpenPechersk = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.OpenSpace,
			Description = "Open space with 35 desks",
			CoworkingId = coworkingPechersk.Id
		};

		var workspacePrivatePechersk = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.PrivateRoom,
			Description = "4 private rooms available",
			CoworkingId = coworkingPechersk.Id
		};

		var workspaceMeetingPechersk = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.MeetingRoom,
			Description = "2 meeting rooms",
			CoworkingId = coworkingPechersk.Id
		};

		var workspaceOpenPodil = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.OpenSpace,
			Description = "20 desk open space",
			CoworkingId = coworkingPodil.Id
		};

		var workspacePrivatePodil = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.PrivateRoom,
			Description = "2 private rooms",
			CoworkingId = coworkingPodil.Id
		};

		var workspaceMeetingPodil = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.MeetingRoom,
			Description = "1 meeting room",
			CoworkingId = coworkingPodil.Id
		};

		var workspaceOpenLvivska = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.OpenSpace,
			Description = "15 desk open space",
			CoworkingId = coworkingLvivska.Id
		};

		var workspaceMeetingLvivska = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.MeetingRoom,
			Description = "1 meeting room",
			CoworkingId = coworkingLvivska.Id
		};

		var workspaceOpenOlimpiiska = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.OpenSpace,
			Description = "40 desk open space",
			CoworkingId = coworkingOlimpiiska.Id
		};

		var workspacePrivateOlimpiiska = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.PrivateRoom,
			Description = "3 private rooms",
			CoworkingId = coworkingOlimpiiska.Id
		};

		var workspaceMeetingOlimpiiska = new Workspace
		{
			Id = Guid.NewGuid(),
			Type = WorkspaceType.MeetingRoom,
			Description = "2 meeting rooms",
			CoworkingId = coworkingOlimpiiska.Id
		};

		modelBuilder.Entity<Workspace>().HasData(
			workspaceOpenPechersk, workspacePrivatePechersk, workspaceMeetingPechersk,
			workspaceOpenPodil, workspacePrivatePodil, workspaceMeetingPodil,
			workspaceOpenLvivska, workspaceMeetingLvivska,
			workspaceOpenOlimpiiska, workspacePrivateOlimpiiska, workspaceMeetingOlimpiiska
		);

		// Seed Rooms
		var room1 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspacePrivatePechersk.Id,
			RoomCount = 4,
			CapacityPerPerson = 2
		};

		var room2 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingPechersk.Id,
			RoomCount = 2,
			CapacityPerPerson = 10
		};

		var room8 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenPechersk.Id,
			RoomCount = 35,
			CapacityPerPerson = 1
		};

		var room3 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspacePrivatePodil.Id,
			RoomCount = 2,
			CapacityPerPerson = 3,
		};

		var room4 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingPodil.Id,
			RoomCount = 1,
			CapacityPerPerson = 10,
		};

		var room9 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenPodil.Id,
			RoomCount = 20,
			CapacityPerPerson = 1,
		};

		var room5 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingLvivska.Id,
			RoomCount = 1,
			CapacityPerPerson = 10,
		};

		var room10 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenLvivska.Id,
			RoomCount = 15,
			CapacityPerPerson = 1,
		};

		var room6 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspacePrivateOlimpiiska.Id,
			RoomCount = 3,
			CapacityPerPerson = 3,
		};

		var room7 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingOlimpiiska.Id,
			RoomCount = 2,
			CapacityPerPerson = 11,
		};

		var room11 = new Room
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenOlimpiiska.Id,
			RoomCount = 15,
			CapacityPerPerson = 1,
		};

		modelBuilder.Entity<Room>().HasData(room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11);

		// Seed WorkspaceAmenity
		var wa1 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenPechersk.Id,
			AmenityId = amenityWifi.Id
		};

		var wa2 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspacePrivatePechersk.Id,
			AmenityId = amenityCoffee.Id
		};

		var wa3 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingPechersk.Id,
			AmenityId = amenityAirConditioning.Id
		};

		var wa4 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenPodil.Id,
			AmenityId = amenityGamepad.Id
		};

		var wa5 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingPodil.Id,
			AmenityId = amenityHeadphones.Id
		};

		var wa6 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenLvivska.Id,
			AmenityId = amenityWifi.Id
		};

		var wa7 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingLvivska.Id,
			AmenityId = amenityCoffee.Id
		};

		var wa8 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceOpenOlimpiiska.Id,
			AmenityId = amenityWifi.Id
		};

		var wa9 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspacePrivateOlimpiiska.Id,
			AmenityId = amenityMicrophone.Id
		};

		var wa10 = new WorkspaceAmenity
		{
			Id = Guid.NewGuid(),
			WorkspaceId = workspaceMeetingOlimpiiska.Id,
			AmenityId = amenityAirConditioning.Id
		};

		modelBuilder.Entity<WorkspaceAmenity>().HasData(
			wa1, wa2, wa3, wa4, wa5, wa6, wa7, wa8, wa9, wa10);

		// Seed User
		var user = new User
		{
			Id = new Guid("8bb9d372-e3f9-433a-bd76-74f8f4887756"),
			Name = "Bozhena Salnikova",
			Email = "bozhena@gmail.com",
		};

		modelBuilder.Entity<User>().HasData(user);

		// Seed Bookings
		var booking1 = new BookingRoom
		{
			Id = Guid.NewGuid(),
			UserId = user.Id,
			RoomId = room1.Id,
			From = new DateTime(2025, 6, 5, 9, 0, 0, DateTimeKind.Utc),
			To = new DateTime(2025, 6, 5, 11, 0, 0, DateTimeKind.Utc),
		};

		var booking2 = new BookingRoom
		{
			Id = Guid.NewGuid(),
			UserId = user.Id,
			RoomId = room2.Id,
			From = new DateTime(2025, 7, 5, 9, 0, 0, DateTimeKind.Utc),
			To = new DateTime(2025, 7, 5, 11, 0, 0, DateTimeKind.Utc),
			ImagePath = "images/mr_1.png",
		};

		modelBuilder.Entity<BookingRoom>().HasData(booking1, booking2);

		var workspaceImages = new List<WorkspaceImage>();
		var imagePathsByType = new Dictionary<WorkspaceType, List<string>>
		{
			[WorkspaceType.OpenSpace] = new List<string>
			{
				"images/os_1.png",
				"images/os_2.png",
				"images/os_3.png",
				"images/os_4.png",
			},
					[WorkspaceType.PrivateRoom] = new List<string>
			{
				"images/pr_1.png",
				"images/pr_2.png",
				"images/pr_3.png",
				"images/pr_4.png",
			},
					[WorkspaceType.MeetingRoom] = new List<string>
			{
				"images/mr_1.png",
				"images/mr_2.png",
				"images/mr_3.png",
				"images/mr_4.png",
			},
		};

		// Group all workspaces into a list
		var allWorkspaces = new List<Workspace>
		{
			workspaceOpenPechersk, workspacePrivatePechersk, workspaceMeetingPechersk,
			workspaceOpenPodil, workspacePrivatePodil, workspaceMeetingPodil,
			workspaceOpenLvivska, workspaceMeetingLvivska,
			workspaceOpenOlimpiiska, workspacePrivateOlimpiiska, workspaceMeetingOlimpiiska,
		};

		// Assign 4 images per workspace based on type
		foreach (var workspace in allWorkspaces)
		{
			if (imagePathsByType.TryGetValue(workspace.Type, out var paths))
			{
				foreach (var path in paths)
				{
					workspaceImages.Add(new WorkspaceImage
					{
						Id = Guid.NewGuid(),
						ImagePath = path,
						WorkspaceId = workspace.Id,
					});
				}
			}
		}

		modelBuilder.Entity<WorkspaceImage>().HasData(workspaceImages);
	}
}