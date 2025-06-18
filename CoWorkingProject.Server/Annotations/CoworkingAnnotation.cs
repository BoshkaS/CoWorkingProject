using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingProject.Server.Annotations;

public class CoworkingAnnotation : BaseEntityAnnotation<Coworking>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CoworkingAnnotation"/> class.
	/// </summary>
	/// <param name="modelBuilder"></param>
	public CoworkingAnnotation(ModelBuilder modelBuilder)
		: base(modelBuilder)
	{
	}

	public override void Annotate()
	{
		this.ModelBuilder.HasKey(a => a.Id);
		this.ModelBuilder.Property(a => a.Id).ValueGeneratedOnAdd();
		this.ModelBuilder.Property(a => a.Description).IsRequired().HasMaxLength(500);
		this.ModelBuilder.Property(a => a.Address).IsRequired().HasMaxLength(100);
		this.ModelBuilder.Property(a => a.Name).IsRequired().HasMaxLength(50);
		this.ModelBuilder.HasMany(a => a.Workspaces).WithOne(a => a.Coworking).HasForeignKey(a => a.CoworkingId);
	}
}
