// <copyright file="BookingRoom.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Entities;

public class BookingRoom
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }
}
