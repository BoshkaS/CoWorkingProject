// <copyright file="WorkspaceAnnotation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Annotations;

using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public class WorkspaceAnnotation : BaseEntityAnnotation<Workspace>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkspaceAnnotation"/> class.
    /// </summary>
    /// <param name="modelBuilder"></param>
    public WorkspaceAnnotation(ModelBuilder modelBuilder)
        : base(modelBuilder)
    {
    }

    public override void Annotate()
    {
        this.ModelBuilder.HasKey(a => a.Id);
        this.ModelBuilder.Property(a => a.Id).ValueGeneratedOnAdd();
        this.ModelBuilder.Property(a => a.Description).IsRequired().HasMaxLength(500);
    }
}
