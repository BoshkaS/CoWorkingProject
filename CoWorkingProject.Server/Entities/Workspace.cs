﻿// <copyright file="Workspace.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Entities;

using CoWorkingProject.Server.Entities.Enums;

public class Workspace
{
    public Guid Id { get; set; }

    public WorkspaceType Type { get; set; }

    public string Description { get; set; } = string.Empty;

	public Guid CoworkingId { get; set; }

	public Coworking Coworking { get; set; }

	public List<WorkspaceAmenity> Amenities { get; set; } = new List<WorkspaceAmenity>();

    public List<Room> Rooms { get; set; } = new List<Room>();

	public List<WorkspaceImage> Images { get; set; } = new List<WorkspaceImage>();
}
