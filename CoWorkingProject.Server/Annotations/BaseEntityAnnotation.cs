// <copyright file="BaseEntityAnnotation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Annotations;

using CoWorkingProject.Server.Annotations.Inrterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class BaseEntityAnnotation<T> : IEntityAnnotation
    where T : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntityAnnotation{T}"/> class.
    /// </summary>
    /// <param name="builder"></param>
    protected BaseEntityAnnotation(ModelBuilder builder)
    {
        this.ModelBuilder = builder.Entity<T>();
    }

    protected EntityTypeBuilder<T> ModelBuilder { get; }

    public abstract void Annotate();
}
