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

        // Seed Workspaces
        var workspaceOpen = new Workspace
        {
            Id = Guid.NewGuid(),
            Type = WorkspaceType.OpenSpace,
            Description = "Open space with 20 desks",
        };

        var workspacePrivate = new Workspace
        {
            Id = Guid.NewGuid(),
            Type = WorkspaceType.PrivateRoom,
            Description = "Private rooms for individuals or small teams",
        };

        var workspaceMeeting = new Workspace
        {
            Id = Guid.NewGuid(),
            Type = WorkspaceType.MeetingRoom,
            Description = "Meeting rooms for up to 20 people",
        };

        modelBuilder.Entity<Workspace>().HasData(workspaceOpen, workspacePrivate, workspaceMeeting);

        // Seed Rooms
        var room1 = new Room
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspacePrivate.Id,
            RoomCount = 2,
            CapacityPerPerson = 5,
        };

        var room2 = new Room
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspacePrivate.Id,
            RoomCount = 4,
            CapacityPerPerson = 2,
        };

        var room3 = new Room
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspacePrivate.Id,
            RoomCount = 7,
            CapacityPerPerson = 1,
        };

        var room4 = new Room
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspacePrivate.Id,
            RoomCount = 1,
            CapacityPerPerson = 10,
        };

        var room5 = new Room
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceMeeting.Id,
            RoomCount = 1,
            CapacityPerPerson = 20,
        };

        var room6 = new Room
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceMeeting.Id,
            RoomCount = 3,
            CapacityPerPerson = 10,
        };

        var room7 = new Room
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceOpen.Id,
            CapacityPerPerson = 20,
        };

        modelBuilder.Entity<Room>().HasData(room1, room2, room3, room4, room5, room6, room7);

        // Seed WorkspaceAmenity
        var wa1 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceOpen.Id,
            AmenityId = amenityWifi.Id,
        };

        var wa2 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspacePrivate.Id,
            AmenityId = amenityCoffee.Id,
        };

        var wa3 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceMeeting.Id,
            AmenityId = amenityWifi.Id,
        };

        var wa4 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceMeeting.Id,
            AmenityId = amenityHeadphones.Id,
        };

        var wa5 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceOpen.Id,
            AmenityId = amenityCoffee.Id,
        };

        var wa6 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspacePrivate.Id,
            AmenityId = amenityHeadphones.Id,
        };

        var wa7 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspacePrivate.Id,
            AmenityId = amenityMicrophone.Id,
        };

        var wa8 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceMeeting.Id,
            AmenityId = amenityAirConditioning.Id,
        };

        var wa9 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceOpen.Id,
            AmenityId = amenityAirConditioning.Id,
        };

        var wa10 = new WorkspaceAmenity
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceOpen.Id,
            AmenityId = amenityGamepad.Id,
        };

        modelBuilder.Entity<WorkspaceAmenity>().HasData(
            wa1, wa2, wa3, wa4, wa5, wa6, wa7, wa8, wa9, wa10);

        // Seed User
        var user = new User
        {
            Id = Guid.NewGuid(),
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
		};

        modelBuilder.Entity<BookingRoom>().HasData(booking1, booking2);
    }
}
