// <copyright file="Amenity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Entities;

public class Amenity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<WorkspaceAmenity> Workspaces { get; set; } = new List<WorkspaceAmenity>();
}
