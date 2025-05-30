// <copyright file="RoomAnnotation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Annotations;

using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public class RoomAnnotation : BaseEntityAnnotation<Room>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoomAnnotation"/> class.
    /// </summary>
    /// <param name="modelBuilder"></param>
    public RoomAnnotation(ModelBuilder modelBuilder)
        : base(modelBuilder)
    {
    }

    public override void Annotate()
    {
        this.ModelBuilder.HasKey(r => r.Id);
        this.ModelBuilder.Property(r => r.Id).ValueGeneratedOnAdd();
        this.ModelBuilder.HasOne(r => r.Workspace).WithMany(r => r.Rooms).HasForeignKey(r => r.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
    }
}
