// <copyright file="WorkspaceAmenityAnnotation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Annotations;

using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public class WorkspaceAmenityAnnotation : BaseEntityAnnotation<WorkspaceAmenity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkspaceAmenityAnnotation"/> class.
    /// </summary>
    /// <param name="modelBuilder"></param>
    public WorkspaceAmenityAnnotation(ModelBuilder modelBuilder)
        : base(modelBuilder)
    {
    }

    public override void Annotate()
    {
        this.ModelBuilder.HasKey(w => w.Id);
        this.ModelBuilder.HasOne(w => w.Amenity).WithMany(w => w.Workspaces).HasForeignKey(w => w.AmenityId);
        this.ModelBuilder.HasOne(w => w.Workspace).WithMany(w => w.Amenities).HasForeignKey(w => w.WorkspaceId);
    }
}
