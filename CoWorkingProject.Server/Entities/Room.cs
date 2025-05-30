// <copyright file="Room.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Entities;

using System.Text.Json.Serialization;

public class Room
{
    public Guid Id { get; set; }

    public Guid WorkspaceId { get; set; }

    [JsonIgnore]
    public Workspace? Workspace { get; set; }

    public int? RoomCount { get; set; }

    public int CapacityPerPerson { get; set; }

    public List<BookingRoom> Bookings { get; set; } = new List<BookingRoom>();
}
