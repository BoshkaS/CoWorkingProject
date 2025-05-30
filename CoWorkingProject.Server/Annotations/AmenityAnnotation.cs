// <copyright file="AmenityAnnotation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Annotations;

using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public class AmenityAnnotation : BaseEntityAnnotation<Amenity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AmenityAnnotation"/> class.
    /// </summary>
    /// <param name="modelBuilder"></param>
    public AmenityAnnotation(ModelBuilder modelBuilder)
        : base(modelBuilder)
    {
    }

    public override void Annotate()
    {
        this.ModelBuilder.HasKey(a => a.Id);
        this.ModelBuilder.Property(a => a.Id).ValueGeneratedOnAdd();
        this.ModelBuilder.Property(a => a.Name).IsRequired().HasMaxLength(50);
    }
}
