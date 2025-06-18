// <copyright file="CoWorkingContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Data;

using System.Runtime.CompilerServices;
using CoWorkingProject.Server.Annotations;
using CoWorkingProject.Server.Annotations.Inrterfaces;
using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public class CoWorkingContext : DbContext
{
    public CoWorkingContext(DbContextOptions<CoWorkingContext> options)
        : base(options)
    {
    }

    public DbSet<Amenity> Amenities { get; set; }

    public DbSet<BookingRoom> BookingRooms { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Workspace> Workspaces { get; set; }

    public DbSet<WorkspaceAmenity> WorkspaceAmenities { get; set; }

    public DbSet<WorkspaceImage> WorkspaceImages { get; set; }

    public DbSet<Coworking> Coworkings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var annotationCollection = new List<IEntityAnnotation>
        {
            new AmenityAnnotation(modelBuilder),
            new BookingRoomAnnotation(modelBuilder),
            new RoomAnnotation(modelBuilder),
            new UserAnnotation(modelBuilder),
            new WorkspaceAmenityAnnotation(modelBuilder),
            new WorkspaceAnnotation(modelBuilder),
            new WorkspaceImageAnnotation(modelBuilder),
            new CoworkingAnnotation(modelBuilder),
        };
        foreach (var annotation in annotationCollection)
        {
            annotation.Annotate();
        }

        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
    }
}