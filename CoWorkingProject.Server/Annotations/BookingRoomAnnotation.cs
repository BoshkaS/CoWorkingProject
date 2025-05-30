// <copyright file="BookingRoomAnnotation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoWorkingProject.Server.Annotations;

using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

public class BookingRoomAnnotation : BaseEntityAnnotation<BookingRoom>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookingRoomAnnotation"/> class.
    /// </summary>
    /// <param name="modelBuilder"></param>
    public BookingRoomAnnotation(ModelBuilder modelBuilder)
        : base(modelBuilder)
    {
    }

    public override void Annotate()
    {
        this.ModelBuilder.HasKey(b => b.Id);
        this.ModelBuilder.Property(b => b.Id).ValueGeneratedOnAdd();
        this.ModelBuilder.HasOne(b => b.User).WithMany(b => b.Workspaces).HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Cascade);
        this.ModelBuilder.HasOne(b => b.Room).WithMany(b => b.Bookings).HasForeignKey(b => b.RoomId).OnDelete(DeleteBehavior.Cascade);
    }
}