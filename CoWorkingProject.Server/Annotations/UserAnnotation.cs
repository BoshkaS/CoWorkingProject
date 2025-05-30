// <copyright file="UserAnnotation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Annotations;

using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public class UserAnnotation : BaseEntityAnnotation<User>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserAnnotation"/> class.
    /// </summary>
    /// <param name="modelBuilder"></param>
    public UserAnnotation(ModelBuilder modelBuilder)
        : base(modelBuilder)
    {
    }

    public override void Annotate()
    {
        this.ModelBuilder.HasKey(a => a.Id);
        this.ModelBuilder.Property(a => a.Id).ValueGeneratedOnAdd();
        this.ModelBuilder.Property(a => a.Name).IsRequired().HasMaxLength(50);
        this.ModelBuilder.Property(a => a.Email).IsRequired().HasMaxLength(50);
    }
}
