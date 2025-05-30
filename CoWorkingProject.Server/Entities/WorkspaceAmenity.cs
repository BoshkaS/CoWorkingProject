// <copyright file="WorkspaceAmenity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Entities;

public class WorkspaceAmenity
{
    public Guid Id { get; set; }

    public Guid WorkspaceId { get; set; }

    public Workspace? Workspace { get; set; }

    public Guid AmenityId { get; set; }

    public Amenity? Amenity { get; set; }
}
